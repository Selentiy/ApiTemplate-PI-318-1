using System;

namespace App.News.Exceptions
{
    public class EntityNoContentException : Exception
    {
        public Type EntityType { get; private set; }

        public EntityNoContentException(Type entityType)
        {
            EntityType = entityType;
        }

        public EntityNoContentException(Type entityType, string message) : base(message)
        {
            EntityType = entityType;
        }
    }
}
