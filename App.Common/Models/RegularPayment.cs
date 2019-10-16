using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    public class RegularPayment
    {
        public int PaymentID { get; set; }
        public string Payer { get; set; }
        public string Recipient { get; set; }
        public double Payment { get; set; }
        public int Period { get; set; }
        public int MaxPayment { get; set; }
        public DateTime DateOfLastPay { get; set; }
    }
}
