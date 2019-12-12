using System;
using System.Collections.Generic;
using System.Text;

namespace App.Currencies.Exceptions
{
    public class FutureDateException : ArgumentException
    {
        public DateTime Date { get; private set; }

        public FutureDateException(string message, DateTime date, string paramName) : base(message, paramName)
        {
            Date = date;
        }

    }
}
