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

        public InMemoryCurrencyRepository()
        {
            CreateData();
        }

        public IEnumerable<string> GetCurrencyCodes()
        {
            return _conversionRates[0].Currencies?.ToDictionary(x => x.Key, x => x.Value).Keys;
        }

        public IEnumerable<KeyValuePair<string, decimal>> GetExchangeRates(string code, DateTime date)
        {
            var conversionRate = _conversionRates.Where(cr => cr.Date == date).FirstOrDefault();
            return conversionRate?.Currencies;
        }

        private void CreateData()
        {
            var tempDic1 = new Dictionary<string, decimal>();
            var tempDic2 = new Dictionary<string, decimal>();

            var _conversionRate1 = new ConversionRate(new DateTime(2019, 1, 25));
            tempDic1.Add("UAH", 27.7830473323m);
            tempDic1.Add("RUB", 65.9764487621m);
            tempDic1.Add("EUR", 0.8765548195m);
            tempDic1.Add("USD", 1.0m);
            _conversionRate1.Currencies = tempDic1;


            var _conversionRate2 = new ConversionRate(new DateTime(2019, 10, 1));
            tempDic2.Add("UAH", 24.4715083357m);
            tempDic2.Add("RUB", 65.2378996809m);
            tempDic2.Add("EUR", 0.9147112977m);
            tempDic2.Add("USD", 1.0m);
            _conversionRate2.Currencies = tempDic2;

            _conversionRates = new ConversionRate[] { _conversionRate1, _conversionRate2 };

        }
    }
}
