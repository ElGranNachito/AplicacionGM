using System;
using System.Globalization;

namespace AppGM
{
    public class ViewModelToContenidoPaso : BaseConverter<ViewModelToContenidoPaso>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case ViewModelMensajeCrearRol_DatosRol crdr:
                    return new UserControlCreacionRol_DatosRol
                    {
                        DataContext = crdr
                    };
             
                default:
                    return null;
            }
        }
    }
}
