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
        CalanderServiceClient serviceClient;
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

        private void btnDel_Click(object sender, RoutedEventArgs e) //delete group
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

        private void btnChange_Click(object sender, RoutedEventArgs e) //change group admin
        {
            Groups group = serviceClient.GetGroupByGroupName(GroupChange.Text);
            UsersList users = serviceClient.GetAllUsers();
            Users admin = group.GroupAdmin;
            foreach (Users user in users)
            {
                if (user.UserName == adminChange.Text)
                {
                    admin = user;
                }
            }
            if (admin == null)
            {
                MessageBox.Show("User doesn't exist.", "ERROR", MessageBoxButton.OK);
                return;
            }
            admin.IsGroupAdmin = true;
            serviceClient.UpdateUser(admin);
            group.GroupAdmin = admin;
            serviceClient.InsertUserToGroup(admin, group);
            if (serviceClient.UpdateGroup(group) != 0)
            {
                MessageBox.Show("Group admin changed successfully.", "SUCCESS", MessageBoxButton.OK);
                groupsListView.ItemsSource = groupLst;
                return;
            }
            MessageBox.Show("Group admin couldn't update.", "ERROR", MessageBoxButton.OK);
            return;
        }
    }
}
