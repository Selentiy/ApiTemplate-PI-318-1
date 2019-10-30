using App.Models;
using System.Collections.Generic;

namespace App.Repositories
{
    public interface IRegularPaymentsRepository
    {
        IEnumerable<RegularPayment> GetRegularPayments();
        bool CreateRegularPayment(RegularPayment regularPayment);
        RegularPayment GetRegularPaymentById(int id);
    }
}