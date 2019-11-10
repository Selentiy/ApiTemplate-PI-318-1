using System;

namespace App.Cards.Exceptions
{
    class PastDateException : Exception
    {
        public long Number { get; private set; }
        public DateTime Date { get; private set; }
        public PastDateException(long number, DateTime date) :
            base($"The date {date.ToString("yyyy-MM--dd")} has already passed.")
        {
            Number = number;
            Date = date;
        }
    }
}
