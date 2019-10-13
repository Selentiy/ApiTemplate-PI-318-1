using System;
using System.Collections.Generic;
using System.Text;
using App.RegularPayments.Interfaces;

namespace App.RegularPayments.Models
{
    class RegularPayment : IRegularPayment
    {

        public DateTime DateOfNextPayment()
        {
            throw new NotImplementedException();
        }
    }
}
