using System;
using App.Cards.Interfaces;
using App.Configuration;
using App.Models.Cards;
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

        public void BlockCard(long number, DateTime expiresEnd, ushort CVV)
        {
            _repository.BlockCard(number, expiresEnd, CVV);
        }

        public Card GetCard(long Number, DateTime ExpiresEnd, ushort CVV)
        {
            return _repository.GetCard(Number, ExpiresEnd, CVV);
        }

        public void RemoveLimit(long number, DateTime expiresEnd, ushort CVV)
        {
            _repository.RemoveLimit(number, expiresEnd, CVV);
        }

        public void SetLimit(long number, DateTime expiresEnd, ushort CVV, int limit)
        {
            _repository.SetLimit(number, expiresEnd, CVV, limit);
        }
    }
}
