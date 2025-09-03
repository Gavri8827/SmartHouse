using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.FamilyClass;
using SmartHouse.FunClass;
using SmartHouse.HouseCare;
using SmartHouse.ShopClass;
using SmartHouse.UtilitiesClass;
using Xamarin.Forms;

namespace SmartHouse
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //background1.Source = ImageSource.FromResource("SmartHouse.images.backgroundgray.jpg");
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

        }

        private async void Utilities_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("utilities");

        }

        
        private async void Fun_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("fun");
        }

        private async void Shop_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("shop");
        }

        private async void Family_Clicked_4(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("family");
        }

        private async void houseCare_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("housecare");
        }
    }
}
