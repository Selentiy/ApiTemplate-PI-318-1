using App.Configuration;
using App.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Currencies
{
    public interface ICurrencyManager
    {
        string GetCurrencyCode(int id);
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

        public string GetCurrencyCode(int id)
        {
            return _repository.GetCurrencyCode(id);
        }

        public IEnumerable<string> GetCurrencyCodes()
        {
            return _repository.GetCurrencyCodes();
        }

        public IEnumerable<KeyValuePair<string, decimal>> GetExchangeRate(string fromCode, DateTime date)
        {
            var result = new Dictionary<string, decimal>();

            var exchangeRates = _repository.GetExchangeRates(date)
                ?.ToDictionary(x => x.Key, x => x.Value);

            if (exchangeRates == null)
                return null;

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
