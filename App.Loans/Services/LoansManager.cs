using System.Collections.Generic;
using App.Configuration;
using App.Repositories;
using App.Loans.Interface;

namespace App.Loans
{
    public interface ILoansManager
    {
        IEnumerable<string> GetValues();
    }

    public class LoansManager : ILoansManager, ITransientDependency
    {
        // propoerty should be readonly, so it could not be changed after initialization
        readonly IValuesRepository _repository;
        // resolving repository through constructor dependency
        public LoansManager(IValuesRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<string> GetValues()
        {
            return _repository.GetValues();
        }
    }
}
