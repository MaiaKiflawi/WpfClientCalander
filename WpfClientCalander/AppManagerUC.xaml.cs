using System.Windows;
using System.Windows.Controls;
using WpfClientCalander.ServiceCalander;

namespace WpfClientCalander
{
    /// <summary>
    /// Interaction logic for AppManagerUC.xaml
    /// </summary>
    public partial class AppManagerUC : UserControl
    {
        Users user;
        private CalanderServiceClient serviceClient;
        public AppManagerUC(Users user)
        {
            InitializeComponent();
            this.user = user;
            tblAppManager.Text = $"App Manager - {user.UserName}, Page:";
            serviceClient = new CalanderServiceClient();
        }
        private void userTbls_Click(object sender, RoutedEventArgs e)
        {
            tblsGrid.Children.Clear();
            tblsGrid.Children.Add(new UserTblUC());
        }
        private void adminTbls_Click(object sender, RoutedEventArgs e)
        {
            tblsGrid.Children.Clear();
            tblsGrid.Children.Add(new AdminTblUC());
        }
        private void groupsTbls_Click(object sender, RoutedEventArgs e)
        {
            tblsGrid.Children.Clear();
            tblsGrid.Children.Add(new GroupsTblUC());
        }
        private void EventsTbls_Click(object sender, RoutedEventArgs e)
        {
            tblsGrid.Children.Clear();
            tblsGrid.Children.Add(new EventsTblUC());
        }
        private void UpdateCities_Click(object sender, RoutedEventArgs e)
        {
            serviceClient.UpdateCitiesFromExternalData();
            MessageBox.Show("Cities updated successfully.", "SUCCESS", MessageBoxButton.OK);
        }
    }
}
