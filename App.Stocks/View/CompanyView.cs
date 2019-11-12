using System;
using System.Collections.Generic;
using System.Text;

namespace App.Stocks.View
{
	public class CompanyView
	{
		public long OrgId { get; set; }
		public string FullName { get; set; }
		public string MainTicker { get; set; }

		public string Description { get; set; }
	}
}
