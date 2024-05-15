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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfClientCalander.ServiceCalander;
using Microsoft.Win32;
using System.Text.RegularExpressions;

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
        bool imgAdded;
        public List<string> ImageUris { get; set; } = new List<string>();

        public CreateGroupUC(Users user, ref Grid grid)
        {
            InitializeComponent();
            this.user = user;
            this.grid = grid;
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
                    // Read the image file as a byte array
                    byte[] imageData = File.ReadAllBytes(filePath);

                    // Create a BitmapImage and set its stream source
                    BitmapImage bitmapImage = new System.Windows.Media.Imaging.BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = new MemoryStream(imageData);
                    bitmapImage.EndInit();

                    // Set the Image control source to the created BitmapImage
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
                    // Read the image file as a byte array
                    byte[] imageData = File.ReadAllBytes(filePath);

                    // Create a BitmapImage and set its stream source
                    BitmapImage bitmapImage = new System.Windows.Media.Imaging.BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = new MemoryStream(imageData);
                    bitmapImage.EndInit();

                    // Set the Image control source to the created BitmapImage
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
                MessageBox.Show("Description too long.", "ERROR", MessageBoxButton.OK);
                return;
            }
            group.GroupAdmin = user;
            group.Id = client.InsertGroup(group);
            if (group.Id != 1)
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
            client.InsertUserToGroup(user, group);
            MessageBox.Show("Group created successfully!", "SUCCESS", MessageBoxButton.OK);
            Back();
        }

        private void Back()
        {
            grid.Children.Clear();
            grid.Children.Add(new GroupAdminUC(user, ref grid));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Back();
        }
    }
}
