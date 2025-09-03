using System;
using SmartHouse.Firebase;
using SmartHouse.FunClass.CoponList;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CoponModel = SmartHouse.FunClass.CoponList.CoponList;

namespace SmartHouse.FunClass
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCopon : ContentPage
    {

        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        public AddCopon()
        {
            InitializeComponent();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var newCoupon = new CoponModel
            {
                CoponName = nameEntry.Text,
                CoponNumber = int.TryParse(coponNumberEntry.Text, out var amount1) ? amount1 : 0,
                MoneyAmount = decimal.TryParse(amountEntry.Text, out var amount) ? amount : 0,
                Type = typeEntry.Text,
                ExpirationDate = expirationPicker.Date
            };

            await firebaseHelper.CreateCopon(newCoupon); 

            await Shell.Current.GoToAsync(".."); // חזרה לרשימה
        }

       
    }
}