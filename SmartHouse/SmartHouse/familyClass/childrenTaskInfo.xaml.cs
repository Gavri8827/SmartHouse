using System;
using System.Collections.Generic;
using System.Linq;
using SmartHouse.FamilyClass.ChildrenInfo;
using SmartHouse.FamilyClass.ChildrenTasks;
using SmartHouse.Firebase;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TaskModel = SmartHouse.FamilyClass.ChildrenTasks.ChildrenTasks;

namespace SmartHouse.FamilyClass
{
    [QueryProperty(nameof(ChildKey), "childKey")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChildrenTaskInfo : ContentPage
    {
        private FirebaseHelper firebaseHelper = new FirebaseHelper();

        private string _childKey;
        public string ChildKey
        {
            get => _childKey;
            set
            {
                _childKey = Uri.UnescapeDataString(value);
                LoadChildTasks();
            }
        }

        // שמירה של רשימת משימות כולל מפתחות Firebase
        private List<(string Key, TaskModel Task)> firebaseTasks = new List<(string, TaskModel)>();

        public ChildrenTaskInfo()
        {
            InitializeComponent();
        }

        private async void LoadChildTasks()
        {
            try
            {
                var taskList = await firebaseHelper.GetChildrenTasksByKeyWithKeys(ChildKey);
                firebaseTasks = taskList;
                ChildrenTaskList.ItemsSource = taskList.Select(t => t.Task); // מציג רק את האובייקטים עצמם ברשימה
            }
            catch (Exception ex)
            {
                await DisplayAlert("שגיאה", $"טעינת משימות נכשלה: {ex.Message}", "אישור");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void SwipeItem_delete(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            var taskToDelete = swipeItem?.BindingContext as TaskModel;

            if (taskToDelete != null)
            {
                bool confirm = await DisplayAlert("מחיקה", "האם אתה בטוח שברצונך למחוק את המשימה?", "כן", "לא");
                if (confirm)
                {
                    var item = firebaseTasks.Find(t => t.Task == taskToDelete);
                    if (!string.IsNullOrEmpty(item.Key))
                    {
                        await firebaseHelper.DeleteChildTask(_childKey, item.Key);
                        LoadChildTasks();
                    }
                }
            }
        }

        private async void SwipeItem_Invoked_1(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            var taskToEdit = swipeItem?.BindingContext as TaskModel;

            if (taskToEdit == null) return;

            string newDescription = await DisplayPromptAsync("עריכת משימה", "תיאור חדש:", initialValue: taskToEdit.Description);
            if (!string.IsNullOrWhiteSpace(newDescription))
            {
                var item = firebaseTasks.Find(t => t.Task == taskToEdit);
                if (!string.IsNullOrEmpty(item.Key))
                {
                    taskToEdit.Description = newDescription;
                    await firebaseHelper.UpdateChildTask(_childKey, item.Key, taskToEdit);
                    LoadChildTasks();
                }
            }
        }

        private async void CheckBox_isDone(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            var task = checkBox?.BindingContext as TaskModel;

            if (task != null)
            {
                task.IsDone = e.Value;
                var item = firebaseTasks.Find(t => t.Task == task);
                if (!string.IsNullOrEmpty(item.Key))
                {
                    await firebaseHelper.DeleteChildTask(_childKey,item.Key);
                    LoadChildTasks();
                }
            }
        }
    }
}
