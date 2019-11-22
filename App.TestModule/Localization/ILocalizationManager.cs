using System.Globalization;

namespace App.Example.Localization
{
    /// <summary>
    /// Describes methods to access localized values
    /// </summary>
    public interface ILocalizationManager
    {
        /// <summary>
        /// Get resource by Key using thread current culture. Fallback to default culture if not found.
        /// Throw exception if no resource with provided key was found
        /// </summary>
        string GetResource(string key);
        /// <summary>
        /// Get resource by key using provided culture. Fallback to default culture if not found.
        /// Throw exception if no resource with provided key was found
        /// </summary>
        string GetResource(string key, CultureInfo culture);
    }
}