using System;
using System.Collections.Generic;
using System.Text;
using App.Cards.Interfaces;
using App.Configuration;
using App.Models.Cards.Models;
using App.Repositories;

namespace App.Cards
{
    public class CardsManager : ICardsManager, ITransientDependency
    {
        private readonly ICardsRepository _repository;

        public CardsManager(ICardsRepository repository)
        {
            _repository = repository;
        }

        public bool BlockCard(string OwnerName, long Number, DateTime ExpiresEnd, ushort CVV)
        {
            return _repository.BlockCard(OwnerName, Number, ExpiresEnd, CVV);
        }

        public Card GetCard(long Number, DateTime ExpiresEnd, ushort CVV)
        {
            return _repository.GetCard(Number, ExpiresEnd, CVV);
        }

        public bool RemoveLimit(long number, DateTime expiresEnd, ushort CVV)
        {
            return _repository.RemoveLimit(number, expiresEnd, CVV);
        }

        public bool SetLimit(long number, DateTime expiresEnd, ushort CVV, int limit)
        {
            return _repository.SetLimit( number, expiresEnd, CVV, limit);
        }
    }
}
