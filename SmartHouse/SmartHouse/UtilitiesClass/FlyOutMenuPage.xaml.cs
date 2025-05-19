using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.UtilitiesClass
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyOutMenuPage : ContentPage
    {

        public ListView MenuListView => listview;
        public FlyOutMenuPage()
        {
            InitializeComponent();
        }
    }
}