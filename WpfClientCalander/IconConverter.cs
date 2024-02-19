using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace WpfClientCalander
{
    public class IconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return PackIconKind.Abc;
            switch (value)
            {
                case "Home":
                    return PackIconKind.Home;
                case "Group Admin Page":
                    return PackIconKind.TableAccount;
                case "Manager Settings":
                    return PackIconKind.AccountSupervisorCircle;
                case "My Profile":
                    return PackIconKind.HomeAccount;
                default:
                        break;
                }
            return PackIconKind.Abc;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
