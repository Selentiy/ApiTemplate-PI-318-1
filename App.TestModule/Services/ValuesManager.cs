using System;
using System.Collections.Generic;
using App.Configuration;
using App.Example.Exceptions;
using App.Models.Example;
using App.Repositories;

namespace App.Example.Services
{
    /// <summary>
    /// Example manager class. Which should process business logic, and call required repository
    /// </summary>
    public interface IValuesManager
    {
        /// <summary>
        /// Simple method
        /// </summary>
        IEnumerable<SimpleValue> GetValues();

        /// <exception cref="EntityNotFoundException"></exception>
        SimpleValue GetValueByKey(string key);

        /// <exception cref="InvalidBusinessOperationException"></exception>
        void PerformBusinessOperation();
    }

    public class ValuesManager : IValuesManager, ITransientDependency
    {
        // propoerty should be readonly, so it could not be changed after initialization
        readonly IValuesRepository _repository;
        // resolving repository through constructor dependency
        public ValuesManager(IValuesRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        public SimpleValue GetValueByKey(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var value = _repository.GetValueByKey(key);
            if (value == null)
                throw new EntityNotFoundException(typeof(SimpleValue));

            return value;
        }

        /// <inheritdoc/>
        public IEnumerable<SimpleValue> GetValues()
        {
            return _repository.GetValues();
        }

        /// <inheritdoc/>
        public void PerformBusinessOperation()
        {
            throw new InvalidBusinessOperationException("Invalid operation");
        }
    }
}
