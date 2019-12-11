using System.Linq;
using App.Loans.Models;
using System.Collections.Generic;
namespace App.Loans.Interface
{
    public interface ILoanRepository
    {
        IEnumerable<Loan> GetLoans();

        Loan Get(int Id);
    }
}
