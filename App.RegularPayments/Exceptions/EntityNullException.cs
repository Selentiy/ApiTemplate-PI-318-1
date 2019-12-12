using System;
using System.Collections.Generic;
using System.Text;

namespace App.RegularPayments.Exceptions
{
    public class EntityNullException: Exception
    {
        public Type EntityType { get; private set; }
        public EntityNullException(Type entityType) :
           base($"Entity: {entityType.Name}, is null")
        {
            EntityType = entityType;
        }

    }
}
