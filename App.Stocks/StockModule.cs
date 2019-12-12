using App.Configuration;
using App.Models.Stocks;
using App.Stocks.Database;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Stocks
{
	public class StockModule : IModule
	{
		public void Initialize(IWindsorContainer container)
		{
			RegisterDbContext(container);
		}

		private void RegisterDbContext(IWindsorContainer container)
		{
			container.Register(Component.For<DbContextOptions<StockDbContext>>().UsingFactoryMethod(() =>
			{
				var builder = new DbContextOptionsBuilder<StockDbContext>();
				// for test purpose we are using InMemory database
				builder.UseInMemoryDatabase("StockDb");
				return builder.Options;
			}).LifestyleTransient());

			container.Register(Component.For<StockDbContext>().LifestyleTransient());

			InitializeDbContext(container);
		}

		private void InitializeDbContext(IWindsorContainer container)
		{
			using (var context = container.Resolve<StockDbContext>())
			{
				context.Companies.AddRange(GetCompanies());
				context.SaveChanges();
			}
		}

		public static IEnumerable<Company> GetCompanies()
		{
			List<(string company, string ticker)> companyWithTicker =
				new List<(string company, string ticker)>
			{
				("Amazon", "AMZN"),
				("Facebook", "FB"),
				("CISCO", "CSCO"),
				("Tesla Inc", "TSLA"),
				("City Group", "C"),
				("Apple Inc", "APPL"),
				("General Motors", "GM")
			};

			List<Stock> stocksList = new List<Stock>();
			List<Company> companyList = new List<Company>();
			int orgId = 1;

			foreach (var company in companyWithTicker)
			{
				stocksList.Add(new Stock
				{
					Key = orgId + 30,
					Ticker = company.ticker,
					IsTraded = new Random().Next(2) == 0,
					Candle = new OHCLCandle
					{
						CandleKey = orgId + 50,
						Open = new Random().Next(200, 400),
						High = new Random().Next(200, 400),
						Close = new Random().Next(200, 400),
						Low = new Random().Next(200, 400),
						Date = DateTime.Now.Date
					}
				});

				companyList.Add(new Company
				{
					OrgId = orgId,
					FullName = company.company,
					MainTicker = company.ticker,
					Description = "There could be your advertisement",
					Stocks = new List<Stock> { stocksList.Last() }
				});

				orgId++;
			}
			return companyList;
		}
	}
}
