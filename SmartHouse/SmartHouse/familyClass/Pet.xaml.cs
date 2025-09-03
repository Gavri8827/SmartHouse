using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.FamilyClass.ChildrenInfo;
using SmartHouse.FamilyClass.PetInfo;
using SmartHouse.Firebase;
using SmartHouse.FunClass.CoponList;
using SmartHouse.FunClass;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Drawing;
using PetModel = SmartHouse.FamilyClass.PetInfo.PetInfo;

namespace SmartHouse.FamilyClass
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pet : ContentPage
    {
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        public Pet()
        {
            InitializeComponent();
           
        }


        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                PetList.ItemsSource = await firebaseHelper.GetPetList();
            }
            catch { }
        }


        private async void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            var petToDelete = button?.BindingContext as PetModel;

            if (petToDelete == null)
                return;
            bool isConfirmed = await DisplayAlert("אישור מחיקה", "האם אתה בטוח שברצונך למחוק?", "כן", "לא");
            if (isConfirmed)
            {
                await firebaseHelper.DeletePet(petToDelete.FirebaseKey);
                PetList.ItemsSource = await firebaseHelper.GetPetList();
            }
            else { }
        }

        async void PetList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is PetModel pet)
            {
                // לבטל סימון כדי שבריחזור לא יופיע מסומן
                ((ListView)sender).SelectedItem = null;

                // נווט עם Shell, מעביר את pet.Id כפרמטר
                System.Diagnostics.Debug.WriteLine($"ניווט ל־PetInfoPage עם id = {pet.FirebaseKey}");
                await Shell.Current.GoToAsync(
                $"{nameof(PetInfoPage)}?petId={pet.FirebaseKey}");
            }
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(EditPet));
        }
    }
}

