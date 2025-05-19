using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.familyClass.childrenInfo;
using SmartHouse.houseCare.professional;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.houseCare
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfessionalPage : ContentPage
    {
        ObservableCollection<professinalnfo> CHlist;
        public ProfessionalPage()
        {
            InitializeComponent();
            CHlist = new ObservableCollection<professinalnfo> {
            new professinalnfo{ name= "Haharon cohen", Description = "electricity", phoneNumber = "0506789932"},
            new professinalnfo{ name= "Shalom yoram", Description = "build instractor", phoneNumber = "050987654"},
            };
            professionalList.ItemsSource = CHlist;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }

        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {

        }
    }
}