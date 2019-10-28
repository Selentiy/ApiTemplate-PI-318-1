using System;

namespace App.Models.Accounts
{
    public class Account
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string CheckDigits { get; set; }
        public string BankCode { get; set; }
        public string AccountNumber { get; set; }

        public DateTime? CreatedDate { get; set; }
        public bool IsBlocked { get; set; }
        public string Owner { get; set; }
    }
}
