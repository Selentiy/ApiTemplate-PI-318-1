using System;
using System.Collections.Generic;
using System.Text;
using App.Configuration;
using App.Models.Cards.Models;
using App.Repositories;

namespace App.Cards.Repositories
{
    public class InMemoryCardsRepository : ICardsRepository, ISingletoneDependency
    {
        List<Card> cards = new List<Card>();

        public InMemoryCardsRepository()
        {
            AddSomeCard();
        }
        private void AddSomeCard()
        {
            var card1 = new Card
            {
                OwnerName = "Noah",
                Number = 1234_5678_9012_3456,
                ExpiresEnd = new DateTime(2020, 10, 2),
                CVV = 123,
                Bill = 10_000,
                Limit = 0,
                IsPayPass = true,
                IsBlocked = false
            };
            var card2 = new Card
            {
                OwnerName = "Liam",
                Number = 3214_1111_3333_2222,
                ExpiresEnd = new DateTime(2022, 4, 12),
                CVV = 556,
                Bill = 0,
                Limit = 200,
                IsPayPass = false,
                IsBlocked = false
            };
            var card3 = new Card
            {
                OwnerName = "Mason",
                Number = 5167_1111_2222_3333,
                ExpiresEnd = new DateTime(2019, 9, 1),
                CVV = 911,
                Bill = 0,
                Limit = 1000,
                IsPayPass = true,
                IsBlocked = true
            };

            cards = new List<Card> { card1, card2, card3 };
        }

        private Card GetCardByNumber(long number)
        {
            Card result = null;

            foreach (var card in cards)
            {
                if (card.Number == number)
                {
                    result = card;
                    break;
                }
            }

            return result;
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

            return result;
        }

        public bool BlockCard(string ownerName, long number, DateTime expiresEnd, ushort CVV)
        {
            bool result = false;
            Card temp = GetCardByNumber(number);

            if (temp != null)
            {
                if (temp.ExpiresEnd != expiresEnd || temp.CVV != CVV || temp.OwnerName != ownerName) 
                    temp = null;
            }

            if (temp != null) 
            { 
                temp.IsBlocked = true; 
                result = true; 
            } 
            return result;
        }

        public bool RemoveLimit(long number, DateTime expiresEnd, ushort CVV)
        {
            bool result = false;
            Card temp = GetCardByNumber(number);

            temp = CheckCardByCVV(temp, expiresEnd, CVV);

            if (temp != null)
            { 
                temp.Limit = null; 
                result = true; 
            }
            return result;
        }

        public bool SetLimit(long number, DateTime expiresEnd, ushort CVV, int limit)
        {
            bool result = false;
            Card temp = GetCardByNumber(number);

            temp = CheckCardByCVV(temp, expiresEnd, CVV);

            if (temp != null) 
            { 
                temp.Limit = limit; 
                result = true; 
            }
            return result;
        }
    }
}
