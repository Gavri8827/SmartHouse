using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.familyClass.childrenTasks;
using SQLite;

namespace SmartHouse.familyClass.childrenInfo
{
    public class ChildrenInfo
    {
        [PrimaryKey,AutoIncrement]
        public string Name { get; set; }

        public string Image { get; set; }


        public ObservableCollection<ChildrenTasks> Tasks { get; set; } = new ObservableCollection<ChildrenTasks>();
    }
}
