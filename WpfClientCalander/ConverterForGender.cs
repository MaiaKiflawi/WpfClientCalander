using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfClientCalander
{
    internal class ConverterForGender : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "True":
                case "true":
                    return "Female";
                case "False":
                case "false":
                    return "Male";
            }
            return "Empty";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
