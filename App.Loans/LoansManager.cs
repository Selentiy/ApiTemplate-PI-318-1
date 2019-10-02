using System.Collections.Generic;
using App.Configuration;
using App.Repositories;

namespace App.Loans
{
    /// <summary>
    /// Example manager class. Which should process business logic, and call required repository
    /// </summary>
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
