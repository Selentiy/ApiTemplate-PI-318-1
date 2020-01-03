using System;

namespace App.Accounts.Exceptions
{
    public class InvalidBusinessOperationException : InvalidOperationException
    {
        public InvalidBusinessOperationException(string message) : base(message)
        {

        }
    }
}
