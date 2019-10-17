using App.Configuration;
using App.Models;
using App.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.RegularPayments
{
    public interface ICreateManager
    {
        bool AddRegularPaymant(RegularPayment regularPayment);
    }
    public class CreateManager : ICreateManager, ITransientDependency
    {
        readonly IPaymentsRepository _repository;
        public CreateManager(IPaymentsRepository repo)
        {
            _repository = repo;
        }

        public bool AddRegularPaymant(RegularPayment regularPayment)
        {

            if (regularPayment == null && regularPayment.Payer == regularPayment.Recipient)
                return false;
            _repository.CreateRegularPayment(regularPayment);
            return true;

        }
    }
}

