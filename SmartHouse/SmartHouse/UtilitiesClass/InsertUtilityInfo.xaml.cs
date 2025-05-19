using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.UtilitiesClass
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InsertUtilityInfo : ContentPage
	{
		public InsertUtilityInfo ()
		{
			InitializeComponent ();
		}

        async void Button_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["ElectricityAccount"] = elctricityNumberOcount.Text;
            Application.Current.Properties["ElectricityPhone"] = elctricityNumberPhone.Text;

            Application.Current.Properties["WaterAccount"] = WaterNumberOcount.Text;
            Application.Current.Properties["WaterPhone"] = WaterNumberPhone.Text;

            Application.Current.Properties["GasAccount"] = GasNumberOcount.Text;
            Application.Current.Properties["GasCompany"] = GasCompany.SelectedItem.ToString();;
            Application.Current.Properties["GasPhone"] = GasNumberPhone.Text;

            // שמירת הנתונים כך שלא יימחקו
            await Application.Current.SavePropertiesAsync();

            await DisplayAlert("Success", "Information saved!", "OK");
        }
    }
}