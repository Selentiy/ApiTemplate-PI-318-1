using System.Collections.Generic;
using App.Configuration;
using App.Repositories;
using App.Loans.Interface;

namespace App.Loans
{

    public class LoansManager : ILoansManager, ITransientDependency
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
    }
}
