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
    /// Interaction logic for AppManagerUC.xaml
    /// </summary>
    public partial class AppManagerUC : UserControl
    {
        Users user;
        public AppManagerUC(Users user)
        {
            InitializeComponent();
            this.user = user;
            tblAppManager.Text = $"App Manager - {user.UserName}, Page:";
        }

        private void userTbls_Click(object sender, RoutedEventArgs e)
        {
            //tblsGrid.Children.Clear();
            //tblsGrid.Children.Add(new UserTblUC());
        }

        private void adminTbls_Click(object sender, RoutedEventArgs e)
        {
            //tblsGrid.Children.Clear();
            //tblsGrid.Children.Add(new AdminTblUC());
        }

        private void groupsTbls_Click(object sender, RoutedEventArgs e)
        {
            //tblsGrid.Children.Clear();
            //tblsGrid.Children.Add(new GroupsTblUC());
        }

        private void allTbls_Click(object sender, RoutedEventArgs e)
        {
            //tblsGrid.Children.Clear();
            //tblsGrid.Children.Add(new AllTblUC());
        }
    }
}
