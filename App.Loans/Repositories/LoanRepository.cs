using System.Collections.Generic;
using App.Configuration;
using App.Loans.Interface;
using App.Loans.Models;

namespace App.Loans.Repositories
{
    public class LoanRepository : ILoanRepository, ISingletoneDependency
    {
        public Loan[] loans = new Loan[5];

        public LoanRepository()
        {
            for (int i = 0; i < 5; i++)
            {
                loans[i] = new Loan(i * 1000 + 3000, i * 2 + 12, i + 0.5);
            }
        }
        public IEnumerable<Loan> GetLoans()
        {
            return loans;
        }
        public IEnumerable<string> GetValues()
        {
            int j = 0;
            string[] str = new string[5];
            foreach (Loan a in loans)
            {
                str[j] = a.ToString();
                j++;
            }
            return str;
        }

        public Loan Get(int Id)
        {
            if (Id >= loans.Length || Id < 0)
                return null;
            return loans[Id];
        }
    }
}
