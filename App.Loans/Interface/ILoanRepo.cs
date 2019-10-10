using System;
using System.Collections.Generic;
using System.Text;

namespace App.Loans.Interface
{
    public interface ILoanRepo
    {
        IEnumerable<string> GetValues();
    }
}
