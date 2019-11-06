using System.Collections.Generic;

namespace App.Models.Stocks
{
	public class Company
	{
		public long OrgId { get; set; }
		public string FullName { get; set; }
		public string MainTicker { get; set; }
		
		public string Description { get; set; }
		public List<Stock> Stocks { get; set; }
	}
}
