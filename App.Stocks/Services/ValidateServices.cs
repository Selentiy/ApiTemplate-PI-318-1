using App.Stocks.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace App.Stocks.Services
{
	public class ValidateServices : IValidateServices
	{
		public void ValidateCompany(Company company, bool IsOpenStocks = true)
		{
			if (company == null)
			{
				throw new HttpListenerException(400, "Company doesn't exist");
			}
			if (!IsOpenStocks)
			{
				throw new HttpListenerException(400, "You can't see stocks this company");
			}
		}

		public void ValidateStock(Stock stock)
		{
			if (stock == null)
			{
				throw new HttpListenerException(400, "Stock info with this date doesn't exist");
			}
		}

		public void ValidateStocksCompany(Stock stock, Company company)
		{
			ValidateCompany(company, company?.Stocks?
				.Any(s => s.IsTraded) ?? false);

			ValidateStock(stock);
		}
		public void ValidateDate(string Date)
		{
			if (DateTime.Parse(Date).CompareTo(DateTime.Now) > 0)
			{
				throw new Exception("Incorrect date!");
			}
		}
	}
}
