using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SmartHouse.FunClass.CoponList
{
    public class CoponList
    {
        [PrimaryKey]
        public int CoponNumber { get; set; }

        public string CoponName { get; set; }

        public decimal MoneyAmount { get; set; }

        public string Type { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string FirebaseKey { get; set; }


    }
}
