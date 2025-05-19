using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.familyClass.childrenInfo;
using SmartHouse.familyClass.petInfo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.familyClass
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Pet : ContentPage
	{
        ObservableCollection<PetInfo> Plist;
        public Pet ()
        {
			InitializeComponent ();
            Plist = new ObservableCollection<PetInfo> {
            new PetInfo{ Name= "Lucky", Kind = "Dog", Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9b/Common_Seal_Phoca_vitulina_1.jpg/500px-Common_Seal_Phoca_vitulina_1.jpg"},
            new PetInfo{ Name= "Darvider",Kind = "Snake" ,Image = "https://2.a7.org/files/pictures/781x439/1160971.jpg"},
            };
            PetList.ItemsSource = Plist;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {


        }

        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {

        }

        async void PetList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var pet = e.SelectedItem as PetInfo;
            var PetPage = new PetInfoPage();
            PetPage.BindingContext = pet;
            await Navigation.PushAsync(PetPage);
        }
    }
}