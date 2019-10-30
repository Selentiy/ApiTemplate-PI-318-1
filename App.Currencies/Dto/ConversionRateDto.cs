using System;
using System.Collections.Generic;
using App.Models.Currencies;


namespace App.Currencies.Dto
{
    public class ConversionRateDto
    {
        public IEnumerable<KeyValuePair<string, decimal>> Currencies { get; set; }
        public DateTime Date { get; set; }

        public ConversionRateDto(ConversionRate conversionRate)
        {
            if (conversionRate == null)
                throw new ArgumentNullException(nameof(conversionRate));
            this.Currencies = conversionRate.Currencies;
            this.Date = conversionRate.Date;
        }
    }
}
