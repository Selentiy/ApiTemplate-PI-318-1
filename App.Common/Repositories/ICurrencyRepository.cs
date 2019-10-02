using System;
using System.Collections.Generic;

namespace App.Repositories
{
    public interface ICurrencyRepository
    {
        IEnumerable<string> GetCurrencyCodes();
        decimal GetRate(string code, DateTime date);
    }
}
