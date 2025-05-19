using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.familyClass;
using SmartHouse.FunClass;
using SmartHouse.houseCare;
using SmartHouse.shopcClass;
using SmartHouse.UtilitiesClass;
using Xamarin.Forms;

namespace SmartHouse
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            background1.Source = ImageSource.FromResource("SmartHouse.images.backgroundgray.jpg");
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

        }

        private void Utilities_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage(new Utilities()));

        }

        
        private void Fun_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage(new FunMainPage()));
        }

        private void Shop_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage(new ShopListPage()));
        }

        private void Family_Clicked_4(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage(new family())); 
        }

        private void houseCare_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage(new HouseCareMainPage()));
        }
    }
}
