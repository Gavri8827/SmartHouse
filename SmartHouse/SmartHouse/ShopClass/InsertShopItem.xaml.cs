using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHouse.Firebase;
using Xamarin.Essentials;

namespace SmartHouse.ShopClass
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InsertShopItem : ContentPage
    {
        private FirebaseHelper firebaseHelper = new FirebaseHelper();

        public InsertShopItem()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            // שליפת groupId מה־Preferences
            string groupId = Preferences.Get("GroupId", null);

            if (string.IsNullOrEmpty(groupId))
            {
                await DisplayAlert("שגיאה", "GroupId לא נמצא", "אוקי");
                return;
            }

            var newItem = new ShopListInfo
            {
                Description = ItemShopList.Text,
                Quantity = QuantityItem.Text,
                Kind = KindItem.SelectedItem?.ToString(),
            IsDone = false
            };

            await firebaseHelper.CreateShopListItem(newItem);

            await Shell.Current.GoToAsync("..");
        }
    }
}