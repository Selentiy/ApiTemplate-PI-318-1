using System;
using System.Collections.Generic;
using System.Text;

namespace App.Accounts.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public Type EntityType { get; private set; }

        public EntityNotFoundException(Type entityType) : 
            base($"Entity with type {entityType.Name} not found.")
        {
            EntityType = entityType;
        }

        public EntityNotFoundException(Type entityType, string message) : base(message)
        {
            EntityType = entityType;
        }
    }
}
