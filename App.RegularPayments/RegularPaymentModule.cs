using App.Configuration;
using App.RegularPayments.Database;
using Castle.Windsor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using App.Models;
using System.Text;
using Castle.MicroKernel.Registration;

namespace App.RegularPayments
{
    public class RegularPaymentModule : IModule
    {
        public void Initialize(IWindsorContainer container)
        {
            RegisterDbContext(container);
        }

        private void RegisterDbContext(IWindsorContainer container)
        {

            container.Register(Component.For<DbContextOptions<RegularPaymentsDbContext>>().UsingFactoryMethod(() =>
            {
                var builder = new DbContextOptionsBuilder<RegularPaymentsDbContext>();
                builder.UseInMemoryDatabase("RegularPaymentsDb");
                return builder.Options;
            }).LifestyleTransient());

            container.Register(Component.For<RegularPaymentsDbContext>().LifestyleTransient());

            InitializeDbContext(container);
        }

        private void InitializeDbContext(IWindsorContainer container)
        {
            using (var context = container.Resolve<RegularPaymentsDbContext>())
            {
                context.RegularPayments.AddRange(new[]
                {
                    new RegularPayment {   //PaymentID = 0,
                                           Payer = "1111-2222-3333-4444",
                                           Recipient = "5555-6666-7777-8888",
                                            Amount = 57.15,
                                            Period = 30,
                                             DateOfLastPay = DateTime.Now
                    },
                    new RegularPayment { //PaymentID = 1,
                                         Payer = "7777-8888-3333-4444",
                                           Recipient = "9999-6666-7777-8888",
                                              Amount = 99.99,
                                         Period = 15,
                                        DateOfLastPay = DateTime.Now
                    }
                });
                context.SaveChanges();
            }
        }
    }
}
