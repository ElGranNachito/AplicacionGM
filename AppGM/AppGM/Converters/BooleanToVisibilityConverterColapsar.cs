﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AppGM
{
    /// <summary>
    /// Convierte una valor booleano en un <see cref="Visibility"/> si el booleano es falso el valor de visibility sera Collapsed.
    /// Si el parametro de <see cref="Convert"/> no es null los valores de <see cref="Visibility"/> a los que corresponden los
    /// valores de <see cref="bool"/> se invierten.
    /// </summary>
    [ValueConversion(sourceType: typeof(bool), targetType: typeof(Visibility))]
    public class BooleanToVisibilityConverterColapsar : BaseConverter<BooleanToVisibilityConverterColapsar>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool valor = (bool)value;

            if (parameter == null)
                return valor ? Visibility.Visible : Visibility.Collapsed;

            return valor ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
