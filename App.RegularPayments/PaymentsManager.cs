using App.Configuration;
using App.Models;
using App.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.RegularPayments
{
    public interface IPaymentsManager
    {
        bool AddRegularPaymant(RegularPayment regularPayment);
        IEnumerable<RegularPayment> GetRegularPayments();
        RegularPayment GetRegularPaymentsById(int id);
        DateTime ShowNextPaymentData(int id);
    }

    public class PaymentsManager : IPaymentsManager, ITransientDependency
    {
        readonly IRegularPaymentsRepository _repository;
        public PaymentsManager(IRegularPaymentsRepository repo)
        {
            _repository = repo;
        }

        public bool AddRegularPaymant(RegularPayment regularPayment)
        {

            if (regularPayment == null || regularPayment.Payer.Equals(regularPayment.Recipient))
                return false;

            var sameID = _repository.GetRegularPayments().FirstOrDefault(rp => rp.PaymentID == regularPayment.PaymentID);
            if (sameID != null)
                return false;

            _repository.CreateRegularPayment(regularPayment);
            return true;
        }
 
        public IEnumerable<RegularPayment> GetRegularPayments()
        {
            return _repository.GetRegularPayments();
        }

        public RegularPayment GetRegularPaymentsById(int id)
        {
            return _repository.GetRegularPaymentById(id);
        }

        public DateTime ShowNextPaymentData(int id)
        {
            RegularPayment regularPayment = GetRegularPaymentsById(id);
            return regularPayment.DateOfLastPay.AddDays(regularPayment.Period);
        }
    }
}
