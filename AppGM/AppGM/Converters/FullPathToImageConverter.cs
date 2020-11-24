using System;
using System.Globalization;
using System.Windows.Media.Imaging;

namespace AppGM
{
    public class FullPathToImageConverter : BaseConverter<FullPathToImageConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string tmp = (string) value;

            if (String.IsNullOrEmpty(tmp))
                return null;

            return new BitmapImage(new Uri(tmp, UriKind.Absolute));
        }
    }
}