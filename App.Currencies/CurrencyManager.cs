using App.Configuration;
using App.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        private Dictionary<string, decimal> _exchangeRates; 

        public CurrencyManager(ICurrencyRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<string> GetCurrencyCodes()
        {
            return _repository.GetCurrencyCodes();
        }

        public IEnumerable<KeyValuePair<string, decimal>> GetExchangeRate(string fromCode, DateTime date)
        {
            var result = new Dictionary<string, decimal>();
            _exchangeRates = _repository.GetExchangeRates(fromCode, date).ToDictionary(x => x.Key, x => x.Value);

            foreach (var rate in _exchangeRates)
                result.Add(rate.Key, GetConversionRate(fromCode, rate.Key));

            result.Remove(fromCode);
            return result;
        }

        private decimal GetConversionRate(string from, string to)
        {
            decimal rate = _exchangeRates[from] / _exchangeRates[to];
            return rate;
        }
    }
}
