using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartHouse.Firebase;
using SmartHouse.UtilitiesClass.Account;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.UtilitiesClass
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentHistoryPage : ContentPage
    {
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        private List<HistoryPayment> allPayments = new List<HistoryPayment>();

        public PaymentHistoryPage()
        {
            InitializeComponent();
            UtilityTypePicker.SelectedIndex = 0; // ברירת מחדל: "הכל"
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadAllPayments();
            ApplyFilter();
        }

        private async Task LoadAllPayments()
        {
            allPayments = await firebaseHelper.GetPaymentHistory();
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            string selectedType = UtilityTypePicker.SelectedItem as string;
            DateTime selectedDate = MonthPicker.Date;
            int selectedMonth = selectedDate.Month;
            int selectedYear = selectedDate.Year;

            var filtered = allPayments.Where(p =>
            {
                DateTime parsedDate;
                bool isValidDate = DateTime.TryParse(p.Date, out parsedDate);

                bool matchType = selectedType == "הכל" || p.UtilityType == selectedType;
                bool matchMonth = isValidDate && parsedDate.Month == selectedMonth && parsedDate.Year == selectedYear;

                return matchType && matchMonth;
            }).OrderByDescending(p => p.Date).ToList();

            HistoryListView.ItemsSource = filtered;
        }
    }
}
