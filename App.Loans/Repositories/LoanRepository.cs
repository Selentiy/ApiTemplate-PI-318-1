using System;
using System.Collections.Generic;
using System.Text;
using App.Loans.Interface;

namespace App.Loans.Repositories
{
    public class LoanRepository : ILoanRepo
    {

        public IEnumerable<string> GetValues()
        {
            return new string[] { ""};
        }
    }
}
