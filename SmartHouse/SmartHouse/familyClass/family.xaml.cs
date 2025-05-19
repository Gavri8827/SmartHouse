using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.familyClass;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class family : CarouselPage
    {
		public family ()
		{
			InitializeComponent ();
            image1.Source = ImageSource.FromResource("SmartHouse.images.childrenWork.png");
            image2.Source = ImageSource.FromResource("SmartHouse.images.pet.png");

        }

        private  void Image1_Clicked(object sender, EventArgs e)
        {
             Navigation.PushAsync(new NavigationPage(new childrenWork()));
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage(new Pet()));
        }
    }
}