using App.Configuration;
using App.Models;
using App.RegularPayments.Exceptions;
using App.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.RegularPayments
{
    public interface IPaymentsManager
    {
        void AddRegularPayment(RegularPayment regularPayment);
        IEnumerable<RegularPayment> GetRegularPayments();
        RegularPayment GetRegularPaymentsById(int id);
        DateTime ShowNextPaymentData(int id);
    }

    public class PaymentsManager : IPaymentsManager, ITransientDependency
    {
        readonly IRegularPaymentsRepository _repository;
        readonly ILogger<PaymentsManager> _logger;
        public PaymentsManager(IRegularPaymentsRepository repo, ILogger<PaymentsManager> logger)
        {
            _repository = repo;
            _logger = logger;
        }

        public void AddRegularPayment(RegularPayment regularPayment)
        {
            _logger.LogInformation("AddRegularPaymant method");
            if (regularPayment == null)
                throw new EntityNullException(typeof(RegularPayment));

            if (regularPayment.Payer.Equals(regularPayment.Recipient))
            {
                throw new RegularPaymentSameAccount(regularPayment.Payer);
            }

            var sameID = _repository.GetRegularPayments().FirstOrDefault(rp => rp.PaymentID == regularPayment.PaymentID);
            if (sameID != null)
            {
                throw new RegularPaymentSameId(regularPayment.PaymentID.ToString());
            }

            _repository.CreateRegularPayment(regularPayment);
        }
 
        public IEnumerable<RegularPayment> GetRegularPayments()
        {
            _logger.LogInformation("GetRegularPayments method");
            return _repository.GetRegularPayments();
        }

        public RegularPayment GetRegularPaymentsById(int id)
        {
            _logger.LogInformation("GetRegularPaymentsById with id {id}", id);
            var regularPayment = _repository.GetRegularPaymentById(id);
            if (regularPayment == null)
            {
                throw new EntityNotFoundException(typeof(RegularPayment), id);
            }
            return regularPayment;
        }

        public DateTime ShowNextPaymentData(int id)
        {
            _logger.LogInformation("ShowNextPaymentData with id {id}", id);
            RegularPayment regularPayment = GetRegularPaymentsById(id);
            if (regularPayment == null)
            {
                throw new EntityNotFoundException(typeof(RegularPayment), id);
            }
            return regularPayment.DateOfLastPay.AddDays(regularPayment.Period);
        }
    }
}
