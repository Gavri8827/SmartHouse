using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.FunClass
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FunMainPage : ContentPage
    {
        public FunMainPage()
        {
            InitializeComponent();
        }


        private async void vacations_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(VactionListPage));
        }

        private async void familyTime_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(FamilyTimePage));
        }

        private async void BucketList_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(BucketListPage));
        }

        private async void Copons_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CoponPage));
        }
    }
}