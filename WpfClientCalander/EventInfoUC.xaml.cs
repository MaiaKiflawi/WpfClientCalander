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
    /// Interaction logic for EventInfoUC.xaml
    /// </summary>
    public partial class EventInfoUC : UserControl
    {
        Event myEvent;
        CalanderServiceClient calanderService;
        UserWindow parent;
        UsersList usersList;
        Users user;
        public EventInfoUC(Event myEvent, Users user ,UserWindow userWindow)
        {
            InitializeComponent();
            this.myEvent = myEvent;
            this.DataContext = myEvent;
            this.parent = userWindow;
            this.user = user;
            calanderService = new CalanderServiceClient();
            this.usersList = calanderService.GetUsersByEvent(myEvent);
            ShowEventsUsers();
            attendEvent.IsChecked = usersList.Find(u=>u.Id==user.Id)!=null;
        }

        private void ShowEventsUsers()
        {
            participantsSP.Children.Clear();
            TextBlock tblkPar = new TextBlock();
            tblkPar.FontSize = 20;
            tblkPar.FontWeight = FontWeights.Bold;
            tblkPar.Foreground = new SolidColorBrush(Colors.Black);
            tblkPar.Text = "Attending participants:";
            participantsSP.Children.Add(tblkPar);
            foreach(Users user in usersList)
            {
                TextBlock tblkUser = new TextBlock();
                tblkUser.Text = user.UserName;
                tblkUser.FontSize = 15;
                tblkUser.Foreground = new SolidColorBrush(Colors.Black);
                participantsSP.Children.Add(tblkUser);
            }
        }

        private void attendEvent_Click(object sender, RoutedEventArgs e)
        {
            if (attendEvent.IsChecked == true)
            {
                int added = calanderService.InsertUserToEvent(user, myEvent);
                if (added == 1)
                {
                    usersList.Add(user);    
                    MessageBox.Show("Added successfully.", "YAY", MessageBoxButton.OK);
                    ShowEventsUsers();
                    return;
                }
            }
            else
            {
                int removed = calanderService.DeleteUserToEvent(user, myEvent);
                if (removed == 1)
                {
                    usersList.Remove(user);
                    MessageBox.Show("Removed successfully.", "YAY", MessageBoxButton.OK);
                    ShowEventsUsers();
                    return;
                }
            }
        }

        private void backToCalendar_Click(object sender, RoutedEventArgs e)
        {
            parent.Group_Click(myEvent.EventGroup);
        }
    }
}
