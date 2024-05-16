using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfClientCalander.ServiceCalander;

namespace WpfClientCalander
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private Users user;
        private GroupsList myGroups;
        private CalanderServiceClient serviceClient;
        public bool isProfile = false;
        public UserWindow(Users user)
        {
            InitializeComponent();
            this.user = user;
            welcomeUser.Text = $"Welcome {user.FirstName}!";
            welcomeUser.FontSize = 20;
            welcomeUser.Foreground = new SolidColorBrush(Color.FromArgb(255, 0x7e, 0x4c, 0x45));
            serviceClient = new CalanderServiceClient();
            rdbHome.IsChecked = true;
            rdbHome_Click(null, null);
            if (user.IsManager)
            {
                rdbHome.Visibility = Visibility.Visible;
                rdbGroupAdmin.Visibility = Visibility.Visible;
                rdbManagerSettings.Visibility = Visibility.Visible;
                rdbMyProfile.Visibility = Visibility.Visible;
            }
            else if (user.IsGroupAdmin)
            {
                rdbHome.Visibility = Visibility.Visible;
                rdbGroupAdmin.Visibility = Visibility.Visible;
                rdbManagerSettings.Visibility = Visibility.Collapsed;
                rdbMyProfile.Visibility = Visibility.Visible;
            }
            else
            {
                rdbHome.Visibility = Visibility.Visible;
                rdbGroupAdmin.Visibility = Visibility.Collapsed;
                rdbManagerSettings.Visibility = Visibility.Collapsed;
                rdbMyProfile.Visibility = Visibility.Visible;
            }
            LoadMyGroups();
        }

        internal void LoadMyGroups() 
        { 
            GroupsView.Children.Clear();
            myGroups = serviceClient.GetGroupsByUser(user);
            foreach (Groups group in myGroups) 
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation=Orientation.Horizontal;
                stackPanel.Margin = new Thickness(0, 1, 0, 1);

                RadioButton grpRdb = new RadioButton();
                grpRdb.Content = group.GroupName;
                grpRdb.Style = (Style)FindResource("MenuGroupRadioButtonStyle");
                grpRdb.Click += Group_Click;
                grpRdb.Tag = group;
                stackPanel.Children.Add(grpRdb);
                GroupsView.Children.Add(stackPanel);
            }
        }

        internal void Group_Click(object sender, RoutedEventArgs e)
        {
            isProfile = false;
            rdbHome.IsChecked = rdbGroupAdmin.IsChecked = rdbManagerSettings.IsChecked = rdbMyProfile.IsChecked = false;
            foreach (StackPanel sp in GroupsView.Children)
                (sp.Children[0] as RadioButton).IsChecked = false;
            (sender as RadioButton).IsChecked=true;
            // StackPanel stackPanel = (StackPanel)sender;
            Groups group=(sender as RadioButton).Tag as Groups;
            GroupsWindowsUC uc = new GroupsWindowsUC(group,this);
            ucGrid.Children.Clear();
            ucGrid.Children.Add(uc);
        }

        internal void Group_Click(Groups group, bool fromGroup)
        {
            foreach (StackPanel sp in GroupsView.Children)
            {
                if (((sp.Children[0] as RadioButton).Tag as Groups).Id == group.Id)
                {
                    if (fromGroup)
                    {
                        Group_Click((sp.Children[0] as RadioButton), null);
                    }
                    else
                    {
                        rdbMyProfile_Click(null, null);
                    }
                }
            }    
                        
        }

        private void ClearGroupSelected()
        {
            foreach (StackPanel sp in GroupsView.Children)
                (sp.Children[0] as RadioButton).IsChecked = false;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void rdbHome_Click(object sender, RoutedEventArgs e)
        {
            ClearGroupSelected();
            ucGrid.Children.Clear();
            ucGrid.Children.Add(new ChooseGroupsUC(user, this));
        }


        private void rdbGroupAdmin_Click(object sender, RoutedEventArgs e)
        {
            ClearGroupSelected();
            ucGrid.Children.Clear();
            ucGrid.Children.Add(new GroupAdminUC(user, ref ucGrid));
        }

        private void rdbManagerSettings_Click(object sender, RoutedEventArgs e)
        {
            ClearGroupSelected();
            ucGrid.Children.Clear();
            ucGrid.Children.Add(new AppManagerUC(user));
        }

        private void rdbMyProfile_Click(object sender, RoutedEventArgs e)
        {
            isProfile = true;
            ClearGroupSelected();
            ucGrid.Children.Clear();
            ucGrid.Children.Add(new UserProfileUC(user, this, ref ucGrid));
        }

        internal void LoadEvent(Event myEvent)
        {
            ucGrid.Children.Clear();
            ucGrid.Children.Add(new EventInfoUC(myEvent, user, this));
        }
    }
}
