using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.FamilyClass.PetInfo;
using SmartHouse.Firebase;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PetModel = SmartHouse.FamilyClass.PetInfo.PetInfo;


namespace SmartHouse.FamilyClass
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPet : ContentPage
    {
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        public EditPet()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            
            await DisplayAlert(
                 "הוספת תמונת חיית המחמד",
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

            
            if (result == null)
            {
                await DisplayAlert("בוטל", "לא נבחרה תמונה.", "אוקיי");
                return;
            }
            string imagePath = result.Path;
            var newPet = new PetModel
            {

                Name = EditPetName.Text,
                Kind = PetKind.Text,
                Image = imagePath,
                Insurance = PetInsurance.Text,
                Treatment = TreatmentDate.Date,
                
            };

            await firebaseHelper.CreatePetList(newPet);
            await DisplayAlert("Success", "פרטי בעל החיים נשמרו", "OK");
            // ✅ חזרה אחורה לעמוד עם רשימת בעלי החיים
            await Shell.Current.GoToAsync("..");

        }
    }
}