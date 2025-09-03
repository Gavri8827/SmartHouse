using Firebase.Database;
using Firebase.Database.Query;
using SmartHouse.FamilyClass.ChildrenInfo;
using SmartHouse.FamilyClass.ChildrenTasks;
using SmartHouse.FamilyClass.PetInfo;
using SmartHouse.FunClass.BucketList;
using SmartHouse.FunClass.CoponList;
using SmartHouse.FunClass.VacationPlan;
using SmartHouse.HouseCare.Professional;
using SmartHouse.ShopClass;
using SmartHouse.UtilitiesClass.Account;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using TaskModel = SmartHouse.FamilyClass.ChildrenTasks.ChildrenTasks;
using CoponModel = SmartHouse.FunClass.CoponList.CoponList;
using BucketModel = SmartHouse.FunClass.BucketList.BucketList;



namespace SmartHouse.Firebase
{
    public class FirebaseHelper
    {
        private readonly FirebaseClient firebase;

        public FirebaseHelper()
        {
            firebase = new FirebaseClient("https://smarthouse-e4231-default-rtdb.firebaseio.com/");
        }

        private string GetGroupId()
        {
            return Preferences.Get("GroupId", "");
        }

        // יצירת פריט קניות
        public async Task CreateShopListItem(ShopListInfo item)
        {
            var groupId = GetGroupId();

            await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("ShopList")
                .PostAsync(item);


        }

        // קריאת כל רשימת הקניות
        public async Task<List<ShopListInfo>> GetShopList()
        {
            try
            {
                var groupId = GetGroupId();
                System.Diagnostics.Debug.WriteLine($"GroupId: {groupId}");

                var items = await firebase
                    .Child("Groups")
                    .Child(groupId)
                    .Child("ShopList")
                    .OnceAsync<ShopListInfo>();

                System.Diagnostics.Debug.WriteLine($"Items Count: {items.Count}");

                var result = items.Select(i =>
                {
                    var shopItem = i.Object;
                    shopItem.FirebaseKey = i.Key;
                    return shopItem;
                }).ToList();

                System.Diagnostics.Debug.WriteLine($"Result Count: {result.Count}");

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in GetShopList: " + ex.Message);
                return new List<ShopListInfo>();
            }
        }
        // עדכון פריט
        public async Task UpdateShopList(string itemKey, ShopListInfo updatedItem)
        {
            try
            {
                var groupId = GetGroupId();

                await firebase
                    .Child("Groups")
                    .Child(groupId)
                    .Child("ShopList")
                    .Child(itemKey)
                    .PutAsync(updatedItem);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in UpdateShopList: {ex.Message}");
                // אפשר להחליט אם לזרוק את השגיאה הלאה או לטפל כאן
                throw;
            }
        }

        public async Task<ShopListInfo> GetShopItemByKey(string key)
        {
            var groupId = GetGroupId();
            var item = await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("ShopList")
                .Child(key)
                .OnceSingleAsync<ShopListInfo>();

            item.FirebaseKey = key; // נשמר לצורך עדכון עתידי
            return item;
        }




        // מחיקת פריט
        public async Task DeleteShopList(string itemKey)
        {
            var groupId = GetGroupId();

            await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("ShopList")
                .Child(itemKey)
                .DeleteAsync();
        }

        // קבוצה לפי קטגוריה
        public class ShopItemGroup : ObservableCollection<ShopListInfo>
        {
            public string Kind { get; private set; }

            public ShopItemGroup(string kind, IEnumerable<ShopListInfo> items) : base(items)
            {
                Kind = kind;
            }
        }


        public async Task<List<ShopItemGroup>> GetGroupedShopList()
        {
            var groupId = GetGroupId();

            try
            {
                var items = await firebase
                    .Child("Groups")
                    .Child(groupId)
                    .Child("ShopList")
                    .OnceAsync<ShopListInfo>();

                var list = items.Select(i =>
                {
                    var item = i.Object;
                    item.FirebaseKey = i.Key;
                    return item;
                }).ToList();

                var grouped = list
                    .GroupBy(i => i.Kind ?? "ללא קטגוריה") // כדי למנוע null
                    .Select(g => new ShopItemGroup(g.Key, g))
                    .ToList();

                return grouped;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Error] GetGroupedShopList: {ex.Message}");
                return new List<ShopItemGroup>(); // החזרה של רשימה ריקה במקרה של שגיאה
            }
        }
        ///כיף
        public async Task CreateVacation(VacationPlan1 item)
        {
            var groupId = GetGroupId();

            await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("VacationList")
                .PostAsync(item);


        }

