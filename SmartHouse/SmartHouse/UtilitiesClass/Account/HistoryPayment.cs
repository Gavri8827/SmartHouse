using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.UtilitiesClass.Account
{
    public class HistoryPayment
    {
        public string UtilityType { get; set; } 
        public decimal Amount { get; set; }
        public string Date { get; set; }
        public string FirebaseKey { get; set; }

    }
}
