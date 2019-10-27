using App.Configuration;
using App.Models;
using App.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.RegularPayments.Repositories
{
    public class InMemoryRegularPaymentRepository : IRegularPaymentsRepository, ISingletoneDependency
    {
        private IEnumerable<RegularPayment> regularpayments;

        public InMemoryRegularPaymentRepository()
        {
            regularpayments = RegularPaymentsInitializer.GetRegularPayments();
        }

        public IEnumerable<RegularPayment> GetRegularPayments()
        {
            return regularpayments;
        }

        public void CreateRegularPayment(RegularPayment regularPayment)
        {
            regularpayments = regularpayments.Append(regularPayment);
        }

        public RegularPayment GetRegularPaymentById(int id)
        {
            return regularpayments.FirstOrDefault(rp => rp.PaymentID == id);
        }
    }

    public static class RegularPaymentsInitializer
    {
        public static List<RegularPayment> GetRegularPayments()
        {
            return new List<RegularPayment>()
            {
                new RegularPayment()
                {
                    PaymentID = 0,
                    Payer = "1111-2222-3333-4444",
                    Recipient = "5555-6666-7777-8888",
                    Payment = 57.15,
                    Period = 30,
                   // MaxPayment = 10,
                    DateOfLastPay = DateTime.Now
                },
                new RegularPayment()
                {
                    PaymentID = 1,
                    Payer = "7777-8888-3333-4444",
                    Recipient = "9999-6666-7777-8888",
                    Payment = 99.99,
                    Period = 15,
               //     MaxPayment = 0,
                    DateOfLastPay = DateTime.Now
                }
            };
        }
    }
}