        public async Task<List<VacationPlan1>> GetVacationList()
        {
            try
            {
                var groupId = GetGroupId();
                System.Diagnostics.Debug.WriteLine($"GroupId: {groupId}");

                var items = await firebase
                    .Child("Groups")
                    .Child(groupId)
                    .Child("VacationList")
                    .OnceAsync<VacationPlan1>();

                System.Diagnostics.Debug.WriteLine($"Items Count: {items.Count}");

                var result = items.Select(i =>
                {
                    var VacationItem = i.Object;
                    VacationItem.FirebaseKey = i.Key;
                    return VacationItem;
                }).ToList();

                System.Diagnostics.Debug.WriteLine($"Result Count: {result.Count}");

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in GetVacationList: " + ex.Message);
                return new List<VacationPlan1>();
            }
        }



        public async Task UpdateVacation(string itemKey, VacationPlan1 updatedItem)
        {
            try
            {
                var groupId = GetGroupId();

                await firebase
                    .Child("Groups")
                    .Child(groupId)
                    .Child("VacationList")
                    .Child(itemKey)
                    .PutAsync(updatedItem);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in UpdateVacation: {ex.Message}");
                
                throw;
            }
        }

        public async Task<VacationPlan1> GetVacationByKey(string key)
        {
            var groupId = GetGroupId();
            var item = await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("VacationList")
                .Child(key)
                .OnceSingleAsync<VacationPlan1>();

            item.FirebaseKey = key; // נשמר לצורך עדכון עתידי
            return item;
        }




        // מחיקת פריט
        public async Task DeleteVacation(string itemKey)
        {
            var groupId = GetGroupId();

            await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("VacationList")
                .Child(itemKey)
                .DeleteAsync();
        }
        // קופונים
        public async Task CreateCopon(CoponModel item)
        {
            var groupId = GetGroupId();

            await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("CoponList")
                .PostAsync(item);


        }

        public async Task<List<CoponModel>> GetCoponList()
        {
            try
            {
                var groupId = GetGroupId();
                System.Diagnostics.Debug.WriteLine($"GroupId: {groupId}");

                var items = await firebase
                    .Child("Groups")
                    .Child(groupId)
                    .Child("CoponList")
                    .OnceAsync<CoponModel>();

                System.Diagnostics.Debug.WriteLine($"Items Count: {items.Count}");

                var result = items.Select(i =>
                {
                    var CoponItem = i.Object;
                    CoponItem.FirebaseKey = i.Key;
                    return CoponItem;
                }).ToList();

                System.Diagnostics.Debug.WriteLine($"Result Count: {result.Count}");

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in GetVacationList: " + ex.Message);
                return new List<CoponModel>();
            }
        }



        public async Task UpdateCopon(string itemKey, CoponModel updatedItem)
        {
            try
            {
                var groupId = GetGroupId();

                await firebase
                    .Child("Groups")
                    .Child(groupId)
                    .Child("CoponList")
                    .Child(itemKey)
                    .PutAsync(updatedItem);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in UpdateCopon: {ex.Message}");

                throw;
            }
        }


        // מחיקת פריט
        public async Task DeleteCopon(string itemKey)
        {
            var groupId = GetGroupId();

            await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("CoponList")
                .Child(itemKey)
                .DeleteAsync();
        }

        // רשימת משאלות
        public async Task CreateBucketList(BucketModel item)
        {
            var groupId = GetGroupId();

            await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("BucketList")
                .PostAsync(item);


        }

        public async Task<List<BucketModel>> GetBucketList()
        {
            try
            {
                var groupId = GetGroupId();
                System.Diagnostics.Debug.WriteLine($"GroupId: {groupId}");

                var items = await firebase
                    .Child("Groups")
                    .Child(groupId)
                    .Child("BucketList")
                    .OnceAsync<BucketModel>();

                System.Diagnostics.Debug.WriteLine($"Items Count: {items.Count}");

                var result = items.Select(i =>
                {
                    var BucketItem = i.Object;
                    BucketItem.FirebaseKey = i.Key;
                    return BucketItem;
                }).ToList();

                System.Diagnostics.Debug.WriteLine($"Result Count: {result.Count}");

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in GetBucketList: " + ex.Message);
                return new List<BucketModel>();
            }
        }
        public async Task DeleteBucket(string itemKey)
        {
            var groupId = GetGroupId();

            await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("BucketList")
                .Child(itemKey)
                .DeleteAsync();
        }
        // אנשי מקצוע
        public async Task CreateProfList(Professinalnfo item)
        {
            var groupId = GetGroupId();

            await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("ProfessionalList")
                .PostAsync(item);


        }

