using System.Collections.Generic;
using App.Configuration;
using App.Loans.Interface;

namespace App.Loans
{

    public class LoansManager : ILoanManger, ITransientDependency
    {
        readonly ILoanRepo _repository;
        public LoansManager(ILoanRepo repository)
        {
            _repository = repository;
        }

        public IEnumerable<string> GetValues()
        {
            return _repository.GetValues();
        }

        public IEnumerable<string> AmountOfPaymentsLeft(int Index)
        {
            string a = _repository.Get(Index).AmountOfPaymentsLeft();
            return new string[] { a };
        }
    }
}
