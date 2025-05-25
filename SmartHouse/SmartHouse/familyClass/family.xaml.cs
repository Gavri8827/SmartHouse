using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.familyClass;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.familyClass
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class family : CarouselPage
    {
		public family ()
		{
			InitializeComponent ();
            BackgroundColor = Color.White;
            image1.Source = ImageSource.FromResource("SmartHouse.images.childrenWork.png");
            image2.Source = ImageSource.FromResource("SmartHouse.images.pet.png");

        }

        private async void Image1_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(childrenWork));
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(Pet));
        }
    }
}