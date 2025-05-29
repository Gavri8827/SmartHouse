using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.UtilitiesClass
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class electricity : ContentPage
    {
        const string LastElectricityPaymentKey = "LastElectricityPayment";
        const string ElectricityDateKey = "ElectricityDate";
        public electricity()
        {
            InitializeComponent();
            LoadData();
            
        }

        private void LoadData()
        {
            electricityNumber.Text = Application.Current.Properties.ContainsKey("ElectricityAccount")
                ? Application.Current.Properties["ElectricityAccount"] as string
                : "No data available";
            electricityphoneNumber.Text = Application.Current.Properties.ContainsKey("ElectricityPhone")
                ? Application.Current.Properties["ElectricityPhone"] as string
                : "No data available";

        }





            private void electricity_clicked(object sender, EventArgs e)
        {
            string date = electricityDatePicker.Date.ToString("yyyy-MM-dd");
            string amount = electricityPaymentEntry.Text;
            Preferences.Set(LastElectricityPaymentKey, amount);
            Preferences.Set(ElectricityDateKey, date);
            DisplayAlert("הצלחה", "הנתונים נשמרו בהצלחה!", "אישור");
        }

    }




    }
