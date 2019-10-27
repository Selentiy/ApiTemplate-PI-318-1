﻿using App.Configuration;
using App.Models;
using App.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.RegularPayments
{
    public interface ICreateManager
    {
        bool AddRegularPaymant(RegularPayment regularPayment);
    }
    public interface IPaymentsManager
    {
        IEnumerable<RegularPayment> GetRegularPayments();
        RegularPayment GetRegularPaymentsById(int id);
    }
    public interface IShowNextServise
    {
        DateTime ShowNextData(int id);
    }

    public class PaymentsManager : IPaymentsManager, ICreateManager, IShowNextServise, ITransientDependency
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

        public DateTime ShowNextData(int id)
        {
            return GetRegularPaymentsById(id).DateOfLastPay.AddDays(GetRegularPaymentsById(id).Period);
        }
    }
}
