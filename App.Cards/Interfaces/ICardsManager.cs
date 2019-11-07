using System;
using App.Models.Cards;

namespace App.Cards.Interfaces
{
    public interface ICardsManager
    {
        Card GetCard(long number, DateTime expiresEnd, ushort CVV);
        void SetLimit(long number, DateTime expiresEnd, ushort CVV, int limit);
        void RemoveLimit(long number, DateTime expiresEnd, ushort CVV);
        void BlockCard(long number, DateTime expiresEnd, ushort CVV);
    }
}
