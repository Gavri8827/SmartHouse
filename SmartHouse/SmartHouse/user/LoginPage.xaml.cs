using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SmartHouse.user
{
    public partial class LoginPage : ContentPage
    {
        private FirebaseClient firebase = new FirebaseClient("https://smarthouse-e4231-default-rtdb.firebaseio.com/");
        private List<(Entry nameEntry, Picker rolePicker)> userInputs = new List<(Entry, Picker)>();

        public LoginPage()
        {
            InitializeComponent();
            AddUserInput(); // שורת משתמש ברירת מחדל
        }

        private void OnAddUserClicked(object sender, EventArgs e)
        {
            AddUserInput();
        }

        private void AddUserInput()
        {
            var nameEntry = new Entry { Placeholder = "שם משתמש" };

            var rolePicker = new Picker { Title = "בחר תפקיד" };
            rolePicker.Items.Add("הורה");
            rolePicker.Items.Add("ילד");

            var removeButton = new Button
            {
                Text = "❌ הסר",
                BackgroundColor = Color.LightPink,
                TextColor = Color.Black,
                FontSize = 12
            };

            var userRow = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { nameEntry, rolePicker, removeButton },
                Padding = new Thickness(5),
                BackgroundColor = Color.FromHex("#f9f9f9")
            };

            removeButton.Clicked += (s, e) =>
            {
                UsersStack.Children.Remove(userRow);
                userInputs.Remove((nameEntry, rolePicker));
            };

            UsersStack.Children.Add(userRow);
            userInputs.Add((nameEntry, rolePicker));
        }

        private async void OnCreateGroupClicked(object sender, EventArgs e)
        {
            string groupId = GroupIdEntry.Text?.Trim();
            string groupPassword = GroupPasswordEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(groupId) || string.IsNullOrWhiteSpace(groupPassword))
            {
                await DisplayAlert("שגיאה", "אנא מלא מזהה קבוצה וסיסמה", "אישור");
                return;
            }

            if (userInputs.Count == 0)
            {
                await DisplayAlert("שגיאה", "יש להזין לפחות משתמש אחד", "אישור");
                return;
            }

            var existingGroup = await firebase.Child("Groups").Child(groupId).OnceSingleAsync<object>();
            if (existingGroup != null)
            {
                await DisplayAlert("שגיאה", "קבוצה כבר קיימת", "אישור");
                return;
            }

            foreach (var (nameEntry, rolePicker) in userInputs)
            {
                string name = nameEntry.Text?.Trim();
                string role = rolePicker.SelectedItem?.ToString();

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(role))
                {
                    await DisplayAlert("שגיאה", "ודא שכל המשתמשים כוללים שם ותפקיד", "אישור");
                    return;
                }

                var user = new UserAPP
                {
                    Name = name,
                    Role = role,
                    GroupId = groupId,
                    GroupPassword = groupPassword
                };

                await firebase
                    .Child("Groups")
                    .Child(groupId)
                    .Child("Users")
                    .PostAsync(user);
            }

            // יצירת מבני קבוצה ריקים
            await firebase.Child("Groups").Child(groupId).Child("ShopList").PutAsync(new Dictionary<string, object>());
            await firebase.Child("Groups").Child(groupId).Child("CoponList").PutAsync(new Dictionary<string, object>());
            await firebase.Child("Groups").Child(groupId).Child("VacationList").PutAsync(new Dictionary<string, object>());
            await firebase.Child("Groups").Child(groupId).Child("BucketList").PutAsync(new Dictionary<string, object>());
            await firebase.Child("Groups").Child(groupId).Child("ProfessionalList").PutAsync(new Dictionary<string, object>());
            await firebase.Child("Groups").Child(groupId).Child("ChildrenList").PutAsync(new Dictionary<string, object>());
            await firebase.Child("Groups").Child(groupId).Child("PetList").PutAsync(new Dictionary<string, object>());
            await firebase.Child("Groups").Child(groupId).Child("Utilities").PutAsync(new Dictionary<string, object>());
            await firebase.Child("Groups").Child(groupId).Child("PaymentHistory").PutAsync(new Dictionary<string, object>());

            // שומר את שם המשתמש הראשון
            Preferences.Set("GroupId", groupId);
            Preferences.Set("UserName", userInputs[0].nameEntry.Text);

            await DisplayAlert("הצלחה", "הקבוצה נוצרה בהצלחה!", "המשך");
            await Navigation.PushAsync(new ConnectPage());
        }
    }
}
