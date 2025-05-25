using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.FunClass;
using SmartHouse.houseCare;
using SmartHouse.shopcClass;
using SmartHouse.UtilitiesClass;
using SmartHouse.familyClass;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppShell : Shell
	{
		public AppShell ()
		{
			InitializeComponent ();
            Routing.RegisterRoute(nameof(InsertUtilityInfo), typeof(InsertUtilityInfo));

            Routing.RegisterRoute("home", typeof(MainPage));
            Routing.RegisterRoute("utilities", typeof(UtilitiesMainPage));
            Routing.RegisterRoute("fun", typeof(FunMainPage));
            Routing.RegisterRoute("shop", typeof(ShopListPage));
            Routing.RegisterRoute("housecare", typeof(HouseCareMainPage));
            Routing.RegisterRoute("family", typeof(family));
            Routing.RegisterRoute("about", typeof(About));

            Routing.RegisterRoute(nameof(VactionPage), typeof(VactionPage));
            Routing.RegisterRoute(nameof(FamilyTimePage), typeof(FamilyTimePage));
            Routing.RegisterRoute(nameof(BucketListPage), typeof(BucketListPage));
            Routing.RegisterRoute(nameof(coponPage), typeof(coponPage));

            Routing.RegisterRoute(nameof(ProfessionalPage), typeof(ProfessionalPage));
            Routing.RegisterRoute(nameof(Maintence), typeof(Maintence));

            Routing.RegisterRoute(nameof(childrenWork), typeof(childrenWork));
            Routing.RegisterRoute(nameof(Pet), typeof(Pet));
        }

    }
	}
