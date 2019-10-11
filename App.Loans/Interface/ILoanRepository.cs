using System.Collections.Generic;
using App.Loans.Models;

namespace App.Loans.Interface
{
    public interface ILoanRepository
    {
        IEnumerable<string> GetValues();
        Loan Get(int Index);
    }
}
