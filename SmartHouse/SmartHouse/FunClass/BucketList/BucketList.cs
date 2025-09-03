using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace SmartHouse.FunClass.BucketList
{
    public class BucketList
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Description {  get; set; }

        public bool IsDone { get; set; }

        public string FunImage { get; set; }
        public string FirebaseKey { get; set; }



    }
}
