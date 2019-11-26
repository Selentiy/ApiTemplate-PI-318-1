using App.Configuration;
using App.Currencies.Exceptions;
using App.Currencies.Localization;
using App.Models.Currencies;
using App.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace App.Currencies.Services
{
    public interface ICurrencyManager
    {
        IEnumerable<string> GetCurrencyCodes();
        IEnumerable<KeyValuePair<string, decimal>> GetExchangeRate(string fromCode, DateTime date);
    }

    public class CurrencyManager : ICurrencyManager, ITransientDependency
    {
        private readonly ICurrencyRepository _repository;
        private readonly ILogger<CurrencyManager> _logger;
        private readonly ILocalizationManager _localizationManager;

        public CurrencyManager(ICurrencyRepository repository, ILogger<CurrencyManager> logger, 
            ILocalizationManager localizationManager)
        {
            _repository = repository;
            _logger = logger;
            _localizationManager = localizationManager;
        }

        public IEnumerable<string> GetCurrencyCodes()
        {
            _logger.LogDebug("call GetCurrencyCodes method.");
            var latestConversionRate = _repository.GetConversionRate(DateTime.Today);
            return latestConversionRate.Currencies.ToDictionary(x => x.Key, x => x.Value).Keys;
        }

        public IEnumerable<KeyValuePair<string, decimal>> GetExchangeRate(string fromCode, DateTime date)
        {
            _logger.LogDebug("call GetExchangeRate method with code {code} and date {date}", fromCode, date.ToString("yyyy-MM-dd"));
            if (fromCode == null)
                throw new ArgumentNullException(nameof(fromCode));
            if (!Regex.IsMatch(fromCode, "(?i)^[A-Z]{3}$"))
            {
                var message = _localizationManager.GetResource("CurrencyCodeFormatException");
                throw new CurrencyCodeFormatException(message, fromCode, nameof(fromCode));
            }
            if (date > DateTime.Today)
            {
                var message = _localizationManager.GetResource("FutureDateException");
                throw new FutureDateException(message, date, nameof(date));
            }

            var result = new Dictionary<string, decimal>();

            var conversionRate = _repository.GetConversionRate(date);
            if (conversionRate == null)
            {
                var message = _localizationManager.GetResource("EntityNotFoundException");
                throw new EntityNotFoundException(message, typeof(ConversionRate));
            }

            var exchangeRates = conversionRate.Currencies.ToDictionary(x => x.Key, x => x.Value);
            if (!exchangeRates.Keys.Contains(fromCode))
            {
                var message = _localizationManager.GetResource("EntityNotFoundException");
                throw new EntityNotFoundException(message, typeof(ConversionRate));
            }

            foreach (var rate in exchangeRates)
                result.Add(rate.Key, GetConversionRate(exchangeRates, fromCode, rate.Key));

            result.Remove(fromCode);
            return result;
        }

        private decimal GetConversionRate(Dictionary<string, decimal> exchangeRates, string from, string to)
        {
            decimal rate = exchangeRates[from.ToUpper()] / exchangeRates[to.ToUpper()];
            return rate;
        }
    }
}
