using System;
using System.Collections.Generic;
using System.Linq;
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
        GroupAdminUC parent;
        EventList list;
        private CalanderServiceClient serviceClient;
        public AddEventsUC(Groups group, GroupAdminUC uc)
        {
            InitializeComponent();
            this.group = group;
            this.DataContext = group;
            this.parent = uc;
            this.serviceClient = new CalanderServiceClient();
            list = serviceClient.GetEventsByGroup(group);
            ShowEvents();
            SetSelectionDates();
        }

        private void ShowEvents()
        {
            existingEvents.Children.Clear();
            TextBlock tblk = new TextBlock();
            tblk.FontSize = 15;
            tblk.FontWeight = FontWeights.Bold;
            tblk.Foreground = new SolidColorBrush(Colors.Black);
            tblk.Text = "Existing Events:";
            existingEvents.Children.Add(tblk);
            foreach (Event events in list)
            {
                TextBlock tblkEvents = new TextBlock();
                tblkEvents.Text = events.EventName + ":" + events.EventStart + "-" + events.EventEnd;
                tblkEvents.FontSize = 13;
                tblk.Foreground = new SolidColorBrush(Colors.Black);
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
            dtpEnd.BlackoutDates.Add(cdr1);
            dtpEnd.BlackoutDates.Add(cdr2);
            dtpEnd.DisplayDateStart = DateTime.Today.AddYears(-50);
        }

        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            Event events = new Event();
            events.EventGroup.Id = this.group.Id;
            events.EventName = tbxEventName.Text;
            events.EventStart = (DateTime)dtpStart.SelectedDate;
            events.EventEnd = (DateTime)dtpEnd.SelectedDate;
        }
    }
}
