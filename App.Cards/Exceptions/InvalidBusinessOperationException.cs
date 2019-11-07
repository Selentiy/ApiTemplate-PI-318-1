using System;

namespace App.Cards.Exceptions
{
    public class InvalidBusinessOperationException: Exception
    {
        public long Number { get; private set; }
        public InvalidBusinessOperationException(long number, string message) : base(message) 
        {
            Number = number;
        }
    }
}
