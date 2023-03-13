using System.Collections.Generic;
using App.Configuration;
using App.Loans.Interface;
using App.Loans.Models;
using Microsoft.Extensions.Logging;
using App.Loans.Exceptions;

namespace App.Loans
{
    public class LoansManager : ILoanManger, ITransientDependency
    {
        readonly ILoanRepository _repository;
        private readonly ILogger<LoansManager> _logger;

        public LoansManager(ILoanRepository repository,
            ILogger<LoansManager> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        public IEnumerable<Loan> GetLoans()
        {
            return _repository.GetLoans();
        }

        public string AmountOfPaymentsLeft(int Id)
        {
            var GetLoan = _repository.Get(Id);
            _logger.LogDebug("Method:AmountOfPaymentsLeft");
            if (GetLoan == null)
                throw new EntityNotFoundException(typeof(Loan));
            _logger.LogDebug("Method:AmountOfPaymentsLeft");
            if (GetLoan.AmountOfPaymentsLeft() == 0)
                throw new LoanWasClosedException(Id);
            string a = System.Convert.ToString(GetLoan.AmountOfPaymentsLeft());
            return a;
        }
    }
}
