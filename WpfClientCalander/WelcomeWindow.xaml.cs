using System.Windows;

namespace WpfClientCalander
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            this.Close();
            loginWindow.ShowDialog();
        }

        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            SignupWindow signupWindow = new SignupWindow();
            this.Close();
            signupWindow.ShowDialog();
        }
    }
}
