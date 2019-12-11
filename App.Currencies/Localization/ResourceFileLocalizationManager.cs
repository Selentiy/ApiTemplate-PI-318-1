using App.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace App.Currencies.Localization
{
    public class ResourceFileLocalizationManager : ILocalizationManager, ITransientDependency
    {
        private const string DefaultCulture = "en-US";
        private const string ResourceFileFormat = "{0}.Resource.json";
        private const string ResourceFilePath = "Resources";

        private readonly ILogger<ResourceFileLocalizationManager> _logger;

        public ResourceFileLocalizationManager(ILogger<ResourceFileLocalizationManager> logger)
        {
            _logger = logger;
        }

        public string GetResource(string key)
        {
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            var currentCultureInfo = Thread.CurrentThread.CurrentCulture;
            return GetResource(key, currentCultureInfo);
        }

        public string GetResource(string key, CultureInfo cultureInfo)
        {
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (cultureInfo == null)
                throw new ArgumentNullException(nameof(cultureInfo));

            var specificCultureResource = GetResourceForCulture(key, cultureInfo);
            if (specificCultureResource != null)
                return specificCultureResource;

            var defaultCultureInfo = new CultureInfo(DefaultCulture);
            if (cultureInfo.ThreeLetterISOLanguageName != defaultCultureInfo.ThreeLetterISOLanguageName)
            {
                var defaultCultureResource = GetResourceForCulture(key, defaultCultureInfo);
                if (defaultCultureResource != null)
                    return defaultCultureResource;
            }

            _logger.LogError($"Failed to find any resource for key: {key}.");
            throw new Exception($"Failed to find any resource for key: {key}.");
        }

        private string GetResourceForCulture(string key, CultureInfo cultureInfo)
        {
            var fileSuffix = cultureInfo.ThreeLetterISOLanguageName;
            var fileName = String.Format(ResourceFileFormat, fileSuffix);
            var filePath = Path.Combine(ResourceFilePath, fileName);

            if (GetAbsolutePath(filePath) == null)
                return null;

            try
            {
                using(var fileStream = new StreamReader(GetFileStream(filePath)))
                using(var reader = new JsonTextReader(fileStream))
                {
                    JObject resourceObject = (JObject)JToken.ReadFrom(reader);
                    if (resourceObject == null)
                    {
                        _logger.LogError($"Failed to read localization file: {filePath}");
                        return null;
                    }

                    var prop = resourceObject.GetValue(key);
                    if (prop == null)
                        return null;
                    if (prop.Type != JTokenType.String)
                    {
                        _logger.LogError($"Localization file have invalid format. Expected to be dictionary: {filePath}");
                        return null;
                    }

                    return prop.Value<string>();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to read localization file: {filePath}", ex);
                throw ex;
            }
        }

        private Stream GetFileStream(string path)
        {
            var absolutePath = GetAbsolutePath(path);
            if (absolutePath == null)
                throw new Exception($"Could not find resource on path {path}.");
            var currentAssembly = typeof(ResourceFileLocalizationManager).Assembly;
            return currentAssembly.GetManifestResourceStream(absolutePath);
        }

        private string GetAbsolutePath(string resourcePath)
        {
            var path = resourcePath.Replace('\\', '.').Replace('/', '.');

            var absolutePath = typeof(ResourceFileLocalizationManager).Assembly
                .GetManifestResourceNames()
                .FirstOrDefault(r => r.EndsWith(path));
            return absolutePath;
        }


    }
}
