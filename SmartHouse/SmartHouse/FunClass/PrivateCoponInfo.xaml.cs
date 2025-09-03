using System.Linq;
using SmartHouse.Firebase;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.FunClass
{
    [QueryProperty(nameof(publicCoponId), "id")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrivateCoponInfo : ContentPage
    {
        private string privateCoponId;
        private FirebaseHelper firebaseHelper = new FirebaseHelper();

        public string publicCoponId
        {
            get => privateCoponId;
            set
            {
                privateCoponId = value;
                LoadCoupon(privateCoponId);
            }
        }

        public PrivateCoponInfo()
        {
            InitializeComponent();
        }

        private async void LoadCoupon(string couponId)
        {
            var all = await firebaseHelper.GetCoponList();
            var coupon = all.FirstOrDefault(c => c.FirebaseKey == couponId);
            if (coupon != null)
                BindingContext = coupon;
            else
                await DisplayAlert("שגיאה", "הקופון לא נמצא", "אוקיי");
        }
    }
}
