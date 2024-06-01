using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace WpfClientCalander
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.Stretch = Stretch.UniformToFill;
            string uriStr = Environment.CurrentDirectory; //המיקום שבו רץ הפרויקט
            uriStr = uriStr.Substring(0, uriStr.IndexOf("\\bin"));
            uriStr = uriStr + @"\Images\imgGroups\";
            BitmapImage bitmapImage = new BitmapImage();
            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(uriStr);
            FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles(value.ToString() + ".*");
            if (filesInDir.Length > 0)
            {
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(filesInDir[0].FullName);
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                imageBrush.ImageSource = bitmapImage;
            }
            else
            {
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(@"pack://application:,,,/Images/projectLogo.jpg");
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                imageBrush.ImageSource = bitmapImage;
            }
            return imageBrush.ImageSource;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
