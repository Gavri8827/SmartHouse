using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.houseCare
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HouseCareMainPage : ContentPage
    {
        public HouseCareMainPage()
        {
            InitializeComponent();
            prof.Source = ImageSource.FromResource("SmartHouse.images.professional.jpg");
            regmai.Source = ImageSource.FromResource("SmartHouse.images.RegularMaintenance.jpg");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage(new ProfessionalPage()));
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage(new Maintence()));
        }
    }
}