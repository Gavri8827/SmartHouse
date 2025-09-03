using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Firebase;
using SmartHouse.FunClass.VacationPlan;
using SmartHouse.ShopClass;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.FunClass
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
   
    public partial class VactionListPage : ContentPage
    {
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        private VacationPlan1 currentVacation;

       


        public VactionListPage()
        {
            InitializeComponent();
        }




        protected override async void OnAppearing()
        {
            base.OnAppearing();
            vacationListView.ItemsSource = await firebaseHelper.GetVacationList();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(VactionPage));
        }

        private async void VacationListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is VacationPlan1 selectedVacation)
            {
                string action = await DisplayActionSheet(
                    $"What do you want to do with the vacation to {selectedVacation.Destination}?",
                    "Cancel",
                    null,
                    "Edit", "Delete");

                if (action == "Edit")
                {
                    // ניווט למסך העריכה עם הפריט
                    await Shell.Current.GoToAsync(
                       $"{nameof(VacationEditPage)}?itemKey={selectedVacation.FirebaseKey}"
                   );
                }
                else if (action == "Delete")
                {
                    bool confirm = await DisplayAlert("Delete", "Are you sure you want to delete this vacation?", "Yes", "No");
                    if (confirm)
                    {
                        await firebaseHelper.DeleteVacation(selectedVacation.FirebaseKey);
                        vacationListView.ItemsSource = await firebaseHelper.GetVacationList();
                    }

                    // ביטול הבחירה כדי למנוע בחירה תקועה
                    vacationListView.SelectedItem = null;
                }
            }
        }
    }
}