using System.Collections.Generic;
using App.Models.Example;

namespace App.Repositories
{
    /// <summary>
    /// This is example repository, which provide single method to access values
    /// </summary>
    public interface IValuesRepository
    {
        IEnumerable<SimpleValue> GetValues();

        SimpleValue GetValueByKey(string key);
    }
}
