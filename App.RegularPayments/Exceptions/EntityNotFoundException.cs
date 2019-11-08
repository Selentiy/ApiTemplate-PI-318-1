using System;
using System.Collections.Generic;
using System.Text;

namespace App.RegularPayments.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public Type EntityType { get; private set; }
        public int EntityId { get; private set; }

        public EntityNotFoundException(Type entityType, int entityId) :
            base($"Entity: {entityType.Name}, id {entityId} does't exist.")
        {
            EntityType = entityType;
            EntityId = entityId;
        }
    }
}
