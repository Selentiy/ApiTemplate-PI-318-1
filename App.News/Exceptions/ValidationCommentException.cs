using System;

namespace App.News.Exceptions
{
    public class ValidationCommentException : ArgumentException
    {
        public ValidationCommentException(string paramName, string message) : base(message, paramName)
        {

        }
    }
}
