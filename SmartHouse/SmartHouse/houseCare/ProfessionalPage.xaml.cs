using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.FamilyClass.ChildrenInfo;
using SmartHouse.Firebase;
using SmartHouse.HouseCare.Professional;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.HouseCare
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfessionalPage : ContentPage
    {
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        ObservableCollection<Professinalnfo> CHlist;
        public ProfessionalPage()
        {
            InitializeComponent();
            
        }
        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                professionalList.ItemsSource = await firebaseHelper.GetProftList();

            }
            catch { }

        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            
            string name = await DisplayPromptAsync("הוספת מקצוע", "הזן שם מלא:");
            if (string.IsNullOrWhiteSpace(name)) return;

            string desc = await DisplayPromptAsync("הוספת מקצוע", "הזן מקצוע:");
            if (string.IsNullOrWhiteSpace(desc)) return;

            string phone = await DisplayPromptAsync("הוספת מקצוע", "הזן טלפון:");
            if (string.IsNullOrWhiteSpace(phone)) return;

           
             var NewP = new Professinalnfo
            {
                Name = name,
                Description = desc,
                PhoneNumber = phone
            };
            await firebaseHelper.CreateProfList(NewP);
            professionalList.ItemsSource = await firebaseHelper.GetProftList();
        }

        private async void ProfessionalList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var selectedItem = e.Item as Professinalnfo; // שנה ל-class שלך, לדוגמה: ProfessionalInfo

            bool confirm = await DisplayAlert("אישור מחיקה",
                                              $"האם אתה בטוח שברצונך למחוק את {selectedItem.Name}?", // או שדה אחר רלוונטי
                                              "כן", "לא");

            if (confirm)
            {
                // מחיקה מה- Firebase או מהמקור שלך
                await firebaseHelper.DeleteProfessinal(selectedItem.FirebaseKey); // שנה בהתאם לפונקציה שלך

                // רענון הרשימה
                professionalList.ItemsSource = await firebaseHelper.GetProftList();
            }

            // ניקוי הבחירה, כדי למנוע מצב שהפריט יישאר מסומן
            professionalList.SelectedItem = null;
        }

    }
}