using System;

namespace App.Stocks.Exceptions
{
	public class IncorrectParamException : Exception
	{
		public string ParamName { get; private set; }
		public string ParamFormat { get; private set; }
		public new string Message => $"The {ParamName} parameters might have this format {ParamFormat}!";

		public IncorrectParamException(string name, string format)
		{
			ParamName = name;
			ParamFormat = format;
		}
	}
}
