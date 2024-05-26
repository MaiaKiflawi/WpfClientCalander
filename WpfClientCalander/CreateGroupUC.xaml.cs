using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfClientCalander.ServiceCalander;
using Microsoft.Win32;

namespace WpfClientCalander
{
    /// <summary>
    /// Interaction logic for CreateGroupUC.xaml
    /// </summary>
    public partial class CreateGroupUC : UserControl
    {
        Users user;
        Groups group;
        string filePath;
        CalanderServiceClient client;
        private Grid grid;
        UserWindow parent;
        bool imgAdded;
        public List<string> ImageUris { get; set; } = new List<string>();

        public CreateGroupUC(Users user, ref Grid grid, UserWindow parent)
        {
            InitializeComponent();
            this.user = user;
            this.grid = grid;
            this.parent = parent;
            btnOpenFile.Visibility = Visibility.Visible;
            btnChangeFile.Visibility = Visibility.Hidden;
            group = new Groups();
            this.DataContext = group;
            imgAdded = false;
            client = new CalanderServiceClient();
        }
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileDialog.Filter = "Image files (*.png;*.jpeg;)|*.png;*.jpeg;|All files (*.*)|*.*";
                try
                {
                    filePath = openFileDialog.FileName;
                    byte[] imageData = File.ReadAllBytes(filePath);
                    BitmapImage bitmapImage = new System.Windows.Media.Imaging.BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = new MemoryStream(imageData);
                    bitmapImage.EndInit();
                    imgDisplay.Source = bitmapImage;

                    btnOpenFile.Visibility = Visibility.Hidden;
                    btnChangeFile.Visibility = Visibility.Visible;
                    imgAdded = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void btnChangeFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileDialog.Filter = "Image files (*.png;*.jpeg;)|*.png;*.jpeg;|All files (*.*)|*.*";
                try
                {
                    filePath = openFileDialog.FileName;
                    byte[] imageData = File.ReadAllBytes(filePath);
                    BitmapImage bitmapImage = new System.Windows.Media.Imaging.BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = new MemoryStream(imageData);
                    bitmapImage.EndInit();
                    imgDisplay.Source = bitmapImage;

                    btnOpenFile.Visibility = Visibility.Hidden;
                    btnChangeFile.Visibility = Visibility.Visible;
                    imgAdded = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void btnCreateGroup_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(tbxGroupName) == true)
            {
                MessageBox.Show("Group name invalid.", "ERROR", MessageBoxButton.OK);
                return;
            }
            if (tbxGroupDes.Text.Length > 300)
            {
                MessageBox.Show("Description too long.", "ERROR", MessageBoxButton.OK);
                return;
            }
            if (!client.IsGroupNameFree(tbxGroupName.Text))
            {
                MessageBox.Show("Group name already exists.\n Try again.", "ERROR", MessageBoxButton.OK);
                return;
            }
            group.GroupAdmin = user;
            if (client.InsertGroup(group) != 1)
            {
                MessageBox.Show("System error.\n Try again.", "ERROR", MessageBoxButton.OK);
                return;
            }
            if (imgAdded)
            {
                FileInfo fileInfo = new FileInfo(filePath);
                string strUri = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf(@"\bin")) 
                    + @"/Images/imgGroups/" + group.GroupName + filePath.Substring(filePath.LastIndexOf("."));
                fileInfo.MoveTo(strUri);
            }
            Groups groupTemp = client.GetGroupByGroupName(tbxGroupName.Text);
            client.InsertUserToGroup(user, groupTemp);
            MessageBox.Show("Group created successfully!", "SUCCESS", MessageBoxButton.OK);
            parent.LoadMyGroups();
            Back();
        }
        private void Back()
        {
            grid.Children.Clear();
            grid.Children.Add(new GroupAdminUC(user, ref grid, parent));
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Back();
        }
    }
}
