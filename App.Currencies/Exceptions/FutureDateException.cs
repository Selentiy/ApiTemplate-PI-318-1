using System;
using System.Collections.Generic;
using System.Text;

namespace App.Currencies.Exceptions
{
    public class FutureDateException : ArgumentException
    {
        public DateTime Date { get; private set; }

        public FutureDateException(DateTime date, string paramName) : 
            base($"The date {date.ToString("yyyy-MM-dd")} has not come yet.", paramName)
        {
            Date = date;
        }

        public FutureDateException(string message, DateTime date, string paramName) : base(message, paramName)
        {
            Date = date;
        }

    }
}
