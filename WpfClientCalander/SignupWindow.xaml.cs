using MaterialDesignThemes.Wpf;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfClientCalander.ServiceCalander;

namespace WpfClientCalander
{
    /// <summary>
    /// Interaction logic for SignupWindow.xaml
    /// </summary>
    public partial class SignupWindow : Window
    {
        private Users user;
        private CalanderServiceClient myService;
        public SignupWindow()
        {
            InitializeComponent();
            user = new Users();
            user.BDate = DateTime.Today.AddYears(-20);
            this.DataContext = user;
            myService = new CalanderServiceClient();
            cmbCity.ItemsSource = myService.GetAllCities();
            cmbCity.DisplayMemberPath = "CityName";
        }

        private void btnSignup_Click(object sender, RoutedEventArgs e) //signup
        {
            if (!CheckData())
            {
                MessageBox.Show("Incorrect content.\n Try again.", "ERROR", MessageBoxButton.OK);
            }
            if (!myService.IsUsernameFree(tbxUsername.Text)) //Check if username is free
            {
                MessageBox.Show("ERROR", "username is used", MessageBoxButton.OK);
            }
            user.Gender = cmbGender.Text=="Female"? true: false;
            user.CityName = cmbCity.SelectedValue as City;
            user.BDate = DateTime.Parse(dtpBday.Text);
            user.Password = pbxPassword.Password;
            if(myService.InsertUser(user)!=1)
            {
                MessageBox.Show("Something went wrong with server.\n Try again later.", "ERROR", MessageBoxButton.OK);
                return;
            }
            MessageBox.Show("Signed up successfully!", "SUCCESS", MessageBoxButton.OK);
            user = myService.Login(user);
            UserWindow userWindow = new UserWindow(user);
            this.Close();
            userWindow.ShowDialog();
        }

        private void btnLoginPage_Click(object sender, RoutedEventArgs e) //go to login page
        {
            LoginWindow loginWindow = new LoginWindow();
            this.Close();
            loginWindow.ShowDialog();
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

        private void RevealPasswordCheck(object sender, MouseButtonEventArgs e) //show or hide confirm password
        {
            tbxPasswordCheck.Text = pbxPasswordCheck.Password;
            PackIcon packIcon = sender as PackIcon;
            if (packIcon.Kind == PackIconKind.Eye)
            {
                packIcon.Kind = PackIconKind.EyeOff;
                tbxPasswordCheck.Visibility = Visibility.Collapsed;
                pbxPasswordCheck.Visibility = Visibility.Visible;
            }
            else
            {
                packIcon.Kind = PackIconKind.Eye;
                tbxPasswordCheck.Visibility = Visibility.Visible;
                pbxPasswordCheck.Visibility = Visibility.Collapsed;
            }
        }

        private void pbxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ValidationPassword validation = new ValidationPassword();
            ValidationResult result = validation.Validate(pbxPassword.Password, CultureInfo.CurrentCulture);
            if (result.IsValid) // valid
            {
                user.Password = pbxPassword.Password;
                lbPasswordError.Content = null;
            }
            else // not valid
            {
                user.Password = string.Empty;
                lbPasswordError.Content= result.ErrorContent.ToString();

            }
        }

        private void pbxPasswordCheck_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pbxPassword.Password.Equals(pbxPasswordCheck.Password)) // valid
            {
                user.Password = pbxPassword.Password;
                lbPasswordCheckError.Content = null;
            }
            else // not valid
            {
                user.Password = string.Empty;
                lbPasswordCheckError.Content = "doesn't match password";
            }
        }

        private bool CheckData()
        {
            if (tbxFirstname.Text.Equals(string.Empty)) return false;
            if (tbxLastname.Text.Equals(string.Empty)) return false;
            if (cmbGender.SelectedIndex == -1) return false;
            if (cmbCity.SelectedIndex == -1) return false;
            if (tbxPhone.Text.Equals(string.Empty)) return false;
            if (tbxUsername.Text.Equals(string.Empty)) return false;
            if (pbxPassword.Password.Equals(string.Empty)) return false;
            if (Validation.GetHasError(tbxFirstname)) return false;
            if (Validation.GetHasError(dtpBday)) return false;
            if (Validation.GetHasError(tbxLastname)) return false;
            if (Validation.GetHasError(tbxPhone)) return false;
            if (Validation.GetHasError(tbxUsername)) return false;
            return true;
        }

        private void tbxPasswordCheck_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbxPasswordCheck.Visibility==Visibility.Visible)
                pbxPasswordCheck.Password= tbxPasswordCheck.Text;
        }

        private void tbxPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbxPassword.Visibility == Visibility.Visible)
                pbxPassword.Password = tbxPassword.Text;
        }
    }
}
