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
		public FunMainPage ()
		{
			InitializeComponent ();
		}


        private void vacations_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage(new VactionPage()));
        }

        private void familyTime_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage(new FamilyTimePage()));
        }

        private void BucketList_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage(new BucketListPage()));
        }

        private void Copons_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage(new coponPage()));
        }
    }
}