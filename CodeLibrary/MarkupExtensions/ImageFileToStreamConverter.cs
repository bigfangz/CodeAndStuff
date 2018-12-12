

namespace ZacksSampleCode.MarkupExtensions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using System.Windows.Media.Imaging;

    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class ImageFileToStreamConverter : IValueConverter
    {

        public static BitmapImage _convert(string Filename)
        {
            if (!File.Exists(Filename))
            {
                throw new FileNotFoundException(string.Format("Could not find image file {0}.", Filename));
            }
            BitmapImage image = new BitmapImage();
            using (FileStream stream = File.OpenRead(Filename))
            {
                image.BeginInit();
                image.StreamSource = stream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
            }
            return image;
        }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string path = value as string;
            if (path == null)
            {
                throw new FormatException(string.Format("Image File Name is not a string"));
            }
            return _convert(path);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
