using System;

namespace App.Currencies.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public Type EntityType { get; private set; }
        public EntityNotFoundException(string message, Type entityType) 
            : base(message)
        {
            EntityType = entityType;
        }
    }
}
