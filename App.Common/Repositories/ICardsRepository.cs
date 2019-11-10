using App.Models.Cards;
using System;

namespace App.Repositories
{
    public interface ICardsRepository
    {
        Card GetCard(long number, DateTime expiresEnd, ushort CVV);
        void BlockCard(long number, DateTime expiresEnd, ushort CVV);
        void RemoveLimit(long number, DateTime expiresEnd, ushort CVV);
        void SetLimit(long number, DateTime expiresEnd, ushort CVV, int limit);
    }
}
