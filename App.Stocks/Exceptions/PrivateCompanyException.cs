using System;

namespace App.Stocks.Exceptions
{
	public class PrivateCompanyException : Exception
	{
		public int CompanyId { get; private set; }
		public new string Message => $"The company with id {CompanyId} is private!";
		public PrivateCompanyException(int id)
		{
			CompanyId = id;
		}
	}
}
