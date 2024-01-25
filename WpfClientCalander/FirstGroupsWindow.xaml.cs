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
using System.Windows.Shapes;
using WpfClientCalander.ServiceCalander;

namespace WpfClientCalander
{
    /// <summary>
    /// Interaction logic for FirstGroupsWindow.xaml
    /// </summary>
    public partial class FirstGroupsWindow : Window
    {
        public FirstGroupsWindow(Users user)
        {
            InitializeComponent();
            CalanderServiceClient serviceClient = new CalanderServiceClient();
            GroupsList list= serviceClient.GetAllGroups();
            foreach (Groups group in list)
            {
                GroupsInfoViewUC uC = new GroupsInfoViewUC(group);
                uC.Margin = new Thickness(3);
                panel.Children.Add(uC); 
            }
        }
    }
}
