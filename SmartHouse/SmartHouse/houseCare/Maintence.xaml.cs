using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.houseCare.houseTasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.houseCare
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Maintence : ContentPage
    {
        public Maintence()
        {
            InitializeComponent();
            BuildTasksGrid();
        }
        private void BuildTasksGrid()
        {
            // אתחול רשימת המטלות – ניתן לטעון נתונים ממקור חיצוני
            var tasks = new List<TaskItem>
            {
                new TaskItem { TaskName = "ניקוי מזגנים" },
                new TaskItem { TaskName = "בדיקת תאורה" },
                new TaskItem { TaskName = "בדיקת תאורה" },
                new TaskItem { TaskName = "בדיקת תאורה" }
                // ניתן להוסיף מטלות נוספות כאן...
            };
            TasksGrid.RowDefinitions.Clear();
            for (int i = 0; i < tasks.Count; i++)
            {
                TasksGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            // ניקוי תוכן קיים ב-Grid לפני הוספת פריטים חדשים
            TasksGrid.Children.Clear();

            // הוספת כל מטלה כאלמנט בתוך Frame לשורה המתאימה
            for (int i = 0; i < tasks.Count; i++)
            {
                var task = tasks[i];
                var contentLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                

                // הוספת Label למטה, שמציג את שם המטלה
                var label = new Label
                {
                    Text = task.TaskName, // task הוא המשתנה שמייצג את המטלה שנמצאת בלולאה
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                contentLayout.Children.Add(label);

                var label1 = new Label
                {
                    Text = task.CompletedDate.ToString(), 
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                contentLayout.Children.Add(label1);


                var image = new Image
                {
                    Source = "task.png", 
                    Aspect = Aspect.AspectFit,
                    HeightRequest = 80,
                    WidthRequest = 80,
                    HorizontalOptions = LayoutOptions.Center
                };
                contentLayout.Children.Add(image);



                var frame = new Frame
                {
                    Padding = 15,
                    Margin = 5,
                    BackgroundColor = Color.LightGray,
                    CornerRadius = 10,
                    HasShadow = true,
                    Content = contentLayout
                    
                };

                
                var tapGesture = new TapGestureRecognizer();
                tapGesture.Tapped += async (s, e) =>
                {
                    string result = await DisplayPromptAsync(
                        "הזן תאריך",
                        $"הזן את תאריך ביצוע המשימה '{task.TaskName}' (לדוגמה: 2025-05-03):");

                    if (DateTime.TryParse(result, out DateTime date))
                    {
                        task.CompletedDate = date;
                        await DisplayAlert("מצוין", "התאריך נקלט בהצלחה", "אישור");
                    }
                    else
                    {
                        await DisplayAlert("שגיאה", "פורמט תאריך לא חוקי", "אישור");
                    }
                };
                frame.GestureRecognizers.Add(tapGesture);

                
                TasksGrid.Children.Add(frame, 0, i);
            }
        }
    }
}