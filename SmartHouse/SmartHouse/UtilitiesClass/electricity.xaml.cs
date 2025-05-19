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


        private string AcountelectricityNumber;
        private string LastelectricityPayment;
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





            private void Button_Clicked(object sender, EventArgs e)
        {
            
            Preferences.Set(LastelectricityPayment, electrictyPaymenr.Text);
        }

       
    }
}