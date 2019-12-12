using System;
using System.Collections.Generic;
using System.Text;

namespace App.Currencies.Exceptions
{
    public class CurrencyCodeFormatException : ArgumentException
    {
        public string CurrencyCode { get; private set; }

        public CurrencyCodeFormatException(string message, string currencyCode, string paramName) :
            base(message, paramName)
        {
            CurrencyCode = currencyCode;
        }
    }
}
