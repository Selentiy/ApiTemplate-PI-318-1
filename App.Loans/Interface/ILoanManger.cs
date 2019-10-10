using System.Collections.Generic;
namespace App.Loans.Interface
{
    public interface ILoanManger
    {
        IEnumerable<string> GetValues();
        IEnumerable<string> AmountOfPaymentsLeft(int Index);
    }
}
