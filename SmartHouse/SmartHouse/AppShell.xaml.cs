using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.FunClass;
using SmartHouse.HouseCare;
using SmartHouse.ShopClass;
using SmartHouse.UtilitiesClass;
using SmartHouse.FamilyClass;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHouse.user;
using Xamarin.Essentials;

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
            Routing.RegisterRoute("family", typeof(Family));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(ConnectPage), typeof(ConnectPage));

            Routing.RegisterRoute(nameof(EditPet), typeof(EditPet));
            Routing.RegisterRoute(nameof(InsertShopItem), typeof(InsertShopItem));

            Routing.RegisterRoute(nameof(VactionPage), typeof(VactionPage));
            Routing.RegisterRoute(nameof(VactionListPage), typeof(VactionListPage));
            Routing.RegisterRoute(nameof(FamilyTimePage), typeof(FamilyTimePage));
            Routing.RegisterRoute(nameof(BucketListPage), typeof(BucketListPage));
            Routing.RegisterRoute(nameof(CoponPage), typeof(CoponPage));
            Routing.RegisterRoute(nameof(AddCopon), typeof(AddCopon));

            Routing.RegisterRoute(nameof(PrivateCoponInfo), typeof(SmartHouse.FunClass.PrivateCoponInfo));
            Routing.RegisterRoute(nameof(ProfessionalPage), typeof(ProfessionalPage));
            Routing.RegisterRoute(nameof(Maintence), typeof(Maintence));

            Routing.RegisterRoute(nameof(ChildrenWork), typeof(ChildrenWork));
            Routing.RegisterRoute(nameof(Pet), typeof(Pet));
            Routing.RegisterRoute(nameof(PetInfoPage), typeof(PetInfoPage));
            Routing.RegisterRoute(nameof(ChildrenTaskInfo), typeof(ChildrenTaskInfo));
            Routing.RegisterRoute(nameof(VacationEditPage), typeof(VacationEditPage));
           


            Routing.RegisterRoute(nameof(ShopItemDetail), typeof(ShopItemDetail));
        }

    }
	}
