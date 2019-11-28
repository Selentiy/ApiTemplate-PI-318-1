using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Models.Stocks
{
	public class Company
	{
		[Key]
		public int OrgId { get; set; }
		public string FullName { get; set; }
		public string MainTicker { get; set; }
		
		public string Description { get; set; }
		public ICollection<Stock> Stocks { get; set; }
	}
}
