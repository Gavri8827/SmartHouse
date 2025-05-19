using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace SmartHouse
{
    public partial class App : Application

         
    {

         private static SQLhelper db;

        public static SQLhelper MyDatabase
        {
            get
            {
                if (db == null)
                {
                    db = new SQLhelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyStore.db3"));
                }
                return db;
            }
        }

        public App()
        {
            InitializeComponent();
           
            MainPage = new NavigationPage(new MainPage());
        }

        

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
