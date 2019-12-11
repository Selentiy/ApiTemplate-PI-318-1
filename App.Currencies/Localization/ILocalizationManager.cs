using System.Globalization;

namespace App.Currencies.Localization
{
    public interface ILocalizationManager
    {
        string GetResource(string key);
        string GetResource(string key, CultureInfo cultureInfo);
    }
}
