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
        GroupsList groupLst;
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
                if (user.IsGroupAdmin) {
                    MyAdmins my = new MyAdmins() { Admin = user };
                    my.Groups=serviceClient.GetAllGroups().FindAll(g=>g.GroupAdmin.Id==user.Id);
                    adminsLst.Add(my);
                }
            }
            adminsListView.ItemsSource = adminsLst;
            //ShowGroups();
        }

        //private void ShowGroups()
        //{
        //    Expander expander = new Expander();
        //    StackPanel spGroupLst = new StackPanel();
        //    expander.Width = 250;
        //    expander.Header = "Groups:";
        //    foreach (Groups group in groupLst)
        //    {
        //        GroupsList groups = serviceClient.GetGroupsByUser(group.GroupAdmin);
        //        foreach (Groups adminGroup in groups)
        //        {
        //            TextBlock tblkUser = new TextBlock();
        //            tblkUser.Text = (adminGroup.GroupAdmin.UserName).ToString();
        //            tblkUser.FontSize = 15;
        //            tblkUser.Foreground = new SolidColorBrush(Colors.Black);
        //            spGroupLst.Children.Add(tblkUser);
        //        }
        //        expander.Content = spGroupLst;
        //        DataTemplate dataTemplate = new DataTemplate();
        //        FrameworkElementFactory factory = new FrameworkElementFactory(typeof(Expander));
        //        factory.SetValue(Expander.HeaderProperty, expander.Header);
        //        factory.SetValue(Expander.WidthProperty, expander.Width);
        //        factory.SetValue(Expander.ContentProperty, spGroupLst);
        //        dataTemplate.VisualTree = factory;
        //        gridGroups.CellTemplate = dataTemplate;
        //    }
        //}
        
    }
}