        public async Task<List<Professinalnfo>> GetProftList()
        {
            try
            {
                var groupId = GetGroupId();
                System.Diagnostics.Debug.WriteLine($"GroupId: {groupId}");

                var items = await firebase
                    .Child("Groups")
                    .Child(groupId)
                    .Child("ProfessionalList")
                    .OnceAsync<Professinalnfo>();

                System.Diagnostics.Debug.WriteLine($"Items Count: {items.Count}");

                var result = items.Select(i =>
                {
                    var Professinal = i.Object;
                    Professinal.FirebaseKey = i.Key;
                    return Professinal;
                }).ToList();

                System.Diagnostics.Debug.WriteLine($"Result Count: {result.Count}");

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in GetProfessinalList: " + ex.Message);
                return new List<Professinalnfo>();
            }
        }
        public async Task DeleteProfessinal(string itemKey)
        {
            var groupId = GetGroupId();

            await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("ProfessionalList")
                .Child(itemKey)
                .DeleteAsync();
        }
        /// ילדים
        public async Task CreateChildrenList(ChildrenInfo item)
        {
            var groupId = GetGroupId();

            // שליחת האובייקט וקבלת הקשר לנתיב שנוצר
            var result = await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("ChildrenList")
                .PostAsync(item);

            string childId = result.Key; // זה המפתח שנוצר אוטומטית
            item.FirebaseKey = childId;

            // עדכון FirebaseKey בתוך האובייקט שנשמר
            await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("ChildrenList")
                .Child(childId)
                .PutAsync(item);

            // יוצרים טבלת משימות ריקה עבור הילד
            await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("ChildrenList")
                .Child(childId)
                .Child("ChildTasks")
                .PutAsync(new Dictionary<string, object>());
        }
        public async Task<List<ChildrenInfo>> GetChildrenList()
        {
            try
            {
                var groupId = GetGroupId();
                System.Diagnostics.Debug.WriteLine($"GroupId: {groupId}");

                var items = await firebase
                    .Child("Groups")
                    .Child(groupId)
                    .Child("ChildrenList")
                    .OnceAsync<ChildrenInfo>();

                System.Diagnostics.Debug.WriteLine($"Items Count: {items.Count}");

                var result = items.Select(i =>
                {
                    var Children = i.Object;
                    Children.FirebaseKey = i.Key;
                    return Children;
                }).ToList();

                System.Diagnostics.Debug.WriteLine($"Result Count: {result.Count}");

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in GetProfessinalList: " + ex.Message);
                return new List<ChildrenInfo>();
            }
        }
        public async Task DeleteChildren(string itemKey)
        {
            var groupId = GetGroupId();

            await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("ChildrenList")
                .Child(itemKey)
                .DeleteAsync();
        }

        public async Task AddNewChildTasks(string childKey, string newTask)
        {
            var groupId = GetGroupId();

            var newChildTask = new ChildrenTasks
            {
                Description = newTask,
                IsDone = false
            };

            // שמור את המשימה תחת הילד הספציפי
            await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("ChildrenList")
                .Child(childKey)
                .Child("ChildTasks")
                .PostAsync(newChildTask);

            await Shell.Current.GoToAsync(".."); // חזרה אחורה
        }


        public async Task<List<ChildrenTasks>> GetChildrenTasksByKey(string key)
        {
            var groupId = GetGroupId();

            var tasks = await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("ChildrenList")
                .Child(key)
                .Child("ChildTasks")
                .OnceAsync<ChildrenTasks>();

            // מחזיר את הרשימה של המשימות בלי FirebaseKey
            var taskList = tasks.Select(x => x.Object).ToList();

            return taskList;
        }

