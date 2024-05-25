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
using System.Windows.Resources;
using System.Windows.Shapes;
using WpfClientCalander.ServiceCalander;

namespace WpfClientCalander
{
    public class EventUsers
    {
        public Event Event { get; set; }
        public List<Users> User { get; set; }
    }
    /// <summary>
    /// Interaction logic for EventsTblUC.xaml
    /// </summary>
    public partial class EventsTblUC : UserControl
    {
        CalanderServiceClient serviceClient;
        EventList eventLst;
        List<EventUsers> eUsersLst;
        public EventsTblUC()
        {
            InitializeComponent();
            serviceClient = new CalanderServiceClient();
            eventLst = serviceClient.GetAllEvents();
            eUsersLst = new List<EventUsers>();
            foreach (Event e in eventLst)
            {
                EventUsers lst = new EventUsers() { Event = e };
                lst.User = serviceClient.GetUsersByEvent(e);
                eUsersLst.Add(lst);
            }
            eventsListView.ItemsSource = eUsersLst;
            
        }

        private void eventName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Retrieve the user object associated with the TextBox
                Event events = textBox.DataContext as Event;

                if (events != null && events.EventName != string.Empty)
                {
                    if (serviceClient.IsEventNameFree(events.EventName))
                    {
                        // Call the service client to update the user
                        serviceClient.UpdateEvent(events);
                    }
                    else
                    {
                        MessageBox.Show("Event name taken. \n Try again.", "ERROR", MessageBoxButton.OK);
                    }
                }
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            foreach (EventUsers events in eUsersLst)
            {
                if (events.Event.EventName == eventToDel.Text)
                {
                    serviceClient.DeleteEvent(events.Event);
                    eventToDel.Text = "";
                    eventLst = serviceClient.GetAllEvents();
                    eUsersLst = new List<EventUsers>();
                    foreach (Event myEvent in eventLst)
                    {
                        EventUsers lst = new EventUsers() { Event = myEvent };
                        lst.User = serviceClient.GetUsersByEvent(myEvent);
                        eUsersLst.Add(lst);
                    }
                    eventsListView.ItemsSource = eUsersLst;
                    MessageBox.Show("User deleted successfully.", "SUCCESS", MessageBoxButton.OK);
                    return;
                }
            }
            MessageBox.Show("Username doesn't exist.", "ERROR", MessageBoxButton.OK);
            return;
        }
    }
}
