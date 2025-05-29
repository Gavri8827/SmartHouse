using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.houseCare.professional
{
    public class TaskItem
    {
        public string TaskName { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string IconSource { get; set; }
    }
}

