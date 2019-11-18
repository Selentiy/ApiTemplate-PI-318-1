using System;
using System.Collections.Generic;
using System.Linq;
using App.Configuration;
using App.Example.Database;
using App.Models.Example;
using App.Repositories;

namespace App.Example.Repositories
{
    // Implementation of repository, which uses EntityFramework as data storage
    public class EfValuesRepository : IValuesRepository, IDisposable, ITransientDependency
    {
        private readonly ExampleDbContext _dbContext;
        public EfValuesRepository(ExampleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public SimpleValue GetValueByKey(string key)
        {
            return _dbContext.SimpleValues.Where(sv => sv.Key == key).FirstOrDefault();
        }

        public IEnumerable<SimpleValue> GetValues()
        {
            var values = _dbContext.SimpleValues.ToList();
            return values;
        }

        // whn disposing, should dispose related DbContext to prevent memory leak
        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}