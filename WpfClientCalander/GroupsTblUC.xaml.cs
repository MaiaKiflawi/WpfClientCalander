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
    /// Interaction logic for GroupsTblUC.xaml
    /// </summary>
    public partial class GroupsTblUC : UserControl
    {
        private CalanderServiceClient serviceClient;
        GroupsList groupLst;
        public GroupsTblUC()
        {
            InitializeComponent();
            serviceClient = new CalanderServiceClient();
            groupLst = serviceClient.GetAllGroups();
            foreach (Groups group in groupLst)
            {
                group.Users = serviceClient.GetUsersByGroup(group);
            }
            groupsListView.ItemsSource = groupLst;
            ShowUsersLst();
        }

        private void ShowUsersLst()
        {
            Expander expander = new Expander();
            StackPanel spParLst = new StackPanel();
            expander.Width = 250;
            expander.Header = "Participants:";
            foreach (Groups group in groupLst)
            {
                UsersList users = serviceClient.GetUsersByGroup(group);
                foreach (Users user in users)
                {
                    TextBlock tblkUser = new TextBlock();
                    tblkUser.Text = (user.UserName).ToString();
                    tblkUser.FontSize = 15;
                    tblkUser.Foreground = new SolidColorBrush(Colors.Black);
                    spParLst.Children.Add(tblkUser);
                }
                expander.Content = spParLst;
                DataTemplate dataTemplate = new DataTemplate();
                FrameworkElementFactory factory = new FrameworkElementFactory(typeof(Expander));
                factory.SetValue(Expander.HeaderProperty, expander.Header);
                factory.SetValue(Expander.WidthProperty, expander.Width);
                factory.SetValue(Expander.ContentProperty, spParLst);
                dataTemplate.VisualTree = factory;
                gridUsers.CellTemplate = dataTemplate;
            }
        }

        private void groupName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Retrieve the user object associated with the TextBox
                Groups group = textBox.DataContext as Groups;

                if (group != null && group.GroupName != string.Empty)
                {
                    if (serviceClient.IsGroupNameFree(group.GroupName))
                    {
                        // Call the service client to update the user
                        serviceClient.UpdateGroup(group);
                    }
                    else
                    {
                        MessageBox.Show("Group name taken. \n Try again.", "ERROR", MessageBoxButton.OK);
                    }
                }
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            foreach (Groups group in groupLst)
            {
                if (group.GroupName == gnameToDel.Text)
                {
                    serviceClient.DeleteGroup(group);
                    groupLst = serviceClient.GetAllGroups();
                    gnameToDel.Text = "";
                    groupsListView.ItemsSource = groupLst;
                    MessageBox.Show("Group deleted successfully.", "SUCCESS", MessageBoxButton.OK);
                    return;
                }
            }
            MessageBox.Show("Group name doesn't exist.", "ERROR", MessageBoxButton.OK);
            return;
        }

        //?
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            Groups group = serviceClient.GetGroupByGroupName(GroupChange.Text);
            group.GroupName = adminChange.Text;
            if (serviceClient.UpdateGroup(group) != 1)
            {
                MessageBox.Show("Group admin changed successfully.", "SUCCESS", MessageBoxButton.OK);
                return;
            }
            MessageBox.Show("Group admin couldn't update.", "ERROR", MessageBoxButton.OK);
            return;
        }
    }
}
