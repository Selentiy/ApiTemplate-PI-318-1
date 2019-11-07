using System;

namespace App.Stocks.Exceptions
{
	public class PrivateCompanyException : Exception
	{
		public int CompanyId { get; private set; }
		
		public PrivateCompanyException(int id) :
			base($"The company with id {id} is private!")
		{
			CompanyId = id;
		}
	}
}
