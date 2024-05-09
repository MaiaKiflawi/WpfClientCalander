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
using WpfClientCalander.ServiceCalander;

namespace WpfClientCalander
{
    /// <summary>
    /// Interaction logic for GroupsTblUC.xaml
    /// </summary>
    public partial class GroupsTblUC : UserControl
    {
        private CalanderServiceClient serviceClient;
        GroupsList groupLst;
        public GroupsTblUC()
        {
            InitializeComponent();
            serviceClient = new CalanderServiceClient();
            groupLst = serviceClient.GetAllGroups();
            groupsListView.ItemsSource = groupLst;
            ShowUsersLst();
        }

        //למה זה לא עובד?
        private void ShowUsersLst()
        {
            Expander expander = new Expander();
            expander.Width = 150;
            expander.Header = "SHOW";
            TextBlock tblkPar = new TextBlock();
            tblkPar.FontSize = 20;
            tblkPar.FontWeight = FontWeights.Bold;
            tblkPar.Foreground = new SolidColorBrush(Colors.Black);
            tblkPar.Text = "Participants:";
            expander.Content = tblkPar;
            StackPanel spParLst = new StackPanel();
            spParLst.Children.Clear();
            foreach (Groups group in groupLst)
            {
                UsersList users = serviceClient.GetUsersByGroup(group);
                foreach (Users user in users)
                {
                    TextBlock tblkUser = new TextBlock();
                    tblkUser.Text = (user.UserName).ToString();
                    tblkUser.FontSize = 15;
                    tblkUser.Foreground = new SolidColorBrush(Colors.Black);
                    spParLst.Children.Add(tblkUser);
                }
                expander.Content = spParLst;
                DataTemplate dataTemplate = new DataTemplate();
                FrameworkElementFactory factory = new FrameworkElementFactory(typeof(Expander));
                factory.SetValue(Expander.HeaderProperty, expander.Header);
                factory.SetValue(Expander.ContentProperty, expander.Content.ToString());
                factory.SetBinding(Expander.ContentProperty, new Binding(expander.ContentStringFormat));
                dataTemplate.VisualTree = factory;
                gridUsers.CellTemplate = dataTemplate;
            }
        }

        private void groupName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Retrieve the user object associated with the TextBox
                Groups group = textBox.DataContext as Groups;

                if (group != null && group.GroupName != string.Empty)
                {
                    if (serviceClient.IsGroupNameFree(group.GroupName))
                    {
                        // Call the service client to update the user
                        serviceClient.UpdateGroup(group);
                    }
                    else
                    {
                        MessageBox.Show("Group name taken. \n Try again.", "ERROR", MessageBoxButton.OK);
                    }
                }
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            foreach (Groups group in groupLst)
            {
                if (group.GroupName == gnameToDel.Text)
                {
                    serviceClient.DeleteGroup(group);
                    groupLst = serviceClient.GetAllGroups();
                    gnameToDel.Text = "";
                    groupsListView.ItemsSource = groupLst;
                    MessageBox.Show("Group deleted successfully.", "SUCCESS", MessageBoxButton.OK);
                    return;
                }
            }
            MessageBox.Show("Group name doesn't exist.", "ERROR", MessageBoxButton.OK);
            return;
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
