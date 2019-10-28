using System;
using App.Models.Example;

namespace App.Example.Dto
{
    public class SimpleValueDto
    {
        public string Value { get; set; }
        public SimpleValueDto(SimpleValue simpleValue)
        {
            if (simpleValue == null)
                throw new ArgumentNullException(nameof(simpleValue));
            this.Value = simpleValue.Value;
        }
    }
}
