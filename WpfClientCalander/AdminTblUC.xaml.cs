using System.Collections.Generic;
using System.Windows.Controls;
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
