using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using SmartHouse.user;

namespace SmartHouse
{
    public partial class App : Application

         
    {


        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            // בטעינה ראשונית, ננווט ל-login
            Shell.Current.GoToAsync("//login");

        }

        

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
