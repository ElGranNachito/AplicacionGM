using System;
using System.Globalization;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Convierte de un <see cref="ViewModel"/> a un <see cref="UserControl"/> para mostrar
    /// como contenido de un paso
    /// </summary>
    public class ViewModelToContenidoPaso : BaseConverter<ViewModelToContenidoPaso>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                //Introduccion de datos basicos de un rol
                case ViewModelCrearRol_DatosRol crdr:
                    return new UserControlCreacionRol_DatosRol
                    {
                        ViewModel = crdr
                    };

                //Seleccion de mapa de un rol
                case ViewModelCrearRol_DatosMapa crdt:
                    return new UserControlCreacionRol_DatosMapa
                    {
                        DataContext = crdt
                    };

                //Creacion de personajes de un rol
                case ViewModelDatosPersonajesRol crdp:
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
