using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfClientCalander.ServiceCalander;
using Color = System.Windows.Media.Color;

namespace WpfClientCalander
{
    /// <summary>
    /// Interaction logic for UserProfileUC.xaml
    /// </summary>
    public partial class UserProfileUC : UserControl
    {
        Users user;
        List<TextBlock> dates;
        List<StackPanel> events;
        string[] days = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        EventList userEvents;
        CalanderServiceClient calanderService;
        UserWindow parent;
        Grid grid;
        public UserProfileUC(Users user, UserWindow userWindow, ref Grid grid)
        {
            InitializeComponent();
            this.user = user;
            parent = userWindow;
            calanderService = new CalanderServiceClient();
            userEvents = calanderService.GetEventsByUser(user);
            dates = new List<TextBlock>();
            events = new List<StackPanel>();
            SetGridLines();
            LoadDates(DateTime.Today.Month, DateTime.Today.Year);
            SetMonthYears();
            cmbMonth.SelectedIndex = DateTime.Today.Month - 1;
            this.grid = grid;
        }
        private void LoadDates(int month, int year) //load dates and events into grid
        {
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            DateTime lastDayOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            EventList monthEvents = new EventList();
            foreach (Event myEvent in userEvents)
            {
                // Check if the event spans into the current month
                if (myEvent.EventEnd >= firstDayOfMonth && myEvent.EventStart <= lastDayOfMonth)
                {
                    monthEvents.Add(myEvent);
                }
            }

            int last = DateTime.DaysInMonth(year, month) + (int)firstDayOfMonth.DayOfWeek;
            DateTime dt = firstDayOfMonth;
            for (int i = (int)firstDayOfMonth.DayOfWeek; i < last; i++)
            {
                dates[i].Text = dt.ToString("dd/MM/yyyy");
                List<Event> today = monthEvents.FindAll(m => m.EventStart.Date <= dt.Date && m.EventEnd.Date >= dt.Date);
                foreach (Event myEvent in today)
                {
                    Button button = new Button
                    {
                        Content = myEvent.EventName,
                        Tag = myEvent,
                        Style = (Style)FindResource("EventButton")
                    };
                    button.Click += ViewEvent_Click;
                    events[i].Children.Add(button);
                }
                dt = dt.AddDays(1);
            }
        }

        private void ViewEvent_Click(object sender, RoutedEventArgs e)
        {
            parent.LoadEvent((sender as Button).Tag as Event);
        }

        private void SetMonthYears() //create year and month comboboxitems
        {
            cmbMonth.ItemsSource = DateTimeFormatInfo.CurrentInfo.MonthNames;
            for (int year = DateTime.Today.AddYears(5).Year; year > DateTime.Today.AddYears(-50).Year; year--)
                cmbYear.Items.Add(year);
            cmbYear.SelectedItem = DateTime.Today.Year;
            cmbMonth.SelectedItem = DateTime.Today.Month;
        }
        private void SetGridLines() //create timetable
        {
            for (int i = 1; i <= 13; i++)
            {
                for (int j = 1; j <= 7; j++)
                {
                    if (i == 1)
                    {
                        Border myBorder = new Border();
                        myBorder.Height = 20;
                        myBorder.Width = 830 / 7;
                        myBorder.Background = new SolidColorBrush(Color.FromArgb(255, 0xc0, 0xb3, 0xc2));
                        myBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0x7e, 0x4c, 0x45));
                        myBorder.BorderThickness = new Thickness(1);
                        myBorder.Margin = new Thickness(.3);
                        TextBlock textBlock = new TextBlock();
                        textBlock.Text = days[j - 1];
                        myBorder.Child = textBlock;
                        calWP.Children.Add(myBorder);
                    }
                    else if (i % 2 == 0)
                    {
                        Border myBorder = new Border();
                        myBorder.Height = 18;
                        myBorder.Width = 830 / 7;
                        myBorder.Background = new SolidColorBrush(Color.FromArgb(255, 0xf3, 0xe5, 0xf5));
                        myBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0x7e, 0x4c, 0x45));
                        myBorder.BorderThickness = new Thickness(1);
                        myBorder.Margin = new Thickness(.3);
                        TextBlock tb = new TextBlock();
                        myBorder.Child = tb;
                        dates.Add(tb);
                        calWP.Children.Add(myBorder);
                    }
                    else
                    {
                        ScrollViewer scrollViewer = new ScrollViewer();
                        Border myBorder = new Border();
                        myBorder.Height = 57;
                        myBorder.Width = 830 / 7;
                        myBorder.Background = new SolidColorBrush(Color.FromArgb(255, 0xff, 0xf3, 0xe0));
                        myBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0x7e, 0x4c, 0x45));
                        myBorder.BorderThickness = new Thickness(1);
                        myBorder.Margin = new Thickness(.3);
                        StackPanel st = new StackPanel();
                        scrollViewer.Content = st;
                        myBorder.Child = scrollViewer;
                        events.Add(st);
                        calWP.Children.Add(myBorder);
                    }
                }
            }
        }
        private void cmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbYear.SelectedIndex != -1 && cmbMonth.SelectedIndex != -1)
            {
                foreach (TextBlock tb in dates) tb.Text = string.Empty;
                foreach (StackPanel stackPanel in events) stackPanel.Children.Clear();

                LoadDates(cmbMonth.SelectedIndex + 1, (int)cmbYear.SelectedValue);
            }
        }
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(new UserSettingsUC(user, parent, ref grid));
        }
        private void logOut_Click(object sender, RoutedEventArgs e)
        {
            WelcomeWindow welcomeWindow = new WelcomeWindow();
            parent.Close();
            welcomeWindow.ShowDialog();
        }
    }
}
