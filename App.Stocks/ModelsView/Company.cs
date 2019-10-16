using System;
using System.Collections.Generic;
using System.Text;

namespace App.Stocks.ModelsView
{
	public class Company
	{
		public long Org_Id { get; set; }
		public string FullName { get; set; }
		public string MainTicker { get; set; }

		public string Description { get; set; }
		public List<Stock> Stocks { get; set; }
	}

	public class CompanyView
	{
		public long Org_Id { get; set; }
		public string FullName { get; set; }
		public string MainTicker { get; set; }

		public string Description { get; set; }
	}
}
