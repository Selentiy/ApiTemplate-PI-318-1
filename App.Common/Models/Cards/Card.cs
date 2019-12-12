using System;

namespace App.Models.Cards
{
    public class Card
    {
        public string OwnerName { get; set; }
        public long Number { get; set; }
        public DateTime ExpiresEnd { get; set; }
        public ushort CVV { get; set; }

        public int Bill { get; set; }

        public int? Limit { get; set; }
        public bool IsPayPass { get; set; }
        public bool IsBlocked { get; set; }
    }
}
