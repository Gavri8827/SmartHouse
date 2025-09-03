using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SmartHouse.FunClass.VacationPlan
{
   public class VacationPlan1
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Destination { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string FirebaseKey { get; set; }
    }

}