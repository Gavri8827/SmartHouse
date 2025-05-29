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
           
        }

        private async void Pro_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ProfessionalPage));
        }

        private async void Maintance_clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(Maintence));
        }

        private async void Summary_Clicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync(nameof(Maintence));
        }
    }
}