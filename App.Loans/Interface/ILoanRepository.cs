using System.Collections.Generic;
using App.Loans.Models;

namespace App.Loans.Interface
{
    public interface ILoanRepository
    {
        IEnumerable<Loan> GetLoans();

        Loan Get(int Id);
    }
}
