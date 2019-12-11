using System;
using System.Collections.Generic;
using System.Text;

namespace App.RegularPayments.Exceptions
{
    public class ValidationRegularPaymentException : ArgumentException
    {
        public ValidationRegularPaymentException(string paramName, string message) : base(message, paramName) { }
    }
}
