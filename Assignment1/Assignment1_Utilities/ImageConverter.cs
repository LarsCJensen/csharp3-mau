using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Assignment1_Utilities
{
    /// <summary>
    /// Converter between string and BitmapImage
    /// </summary>
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(ImageSource))
            {
                try
                {
                    // Handle if value is string (always in our case)
                    if (value is string)
                    {
                        string str = (string)value;
                        return new BitmapImage(new Uri(str, UriKind.RelativeOrAbsolute));
                    }
                    // Handle if value is Uri
                    else if (value is Uri)
                    {
                        Uri uri = (Uri)value;
                        return new BitmapImage(uri);
                    }
                }
                catch (FileNotFoundException exc)
                {
                    // If file not found, fallback to standard null value
                    return null;
                }
                catch (NotSupportedException exc)
                {
                    // If not a compatible image
                    return null;
                }
                // Always return null upon exception
                catch (Exception exc)
                {
                    return null;
                }

            }
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
