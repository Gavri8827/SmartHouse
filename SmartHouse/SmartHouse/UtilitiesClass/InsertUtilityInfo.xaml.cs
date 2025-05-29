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
            var gasConsumerNumber = GasConsumerEntry.Text?.Trim();
            var gasCompany = GasCompanyPicker.SelectedItem as string;

            Application.Current.Properties["ElectricityAccount"] = elctricityNumberOcount.Text;
            Application.Current.Properties["ElectricityPhone"] = elctricityNumberPhone.Text;

            Application.Current.Properties["WaterAccount"] = WaterNumberOcount.Text;
            Application.Current.Properties["WaterPhone"] = WaterNumberPhone.Text;

            // שמירה ב-Application.Properties
            Application.Current.Properties["GasAccount"] = gasConsumerNumber;
            Application.Current.Properties["GasCompany"] = gasCompany;
            Application.Current.Properties["GasPhone"] = GasNumberPhone.Text;

            // שמירת הנתונים כך שלא יימחקו
            await Application.Current.SavePropertiesAsync();
            await DisplayAlert("הצלחה", "הנתונים נשמרו בהצלחה!", "אישור");

        }
    }
}