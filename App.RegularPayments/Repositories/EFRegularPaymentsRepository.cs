using App.Configuration;
using App.Models;
using App.RegularPayments.Database;
using App.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.RegularPayments.Repositories
{
    public class EFRegularPaymentsRepository : IRegularPaymentsRepository, ITransientDependency, IDisposable
    {
        private readonly RegularPaymentsDbContext _dbContext;

        public EFRegularPaymentsRepository(RegularPaymentsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateRegularPayment(RegularPayment regularPayment)
        {
            _dbContext.Add(regularPayment);
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        public RegularPayment GetRegularPaymentById(int id)
        {
            var regularPayment = _dbContext.RegularPayments.Where(p => p.PaymentID == id).FirstOrDefault();
            return regularPayment;
        }

        public IEnumerable<RegularPayment> GetRegularPayments()
        {
            var regularPayments = _dbContext.RegularPayments.ToList();
            return regularPayments;
        }
    }
}
