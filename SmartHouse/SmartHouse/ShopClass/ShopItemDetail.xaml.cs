using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHouse.ShopClass;
using System;
using SmartHouse.Firebase;

namespace SmartHouse.ShopClass
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(ItemKey), "itemKey")]
    public partial class ShopItemDetail : ContentPage
    {
        private string itemKey;
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        private ShopListInfo currentItem;

        public string ItemKey
        {
            get => itemKey;
            set
            {
                itemKey = Uri.UnescapeDataString(value);
                LoadItem(); // נטען את הפריט לפי המפתח
            }
        }

        public ShopItemDetail()
        {
            InitializeComponent();
        }

        private async void LoadItem()
        {
            currentItem = await firebaseHelper.GetShopItemByKey(itemKey); // הפונקציה תמצא לפי key
            if (currentItem != null)
            {
                itemDes.Text = currentItem.Description;
                itemAm.Text = currentItem.Quantity;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (currentItem == null) return;

            currentItem.Description = itemDes.Text;
            currentItem.Quantity = itemAm.Text;

            await firebaseHelper.UpdateShopList(itemKey, currentItem);
            await DisplayAlert("Success", "Item updated", "OK");

            await Shell.Current.GoToAsync(".."); // חזרה אחורה
        }
    }
}
