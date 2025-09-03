using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.FamilyClass.PetInfo;
using SmartHouse.Firebase;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PetModel = SmartHouse.FamilyClass.PetInfo.PetInfo;

namespace SmartHouse.FamilyClass
{
    [QueryProperty(nameof(PetId), "petId")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PetInfoPage : ContentPage
    {
        private string petId;
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        public string PetId
        {
            get => petId;
            set
            {
                petId =  Uri.UnescapeDataString(value);
                LoadPetById(petId);
            }
        }
        public PetInfoPage()
        {
            InitializeComponent();
        }

        private async void LoadPetById(string id)
        {
            var all = await firebaseHelper.GetPetList();
            var Pet = all.FirstOrDefault(c => c.FirebaseKey == id);
            if (Pet != null)
                BindingContext = Pet;
            else
                await DisplayAlert("שגיאה", "פרטי בעל החיים לא נמצאים", "אוקיי");
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is PetModel currentPet)
            {
                try
                {
                    currentPet.Treatment = date.Date;

                    await firebaseHelper.UpdatePet(currentPet.FirebaseKey, currentPet);

                    await DisplayAlert("הצלחה", "תאריך הטיפול עודכן ונשמר בהצלחה!", "אוקיי");
                    
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"שגיאה בשמירת תאריך: {ex.Message}");
                    await DisplayAlert("שגיאה", "אירעה שגיאה בעת שמירת תאריך הטיפול", "אוקיי");
                }
            }
            else
            {
                await DisplayAlert("שגיאה", "לא נמצאה חיית מחמד לעדכון", "אוקיי");
            }
        }

        private void Date_DateSelected(object sender, DateChangedEventArgs e)
        {

        }
    }
}