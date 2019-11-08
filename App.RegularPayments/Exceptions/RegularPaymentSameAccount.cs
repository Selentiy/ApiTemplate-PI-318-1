using System;
using System.Collections.Generic;
using System.Text;

namespace App.RegularPayments.Exceptions
{
    public class RegularPaymentSameAccount : ValidationRegularPaymantException
    {
        public RegularPaymentSameAccount(string paramName) : base(paramName, $"Invoices of Payer and Recipient {paramName} are same") { }
    }
}
