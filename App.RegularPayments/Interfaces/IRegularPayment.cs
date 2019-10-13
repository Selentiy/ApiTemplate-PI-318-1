using System;
using System.Collections.Generic;
using System.Text;

namespace App.RegularPayments.Interfaces
{
    public interface IRegularPayment
    {
        DateTime DateOfNextPayment();
    }
}
