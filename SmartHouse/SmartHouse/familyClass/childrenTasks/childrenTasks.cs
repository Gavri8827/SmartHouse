using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SmartHouse.familyClass.childrenTasks
{
    public class ChildrenTasks
    {
        [PrimaryKey]
        public string Description { get; set; }
        public bool IsDone { get; set; }
    }
}
