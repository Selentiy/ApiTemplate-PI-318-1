using System;
using System.Collections.Generic;
using System.Text;

namespace App.Stocks.Exceptions
{
	public class IncorrectDateException : IncorrectParamException
	{
		public IncorrectDateException() :
			base("Date", "yyyy-MM-dd")
		{

		}
	}
}
