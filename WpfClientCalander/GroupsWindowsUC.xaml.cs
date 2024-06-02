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
    /// Interaction logic for GroupsWindowsUC.xaml
    /// </summary>
    public partial class GroupsWindowsUC : UserControl
    {
        List<TextBlock> dates;
        List<StackPanel> events;
        string[] days = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        Groups group;
        EventList groupEvents;
        CalanderServiceClient calanderService;
        UserWindow parent;
        public GroupsWindowsUC(Groups group, UserWindow userWindow)
        {
            InitializeComponent();
            parent = userWindow;
            this.group = group;
            this.DataContext = group;
            calanderService = new CalanderServiceClient();
            groupEvents = calanderService.GetEventsByGroup(group);
            dates = new List<TextBlock>();
            events = new List<StackPanel>();
            SetGridLines();
            LoadDates(DateTime.Today.Month, DateTime.Today.Year);
            SetMonthYears();
            cmbMonth.SelectedIndex = DateTime.Today.Month - 1;
        }
        private void LoadDates(int month, int year) //load dates in grid
        {
            DateTime dt = new DateTime(year, month, 1);
            EventList monthEvents = new EventList();
            foreach (Event myEvent in groupEvents)
            {
                if (myEvent.EventStart.Month.Equals(dt.Month) && myEvent.EventStart.Year.Equals(dt.Year))
                    monthEvents.Add(myEvent);
            }
            int last = DateTime.DaysInMonth(year, month) + (int)dt.DayOfWeek;
            for (int i = (int)dt.DayOfWeek; i < last; i++)
            {
                dates[i].Text = dt.ToString("dd/MM/yyyy");
                List<Event> today = monthEvents.FindAll(m => m.EventStart.Day <= dt.Day && m.EventEnd.Day >= dt.Day);
                foreach (Event myEvent in today)
                {
                    Button button = new Button();
                    button.Content = myEvent.EventName;
                    button.Tag = myEvent;
                    button.Click += ViewEvent_Click;
                    button.Style = (Style)FindResource("EventButton");
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
                        st.Children.Add(scrollViewer);
                        myBorder.Child = st;
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

    }
}