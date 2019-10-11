using System;
using System.Collections.Generic;
using System.Text;
using App.Configuration;
using App.Models.Cards.Models;
using App.Repositories;

namespace App.Cards.Repositories
{
    public class InMemoryCardsRepository: ICardsRepository, ITransientDependency
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

            cards = new List<Card> { card1, card2, card3};
        }

        public Card GetCard(long number, DateTime expiresEnd, ushort CVV)
        {
            Card result = null;
            foreach(var card in cards)
            {
                if (card.Number == number) 
                { 
                    result = card; 
                    break; 
                }
            }

            if (result != null)
            {
                if (result.ExpiresEnd != expiresEnd || result.CVV != CVV)
                    result = null;
            }

            return result;
        }

        public bool BlockCard(string ownerName, long number, DateTime expiresEnd, ushort CVV)
        {
            bool result = false;
            Card temp = null;

            foreach (var card in cards)
            {
                if (card.Number == number) 
                { temp = card; 
                    break; 
                }
            }

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
            Card temp = null;

            foreach (var card in cards)
            {
                if (card.Number == number) 
                { 
                    temp = card; 
                    break; 
                }
            }

            if (temp != null)
            {
                if (temp.ExpiresEnd != expiresEnd || temp.CVV != CVV)
                    temp = null;
            }

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
            Card temp = null;

            foreach (var card in cards)
            {
                if (card.Number == number) 
                { 
                    temp = card; 
                    break; 
                }
            }

            if (temp != null)
            {
                if (temp.ExpiresEnd != expiresEnd || temp.CVV != CVV)
                    temp = null;
            }

            if (temp != null) 
            { 
                temp.Limit = limit; 
                result = true; 
            }
            return result;
        }
    }
}
