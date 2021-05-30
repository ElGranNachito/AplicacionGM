using System;
using System.CodeDom;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AppGM
{
    /// <summary>
    /// Toma un <see cref="int"/> como parametro y devuelve una nueva instancia de una clase <see cref="Thickness"/> con valor uniforme en para todos los lados
    /// </summary>
    [ValueConversion(typeof(int), typeof(Thickness), ParameterType = typeof(int))]
    public class IntToThicknessConverter : BaseConverter<IntToThicknessConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(parameter == null)
				return new Thickness((int)value);

            int valorParametro = (int) parameter;
            int valor = (int) value;

            Thickness temp = new Thickness(0);

            if ((valorParametro & 1) != 0)
                temp.Left = valor;

            if ((valorParametro & 2) != 0)
                temp.Top = valor;

            if ((valorParametro & 4) != 0)
                temp.Right = valor;

            if ((valorParametro & 8) != 0)
                temp.Bottom = valor;

            return temp;
        }
    }
}
