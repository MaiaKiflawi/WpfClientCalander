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
    /// Interaction logic for ChooseGroupsUC.xaml
    /// </summary>
    public partial class ChooseGroupsUC : UserControl
    {
        public ChooseGroupsUC(Users user)
        {
            InitializeComponent();
            CalanderServiceClient serviceClient = new CalanderServiceClient();
            GroupsList list = serviceClient.GetAllGroups();
            foreach (Groups group in list)
            {
                GroupsInfoViewUC uC = new GroupsInfoViewUC(group);
                uC.Margin = new Thickness(10);
                panel.Children.Add(uC);
            }
        }

        private void panel_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

    }
}
