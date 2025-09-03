using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.FamilyClass.ChildrenTasks;
using SmartHouse.Firebase;
using SmartHouse.FunClass.BucketList;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BucketModel = SmartHouse.FunClass.BucketList.BucketList;

namespace SmartHouse.FunClass
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BucketListPage : ContentPage
    {
        private FirebaseHelper firebaseHelper = new FirebaseHelper();

        
        public BucketListPage()
        {
            InitializeComponent();
           
            
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                bucketListItems.ItemsSource = await firebaseHelper.GetBucketList();
            }
            catch { }
        }


        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {

            string description = await DisplayPromptAsync("פריט חדש", "איפה היית רוצה לבלות?");
            if (string.IsNullOrWhiteSpace(description))
                return;


            /* 3) הסבר קצר למשתמש לפני פתיחת הגלריה */
            await DisplayAlert(
                "בחירת תמונה",
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

            await firebaseHelper.CreateBucketList(new BucketModel
            {
                Description = description,
                FunImage = imagePath,
                IsDone = false

            });
            await Shell.Current.GoToAsync("..");



        }

        private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value) // רק אם סומן כ-true (כלומר המשימה סומנה כבוצעה)
            {
                var checkBox = sender as CheckBox;
                var task = checkBox?.BindingContext as BucketModel;

                if (task != null)
                {
                    
                    await firebaseHelper.DeleteBucket(task.FirebaseKey);

                    
                    bucketListItems.ItemsSource = await firebaseHelper.GetBucketList();
                }
            }
        }
    }
}
