using System.Collections.Generic;
using App.Configuration;
using App.Loans.Interface;

namespace App.Loans
{

    public class LoansManager : ILoanManger, ITransientDependency
    {
        readonly ILoanRepository _repository;
        public LoansManager(ILoanRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<string> GetValuesInStringArray()
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
