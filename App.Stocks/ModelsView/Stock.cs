using System;
using System.Collections.Generic;
using System.Text;

namespace App.Stocks.ModelsView
{
	public class Stock
	{
		public string Ticker { get; set; }

		public bool IsTraded { get; set; }
		public OHCL_Candle Candle { get; set; }

		public bool CompareDate(DateTime date) => date.ToShortDateString()
			.CompareTo(Candle.Date.ToShortDateString()) == 0;
	}

	/// <summary>
	/// Class for keeping daily ticker cost info
	/// </summary>
	public class OHCL_Candle 
	{
		public decimal Open { get; set; }
		public decimal High { get; set; }
		public decimal Close { get; set; }
		public decimal Low { get; set; }

		public DateTime Date { get; set; }
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

	public class StocksListView
	{
		public IEnumerable<StocksListItemView> Stocks { get; set; }
		public string Company_Name { get; set; }
	}
}
