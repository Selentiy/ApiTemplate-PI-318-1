using App.Models.Currencies;
using System;
using System.Collections.Generic;

namespace App.Repositories
{
    public interface ICurrencyRepository
    {
        ConversionRate GetConversionRate(DateTime date);
    }
}
