﻿using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfClientCalander.ServiceCalander;

namespace WpfClientCalander
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private CalanderServiceClient serviceCal;

        private Users user;
        public LoginWindow()
        {
            InitializeComponent();
            serviceCal = new CalanderServiceClient();
            user = new Users();
            this.DataContext = user;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e) //login
        {
            if (Validation.GetHasError(tbxUsername))
            {
                MessageBox.Show("Username invalid.", "ERROR", MessageBoxButton.OK);
                return;
            }
            if (Validation.GetHasError(tbxPassword))
            {
                MessageBox.Show("Password invalid.", "ERROR", MessageBoxButton.OK);
                return;
            }
            Users temp = serviceCal.Login(user);
            if (temp == null)
            {
                MessageBox.Show("User does not exist.\nGo to sign up.", "ERROR", MessageBoxButton.OK);
                return;
            }
            user = temp;
            tbxUsername.Text = pbxPassword.Password = string.Empty;
            UserWindow userWindow = new UserWindow(user);
            this.Close();
            userWindow.ShowDialog();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e) //clear
        {
            tbxPassword.Text = pbxPassword.Password = tbxPassword.Text = string.Empty;
        }

        private void btnSignupPage_Click(object sender, RoutedEventArgs e) //go to signup page
        {
            SignupWindow signupWindow = new SignupWindow();
            this.Close();
            signupWindow.ShowDialog();
        }

        private void RevealPassword(object sender, MouseButtonEventArgs e) //show or hide password
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

        private void pbxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            tbxPassword.Text = pbxPassword.Password;
        }

        private void tbxPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbxPassword.Visibility == Visibility.Visible)
                pbxPassword.Password = tbxPassword.Text;
        }

    }
}
