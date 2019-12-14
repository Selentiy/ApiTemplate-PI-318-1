using App.Configuration;
using App.Currencies.Database;
using App.Currencies.Exceptions;
using App.Currencies.Localization;
using App.Models.Currencies;
using App.Repositories;
using System;
using System.Linq;

namespace App.Currencies.Repositories
{
    public class EfCurrencyRepository : ICurrencyRepository, IDisposable, ITransientDependency
    {
        private readonly CurrenciesDbContext _dbContext;
        private readonly ILocalizationManager _localizationManager;

        public EfCurrencyRepository(CurrenciesDbContext dbContext, 
            ILocalizationManager localizationManager)
        {
            _dbContext = dbContext;
            _localizationManager = localizationManager;
        }

        public ConversionRate GetConversionRate(DateTime date)
        {
            if (date > DateTime.Today)
            {
                var message = _localizationManager.GetResource("FutureDateException");
                throw new FutureDateException(message, date, nameof(date));
            }
            return _dbContext.ConversionRates.Where(cr => cr.Date == date).FirstOrDefault();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
