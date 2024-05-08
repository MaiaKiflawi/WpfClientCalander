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
    /// Interaction logic for UserTblUC.xaml
    /// </summary>
    public partial class UserTblUC : UserControl
    {
        private CalanderServiceClient serviceClient;
        UsersList userLst;
        public UserTblUC()
        {
            InitializeComponent();
            serviceClient = new CalanderServiceClient();
            userLst = serviceClient.GetAllUsers();
            usersListView.ItemsSource = userLst;
        }

        private void UserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Retrieve the user object associated with the TextBox
                Users user = textBox.DataContext as Users;

                if (user != null && user.UserName != string.Empty)
                {
                    if (serviceClient.IsUsernameFree(user.UserName))
                    {
                        // Call the service client to update the user
                        serviceClient.UpdateUser(user);
                    }
                    else
                    {
                        MessageBox.Show("Username taken. \n Try again.", "ERROR", MessageBoxButton.OK);
                    }
                }

            }
        }

        private void ManagerChanged(object sender, RoutedEventArgs e)
        {

            CheckBox cbMan = (sender as CheckBox);
            Users user = (sender as CheckBox).Tag as Users;
            user.IsManager =(bool) cbMan.IsChecked;
            serviceClient.UpdateUser(user);
            if (cbMan.IsChecked == true)
            {
                cbMan.Background = Brushes.Maroon;
                user.IsManager = true;
            }
            else
            {
                cbMan.Background = Brushes.Gray;
                user.IsManager = false;
                serviceClient.UpdateUser(user);
            }
        }

        private void GroupAdminChanged(object sender, RoutedEventArgs e)
        {
            CheckBox cbGAdm = (sender as CheckBox);
            Users user = (sender as CheckBox).Tag as Users;
            user.IsGroupAdmin = (bool) cbGAdm.IsChecked;
            serviceClient.UpdateUser(user);
            if (cbGAdm.IsChecked == true)
            {
                cbGAdm.Background = Brushes.Maroon;
            }
            else
            {
                cbGAdm.Background = Brushes.Gray;
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            foreach(Users user in userLst)
            {
                if (user.UserName == unameToDel.Text)
                {
                    serviceClient.DeleteUser(user);
                    userLst = serviceClient.GetAllUsers();
                    unameToDel.Text = "";
                    usersListView.ItemsSource = userLst;
                    MessageBox.Show("User deleted successfully.", "SUCCESS", MessageBoxButton.OK);
                    return;
                }
            }
            MessageBox.Show("Username doesn't exist.", "ERROR", MessageBoxButton.OK);
            return;
        }
    }
}
