using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.user
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConnectPage : ContentPage
    {
        public ConnectPage()
        {
            InitializeComponent();
        }
        private FirebaseClient firebase = new FirebaseClient("https://smarthouse-e4231-default-rtdb.firebaseio.com/");

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string groupId = GroupIdEntry.Text?.Trim();
            string groupPassword = GroupPasswordEntry.Text?.Trim();
            string userName = NameEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(groupId) || string.IsNullOrWhiteSpace(groupPassword) || string.IsNullOrWhiteSpace(userName))
            {
                await DisplayAlert("שגיאה", "אנא מלא את כל השדות", "אישור");
                return;
            }

            try
            {
                // קבלת רשימת המשתמשים בקבוצה
                var users = await firebase.Child("Groups").Child(groupId).Child("Users").OnceAsync<UserAPP>();

                // חיפוש משתמש עם שם תואם
                var userRecord = users.FirstOrDefault(u => u.Object.Name == userName);

                if (userRecord == null)
                {
                    await DisplayAlert("שגיאה", "משתמש לא נמצא בקבוצה", "אישור");
                    return;
                }

                // בדיקת סיסמה
                if (userRecord.Object.GroupPassword != groupPassword)
                {
                    await DisplayAlert("שגיאה", "קוד קבוצה שגוי", "אישור");
                    return;
                }

                // שמירת פרטים ב-Preferences (או היכן שנוח לך)
                Xamarin.Essentials.Preferences.Set("GroupId", groupId);
                Xamarin.Essentials.Preferences.Set("UserName", userName);
                

                // מעבר למסך הראשי
                await Navigation.PushAsync(new MainPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("שגיאה", $"אירעה שגיאה: {ex.Message}", "אישור");
            }
        }

    }
}