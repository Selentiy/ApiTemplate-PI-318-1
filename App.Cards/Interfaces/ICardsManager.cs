using System;
using System.Collections.Generic;
using System.Text;
using App.Models.Cards.Models;

namespace App.Cards.Interfaces
{
    public interface ICardsManager
    {
        Card GetCard(long number, DateTime expiresEnd, ushort CVV);
        bool SetLimit(long number, DateTime expiresEnd, ushort CVV, int limit);
        bool RemoveLimit(long number, DateTime expiresEnd, ushort CVV);
        bool BlockCard(string ownerName, long number, DateTime expiresEnd, ushort CVV);
    }
}
