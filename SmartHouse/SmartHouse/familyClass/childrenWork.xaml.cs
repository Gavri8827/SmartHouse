using System;
using System.Collections.Generic;
using Xamarin.Forms;
using SmartHouse.FamilyClass.ChildrenInfo;
using Xamarin.Forms.Xaml;
using SmartHouse.FamilyClass.ChildrenTasks;
using Xamarin.Essentials;
using System.Collections.ObjectModel;
using SmartHouse.Firebase;
using ChildModel = SmartHouse.FamilyClass.ChildrenInfo.ChildrenInfo;

namespace SmartHouse.FamilyClass
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChildrenWork : ContentPage
    {
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        public ChildrenWork()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                childrenList.ItemsSource = await firebaseHelper.GetChildrenList();
            }
            catch { }
        }

        private async void ImageWrite_Clicked(object sender, EventArgs e)
        {
            string newTask = await DisplayPromptAsync("הוספת משימה חדשה", "תיאור המשימה:");
            if (!string.IsNullOrWhiteSpace(newTask))
            {
                var button = sender as ImageButton;
                var child = button?.CommandParameter as ChildModel;

                if (child != null)
                {
                    await firebaseHelper.AddNewChildTasks(child.FirebaseKey, newTask);
                    
                }
            }
        }

        async void ChildrenList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is ChildModel child)
            {
                ((ListView)sender).SelectedItem = null;
                // ניווט ב-Shell, מעביר את השם כפרמטר
                await Shell.Current.GoToAsync(
                    $"{nameof(ChildrenTaskInfo)}?childKey={Uri.EscapeDataString(child.FirebaseKey)}"
                );
            }
        }

       

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            string name = await DisplayPromptAsync("הוספת ילד", "שם הילד");

            if (string.IsNullOrWhiteSpace(name))
            {
                await DisplayAlert("Invalid", "Blank or incorrect", "OK");
                return;
            }

            await DisplayAlert(
                "הוספת תמונת הילד",
                "המסך הבא יפתח את הגלריה של המכשיר.\n" +
                "בחר/י תמונה ולאחר מכן אשר/י בלחיצה על ✓ או DONE.",
                "הבנתי"
            );

            Plugin.Media.Abstractions.MediaFile result = null;

            try
            {
                result = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full
                });
            }
            catch (Plugin.Media.Abstractions.MediaPermissionException pEx)
            {
                System.Diagnostics.Debug.WriteLine($"[BucketListPage] PermissionException: {pEx.Message}");
                await DisplayAlert("הרשאה נדחתה", "לא ניתן לגשת לתמונות ללא הרשאה.", "אוקיי");
                return;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                System.Diagnostics.Debug.WriteLine($"[BucketListPage] FeatureNotSupportedException: {fnsEx.Message}");
                await DisplayAlert("שגיאה", "מכשיר זה לא תומך בחירת תמונה דרך גלריה.", "אוקיי");
                return;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[BucketListPage] Exception בזמן PickPhotoAsync: {ex}");
                await DisplayAlert("שגיאה", "ארעה שגיאה בלתי צפויה: " + ex.Message, "אוקיי");
                return;
            }

            /* 5) המשתמש ביטל? */
            if (result == null)
            {
                await DisplayAlert("בוטל", "לא נבחרה תמונה.", "אוקיי");
                return;
            }
            string imagePath = result.Path;

            await firebaseHelper.CreateChildrenList(new ChildModel
            {
                Name = name,
                Image = imagePath
            });

            // ✅ רענון הרשימה לאחר הוספת ילד חדש
            childrenList.ItemsSource = await firebaseHelper.GetChildrenList();
        }

      

        private async void SwipeItem_delete(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            var NameToDelete = swipeItem?.BindingContext as ChildModel;
            try
            {
                await firebaseHelper.DeleteChildren(NameToDelete.FirebaseKey);
                childrenList.ItemsSource = await firebaseHelper.GetChildrenList();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Something went wrong: " + ex.Message, "OK");
            }
        }
    }
}
