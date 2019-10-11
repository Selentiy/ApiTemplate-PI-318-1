using System.Collections.Generic;
namespace App.Loans.Interface
{
    public interface ILoanManger
    {
        IEnumerable<string> GetValuesInStringArray();
        IEnumerable<string> AmountOfPaymentsLeft(int Index);
    }
}
