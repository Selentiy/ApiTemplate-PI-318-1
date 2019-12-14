using System;

namespace App.News.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public Type EntityType { get; private set; }
        public int EntityId { get; private set; }

        public EntityNotFoundException(Type entityType, int entityId) : 
            base($"entity {entityType.Name} with id {entityId} does't exist.")
        {
            EntityType = entityType;
            EntityId = entityId;
        }
    }
}
