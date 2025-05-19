using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.FunClass.bucketList;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.FunClass
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BucketListPage : ContentPage
    {

        ObservableCollection<bucketListInfo> bucketListInfos;
        public BucketListPage()
        {
            InitializeComponent();
            bucketListInfos = new ObservableCollection<bucketListInfo>() { new bucketListInfo { Description = "visit the maldives", FunImage = ImageSource.FromResource("SmartHouse.images.view.jpg"), IsDune = false } };
            myView.ItemsSource = bucketListInfos;
        } 
    }
}