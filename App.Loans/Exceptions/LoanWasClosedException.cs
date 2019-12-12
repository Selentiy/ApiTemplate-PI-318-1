using System;
using System.Collections.Generic;

namespace App.Loans.Exceptions
{
    public class LoanWasClosedException : Exception
    {
        /// <summary>
        /// Id = Id кредита 
        /// </summary>
        public int Id { get; private set; }

        public LoanWasClosedException(int Id)
        {
            this.Id = Id;
        }
    }
}
