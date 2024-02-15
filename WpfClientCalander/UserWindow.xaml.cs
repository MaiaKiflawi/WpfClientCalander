using System;
using System.Collections.Generic;
using System.IO;
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
        private GroupsList myGroups;
        private CalanderServiceClient serviceClient;
        public UserWindow(Users user)
        {
            InitializeComponent();
            this.user = user;
            serviceClient = new CalanderServiceClient();
            myGroups= serviceClient.GetGroupsByUser(user);
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

            GroupsView.Children.Clear();
            foreach (Groups group in myGroups) 
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation=Orientation.Horizontal;

                TextBlock textBlock = new TextBlock();
                textBlock.Text = group.GroupName;

                Ellipse ellipse = new Ellipse();
                ellipse.VerticalAlignment = VerticalAlignment.Center;
                ellipse.HorizontalAlignment = HorizontalAlignment.Center;
                ellipse.Height = ellipse.Width = 100;
                ellipse.Margin = new Thickness(5);
                ImageBrush imageBrush = GetGroupImage(group.GroupName);
                ellipse.Fill = imageBrush;

                stackPanel.Children.Add(ellipse);
                stackPanel.Children.Add(textBlock);
                stackPanel.Tag = group;
                stackPanel.MouseDown += StackPanel_MouseDown;
                GroupsView.Children.Add(stackPanel);
            }
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel stackPanel= (StackPanel)sender;
            Groups group=stackPanel.Tag as Groups;

        }

        private ImageBrush GetGroupImage(string groupName)
        {
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.Stretch = Stretch.UniformToFill;
            string uriStr = Environment.CurrentDirectory; //המיקום שבו רץ הפרויקט
            uriStr = uriStr.Substring(0, uriStr.IndexOf("\\bin"));
            uriStr = uriStr + @"\Images\imgGroups\";
            BitmapImage bitmapImage = new BitmapImage();
            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(uriStr);
            FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles(groupName + ".*");
            if (filesInDir.Length > 0)
            {
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(filesInDir[0].FullName);
                bitmapImage.EndInit();
                imageBrush.ImageSource = bitmapImage;
            }
            else
            {
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(@"pack://application:,,,/Images/projectLogo.jpg");
                bitmapImage.EndInit();
                imageBrush.ImageSource = bitmapImage;
            }
            return imageBrush;
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

        private void rdHome_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
