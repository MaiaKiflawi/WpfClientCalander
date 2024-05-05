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
        public UserTblUC()
        {
            InitializeComponent();
            serviceClient = new CalanderServiceClient();
            UsersList userLst = serviceClient.GetAllUsers();
            usersListView.ItemsSource = userLst;
            SetStatus();
        }

        private void SetStatus()
        {
            CheckBox cbxManager = new CheckBox();
            cbxManager = cbxManager as CheckBox;
            if (cbxManager.IsChecked == true )
            {
                cbxManager.Background = Brushes.Maroon;
            }
            else
            {
                cbxManager.Background = Brushes.Gray;
            }
            CheckBox cbxGroupAdmin = new CheckBox();
            cbxGroupAdmin = cbxGroupAdmin as CheckBox;
            if (cbxGroupAdmin.IsChecked == true)
            {
                cbxGroupAdmin.Background = Brushes.Maroon;
            }
            else
            {
                cbxGroupAdmin.Background = Brushes.Gray;
            }
        }

        private void ChangedStatus()
        {

        }

        private void UserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Retrieve the user object associated with the TextBox
                Users user = textBox.DataContext as Users;

                if (user != null)
                {
                    // Call the service client to update the user
                    serviceClient.UpdateUser(user);
                }

            }
        }

        private void ManagerChanged(object sender, RoutedEventArgs e)
        {
            
        }

        private void GroupAdminChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
