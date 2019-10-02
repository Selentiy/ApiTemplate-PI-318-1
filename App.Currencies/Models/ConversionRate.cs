using System;
using System.Collections.Generic;

namespace App.Currencies.Models
{
    public class ConversionRate
    {
        public IEnumerable<KeyValuePair<string, decimal>> Currencies { get; set; }
        public DateTime Date { get; set; }

        public ConversionRate(DateTime date)
        {
            Date = date;
        }
    }
}
