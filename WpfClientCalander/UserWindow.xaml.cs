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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfClientCalander.ServiceCalander;

namespace WpfClientCalander
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private Users user;

        public UserWindow(Users user)
        {
            InitializeComponent();
            this.user = user;
            if (user.IsManager)
            {
                rdbHome.Visibility = Visibility.Visible;
                rdbGroupAdmin.Visibility = Visibility.Visible;
                rdbManagerSettings.Visibility = Visibility.Visible;
                rdbMyProfile.Visibility = Visibility.Visible;
                
            }
            if (user.IsGroupAdmin)
            {
                rdbHome.Visibility = Visibility.Visible;
                rdbGroupAdmin.Visibility = Visibility.Visible;
                rdbManagerSettings.Visibility = Visibility.Collapsed;
                rdbMyProfile.Visibility = Visibility.Visible;

            }
            else
            {
                rdbHome.Visibility = Visibility.Visible;
                rdbGroupAdmin.Visibility = Visibility.Collapsed;
                rdbManagerSettings.Visibility = Visibility.Collapsed;
                rdbMyProfile.Visibility = Visibility.Visible;
            }
        }

        private void Themes_Click(object sender, RoutedEventArgs e)
        {
            //if (Themes.IsChecked == true)
            //    ThemesController.SetTheme(ThemesController.ThemeTypes.Dark);
            //else
            //    ThemesController.SetTheme(ThemesController.ThemeTypes.Light);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void rdbHome_Click(object sender, RoutedEventArgs e)
        {
            ucGrid.Children.Clear();
            ucGrid.Children.Add(new ChooseGroupsUC(user));
        }


        private void rdbGroupAdmin_Click(object sender, RoutedEventArgs e)
        {
            ucGrid.Children.Clear();
            ucGrid.Children.Add(new GroupAdminUC(user, ref ucGrid));
        }

        private void rdbManagerSettings_Click(object sender, RoutedEventArgs e)
        {
            ucGrid.Children.Clear();
            ucGrid.Children.Add(new AppManagerUC(user));
        }

        private void rdbMyProfile_Click(object sender, RoutedEventArgs e)
        {
            ucGrid.Children.Clear();
            ucGrid.Children.Add(new UserProfileUC(user));
        }
    }
}
