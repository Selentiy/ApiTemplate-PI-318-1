using System;
using System.Collections.Generic;
using System.Text;

namespace App.Currencies.Exceptions
{
    public class FutureDateException : Exception
    {
        public DateTime Date { get; private set; }

        public FutureDateException() { }

        public FutureDateException(DateTime date) : base($"The date {date.ToString("yyyy-MM-dd")} has not come yet.")
        {
            Date = date;
        }

        public FutureDateException(string message, DateTime date) : base(message)
        {
            Date = date;
        }

    }
}
