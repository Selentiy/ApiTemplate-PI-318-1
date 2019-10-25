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
		IEnumerable<StocksListItemView> CompanyStocks(int companyId);
		StocksListItemView CompanyStockByDate(int companyId, DateTime date);
	}

	public class StockManager : IStocksManager, ITransientDependency
	{
		readonly ICompaniesRepository _repository;

		public StockManager(ICompaniesRepository repository)
		{
			_repository = repository;
		}

		public StocksListItemView CompanyStockByDate(int companyId, DateTime date)
		{
			var company = _repository.CompanyById(companyId);
			if (company == null)
			{
				throw new Exception($"Company with id {companyId} not found!");
			}

			var stock = company.Stocks.Where(el => el.CompareDate(date)).FirstOrDefault();


			var stockView = GetStockView(stock);
			return stockView;
		}

		public IEnumerable<StocksListItemView> CompanyStocks(int companyId)
		{
			var company = _repository.CompanyById(companyId);

			List<StocksListItemView> stocksView = new List<StocksListItemView>();

			foreach (var s in company.Stocks)
			{
				stocksView.Add(GetStockView(s));
			}

			return stocksView;
		}

		public StocksListItemView GetStockView(Stock stock)
			=> new StocksListItemView
			{
				Ticker = stock.Ticker,
				Open = stock.Candle.Open,
				High = stock.Candle.High,
				Close = stock.Candle.Close,
				Low = stock.Candle.Low,
				Date = stock.Candle.Date,
				IsTraded = stock.IsTraded
			}; 
	}
}
