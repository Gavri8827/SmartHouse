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
using static System.Net.Mime.MediaTypeNames;
using AccountModel = SmartHouse.UtilitiesClass.Account.Account;


namespace SmartHouse.UtilitiesClass
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InsertUtilityInfo : ContentPage
	{
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        public InsertUtilityInfo ()
		{
			InitializeComponent ();
		}

        async void Button_Clicked(object sender, EventArgs e)
        {
            var gasConsumerNumber = GasConsumerEntry.Text?.Trim();
            var gasCompany = GasCompanyPicker.SelectedItem as string;

            // יצירת מופע של Acount
            var account = new AccountModel
            {
                Electricity = elctricityNumberPhone.Text?.Trim(),
                ElectricityNumber = elctricityNumberOcount.Text?.Trim(),

                Water = WaterNumberPhone.Text?.Trim(),
                WaterNumber = WaterNumberOcount.Text?.Trim(),

                GasCompany = gasCompany,
                GasNumber = gasConsumerNumber,
                GasPhoneNumber = GasNumberPhone.Text?.Trim(),
                GasPayment = 0,  
                WaterPayment = 0,
                ElectricityPayment = 0
            };

            // שליחה ל-Firebase
            try
            {
                await firebaseHelper.CreateUtilitiesList(account);
                await DisplayAlert("הצלחה", "הנתונים נשמרו בהצלחה ב־Firebase!", "אישור");
            }
            catch (Exception ex)
            {
                await DisplayAlert("שגיאה", $"אירעה שגיאה: {ex.Message}", "אישור");
            }

        }
    }
}