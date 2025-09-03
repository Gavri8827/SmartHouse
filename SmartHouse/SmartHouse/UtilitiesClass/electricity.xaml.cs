using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHouse.Firebase;
using SmartHouse.UtilitiesClass.Account;
using AccountModel = SmartHouse.UtilitiesClass.Account.Account;


namespace SmartHouse.UtilitiesClass
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Electricity : ContentPage
    {

        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        private AccountModel currentAccount;

       
        public Electricity()
        {
            InitializeComponent();
            
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadElectricityData();
        }

        private async Task LoadElectricityData()
        {
            var list = await firebaseHelper.GetUtilitiesList(); // מחזיר את כל רשומות התשתיות לקבוצה
            currentAccount = list.FirstOrDefault();

            if (currentAccount != null)
            {
                electricityNumber.Text = currentAccount.ElectricityNumber ?? "לא נמצא";
                electricityphoneNumber.Text = currentAccount.Electricity ?? "לא נמצא";

                electricityPaymentEntry.Text = currentAccount.ElectricityPayment > 0
                    ? currentAccount.ElectricityPayment.ToString("F2")
                    : "";

                if (!string.IsNullOrEmpty(currentAccount.ElectricityDate))
                {
                    if (DateTime.TryParse(currentAccount.ElectricityDate, out DateTime parsedDate))
                    {
                        electricityDatePicker.Date = parsedDate;
                    }
                }
            }
            else
            {
                await DisplayAlert("שגיאה", "לא נמצאו נתוני חשמל", "אישור");
            }
        }





        private async void electricity_clicked(object sender, EventArgs e)
        {
            if (currentAccount == null)
            {
                await DisplayAlert("שגיאה", "אין נתוני חשמל קיימים", "אישור");
                return;
            }

            if (!decimal.TryParse(electricityPaymentEntry.Text, out decimal payment))
            {
                await DisplayAlert("שגיאה", "אנא הזן סכום תקין", "אישור");
                return;
            }

            currentAccount.ElectricityPayment = payment;
            currentAccount.ElectricityDate = electricityDatePicker.Date.ToString("yyyy-MM-dd");

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
                UtilityType = "Electricity",
                Amount = payment,
                Date = electricityDatePicker.Date.ToString("yyyy-MM-dd")
            };

            await firebaseHelper.AddPaymentToHistory(paymentItem);

        }


    }




}
