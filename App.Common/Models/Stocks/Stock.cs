using System;

namespace App.Models.Stocks
{
	public class Stock
	{
		public string Ticker { get; set; }

		public bool IsTraded { get; set; }
		public OHCLCandle Candle { get; set; }

		public bool CompareDate(DateTime date) => date.ToShortDateString()
			.CompareTo(Candle.Date.ToShortDateString()) == 0;
	}

	/// <summary>
	/// Class for keeping daily ticker cost info
	/// </summary>
	public class OHCLCandle 
	{
		public decimal Open { get; set; }
		public decimal High { get; set; }
		public decimal Close { get; set; }
		public decimal Low { get; set; }

		public DateTime Date { get; set; }
	}
}
