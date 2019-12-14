using App.Cards.Database;
using App.Cards.Exceptions;
using App.Configuration;
using App.Models.Cards;
using App.Repositories;
using System;
using System.Linq;

namespace App.Cards.Repositories
{
    public class EFCardsRepository: ICardsRepository, IDisposable, ITransientDependency
    {
        private readonly CardsDbContext _dbContext;

        public EFCardsRepository(CardsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Card GetCardByNumber(long number)
        {
            return _dbContext.Cards.Where(c => c.Number == number).FirstOrDefault();
        }

        private Card CheckCardByCVV(Card card, DateTime expiresEnd, ushort CVV)
        {
            if (card != null)
            {
                if (card.ExpiresEnd != expiresEnd || card.CVV != CVV)
                    card = null;
            }

            return card;
        }

        

        public Card GetCard(long number, DateTime expiresEnd, ushort CVV)
        {
            Card result = GetCardByNumber(number);

            result = CheckCardByCVV(result, expiresEnd, CVV);

            if (result == null)
            {
                throw new EntityNotFoundException(number, typeof(Card));
            }

            return result;
        }
        private void CheckCardOperationsByExeption(long number, DateTime expiresEnd, ushort CVV)
        {
            Card temp = GetCard(number, expiresEnd, CVV);

            if (temp.IsBlocked)
            {
                throw new BlockedCardException(number, "Card is blocked");
            }
            else if (expiresEnd < DateTime.Now)
            {
                throw new PastDateException(number, expiresEnd);
            }
        }
        public void BlockCard(long number, DateTime expiresEnd, ushort CVV)
        {
            Card temp = GetCard(number, expiresEnd, CVV);
            CheckCardOperationsByExeption(number, expiresEnd, CVV);

            temp.IsBlocked = true;

            _dbContext.Update(temp);
            _dbContext.SaveChanges();
        }
        public void RemoveLimit(long number, DateTime expiresEnd, ushort CVV)
        {
            Card temp = GetCard(number, expiresEnd, CVV);
            CheckCardOperationsByExeption(number, expiresEnd, CVV);

            temp.Limit = null;
            _dbContext.Update(temp);
            _dbContext.SaveChanges();
        }

        public void SetLimit(long number, DateTime expiresEnd, ushort CVV, int limit)
        {
            Card temp = GetCard(number, expiresEnd, CVV);
            CheckCardOperationsByExeption(number, expiresEnd, CVV);

            temp.Limit = limit;
            _dbContext.Update(temp);
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
