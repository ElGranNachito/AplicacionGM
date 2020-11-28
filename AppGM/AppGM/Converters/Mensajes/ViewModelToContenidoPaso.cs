using System;
using System.Globalization;
using AppGM.Core;

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

                case ViewModelMensajeCrearRol_DatosMapa crdt:
                    return new UserControlCreacionRol_DatosMapa
                    {
                        DataContext = crdt
                    };

                default:
                    return null;
            }
        }
    }
}
