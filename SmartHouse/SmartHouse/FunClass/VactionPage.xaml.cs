using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Firebase;
using SmartHouse.FunClass.VacationPlan;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.FunClass
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VactionPage : ContentPage
    {
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        public ObservableCollection<Participant> Participants { get; set; }

        public VactionPage()
        {
            InitializeComponent();

            Participants = new ObservableCollection<Participant>
            {
                new Participant { Name = "Alice" },
                new Participant { Name = "Bob" },
                new Participant { Name = "Charlie" }
            };

            BindingContext = this;
        }

        private async void OnOpenMapClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(destinationEntry.Text))
            {
                var location = destinationEntry.Text;
                await Launcher.OpenAsync($"https://www.google.com/maps/search/?api=1&query={Uri.EscapeDataString(location)}");
            }
            else
            {
                await DisplayAlert("Error", "Please enter a destination.", "OK");
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            var newPlan = new VacationPlan1
            {
               
                Destination = destinationEntry.Text,
                StartDate = startDatePicker.Date,
                EndDate = endDatePicker.Date,
                
            };

            await firebaseHelper.CreateVacation(newPlan);

           
            await DisplayAlert("Success", "Vacation saved!", "OK");

            await Shell.Current.GoToAsync("..");

        }
    }

    public class Participant
    {
        public string Name { get; set; }
        public bool IsConfirmed { get; set; }
    }
}