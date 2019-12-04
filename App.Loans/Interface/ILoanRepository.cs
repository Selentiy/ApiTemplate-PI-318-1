using System.Linq;
using App.Loans.Models;

namespace App.Loans.Interface
{
    public interface ILoanRepository
    {
        IQueryable<Loan> GetLoans();

        Loan Get(int Id);
    }
}
