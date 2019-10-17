using App.Models;
using System.Collections;
using System.Collections.Generic;

namespace App.Repositories
{
    public interface IPaymentsRepository
    {
        IEnumerable<RegularPayment> GetRegularPayments();
        void CreateRegularPayment(RegularPayment regularPayment);
    }
}
