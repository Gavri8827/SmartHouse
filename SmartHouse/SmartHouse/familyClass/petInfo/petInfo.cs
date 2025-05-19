using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SmartHouse.familyClass.petInfo
{
    class PetInfo
    {
        [PrimaryKey]
        public string Name { get; set; }

        public string Image { get; set; }

        public string Kind { get; set; }

        public string Insurance { get; set; }
    }
}
