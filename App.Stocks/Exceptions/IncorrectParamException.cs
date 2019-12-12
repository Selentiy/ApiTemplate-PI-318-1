using System;

namespace App.Stocks.Exceptions
{
	public class IncorrectParamException : Exception
	{
		public string ParamName { get; private set; }
		public string ParamFormat { get; private set; }

		public IncorrectParamException(string name, string format) : 
			base($"The {name} parameters might have this format {format}!")
		{
			ParamName = name;
			ParamFormat = format;
		}
	}
}
