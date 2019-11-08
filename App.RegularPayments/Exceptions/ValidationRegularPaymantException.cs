using System;
using System.Collections.Generic;
using System.Text;

namespace App.RegularPayments.Exceptions
{
    public class ValidationRegularPaymantException : ArgumentException
    {
        public ValidationRegularPaymantException(string paramName, string message) : base(message, paramName) { }
    }
}
