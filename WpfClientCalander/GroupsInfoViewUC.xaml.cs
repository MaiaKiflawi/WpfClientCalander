using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using WpfClientCalander.ServiceCalander;

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
            //העלאת התמונה של הקבוצה מהמערך של בייטים
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
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                imgName.ImageSource = bitmapImage;
            }
            else
            {
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(@"pack://application:,,,/Images/projectLogo.jpg");
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
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
                int removed = serviceClient.DeleteUserFromGroup(user, group);
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