        public async Task DeleteChildTask(string childKey, string taskKey)
        {
            var groupId = GetGroupId();
            await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("ChildrenList")
                .Child(childKey)
                .Child("ChildTasks")
                .Child(taskKey)
                .DeleteAsync();
        }
        public async Task UpdateChildTask(string childKey, string taskKey, TaskModel updatedTask)
        {
            var groupId = GetGroupId();
            await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("ChildrenList")
                .Child(childKey)
                .Child("ChildTasks")
                .Child(taskKey)
                .PutAsync(updatedTask);
        }
        public async Task<List<(string Key, ChildrenTasks Task)>> GetChildrenTasksByKeyWithKeys(string childKey)
        {
            var groupId = GetGroupId();

            try
            {
                var tasks = await firebase
                    .Child("Groups")
                    .Child(groupId)
                    .Child("ChildrenList")
                    .Child(childKey)
                    .Child("ChildTasks")
                    .OnceAsync<ChildrenTasks>();

                return tasks.Select(t => (t.Key, t.Object)).ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetChildrenTasksByKeyWithKeys: {ex.Message}");
                return new List<(string, ChildrenTasks)>();
            }
        }
        // בעלי חיים - חיות מחמד
        public async Task CreatePetList(PetInfo item)
        {
            var groupId = GetGroupId();

            await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("PetList")
                .PostAsync(item);


        }

        public async Task<List<PetInfo>> GetPetList()
        {
            try
            {
                var groupId = GetGroupId();
                System.Diagnostics.Debug.WriteLine($"GroupId: {groupId}");

                var items = await firebase
                    .Child("Groups")
                    .Child(groupId)
                    .Child("PetList")
                    .OnceAsync<PetInfo>();

                System.Diagnostics.Debug.WriteLine($"Items Count: {items.Count}");

                var result = items.Select(i =>
                {
                    var Pet = i.Object;
                    Pet.FirebaseKey = i.Key;
                    return Pet;
                }).ToList();

                System.Diagnostics.Debug.WriteLine($"Result Count: {result.Count}");

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in GetProfessinalList: " + ex.Message);
                return new List<PetInfo>();
            }
        }
        public async Task DeletePet(string itemKey)
        {
            var groupId = GetGroupId();

            await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("PetList")
                .Child(itemKey)
                .DeleteAsync();
        }

        public async Task UpdatePet(string itemKey, PetInfo updatedItem)
        {
            try
            {
                var groupId = GetGroupId();

                await firebase
                    .Child("Groups")
                    .Child(groupId)
                    .Child("PetList")
                    .Child(itemKey)
                    .PutAsync(updatedItem);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in UpdateCopon: {ex.Message}");

                throw;
            }
        }
        public async Task CreateUtilitiesList(Account item)
        {
            var groupId = GetGroupId();

            await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("Utilities")
                .PostAsync(item);


        }

        public async Task UpdateUtilities(string itemKey, Account updatedItem)
        {
            try
            {
                var groupId = GetGroupId();

                await firebase
                    .Child("Groups")
                    .Child(groupId)
                    .Child("Utilities")
                    .Child(itemKey)
                    .PutAsync(updatedItem);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in UpdateUtilities: {ex.Message}");

                throw;
            }
        }

        public async Task<List<Account>> GetUtilitiesList()
        {
            try
            {
                var groupId = GetGroupId();
                System.Diagnostics.Debug.WriteLine($"GroupId: {groupId}");

                var items = await firebase
                    .Child("Groups")
                    .Child(groupId)
                    .Child("Utilities")
                    .OnceAsync<Account>();

                System.Diagnostics.Debug.WriteLine($"Items Count: {items.Count}");

                var result = items.Select(i =>
                {
                    var acount = i.Object;
                    acount.FirebaseKey = i.Key;
                    return acount;
                }).ToList();

                System.Diagnostics.Debug.WriteLine($"Result Count: {result.Count}");

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in GetProfessinalList: " + ex.Message);
                return new List<Account>();
            }
        }

        // דוח הסטיוריית תשלומים חשבונות
        public async Task AddPaymentToHistory(HistoryPayment item)
        {
            var groupId = GetGroupId();

            await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("PaymentHistory")
                .PostAsync(item);
        }

        public async Task<List<HistoryPayment>> GetPaymentHistory()
        {
            var groupId = GetGroupId();
            var history = await firebase
                .Child("Groups")
                .Child(groupId)
                .Child("PaymentHistory")
                .OnceAsync<HistoryPayment>();

            return history.Select(x =>
            {
                x.Object.FirebaseKey = x.Key;
                return x.Object;
            }).ToList();
        }


    }






}
