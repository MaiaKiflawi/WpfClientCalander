using MaterialDesignThemes.Wpf;
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
using System.Windows.Shapes;
using WpfClientCalander.ServiceCalander;

namespace WpfClientCalander
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private CalanderServiceClient serviceCal;

        public LoginWindow()
        {
            InitializeComponent();
            serviceCal = new CalanderServiceClient();
            
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Users user = new Users();
            user.UserName = tbxUsername.Text;
            user.Password = pbxPassword.Password;
            user = serviceCal.Login(user);
            if (user == null)
            {
                MessageBox.Show("User does not exist.\nGo to sign up", "ERROR", MessageBoxButton.OK);
                return;
            }
            if (user.IsGroupAdmin)
            {

            }
            else if (user.IsManager)
            {

            }
            else
            {
                UserWindow userWindow = new UserWindow();
                userWindow.ShowDialog();
            }
            tbxUsername.Text = pbxPassword.Password = string.Empty;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSignupPage_Click(object sender, RoutedEventArgs e)
        {
            SignupWindow signupWindow = new SignupWindow();
            this.Close();
            signupWindow.ShowDialog();
        }

        private void RevealPassword(object sender, MouseButtonEventArgs e)
        {
            tbxPassword.Text = pbxPassword.Password;
            PackIcon packIcon = sender as PackIcon;
            if (packIcon.Kind == PackIconKind.Eye) 
            {
                packIcon.Kind = PackIconKind.EyeOff;
                tbxPassword.Visibility = Visibility.Collapsed;
                pbxPassword.Visibility = Visibility.Visible;
            }
            else
            {
                packIcon.Kind = PackIconKind.Eye;
                tbxPassword.Visibility = Visibility.Visible;
                pbxPassword.Visibility = Visibility.Collapsed;
            }
        }
    }
}
