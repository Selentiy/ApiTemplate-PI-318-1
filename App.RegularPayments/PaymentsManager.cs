using App.Configuration;
using App.RegularPayments.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.RegularPayments
{
    public interface IPaymentsManager
    {
        IEnumerable<RegularPayment> GetRegularPayments();

    }

    public class PaymentsManager : IPaymentsManager, ITransientDependency
    {
        readonly IPaymentsManager _repository;
        public PaymentsManager(IPaymentsManager repo)
        {
            _repository = repo;
        }

        public IEnumerable<RegularPayment> GetRegularPayments()
        {
            return _repository.GetRegularPayments();
        }
    }
}
