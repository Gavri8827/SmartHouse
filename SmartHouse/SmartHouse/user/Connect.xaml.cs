using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.user
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Connect : ContentPage
    {
        public Connect()
        {
            InitializeComponent();
        }

        private async void LogIn(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("LoginPage");
        }

        private async void Connect_(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("ConnectPage");
        }
    }
    }
