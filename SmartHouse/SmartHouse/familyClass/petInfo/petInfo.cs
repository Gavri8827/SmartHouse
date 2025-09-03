using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.ComponentModel; // נדרש כדי לממש INotifyPropertyChanged
using SQLite;

namespace SmartHouse.FamilyClass.PetInfo
{
    public class PetInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Image { get; set; }

        public string Kind { get; set; }

        public string Insurance { get; set; }

        private DateTime _treatment;
        public DateTime Treatment
        {
            get => _treatment;
            set
            {
                if (_treatment != value)
                {
                    _treatment = value;
                    OnPropertyChanged(nameof(Treatment));
                }
            }
        }

        public string FirebaseKey { get; set; }

        protected void OnPropertyChanged(string propertyName) =>
       PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
