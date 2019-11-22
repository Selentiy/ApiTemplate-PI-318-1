using App.Configuration;
using App.Currencies.Database;
using App.Currencies.Exceptions;
using App.Models.Currencies;
using App.Repositories;
using System;
using System.Linq;

namespace App.Currencies.Repositories
{
    public class EfCurrencyRepository : ICurrencyRepository, IDisposable, ITransientDependency
    {
        private readonly CurrenciesDbContext _dbContext;

        public EfCurrencyRepository(CurrenciesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ConversionRate GetConversionRate(DateTime date)
        {
            if (date > DateTime.Today)
                throw new FutureDateException(date, nameof(date));
            return _dbContext.ConversionRates.Where(cr => cr.Date == date).FirstOrDefault();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
