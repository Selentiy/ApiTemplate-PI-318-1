using App.Configuration;
using App.Models.Stocks;
using App.Repositories.Stocks;
using App.Stocks.View;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Stocks.Services
{
	public interface IStocksManager
	{
		IEnumerable<Stock> CompanyStocks(int companyId);
		Stock CompanyStockByDate(int companyId, DateTime date);
	}

	public class StockManager : IStocksManager, ITransientDependency
	{
		readonly ICompaniesRepository _repository;

		public StockManager(ICompaniesRepository repository)
		{
			_repository = repository;
		}

		public Stock CompanyStockByDate(int companyId, DateTime date)
		{
			var company = _repository.CompanyById(companyId);
			if (company == null)
			{
				throw new Exception($"Company with id {companyId} not found!");
			}

			var stock = company.Stocks.Where(el => el.CompareDate(date)).FirstOrDefault();
			return stock;
		}

		public IEnumerable<Stock> CompanyStocks(int companyId)
		{
			var company = _repository.CompanyById(companyId);

			List<Stock> stocks = new List<Stock>();

			foreach (var s in company.Stocks)
			{
				stocks.Add(s);
			}

			return stocks;
		} 
	}
}
