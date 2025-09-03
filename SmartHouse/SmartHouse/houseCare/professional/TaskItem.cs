using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.HouseCare.Professional
{
    public class TaskItem
    {
        public string TaskName { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string IconSource { get; set; }

        public string FirebaseKey { get; set; }
    }
}

