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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WpfClientCalander
{
    /// <summary>
    /// Interaction logic for UserSettingsUC.xaml
    /// </summary>
    public partial class UserSettingsUC : UserControl
    {
        Grid grid;
        Users user;
        UserWindow parent;
        CalanderServiceClient serviceClient;
        public UserSettingsUC(Users user, UserWindow userWindow, ref Grid grid)
        {
            InitializeComponent();
            parent = userWindow;
            serviceClient = new CalanderServiceClient();
            this.user = user;
            this.DataContext = user;
            this.grid = grid;
            ShowStatus();
        }

        private void ShowStatus()
        {
            TextBlock txbManager = new TextBlock();
            txbManager.FontSize = 20;
            txbManager.Margin = new Thickness(0, 0, 10, 0);
            txbManager.FontWeight = FontWeights.Bold;
            txbManager.Foreground = new SolidColorBrush(Color.FromArgb(255, 0x80, 0x5d, 0x57));
            if (user.IsManager)
            {
                txbManager.Text = "App Manager.";
            }
            else
            {
                txbManager.Text = "Not App Manager.";
            }
            TextBlock txbGAdmin = new TextBlock();
            txbGAdmin.FontSize = 20;
            txbGAdmin.Margin = new Thickness(0, 0, 10, 0);
            txbGAdmin.FontWeight = FontWeights.Bold;
            txbGAdmin.Foreground = new SolidColorBrush(Color.FromArgb(255, 0x80, 0x5d, 0x57));
            if (user.IsGroupAdmin)
            {
                txbGAdmin.Text = "Group Admin.";
            }
            else
            {
                txbGAdmin.Text = "Not Group Admin.";
            }
            statusSP.Children.Add(txbManager);
            statusSP.Children.Add(txbGAdmin);
        }
        
        private void UserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Retrieve the user object associated with the TextBox
                Users user = textBox.DataContext as Users;

                if (user != null && user.UserName != string.Empty)
                {
                    if (serviceClient.IsUsernameFree(user.UserName) && serviceClient.IsUserNameID(user))
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

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void City_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PhoneNum_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Back()
        {
            grid.Children.Clear();
            grid.Children.Add(new UserProfileUC(user, parent, ref grid));
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Back();
        }
    }
}
