using App.Configuration;
using App.Models;
using App.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.RegularPayments
{
    public interface IShowNextServise
    {
        DateTime ShowNextData(RegularPayment regularPayments);
    }
    public class ShowNextServise : IShowNextServise, ITransientDependency
    {
        public DateTime ShowNextData(RegularPayment regularPayments)
        {
            return regularPayments.DateOfLastPay.AddDays(regularPayments.Period);
        }
    }
}
