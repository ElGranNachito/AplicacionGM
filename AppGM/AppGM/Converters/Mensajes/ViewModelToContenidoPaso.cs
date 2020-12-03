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
                        ViewModel = crdr
                    };

                case ViewModelMensajeCrearRol_DatosMapa crdt:
                    return new UserControlCreacionRol_DatosMapa
                    {
                        DataContext = crdt
                    };

                case ViewModelMensajeCrearRol_DatosPersonajes crdp:
                    return new UserControlCreacionRol_Personajes
                    {
                        DataContext = crdp
                    };

                default:
                    return null;
            }
        }
    }
}
