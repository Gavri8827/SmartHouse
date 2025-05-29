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
    public partial class gas : ContentPage
    {
        const string GasPaymentKey = "GasPayment";
        const string GasDateKey = "GasDate";

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

        private void gas_clicked(object sender, EventArgs e)
        {

            var date = gasDatePicker.Date.ToString("yyyy-MM-dd");
            var amount = gasPaymentEntry.Text;
            Preferences.Set(GasDateKey, date);
            Preferences.Set(GasPaymentKey, amount);
            DisplayAlert("הצלחה", "הנתונים נשמרו בהצלחה!", "אישור");
        }

    }
}