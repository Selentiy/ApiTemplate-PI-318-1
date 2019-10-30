using App.Configuration;
using App.Currencies.Exceptions;
using App.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace App.Currencies
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
            var conversionRates = _repository.GetConversionRates()?.ToList();
            var latestConversionRate = conversionRates[conversionRates.Count - 1];
            return latestConversionRate.Currencies.ToDictionary(x => x.Key, x => x.Value).Keys;
        }

        public IEnumerable<KeyValuePair<string, decimal>> GetExchangeRate(string fromCode, DateTime date)
        {
            string pattern = "(?i)^[A-Z]{3}$";
            if (!Regex.IsMatch(fromCode, pattern))
                throw new CurrencyCodeFormatException("Currency code must consist of three letters", fromCode);
            if (date > DateTime.Today)
                throw new FutureDateException("The date has not come yet.", date);

            var result = new Dictionary<string, decimal>();

            var conversionRate = _repository.GetConversionRate(date);
            var exchangeRates = conversionRate.Currencies?.ToDictionary(x => x.Key, x => x.Value);

            foreach (var rate in exchangeRates)
                result.Add(rate.Key, GetConversionRate(exchangeRates, fromCode, rate.Key));

            result.Remove(fromCode);
            return result;
        }

        private decimal GetConversionRate(Dictionary<string, decimal> exchangeRates, string from, string to)
        {
            decimal rate = exchangeRates[from] / exchangeRates[to];
            return rate;
        }
    }
}
