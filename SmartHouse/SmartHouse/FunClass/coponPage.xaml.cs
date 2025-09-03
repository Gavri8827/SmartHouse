using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.FamilyClass.ChildrenTasks;
using SmartHouse.FamilyClass;
using SmartHouse.FunClass.CoponList;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHouse.Firebase;
using CoponModel = SmartHouse.FunClass.CoponList.CoponList;


namespace SmartHouse.FunClass
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoponPage : ContentPage
    {
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        public CoponPage()
        {
            InitializeComponent();
            
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                CoponList.ItemsSource = await firebaseHelper.GetCoponList();
            }
            catch { }
        }


            private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AddCopon));
        }

        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            var taskToDelete = swipeItem?.BindingContext as CoponModel;
            try
            {
                await firebaseHelper.DeleteCopon(taskToDelete.FirebaseKey);

                // רענון הרשימה אחרי מחיקה
                CoponList.ItemsSource = await firebaseHelper.GetCoponList();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Something went wrong: " + ex.Message, "OK");
            }
        }

        private async void SwipeItem_Invoked_1(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            var MoneyAmountToEdit = swipeItem?.BindingContext as CoponModel;

            if (MoneyAmountToEdit == null)
            {
                await DisplayAlert("Error", "Could not identify the task.", "OK");
                return;
            }
            decimal oldValue = MoneyAmountToEdit.MoneyAmount;
            string userInput = await DisplayPromptAsync("Edit amount", "Enter amount used:", initialValue: "0");

            if (!decimal.TryParse(userInput, out var usedAmount))
            {
                await DisplayAlert("Invalid Input", "Please enter a valid number.", "OK");
                return;
            }

            // חישוב הערך החדש
            decimal newValue = oldValue - usedAmount;

            // עדכון הערך החדש
            MoneyAmountToEdit.MoneyAmount = newValue;
            await firebaseHelper.UpdateCopon(MoneyAmountToEdit.FirebaseKey, MoneyAmountToEdit);


            // רענון
            CoponList.ItemsSource = await firebaseHelper.GetCoponList();
        }

        private async void CoponList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is CoponModel copon)
            {
                ((ListView)sender).SelectedItem = null; // לביטול החזקה של הפריט מסומן
                System.Diagnostics.Debug.WriteLine($"ניווט ל־PrivateCoponInfo עם id = {copon.FirebaseKey}");
                await Shell.Current.GoToAsync(
                $"{nameof(PrivateCoponInfo)}?id={copon.FirebaseKey}");
            }

        }
    }
}