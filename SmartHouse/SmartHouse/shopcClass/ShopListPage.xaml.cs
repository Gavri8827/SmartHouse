using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.shopcClass
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShopListPage : ContentPage
    {
        ObservableCollection<ShopListInfo> CHlist;
        public ShopListPage()
        {
            InitializeComponent();
            CHlist = new ObservableCollection<ShopListInfo> {
            new ShopListInfo{ Description= "tomatoo",Quantity="6", IsDone = false},
            new ShopListInfo{ Description= "milk",Quantity="6", IsDone = false},
            };
            ShopList.ItemsSource = CHlist;
        }


        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            string newItem = await DisplayPromptAsync("New item", "Enter description");
            string quantityText = await DisplayPromptAsync("New item", "Enter quantity", keyboard: Keyboard.Numeric);

            if (!string.IsNullOrWhiteSpace(newItem) && !string.IsNullOrWhiteSpace(quantityText))
            {
           
                if (int.TryParse(quantityText, out int quantity))
                {
                   
                    CHlist.Add(new ShopListInfo
                    {
                        Description = newItem,
                        Quantity = quantity.ToString(), 
                        IsDone = false
                    });
                }
                else
                {
                    await DisplayAlert("Error", "Quantity must be a number", "OK");
                }
            }
        }
    }
}