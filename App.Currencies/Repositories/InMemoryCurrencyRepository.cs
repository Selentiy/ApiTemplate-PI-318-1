using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Configuration;
using App.Currencies.Models;
using App.Repositories;

namespace App.Currencies.Repositories
{
    /// <summary>
    /// Fake repository implementation, which stores value in memory
    /// </summary>
    public class InMemoryCurrencyRepository : ICurrencyRepository, ITransientDependency
    {
        private ConversionRate[] _conversionRates;
        private ConversionRate _conversionRate1, _conversionRate2;

        public InMemoryCurrencyRepository()
        {
            CreateData();
        }

        public IEnumerable<string> GetCurrencyCodes()
        {
            return _conversionRates[0].Currencies.Select(cur => cur.Code);
        }

        public decimal GetRate(string code, DateTime date)
        {
            ConversionRate conversionRate = _conversionRates.Where(cr => cr.Date == date).FirstOrDefault();
            return conversionRate.Currencies.Select(cur => cur.Rate).FirstOrDefault();
        }

        private void CreateData()
        {
            Currency[] currencies1 = new Currency[]
            {
                new Currency("UAH", 27.7830473323m),
                new Currency("RUB", 65.9764487621m),
                new Currency("EUR", 0.8765548195m),
                new Currency("USD", 1.0m)
              };
            _conversionRate1 = new ConversionRate(new DateTime(2019, 1, 25));
            _conversionRate1.Currencies = currencies1;

            Currency[] currencies2 = new Currency[]
            {
                new Currency("UAH", 24.4715083357m),
                new Currency("RUB", 65.2378996809m),
                new Currency("EUR", 0.9147112977m),
                new Currency("USD", 1.0m)
            };
            _conversionRate2 = new ConversionRate(new DateTime(2019, 10, 1));
            _conversionRate2.Currencies = currencies2;

            _conversionRates = new ConversionRate[] { _conversionRate1, _conversionRate2 };

        }
    }
}
