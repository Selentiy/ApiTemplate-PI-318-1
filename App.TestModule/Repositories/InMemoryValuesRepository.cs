using System;
using System.Collections.Generic;
using System.Linq;
using App.Models.Example;
using App.Repositories;

namespace App.Example.Repositories
{
    /// <summary>
    /// Fake repository implementation, which stores value in memory
    /// </summary>
    public class InMemoryValuesRepository : IValuesRepository
    {
        static IReadOnlyCollection<SimpleValue> Values = new List<SimpleValue>
        {
            new SimpleValue { Value = "value1" },
            new SimpleValue { Value = "value2" },
        };

        /// <inheritdoc/>
        public SimpleValue GetValueByKey(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            return Values.FirstOrDefault(x => x.Value == key);
        }

        /// <inheritdoc/>
        public IEnumerable<SimpleValue> GetValues()
        {
            return Values;
        }
    }
}