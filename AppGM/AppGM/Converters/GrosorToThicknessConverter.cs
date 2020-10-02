using System;
using System.Windows;
using System.Globalization;
using AppGM.Core;

namespace AppGM
{
    public class GrosorToThicknessConverter : BaseConverter<GrosorToThicknessConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Grosor grosor = (Grosor)value;

            return new Thickness(grosor.Izquierdo, grosor.Superior, grosor.Derecho, grosor.Inferior);
        }
    }
}
