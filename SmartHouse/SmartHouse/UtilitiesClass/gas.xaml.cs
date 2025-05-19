using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.UtilitiesClass
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class gas : ContentPage
    {
        public gas()
        {
            InitializeComponent();
            LoadData();
        }
        
        private void LoadData()
        {
            GasNumber.Text = Application.Current.Properties.ContainsKey("GasAccount")
                ? Application.Current.Properties["GasAccount"] as string
                : "No data available";
            GasCompany.Text = Application.Current.Properties.ContainsKey("GasCompany")
                ? Application.Current.Properties["GasCompany"] as string
                : "No data available";
            GasPhoneNumber.Text = Application.Current.Properties.ContainsKey("GasPhone")
              ? Application.Current.Properties["GasPhone"] as string
              : "No data available";

        }
    }
}