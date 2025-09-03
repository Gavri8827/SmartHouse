using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.FamilyClass.ChildrenTasks;
using SQLite;

namespace SmartHouse.FamilyClass.ChildrenInfo
{
    public class ChildrenInfo
    {
        [PrimaryKey]
         public string Name { get; set; }

        public string Image { get; set; }

        public string FirebaseKey { get; set; }


    }
}
