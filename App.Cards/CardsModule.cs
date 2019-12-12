using App.Cards.Database;
using App.Configuration;
using App.Models.Cards;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace App.Cards
{
    public class CardsModule : IModule
    {
        public void Initialize(IWindsorContainer container)
        {
            RegisterDbContext(container);
        }

        private void RegisterDbContext(IWindsorContainer container)
        {
            container.Register(Component
                .For<DbContextOptions<CardsDbContext>>()
                .UsingFactoryMethod(() =>
                {
                    var builder = new DbContextOptionsBuilder<CardsDbContext>();
                    // for test purpose we are using InMemory database
                    builder.UseInMemoryDatabase("CardsDb");
                    return builder.Options;
                })
                .LifestyleTransient());

            container.Register(Component.For<CardsDbContext>().LifestyleTransient());

            InitializeDbContext(container);
        }

        private void InitializeDbContext(IWindsorContainer container)
        {
            using (var context = container.Resolve<CardsDbContext>())
            {
                context.Cards.AddRange(AddSomeCards());

                context.SaveChanges();
            }
        }
        private Card[] AddSomeCards()
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

            return new Card[] { card1, card2, card3 };
        }
    }
}
