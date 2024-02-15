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
using System.IO;
using WpfClientCalander.ServiceCalander;
using System.Configuration;

namespace WpfClientCalander
{
    /// <summary>
    /// Interaction logic for GroupsInfoViewUC.xaml
    /// </summary>
    public partial class GroupsInfoViewUC : UserControl
    {
        Groups group;
        Users user;
        private CalanderServiceClient serviceClient;
        public GroupsInfoViewUC(Groups group, Users user)
        {
            InitializeComponent();
            this.group = group;
            this.DataContext = group;
            this.user = user;
            serviceClient = new CalanderServiceClient();
            string encodedGroupName = group.GroupName;
            string uriStr = Environment.CurrentDirectory; //המיקום שבו רץ הפרויקט
            uriStr = uriStr.Substring(0, uriStr.IndexOf("\\bin"));
            uriStr = uriStr + @"\Images\imgGroups\";
            BitmapImage bitmapImage = new BitmapImage(); 
            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(uriStr);
            FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles(encodedGroupName+".*");
            if (filesInDir.Length > 0)
            {
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(filesInDir[0].FullName);
                bitmapImage.EndInit();
                imgName.ImageSource = bitmapImage;
            }
            else
            {
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(@"pack://application:,,,/Images/projectLogo.jpg");
                bitmapImage.EndInit();
                imgName.ImageSource = bitmapImage;
            }
        }

        private void tgbJoinGroup_Click(object sender, RoutedEventArgs e)
        {
            tgbJoinGroup.IsChecked = true;
            int worked = serviceClient.InsertUserToGroup(user,group);
            if (worked == 1)
            {
                MessageBox.Show("Added successfully.", "YAY", MessageBoxButton.OK);
                return;
            }
        }
    }
}
