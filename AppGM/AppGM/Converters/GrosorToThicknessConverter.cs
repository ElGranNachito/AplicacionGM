using System;
using System.Windows;
using System.Globalization;
using System.Windows.Data;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Convierte un valor de <see cref="Grosor"/> a <see cref="Thickness"/>
    /// </summary>
    [ValueConversion(sourceType: typeof(Grosor), targetType: typeof(Thickness))]
    public class GrosorToThicknessConverter : BaseConverter<GrosorToThicknessConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Grosor grosor = (Grosor)value;

            return new Thickness(grosor.Izquierdo, grosor.Superior, grosor.Derecho, grosor.Inferior);
        }
    }
}