using App.Configuration;
using App.Repositories;
using App.Stocks.ModelsView;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Stocks.Interfaces
{
	public interface IStocksManager
	{
		Task<IEnumerable<StocksListItemView>> CompanyStocks(int companyId);
		Task<StocksListItemView> CompanyStockByDate(int companyId, DateTime date);

	}
}
