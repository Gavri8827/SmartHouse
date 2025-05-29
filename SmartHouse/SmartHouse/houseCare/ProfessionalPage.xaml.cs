using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.familyClass.childrenInfo;
using SmartHouse.houseCare.professional;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.houseCare
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfessionalPage : ContentPage
    {
        ObservableCollection<professinalnfo> CHlist;
        public ProfessionalPage()
        {
            InitializeComponent();
            CHlist = new ObservableCollection<professinalnfo> {
            new professinalnfo{ name= "שרון כהן", Description = "חשמלאי", phoneNumber = "0506789932"},
            new professinalnfo{ name= "יורם שלום", Description = "קבלן בנייה", phoneNumber = "050987654"},
            };
            professionalList.ItemsSource = CHlist;
        }

        // האירוע של הכפתור בתפריט
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            // מבקשים מהמשתמש פרטים
            string name = await DisplayPromptAsync("הוספת מקצוע", "הזן שם מלא:");
            if (string.IsNullOrWhiteSpace(name)) return;

            string desc = await DisplayPromptAsync("הוספת מקצוע", "הזן מקצוע:");
            if (string.IsNullOrWhiteSpace(desc)) return;

            string phone = await DisplayPromptAsync("הוספת מקצוע", "הזן טלפון:");
            if (string.IsNullOrWhiteSpace(phone)) return;

            // מוסיפים לרשימה
            CHlist.Add(new professinalnfo
            {
                name = name,
                Description = desc,
                phoneNumber = phone
            });
        }
    


private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }

        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {

        }
    }
}