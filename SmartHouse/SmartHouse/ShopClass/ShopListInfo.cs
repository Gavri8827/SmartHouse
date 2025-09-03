using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SmartHouse.ShopClass
{
    public class ShopListInfo
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Description { get; set; }

        public string FirebaseKey { get; set; }

        public string Kind { get; set; }

        public string Quantity { get; set; }
        public bool IsDone { get; set; }
    }
}
