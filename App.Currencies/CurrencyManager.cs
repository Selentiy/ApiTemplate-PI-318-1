using App.Configuration;
using App.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Currencies
{
    public interface ICurrencyManager
    {
        IEnumerable<string> GetCurrencyCodes();
        decimal GetRate(string code, DateTime date);
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
            return _repository.GetCurrencyCodes();
        }

        public decimal GetRate(string code, DateTime date)
        {
            return _repository.GetRate(code, date);
        }
    }
}
