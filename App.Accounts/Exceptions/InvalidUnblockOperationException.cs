using System;

namespace App.Accounts.Exceptions
{
    public class InvalidUnblockOperationException : InvalidBusinessOperationException
    {
        public Type EntityType { get; private set; }
        public int ItemId { get; private set; }

        public InvalidUnblockOperationException(Type entityType, int itemId) :
            base($"Item {entityType.Name} with id {itemId} is already unblocked")
        {
            EntityType = entityType;
            ItemId = itemId;
        }
    }
}
