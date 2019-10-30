using System;
using System.Collections.Generic;
using System.Text;

namespace App.Currencies.Exceptions
{
    public class CurrencyCodeFormatException : Exception
    {
        public string CurrencyCode { get; private set; }

        public CurrencyCodeFormatException() { }

        public CurrencyCodeFormatException(string currencyCode)
        {
            CurrencyCode = currencyCode;
        }

        public CurrencyCodeFormatException(string message, Exception innerException)
            : base(message, innerException) { }

        public CurrencyCodeFormatException(string message, string currencyCode) : base(message)
        {
            CurrencyCode = currencyCode;
        }
    }
}
