using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models.Payments
{
    public enum Status 
    {
        Success = 1,
        Failure,
        Reversed
    }
    public class Payment
    {
        public int ID { get; set; }
        public string Payer { get; set; }
        public string Recipient { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }
    }
}
