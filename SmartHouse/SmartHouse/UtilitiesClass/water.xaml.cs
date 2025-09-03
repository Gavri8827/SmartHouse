using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Firebase;
using SmartHouse.UtilitiesClass.Account;
using AccountModel = SmartHouse.UtilitiesClass.Account.Account;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.UtilitiesClass
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Water : ContentPage
    {

        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        private AccountModel currentAccount;

        const string WaterDateKey = "WaterDate";
        const string WaterPaymentKey = "WaterPayment";
        public Water()
        {
            InitializeComponent();
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadWaterData();
        }


        private async Task LoadWaterData()
        {
            var list = await firebaseHelper.GetUtilitiesList(); // מחזיר את כל רשומות התשתיות לקבוצה
            currentAccount = list.FirstOrDefault();

            if (currentAccount != null)
            {
                WaterAcountNumber.Text = currentAccount.Water ?? "לא נמצא";
                WaterPropertytNumber.Text = currentAccount.WaterNumber ?? "לא נמצא";

                WaterPaymentEntry.Text = currentAccount.WaterPayment > 0
                    ? currentAccount.WaterPayment.ToString("F2")
                    : "";

                if (!string.IsNullOrEmpty(currentAccount.WaterDate))
                {
                    if (DateTime.TryParse(currentAccount.WaterDate, out DateTime parsedDate))
                    {
                        WaterDatePicker.Date = parsedDate;
                    }
                }
            }
            else
            {
                await DisplayAlert("שגיאה", "לא נמצאו נתוני מים", "אישור");
            }
        }

        async void water_clicked(object sender, EventArgs e)
        {
            if (currentAccount == null)
            {
                await DisplayAlert("שגיאה", "אין נתוני מים קיימים", "אישור");
                return;
            }

            if (!decimal.TryParse(WaterPaymentEntry.Text, out decimal payment))
            {
                await DisplayAlert("שגיאה", "אנא הזן סכום תקין", "אישור");
                return;
            }

            currentAccount.ElectricityPayment = payment;
            currentAccount.WaterDate = WaterDatePicker.Date.ToString("yyyy-MM-dd");

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
                UtilityType = "Water",
                Amount = payment,
                Date = WaterDatePicker.Date.ToString("yyyy-MM-dd")
            };

            await firebaseHelper.AddPaymentToHistory(paymentItem);




        }

    }
    }