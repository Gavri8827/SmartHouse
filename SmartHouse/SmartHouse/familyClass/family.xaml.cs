using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.FamilyClass;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Family : CarouselPage
    {
		public Family ()
		{
			InitializeComponent ();
            BackgroundColor = Color.White;
            image1.Source = ImageSource.FromResource("SmartHouse.images.children.JPG");
            image2.Source = ImageSource.FromResource("SmartHouse.images.cats.jpg");

        }

        private async void Image1_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ChildrenWork));
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(Pet));
        }
    }
}