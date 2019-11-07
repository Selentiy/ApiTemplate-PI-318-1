using System.Collections.Generic;
using App.Loans.Models;

namespace App.Loans.Interface
{
    public interface ILoanRepository
    {
        IEnumerable<string> GetValues();
        IEnumerable<Loan> GetLoans();

        Loan Get(int Index);
    }
}
