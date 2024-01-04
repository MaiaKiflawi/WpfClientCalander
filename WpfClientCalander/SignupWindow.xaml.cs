using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
            this.DataContext = user;
            myService = new CalanderServiceClient();
            cmbCity.ItemsSource = myService.GetAllCities();
        }

        private void btnSignup_Click(object sender, RoutedEventArgs e) //signup
        {
            if (!CheckData())
            {

            }
            if (!myService.IsUsernameFree(tbxUsername.Text))
            {
                
            }
            //Check if username is free
            user.Gender = cmbGender.Text=="Female"? true: false;
            user.CityName = cmbCity.SelectedValue as City;
            user.BDate = DateTime.Parse(dtpBday.Text);
            user.Password = pbxPassword.Password;
            if(myService.InsertUser(user)!=1)
            {
                MessageBox.Show("", "", MessageBoxButton.OK);
                return;
            }
            MessageBox.Show("", "", MessageBoxButton.OK);
            this.Close();
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
            }
            else // not valid
            {
                user.Password = string.Empty;
                HintAssist.SetHelperText(pbxPassword, result.ErrorContent.ToString());

            }
        }

        private void pbxPasswordCheck_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pbxPassword.Password.Equals(pbxPasswordCheck.Password)) // valid
            {
                user.Password = pbxPassword.Password;
            }
            else // not valid
            {
                user.Password = string.Empty;
                HintAssist.SetHelperText(pbxPasswordCheck, "doesn't match password");
            }
        }

        private bool CheckData()
        {
            if (tbxFirstname.Text.Equals(string.Empty)) return false;
            if (tbxLastname.Text.Equals(string.Empty)) return false;
            if (cmbGender.SelectedIndex == -1) return false;
            if (cmbCity.SelectedIndex == -1) return false;
            //bday
            if (tbxPhone.Text.Equals(string.Empty)) return false;
            if (tbxUsername.Text.Equals(string.Empty)) return false;
            if (pbxPassword.Password.Equals(string.Empty)) return false;
            if (Validation.GetHasError(tbxFirstname)) return false;
            if (Validation.GetHasError(tbxLastname)) return false;
            if (Validation.GetHasError(tbxPhone)) return false;
            if (Validation.GetHasError(tbxUsername)) return false;
            return true;
        }
    }
}
