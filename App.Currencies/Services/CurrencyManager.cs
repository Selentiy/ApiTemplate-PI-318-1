using App.Configuration;
using App.Currencies.Exceptions;
using App.Models.Currencies;
using App.Repositories;
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

        public CurrencyManager(ICurrencyRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<string> GetCurrencyCodes()
        {
            var latestConversionRate = _repository.GetConversionRate(DateTime.Today);
            return latestConversionRate.Currencies.ToDictionary(x => x.Key, x => x.Value).Keys;
        }

        public IEnumerable<KeyValuePair<string, decimal>> GetExchangeRate(string fromCode, DateTime date)
        {
            if (fromCode == null)
                throw new ArgumentNullException(nameof(fromCode));
            if (!Regex.IsMatch(fromCode, "(?i)^[A-Z]{3}$"))
                throw new CurrencyCodeFormatException(fromCode);
            if (date > DateTime.Today)
                throw new FutureDateException(date);

            var result = new Dictionary<string, decimal>();

            var conversionRate = _repository.GetConversionRate(date);
            if (conversionRate == null)
                throw new EntityNotFoundException(typeof(ConversionRate));

            var exchangeRates = conversionRate.Currencies.ToDictionary(x => x.Key, x => x.Value);

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
