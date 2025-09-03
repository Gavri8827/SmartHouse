using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Firebase;
using SmartHouse.UtilitiesClass.Account;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AccountModel = SmartHouse.UtilitiesClass.Account.Account;


namespace SmartHouse.UtilitiesClass
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Gas : ContentPage
    {

        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        private AccountModel currentAccount;

        const string GasPaymentKey = "GasPayment";
        const string GasDateKey = "GasDate";

        public Gas()
        {
            InitializeComponent();
            LoadData();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadGasData();
        }

        private async Task LoadGasData()
        {
            var list = await firebaseHelper.GetUtilitiesList(); // מחזיר את כל רשומות התשתיות לקבוצה
            currentAccount = list.FirstOrDefault();
            if (currentAccount != null)
            {
                GasCompany.Text = currentAccount.GasCompany ?? "לא נמצא";
                GasNumber.Text = currentAccount.GasNumber ?? "לא נמצא";
                GasPhoneNumber.Text = currentAccount.GasPhoneNumber ?? "לא נמצא";

                gasPaymentEntry.Text = currentAccount.GasPayment > 0
                    ? currentAccount.GasPayment.ToString("F2")
                    : "";

                if (!string.IsNullOrEmpty(currentAccount.GasDate))
                {
                    if (DateTime.TryParse(currentAccount.GasDate, out DateTime parsedDate))
                    {
                        gasDatePicker.Date = parsedDate;
                    }
                }
            }
            else
            {
                await DisplayAlert("שגיאה", "לא נמצאו נתוני מים", "אישור");
            }
        
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

        private async void gas_clicked(object sender, EventArgs e)
        {

            if (currentAccount == null)
            {
                await DisplayAlert("שגיאה", "אין נתוני חשמל קיימים", "אישור");
                return;
            }

            if (!decimal.TryParse(gasPaymentEntry.Text, out decimal payment))
            {
                await DisplayAlert("שגיאה", "אנא הזן סכום תקין", "אישור");
                return;
            }

            currentAccount.ElectricityPayment = payment;
            currentAccount.ElectricityDate = gasDatePicker.Date.ToString("yyyy-MM-dd");

            try
            {
                await firebaseHelper.UpdateUtilities(currentAccount.FirebaseKey, currentAccount);
                await DisplayAlert("הצלחה", "הנתונים נשמרו בהצלחה!", "אישור");
            }
            catch (Exception ex)
            {
                await DisplayAlert("שגיאה", "שגיאה בשמירה: " + ex.Message, "אישור");
            }

            var paymentItem = new HistoryPayment
            {
                UtilityType = "Gas",
                Amount = payment,
                Date = gasDatePicker.Date.ToString("yyyy-MM-dd")
            };

            await firebaseHelper.AddPaymentToHistory(paymentItem);

        }

    }
}