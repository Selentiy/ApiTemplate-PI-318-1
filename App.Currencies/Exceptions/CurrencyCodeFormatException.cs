using System;
using System.Collections.Generic;
using System.Text;

namespace App.Currencies.Exceptions
{
    public class CurrencyCodeFormatException : Exception
    {
        public string CurrencyCode { get; private set; }

        public CurrencyCodeFormatException() { }

        public CurrencyCodeFormatException(string currencyCode) : base($"Currency code must consist of three letters.")
        {
            CurrencyCode = currencyCode;
        }

        public CurrencyCodeFormatException(string message, string currencyCode) : base(message)
        {
            CurrencyCode = currencyCode;
        }
    }
}
