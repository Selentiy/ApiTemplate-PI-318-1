using System.Collections.Generic;
using App.Loans.Models;

namespace App.Loans.Interface
{
    public interface ILoanRepo
    {
        IEnumerable<string> GetValues();
        Loan Get(int Index);
    }
}
