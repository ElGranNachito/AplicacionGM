using System;
using System.Globalization;
using System.Windows;

namespace AppGM
{
    /// <summary>
    /// Toma un <see cref="int"/> como parametro y devuelve una nueva instancia de una clase <see cref="Thickness"/> con valor uniforme en para todos los lados
    /// </summary>
    public class IntToThicknessConverter : BaseConverter<IntToThicknessConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Thickness((int)value);
        }
    }
}
