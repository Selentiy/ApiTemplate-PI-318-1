using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models.Currencies
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
