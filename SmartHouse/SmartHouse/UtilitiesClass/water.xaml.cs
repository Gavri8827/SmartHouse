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
    public partial class water : ContentPage
    {
        const string WaterDateKey = "WaterDate";
        const string WaterPaymentKey = "WaterPayment";
        public water()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            // טעינת פרטי החשבון מה־Application.Properties
            WaterAcountNumber.Text = Application.Current.Properties.ContainsKey("WaterAccount")
                ? Application.Current.Properties["WaterAccount"] as string
                : "אין נתונים";

            WaterPropertytNumber.Text = Application.Current.Properties.ContainsKey("WaterProperty")
                ? Application.Current.Properties["WaterProperty"] as string
                : "אין נתונים";
        }

        async void water_clicked(object sender, EventArgs e)
        {
            // קריאה מה-DatePicker ומה-Entry
            var date = WaterDatePicker.Date.ToString("yyyy-MM-dd");
            var amount = WaterPaymentEntry.Text?.Trim();

            // שמירה ב-Preferences
            Preferences.Set(WaterDateKey, date);
            Preferences.Set(WaterPaymentKey, amount);

            await DisplayAlert("הצלחה", "הנתונים נשמרו בהצלחה!", "אישור");
        }

    }
    }