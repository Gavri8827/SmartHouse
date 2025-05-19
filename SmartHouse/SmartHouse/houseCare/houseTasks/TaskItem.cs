using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SmartHouse.houseCare.houseTasks
{
    internal class TaskItem
    {
        public string TaskName { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
