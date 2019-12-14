using App.Configuration;
using App.Models.Stocks;
using App.Repositories.Stocks;
using App.Stocks.Exceptions;
using App.Stocks.View;
using Microsoft.Extensions.Logging;
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
		readonly ILogger<StockManager> _logger;

		public StockManager(ICompaniesRepository repository, ILogger<StockManager> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public Stock CompanyStockByDate(int companyId, DateTime date)
		{
			_logger.LogInformation("Call CompanyStockByDate method");
			var company = _repository.CompanyById(companyId);
			if (company == null)
			{
				throw new NotFoundException(typeof(Company), companyId);
			}

			var stock = company.Stocks.Where(el => el.CompareDate(date)).FirstOrDefault();
			return stock;
		}

		public IEnumerable<Stock> CompanyStocks(int companyId)
		{
			_logger.LogInformation("Call CompanyStocks method");
			var company = _repository.CompanyById(companyId);

			List<Stock> stocks = new List<Stock>();
			if (company.Stocks == null)
			{
				throw new PrivateCompanyException(companyId);
			}

			foreach (var s in company.Stocks)
			{
				stocks.Add(s);
			}

			return stocks;
		} 
	}
}
