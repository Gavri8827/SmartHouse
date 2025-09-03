using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SmartHouse.FamilyClass.ChildrenTasks
{
    public class ChildrenTasks
    {
        [PrimaryKey]
        public string Description { get; set; }

        public string ChildName { get; set; }
        public bool IsDone { get; set; }

        public string FirebaseKey { get; set; }
    }
}
