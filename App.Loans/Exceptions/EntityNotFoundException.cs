using System;
using System.Collections.Generic;

namespace App.Loans.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        /// <summary>
        /// TypeOfEntity = Тип модели которая не была найдена, в нашем случае Loan
        /// </summary>
        public Type TypeOfEntity { get; private set; }

        public EntityNotFoundException(Type typeOfEntity)
        {
            TypeOfEntity = typeOfEntity;
        }
    }
}
