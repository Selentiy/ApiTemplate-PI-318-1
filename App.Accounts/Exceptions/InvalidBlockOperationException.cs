using System;

namespace App.Accounts.Exceptions
{
    public class InvalidBlockOperationException : InvalidBusinessOperationException
    {
        public Type EntityType { get; private set; }
        public int ItemId { get; private set; }

        public InvalidBlockOperationException(Type entityType, int itemId) : 
            base($"Item {entityType} with id {itemId} is already blocked")
        {
            EntityType = entityType;
            ItemId = itemId;
        }
    }
}
