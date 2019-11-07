using System;
using App.Cards.Interfaces;
using App.Configuration;
using App.Models.Cards;
using App.Repositories;
using Microsoft.Extensions.Logging;

namespace App.Cards
{
    public class CardsManager : ICardsManager, ITransientDependency
    {
        private readonly ICardsRepository _repository;
        private ILogger<CardsManager> _logger;

        public CardsManager(ICardsRepository repository, ILogger<CardsManager> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public void BlockCard(long number, DateTime expiresEnd, ushort CVV)
        {
            _logger.LogInformation("Call BlockCard method");
            _repository.BlockCard(number, expiresEnd, CVV);
        }

        public Card GetCard(long Number, DateTime ExpiresEnd, ushort CVV)
        {
            _logger.LogInformation("Call GetCard method");
            return _repository.GetCard(Number, ExpiresEnd, CVV);
        }

        public void RemoveLimit(long number, DateTime expiresEnd, ushort CVV)
        {
            _logger.LogInformation("Call RemoveLimit method");
            _repository.RemoveLimit(number, expiresEnd, CVV);
        }

        public void SetLimit(long number, DateTime expiresEnd, ushort CVV, int limit)
        {
            _logger.LogInformation("Call SetLimit method from CardsManager");
            _repository.SetLimit(number, expiresEnd, CVV, limit);
        }
    }
}
