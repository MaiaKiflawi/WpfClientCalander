using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfClientCalander.ServiceCalander;

namespace WpfClientCalander
{
    /// <summary>
    /// Interaction logic for AddEventsUC.xaml
    /// </summary>
    public partial class AddEventsUC : UserControl
    {
        Groups group;
        EventList list;
        Users user;
        private CalanderServiceClient serviceClient;
        private Grid grid;
        public AddEventsUC(Groups group, Users user, ref Grid grid)
        {
            InitializeComponent();
            this.group = group;
            this.DataContext = group;
            this.grid = grid;
            this.user = user;
            this.serviceClient = new CalanderServiceClient();
            list = serviceClient.GetEventsByGroup(group);
            ShowEvents();
            SetSelectionDates();
        }

        private void ShowEvents()
        {
            existingEvents.Children.Clear();
            existingEvents.Margin = new Thickness(3.5);
            TextBlock tblk = new TextBlock();
            tblk.FontSize = 15;
            tblk.FontWeight = FontWeights.Bold;
            tblk.Foreground = new SolidColorBrush(Colors.Black);
            tblk.Text = "Existing Events:";
            existingEvents.Children.Add(tblk);
            TextBlock tb = new TextBlock();
            tb.Text = " ";
            existingEvents.Children.Add(tb);
            foreach (Event events in list)
            {
                TextBlock tblkEvents = new TextBlock();
                tblkEvents.Text = "* " + events.EventName + ":  " + events.EventStart + " - " + events.EventEnd;
                tblkEvents.FontSize = 13;
                tblkEvents.Foreground = new SolidColorBrush(Colors.DarkGray);
                tblkEvents.TextWrapping = TextWrapping.WrapWithOverflow;
                tblkEvents.Margin = new Thickness(2);
                existingEvents.Children.Add(tblkEvents);
            }
        }

        private void SetSelectionDates()
        {
            CalendarDateRange cdr1 = new CalendarDateRange(DateTime.Today.AddYears(5), DateTime.Today.AddYears(1000));
            CalendarDateRange cdr2 = new CalendarDateRange(DateTime.Today.AddYears(-50), DateTime.Today);
            dtpStart.DisplayDateStart = DateTime.Today.AddYears(-50);
            dtpStart.BlackoutDates.Add(cdr1);
            dtpStart.BlackoutDates.Add(cdr2);
            //dtpEnd.BlackoutDates.Add(cdr1);
            //dtpEnd.BlackoutDates.Add(cdr2);
            //dtpEnd.DisplayDateStart = DateTime.Today.AddYears(-50);
        }

        private void Back()
        {
            grid.Children.Clear();
            grid.Children.Add(new GroupAdminUC(user, ref grid));
        }

        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            //if (dtpStart.SelectedDate > dtpEnd.SelectedDate)
            //{
            //    MessageBox.Show("Event start date can't be after event end date.", "ERROR", MessageBoxButton.OK);
            //    return;
            //}
            if (tbxEventName.Text == null || tbxEventName.Text == " " || tbxEventName.Text == "")
            {
                MessageBox.Show("Event name can't be empty.\n Try again.", "ERROR", MessageBoxButton.OK);
                return;
            }
            Event events = new Event();
            events.EventGroup = new Groups();
            events.EventGroup.Id = this.group.Id;
            events.EventName = tbxEventName.Text;
            events.EventStart = DateTime.Parse(dtpStart.Text);
            events.EventEnd = DateTime.Parse(dtpEnd.Text);
            if (serviceClient.InsertEvent(events) != 1)
            {
                MessageBox.Show("System error.\n Try again.", "ERROR", MessageBoxButton.OK);
                return;
            }
            MessageBox.Show("Event added successfully!", "SUCCESS", MessageBoxButton.OK);
            Back();
        }

        private void dtpStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CalendarDateRange cdr = new CalendarDateRange(DateTime.Today, DateTime.Parse(dtpStart.Text).AddDays(-1));
            //dtpEnd.BlackoutDates.Add(cdr);
        }
        private void dtpEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CalendarDateRange cdr = new CalendarDateRange(DateTime.Parse(dtpEnd.Text).AddDays(+1), DateTime.Today.AddYears(1000));
            dtpStart.BlackoutDates.Add(cdr);
        }
    }
}
