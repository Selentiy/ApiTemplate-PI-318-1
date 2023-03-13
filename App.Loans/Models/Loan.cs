using App.Loans.Interface;

namespace App.Loans.Models
{
    public class Loan : ILoan
    {
        private readonly double LoanBalance;
        public int Id { get; set; }
        public double MoneyReturned { get; set; }
        public double LoanBalanceLeft { get; set; }
        public double PercentPerAnnum{ get; set; }
        public int NumberOfPayments { get; set; }
        public double MoneyTaken { get; set; }
        public Loan(int id,double moneytake, int number, double percent)
        {
            this.Id = id;
            this.NumberOfPayments = number;
            this.MoneyTaken = moneytake;
            this.PercentPerAnnum = percent;
            this.MoneyReturned = 0;
            LoanBalance = MoneyTaken - MoneyReturned + (MoneyTaken * NumberOfPayments * PercentPerAnnum / 12) ;
        }
        public Loan() { }
        public int AmountOfPaymentsLeft()
        {
            return (int)(NumberOfPayments - (MoneyReturned / (MoneyTaken / NumberOfPayments)));
        }

        public override string ToString()
        {
            return "Loan Balance: " + LoanBalanceLeft + " ; Amount of monthly payments: " + NumberOfPayments + " by " + PercentPerAnnum + " Percent per annum";
        }
    }
}
