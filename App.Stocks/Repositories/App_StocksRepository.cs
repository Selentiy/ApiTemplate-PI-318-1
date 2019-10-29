using App.Configuration;
using App.Repositories;
using App.Stocks.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Stocks.Repositories
{
	public class App_StocksRepository : IStocksRepository, ITransientDependency
	{
		readonly IQueryable<Company> Companies;

		public App_StocksRepository()
		{
			Companies = CompaniesInitializer.GenerateCompaniesWithTickers();
		}

		public Company CompanyById(int id) => Companies.FirstOrDefault(comp => comp.Org_Id == id);

		public IQueryable<Company> AllCompanies() => Companies;

		static class CompaniesInitializer
		{
			readonly static List<(string company, string ticker)> comp_tick_pairs = 
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

			public static IQueryable<Company> GenerateCompaniesWithTickers()
			{
			    List<Stock> stocks_list = new List<Stock>();
				List<Company> company_list = new List<Company>();

				foreach(var pair in comp_tick_pairs)
				{
					stocks_list.Add(new Stock
					{
						Ticker = pair.ticker,
						IsTraded = new Random().Next(2) == 0,
						Candle = new OHCL_Candle
						{
							Open = new Random().Next(200, 400),
							High = new Random().Next(200, 400),
							Close = new Random().Next(200, 400),
							Low = new Random().Next(200, 400),
							Date = DateTime.Now.AddDays(new Random().Next(1, 7) * -1)
						}
					});

					company_list.Add(new Company
					{
						Org_Id = new Random().Next(1, 100),
						FullName = pair.company,
						MainTicker = pair.ticker,
						Description = "There could be your advertisement",
						Stocks = new List<Stock> { stocks_list.Last() }
					});
				}
				return company_list.AsQueryable();
			}
		}
	}
}
