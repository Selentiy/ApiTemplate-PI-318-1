using App.Configuration;
using App.Currencies.Database;
using App.Models.Currencies;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace App.Currencies
{
    public class CurrenciesModule : IModule
    {
        public void Initialize(IWindsorContainer container)
        {
            RegisterDbContext(container);
        }

        private void RegisterDbContext(IWindsorContainer container)
        {
            container.Register(Component.For<DbContextOptions<CurrenciesDbContext>>()
                .UsingFactoryMethod(() =>
                {
                    var builder = new DbContextOptionsBuilder<CurrenciesDbContext>();
                    builder.UseInMemoryDatabase("CurrenciesDb");
                    return builder.Options;
                }).LifestyleTransient());

            container.Register(Component.For<CurrenciesDbContext>().LifestyleTransient());

            InitializeDbContext(container);
        }

        private void InitializeDbContext(IWindsorContainer container)
        {
            using(var context = container.Resolve<CurrenciesDbContext>())
            {
                var conversionRates = CreateConversionRates();
                context.ConversionRates.AddRange(conversionRates);
                context.SaveChanges();
            }
        }

        private ConversionRate[] CreateConversionRates()
        {
            var tempCurrencies1 = new Dictionary<string, decimal>();
            var tempCurrencies2 = new Dictionary<string, decimal>();

            var _conversionRate1 = new ConversionRate(new DateTime(2019, 1, 25));
            tempCurrencies1.Add("UAH", 27.7830473323m);
            tempCurrencies1.Add("RUB", 65.9764487621m);
            tempCurrencies1.Add("EUR", 0.8765548195m);
            tempCurrencies1.Add("USD", 1.0m);
            _conversionRate1.Currencies = tempCurrencies1;

            var _conversionRate2 = new ConversionRate(DateTime.Today);
            tempCurrencies2.Add("UAH", 24.4715083357m);
            tempCurrencies2.Add("RUB", 65.2378996809m);
            tempCurrencies2.Add("EUR", 0.9147112977m);
            tempCurrencies2.Add("USD", 1.0m);
            _conversionRate2.Currencies = tempCurrencies2;

            return new ConversionRate[] { _conversionRate1, _conversionRate2 };
        }
    }
}
