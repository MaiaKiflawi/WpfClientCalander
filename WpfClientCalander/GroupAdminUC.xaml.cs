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
    /// Interaction logic for GroupAdminUC.xaml
    /// </summary>
    public partial class GroupAdminUC : UserControl
    {
        Users user;
        public GroupAdminUC(Users user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void btnCreateGroup_Click(object sender, RoutedEventArgs e)
        {
            CreateGroupWindow createGroupWindow = new CreateGroupWindow(user);
            createGroupWindow.Show();

            //להוסיף מעבר ביוזר קונטרול ל-קריאט גרופ
        }
    }
}
