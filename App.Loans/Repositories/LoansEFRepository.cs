using System;
using System.Collections.Generic;
using System.Linq;
using App.Loans.Interface;
using App.Loans.Models;
using App.Configuration;

namespace App.Loans.Repositories
{
    public class LoansEFRepository : ILoanRepository, ITransientDependency
    {
        private readonly LoansDBContext _loanDbContext;
        public LoansEFRepository(LoansDBContext loansDBContext)
        {
            _loanDbContext = loansDBContext;
        }
        public IQueryable<Loan> GetLoans() => _loanDbContext.Loans;
        public Loan Get(int id) => _loanDbContext.Loans.Where(f => f.Id == id).FirstOrDefault();
    }
}
