using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Firebase;
using SmartHouse.FunClass.VacationPlan;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.FunClass
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(ItemKey), "itemKey")]





    public partial class VacationEditPage : ContentPage
    {
        private VacationPlan1 currentVacation;
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        private string itemKey;





        public string ItemKey
        {
            get => itemKey;
            set
            {
                itemKey = Uri.UnescapeDataString(value);
                LoadItem(); 
            }
        }

      
        public VacationEditPage()
        {
            InitializeComponent();
        }
        private async void LoadItem()
        {
            currentVacation = await firebaseHelper.GetVacationByKey(itemKey); // הפונקציה תמצא לפי key
            if (currentVacation != null)
            {
                startDatePicker.Date = currentVacation.StartDate;
                endDatePicker.Date = currentVacation.EndDate;
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(destinationEntry.Text))
            {
                await DisplayAlert("Invalid", "Destination is required.", "OK");
                return;
            }

            // עדכן את האובייקט
            currentVacation.Destination = destinationEntry.Text;
            currentVacation.StartDate = startDatePicker.Date;
            currentVacation.EndDate = endDatePicker.Date;


            // שמור למסד
            await firebaseHelper.UpdateVacation(itemKey, currentVacation);

            await DisplayAlert("Saved", "Vacation updated successfully.", "OK");
            await Shell.Current.GoToAsync(".."); // חזרה אחורה
        }
    }
}