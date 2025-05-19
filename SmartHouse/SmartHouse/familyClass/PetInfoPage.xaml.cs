using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.familyClass
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PetInfoPage : ContentPage
	{
		public PetInfoPage ()
		{
			InitializeComponent ();
		}

        async void Button_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["InsuranceNumber"] = InsuranceNumberOcount.Text;

            await DisplayAlert("Success", "Information saved!", "OK");
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            correntDate.Text = date.Date.ToString();
           // Application.Current.Properties["ElectricityAccount"] = elctricityNumberOcount.Text;

        }
    }
}