using App.Models;
using System.Collections.Generic;

namespace App.Repositories
{
    public interface IRegularPaymentsRepository
    {
        IEnumerable<RegularPayment> GetRegularPayments();
        void CreateRegularPayment(RegularPayment regularPayment);
        RegularPayment GetRegularPaymentById(int id);
    }
}