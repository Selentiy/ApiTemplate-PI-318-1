using App.Models.Stocks;
using App.Stocks.View;

namespace App.Stocks.Services
{
	public static class CompanyStocksExtention
	{
		public static StocksListItemView GetStockView(this Stock stock)
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

		public static CompanyView MappSingleCompany(this Company company) =>
			new CompanyView
			{
				OrgId = company.OrgId,
				FullName = company.FullName,
				Description = company.Description,
				MainTicker = company.MainTicker
			};
	}
}
