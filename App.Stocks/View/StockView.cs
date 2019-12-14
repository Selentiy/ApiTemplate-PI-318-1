using System;
using System.Collections.Generic;
using System.Text;

namespace App.Stocks.View
{
	public class StocksListView
	{
		public IEnumerable<StocksListItemView> Stocks { get; set; }
		public string CompanyName { get; set; }
	}

	public class StocksListItemView
	{
		public string Ticker { get; set; }
		public bool IsTraded { get; set; }

		public decimal Open { get; set; }
		public decimal High { get; set; }
		public decimal Close { get; set; }
		public decimal Low { get; set; }

		public DateTime Date { get; set; }
	}
}
