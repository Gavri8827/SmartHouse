using System;
using System.Collections.Generic;
using Xamarin.Forms;
using SmartHouse.familyClass.childrenInfo;
using Xamarin.Forms.Xaml;
using SmartHouse.familyClass.childrenTasks;
using System.Collections.ObjectModel;

namespace SmartHouse.familyClass
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class childrenWork : ContentPage
    {
       // ObservableCollection<ChildrenInfo> CHlist;
        public childrenWork()
        {
            InitializeComponent();
       //     CHlist = new ObservableCollection<ChildrenInfo> {
         //   new ChildrenInfo{ Name= "Uriel", Image = "https://www.israelhayom.co.il/wp-content/uploads/2022/02/15990998663779_b.jpg"},
        //    new ChildrenInfo{ Name= "Nerya", Image = "https://www.seret.co.il/images/actors/VinDiesel/VinDiesel1.jpg"},
          //  };
            //childrenList.ItemsSource = CHlist;
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                childrenList.ItemsSource = await App.MyDatabase.readChildrenTask();
            }
            catch { }

        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void ImageWrite_Clicked(object sender, EventArgs e)
        {
            string newTask = await DisplayPromptAsync("New task", "Enter description");
            if (!string.IsNullOrWhiteSpace(newTask))
            {
                var button = sender as ImageButton;
                var child = button?.CommandParameter as ChildrenInfo;

                if (child != null)
                {
                    child.Tasks.Add(new ChildrenTasks { Description = newTask, IsDone = false });
                }


            }


        }


        async void childrenList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var children = e.SelectedItem as ChildrenInfo;
            var childrenPage = new childrenTaskInfo();
            childrenPage.BindingContext = children;
            await Navigation.PushAsync(childrenPage);
        }

        async void childrenList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var children = e.Item as ChildrenInfo;
            var childrenPage = new childrenTaskInfo();
            childrenPage.BindingContext = children;
            await Navigation.PushAsync(childrenPage);

        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            string name = await DisplayPromptAsync("Please add new name", "add name");

            if (string.IsNullOrWhiteSpace(name))
            {
                await DisplayAlert("Invalid", "Blank or incorect", "OK");
            }
            AddNewChild(name);


        }

        private async void AddNewChild(string name)
        {
            await App.MyDatabase.createChildrenTask(new ChildrenInfo
            {
                Name = name,
             
            });
            await Navigation.PopAsync();

        }
    }
}