using App.Configuration;
using App.Repositories;
using App.Stocks.Interfaces;
using App.Stocks.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Stocks.Services
{
	public class StockManager : IStocksManager, ITransientDependency
	{
		readonly ICompaniesRepository _repository;
		readonly IValidateServices _validateServices;

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

			_validateServices.ValidateStocksCompany(stock, company);

			var stockView = GetStockView(stock);
			return stockView;
		}

		public async Task<IEnumerable<StocksListItemView>> CompanyStocksAsync(int companyId)
		{
			var company = await Task.Run(() => _repository.CompanyById(companyId));

			_validateServices.ValidateCompany(company, company?.Stocks?
				.Any(s => s.IsTraded) ?? false);

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
