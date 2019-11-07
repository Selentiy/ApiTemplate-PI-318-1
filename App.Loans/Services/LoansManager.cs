using System.Collections.Generic;
using App.Configuration;
using App.Loans.Interface;
using App.Loans.Models;

namespace App.Loans
{

    public class LoansManager : ILoanManger, ITransientDependency
    {
        readonly ILoanRepository _repository;
        public LoansManager(ILoanRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Loan> GetLoans()
        {
            return _repository.GetLoans();
        }

        public IEnumerable<string> GetValuesInStringArray()
        {
            return _repository.GetValues();
        }

        public IEnumerable<string> AmountOfPaymentsLeft(int Id)
        {
            string a = System.Convert.ToString(_repository.Get(Id).AmountOfPaymentsLeft());
            return new string[] { a };
        }
    }
}
