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
    /// Interaction logic for GroupsWindowsUC.xaml
    /// </summary>
    public partial class GroupsWindowsUC : UserControl
    {
        Groups group = new Groups();
        public GroupsWindowsUC(ServiceCalander.Groups group)
        {
            InitializeComponent();
            this.group = group;
            this.DataContext = group;
        }
    }
}
