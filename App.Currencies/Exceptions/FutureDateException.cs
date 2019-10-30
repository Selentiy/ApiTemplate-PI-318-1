using System;
using System.Collections.Generic;
using System.Text;

namespace App.Currencies.Exceptions
{
    public class FutureDateException : Exception
    {
        public DateTime Date { get; private set; }

        public FutureDateException() { }

        public FutureDateException(DateTime date)
        {
            Date = date;
        }

        public FutureDateException(string message) : base(message) { }

        public FutureDateException(string message, DateTime date) : base(message)
        {
            Date = date;
        }

        public FutureDateException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}
