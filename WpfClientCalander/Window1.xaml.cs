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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Groups group = new Groups();
        public Window1(ServiceCalander.Groups group)
        {
            InitializeComponent();
            this.group = group;
            this.DataContext = group;
            SetGridLines();
        }

        private void SetGridLines()
        {
            calGrid.ShowGridLines = false;
            calGrid.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x3C, 0x29, 0x29));
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    //calGrid. set border colour
                }

            }
        }
    }
}
