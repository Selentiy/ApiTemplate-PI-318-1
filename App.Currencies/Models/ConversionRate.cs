using System;
using System.Collections.Generic;

namespace App.Currencies.Models
{
    public class ConversionRate
    {
        public IEnumerable<Currency> Currencies { get; set; }
        public DateTime Date { get; set; }

        public ConversionRate(DateTime date)
        {
            Currencies = new List<Currency>();
            Date = date;
        }
    }
}
