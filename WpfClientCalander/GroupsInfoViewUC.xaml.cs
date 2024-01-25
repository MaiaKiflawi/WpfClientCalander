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
    /// Interaction logic for GroupsInfoViewUC.xaml
    /// </summary>
    public partial class GroupsInfoViewUC : UserControl
    {
        Groups group;
        public GroupsInfoViewUC(Groups group)
        {
            InitializeComponent();
            this.group = group;
            this.DataContext = group;
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            try
            {
                bitmapImage.UriSource = new Uri("path://application:,,,/Images/imgGroups/"+group.GroupName+".png");
            }
            catch (Exception)
            {
                try
                {
                    bitmapImage.UriSource = new Uri("path://application:,,,/Images/imgGroups/" + group.GroupName + ".jpeg");
                }
                catch (Exception)
                {
                    bitmapImage.UriSource = new Uri("path://application:,,,/Images/projectLogo.jpg"); //error
                }
            }
            bitmapImage.EndInit();
            imgName.ImageSource = bitmapImage;
        }
    }
}
