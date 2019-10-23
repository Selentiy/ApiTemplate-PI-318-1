using App.Configuration;
using App.Models.Stocks;
using App.Repositories.Stocks;
using App.Stocks.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Stocks.Services
{
	public interface IStocksManager
	{
		Task<IEnumerable<StocksListItemView>> CompanyStocksAsync(int companyId);
		Task<StocksListItemView> CompanyStockByDate(int companyId, DateTime date);
	}

	public class StockManager : IStocksManager, ITransientDependency
	{
		readonly ICompaniesRepository _repository;

		public StockManager(ICompaniesRepository repository)
		{
			_repository = repository;
		}

		public async Task<StocksListItemView> CompanyStockByDate(int companyId, DateTime date)
		{
			var company = await Task.Run(() => _repository.CompanyById(companyId));
			if (company == null)
			{
				throw new Exception($"Company with id {companyId} not found!");
			}

			var stock = await Task.Run(() => company.Stocks.Where(el => el.CompareDate(date)).FirstOrDefault());


			var stockView = GetStockView(stock);
			return stockView;
		}

		public async Task<IEnumerable<StocksListItemView>> CompanyStocksAsync(int companyId)
		{
			var company = await Task.Run(() => _repository.CompanyById(companyId));

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
