using System;
using System.Collections.Generic;
using System.Text;

namespace App.RegularPayments.Exceptions
{
    public class RegularPaymentSameId : ValidationRegularPaymentException
    {
        public RegularPaymentSameId(string paramName) : base(paramName, $"ID {paramName} has been existing") { }
    }
}
