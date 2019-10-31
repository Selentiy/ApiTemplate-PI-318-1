using App.Models.Cards.Models;
using System;

namespace App.Repositories
{
    public interface ICardsRepository
    {
        Card GetCard(long number, DateTime expiresEnd, ushort CVV);
        bool BlockCard(string ownerName, long number, DateTime expiresEnd, ushort CVV);
        bool RemoveLimit(long number, DateTime expiresEnd, ushort CVV);
        bool SetLimit(long number, DateTime expiresEnd, ushort CVV, int limit);
    }
}
