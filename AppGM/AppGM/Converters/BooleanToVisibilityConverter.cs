

using System;
using System.Globalization;
using System.Windows;

namespace AppGM
{
    public class BooleanToVisibilityConverter : BaseConverter<BooleanToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool valor = (bool) value;

            if (parameter == null)
                return valor ? Visibility.Visible : Visibility.Hidden;

            return valor ? Visibility.Hidden : Visibility.Visible;
        }
    }
}
