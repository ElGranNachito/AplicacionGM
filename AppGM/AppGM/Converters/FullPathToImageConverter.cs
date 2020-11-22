using System;
using System.Globalization;
using System.Windows.Media.Imaging;

namespace AppGM
{
    public class FullPathToImageConverter : BaseConverter<FullPathToImageConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (String.IsNullOrEmpty((string) value))
                return null;

            return new BitmapImage(new Uri((string)value, UriKind.Absolute));
        }
    }
}