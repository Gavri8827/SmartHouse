using System;
using System.Collections.Generic;
using SmartHouse.HouseCare.Professional;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace SmartHouse.HouseCare
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
            // אתחול רשימת המטלות – ניתן להטעין נתונים ממקור חיצוני
            var tasks = new List<TaskItem>
            {
                new TaskItem { TaskName = "ניקוי מזגנים", IconSource = "icon_ac.png" },
                new TaskItem { TaskName = "בדיקת תאורה", IconSource = "icon_light.png" },
                new TaskItem { TaskName = "בדיקת צנרת", IconSource = "icon_pipe.png" },
                new TaskItem { TaskName = "בדיקת גלאי עשן", IconSource = "icon_smoke.png" }
            };

            // הכנה ל־Grid
            TasksGrid.RowDefinitions.Clear();
            TasksGrid.Children.Clear();

            for (int i = 0; i < tasks.Count; i++)
            {
                // הגדרת שורה אוטומטית
                TasksGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                var task = tasks[i];

                // Frame לעיטוף תכולת המטלה
                var frame = new Frame
                {
                    CornerRadius = 12,
                    Padding = 10,
                    Margin = new Thickness(0, 5),
                    BackgroundColor = Color.FromHex("#FFF3E0"),    // רקע בהיר
                    BorderColor = Color.FromHex("#FF9800"),        // מסגרת כתומה
                    HasShadow = false
                };

                // Layout אופקי לתמונה וטקסט
                var contentLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 15,
                    VerticalOptions = LayoutOptions.Center
                };

                // אייקון מטלה
                contentLayout.Children.Add(new Image
                {
                    Source = task.IconSource,
                    HeightRequest = 40,
                    WidthRequest = 40,
                    VerticalOptions = LayoutOptions.Center
                });

                // Layout אנכי לטקסט ושדה התאריך
                var textLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Spacing = 4,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };

                // שם המטלה
                textLayout.Children.Add(new Label
                {
                    Text = task.TaskName,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromHex("#5D4037")  // חום כהה
                });

                // DatePicker לבחירת תאריך ביצוע
                var dp = new DatePicker
                {
                    Date = task.CompletedDate ?? DateTime.Today,
                    Format = "yyyy-MM-dd",
                    TextColor = Color.FromHex("#FF5722"), // כתום כהה
                    HorizontalOptions = LayoutOptions.Start
                };
                dp.DateSelected += (s, e) =>
                {
                    task.CompletedDate = e.NewDate;
                };
                textLayout.Children.Add(dp);

                contentLayout.Children.Add(textLayout);
                frame.Content = contentLayout;

                // הוספת ה־Frame ל־Grid
                TasksGrid.Children.Add(frame, 0, i);
            }
        }
    }
}
