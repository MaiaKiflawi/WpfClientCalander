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
   public class MyAdmins
    {
        public Users Admin { get; set; }
        public List<Groups> Groups { get; set; }
    }
    /// <summary>
    /// Interaction logic for AdminTblUC.xaml
    /// </summary>
    public partial class AdminTblUC : UserControl
    {
        CalanderServiceClient serviceClient;
        UsersList usersLst;
        List<MyAdmins> adminsLst;
        public AdminTblUC()
        {
            InitializeComponent();
            serviceClient = new CalanderServiceClient();
            usersLst = serviceClient.GetAllUsers();
            adminsLst = new List<MyAdmins>();
            foreach (Users user in usersLst)
            {
                if (user.IsGroupAdmin) 
                {
                    MyAdmins my = new MyAdmins() { Admin = user };
                    my.Groups=serviceClient.GetAllGroups().FindAll(g=>g.GroupAdmin.Id==user.Id);
                    adminsLst.Add(my);
                }
            }
            adminsListView.ItemsSource = adminsLst;
        }
    }
}
