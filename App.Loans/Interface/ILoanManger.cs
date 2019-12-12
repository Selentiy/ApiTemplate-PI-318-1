﻿using System.Collections.Generic;
using App.Loans.Models;
namespace App.Loans.Interface
{
    public interface ILoanManger
    {
        IEnumerable<Loan> GetLoans();
        string AmountOfPaymentsLeft(int Id);
    }
}
