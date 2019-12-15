using System.Globalization;

namespace App.RegularPayments.Localization
{
    public interface ILocalizationManager
    {
        string GetResource(string key);
        string GetResource(string key, CultureInfo culture);
    }
}
