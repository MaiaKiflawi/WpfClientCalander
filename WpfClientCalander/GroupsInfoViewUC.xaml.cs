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
        ChooseGroupsUC parent;
        private CalanderServiceClient serviceClient;
        public GroupsInfoViewUC(Groups group, Users user, ChooseGroupsUC uc)
        {
            InitializeComponent();
            parent = uc;
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
            if (group.Users.Exists(u => u.Id == user.Id))
            {
                ChangeColour();
                tgbJoinGroup.IsChecked = true;
            }
        }

        internal void ChangeColour()
        {
            titleBgColour.Background = Brushes.AliceBlue;
            flipBgColour.Background = Brushes.AliceBlue;
        }

        internal void ChangeColourBack()
        {
            titleBgColour.Background = Brushes.AntiqueWhite;
            flipBgColour.Background = Brushes.AntiqueWhite;
        }

        private void tgbJoinGroup_Click(object sender, RoutedEventArgs e)
        {
            if (tgbJoinGroup.IsChecked == true)
            {
                int added = serviceClient.InsertUserToGroup(user, group);
                if (added == 1)
                {
                    parent.NewGroup(this);
                    MessageBox.Show("Added successfully.", "YAY", MessageBoxButton.OK);
                    btnFlip.Command.Execute(@"{ x: Static materialDesign:Flipper.FlipCommand}");
                    return;
                }
            }
            else
            {
                int removed = serviceClient.DeleteUserToGroup(user, group);
                if (removed == 1)
                {
                    parent.NewGroup(this);
                    ChangeColourBack();
                    MessageBox.Show("Removed successfully.", "YAY", MessageBoxButton.OK);
                    btnFlip.Command.Execute(@"{ x: Static materialDesign:Flipper.FlipCommand}");
                    return;
                }
            }            
        }
    }
}
