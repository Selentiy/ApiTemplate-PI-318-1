using App.Configuration;
using App.Models.Stocks;
using App.Repositories.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Stocks.Repositories
{
	public class CompaniesRepository : ICompaniesRepository, ITransientDependency
	{
		readonly IQueryable<Company> Companies;

		public CompaniesRepository()
		{
			Companies = CompaniesInitializer.GetCompanies();
		}

		public Company CompanyById(int id) => Companies.FirstOrDefault(comp => comp.OrgId == id);

		public IQueryable<Company> AllCompanies() => Companies;

		static class CompaniesInitializer
		{
			readonly static List<(string company, string ticker)> companyWithTicker = 
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

			public static IQueryable<Company> GetCompanies()
			{
			    List<Stock> stocksList = new List<Stock>();
				List<Company> companyList = new List<Company>();
				int orgId = 0;

				foreach(var company in companyWithTicker)
				{
					stocksList.Add(new Stock
					{
						Ticker = company.ticker,
						IsTraded = new Random().Next(2) == 0,
						Candle = new OHCLCandle
						{
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
				return companyList.AsQueryable();
			}
		}
	}
}
