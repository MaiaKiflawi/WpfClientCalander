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
            //calGrid.ShowGridLines = false;
            for (int i = 1; i <= 11; i++)
            {
                for (int j = 1; j <= 7; j++)
                {
                    if (i == 1)
                    {
                        Border myBorder = new Border();
                        myBorder.Height = 20;
                        myBorder.Width = 830 / 7;
                        myBorder.Background = new SolidColorBrush(Color.FromArgb(255, 0xc0, 0xb3, 0xc2));
                        myBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0x7e, 0x4c, 0x45));
                        myBorder.BorderThickness = new Thickness(1);
                        myBorder.Margin = new Thickness(.3);
                        calWP.Children.Add(myBorder);
                        //Grid.SetRowSpan(myBorder, i);
                        //Grid.SetColumnSpan(myBorder, j);
                    }
                    else if (i % 2 == 0)
                    {
                        Border myBorder = new Border();
                        myBorder.Height = 20;
                        myBorder.Width = 830 / 7;
                        myBorder.Background = new SolidColorBrush(Color.FromArgb(255, 0xf3, 0xe5, 0xf5));
                        myBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0x7e, 0x4c, 0x45));
                        myBorder.BorderThickness = new Thickness(1);
                        myBorder.Margin = new Thickness(.3);
                        calWP.Children.Add(myBorder);
                        //Grid.SetRowSpan(myBorder, i);
                        //Grid.SetColumnSpan(myBorder, j);
                    }
                    else
                    {
                        Border myBorder = new Border();
                        myBorder.Height = 60;
                        myBorder.Width = 830 / 7;
                        myBorder.Background = new SolidColorBrush(Color.FromArgb(255, 0xff, 0xf3, 0xe0));
                        myBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0x7e, 0x4c, 0x45));
                        myBorder.BorderThickness = new Thickness(1);
                        myBorder.Margin = new Thickness(.3);
                        calWP.Children.Add(myBorder);
                        //Grid.SetRowSpan(myBorder, i);
                        //Grid.SetColumnSpan(myBorder, j);
                    }
                    //if (i == 1)
                    //{
                    //    Border myBorder = new Border();
                    //    myBorder.Background = new SolidColorBrush(Color.FromArgb(255, 0xc0, 0xb3, 0xc2));
                    //    myBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0x7e, 0x4c, 0x45));
                    //    myBorder.BorderThickness = new Thickness(2);
                    //    calGrid.Children.Add(myBorder);
                    //    Grid.SetRowSpan(myBorder, i);
                    //    Grid.SetColumnSpan(myBorder, j);
                    //}
                    //else if (i%2 == 0)
                    //{
                    //    Border myBorder = new Border();
                    //    myBorder.Background = new SolidColorBrush(Color.FromArgb(255, 0xf3, 0xe5, 0xf5));
                    //    myBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0x7e, 0x4c, 0x45));
                    //    myBorder.BorderThickness = new Thickness(2);
                    //    calGrid.Children.Add(myBorder);
                    //    Grid.SetRowSpan(myBorder, i);
                    //    Grid.SetColumnSpan(myBorder, j);
                    //}
                    //else
                    //{
                    //    Border myBorder = new Border();
                    //    myBorder.Background = new SolidColorBrush(Color.FromArgb(255, 0xff, 0xf3, 0xe0));
                    //    myBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0x7e, 0x4c, 0x45));
                    //    myBorder.BorderThickness = new Thickness(2);
                    //    calGrid.Children.Add(myBorder);
                    //    Grid.SetRowSpan(myBorder, i);
                    //    Grid.SetColumnSpan(myBorder, j);
                    //}
                }

            }
        }
    }
}
