using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.FamilyClass.ChildrenInfo;
using SmartHouse.FamilyClass.ChildrenTasks;
using SmartHouse.Firebase;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.ShopClass
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShopListPage : ContentPage
    {
        private FirebaseHelper firebaseHelper = new FirebaseHelper();


        public ShopListPage()
        {
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                ShopList.ItemsSource = await firebaseHelper.GetGroupedShopList(); 

            }
            catch { }

        }
        public class ShopItemGroup : ObservableCollection<ShopListInfo>
        {
            public string Kind { get; private set; }

            public ShopItemGroup(string kind, IEnumerable<ShopListInfo> items) : base(items)
            {
                Kind = kind;
            }
        }



        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(InsertShopItem));

        }


        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            var ItemToDelete = swipeItem?.BindingContext as ShopListInfo;
            var ItemCode = ItemToDelete.FirebaseKey;
            try
            {
                await firebaseHelper.DeleteShopList(ItemCode);


                ShopList.ItemsSource = await firebaseHelper.GetGroupedShopList();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Something went wrong: " + ex.Message, "OK");
            }
        }

        private async void SwipeItem_Invoked_1(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            var ItemToEdit = swipeItem?.BindingContext as ShopListInfo;
            var ItemCode = ItemToEdit.FirebaseKey;

            if (ItemToEdit == null)
            {
                await DisplayAlert("Error", "Could not identify the task.", "OK");
                return;
            }
            await Shell.Current.GoToAsync($"{nameof(ShopItemDetail)}?itemKey={ItemCode}");



        }

        private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value) // רק אם סומן כ-true (כלומר המשימה סומנה כבוצעה)
            {
                var checkBox = sender as CheckBox;
                var Item = checkBox?.BindingContext as ShopListInfo;
                var ItemCode = Item.FirebaseKey;
                try
                {
                    await firebaseHelper.DeleteShopList(ItemCode);


                    ShopList.ItemsSource = await firebaseHelper.GetGroupedShopList();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Something went wrong: " + ex.Message, "OK");
                }
            }
        }
    }
}