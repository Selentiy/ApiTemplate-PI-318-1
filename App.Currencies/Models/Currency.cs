using System;
using System.Collections.Generic;
using System.Text;

namespace App.Currencies.Models
{
    public class Currency
    {
        public string Name { get; set; }
        public decimal Rate { get; set; }

        public Currency(string name, decimal rate)
        {
            Name = name;
            Rate = rate;
        }
    }
}
