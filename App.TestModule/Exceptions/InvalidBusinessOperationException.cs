using System;

namespace App.Example.Exceptions
{
    public class InvalidBusinessOperationException : InvalidOperationException
    {
        public InvalidBusinessOperationException(string message) : base(message) { }
    }
}
