using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Convierte de un <see cref="ViewModel"/> a un <see cref="UserControl"/> para mostrar
    /// como contenido de un mensaje (popup)
    /// </summary>
    [ValueConversion(sourceType: typeof(ViewModel), targetType: typeof(UserControl))]
    public class ViewModelToContenidoMensajeConverter : BaseConverter<ViewModelToContenidoMensajeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ViewModel vm)
            {
                switch (vm)
                {
                    //Creacion de una unidad para un mapa
                    case ViewModelCrearUnidadMapa vcu:
                        return new UserControlMensajeCrearUnidadMapa
                        {
                            DataContext = vcu
                        };

                    //Creacion de un rol
                    case ViewModelCrearRol vmc:
                        return new UserControlMensajeConPasos
                        {
                            DataContext = vmc
                        };

                    //Creacion de un personaje
                    case ViewModelCrearPersonaje vmc:
                        return new UserControlCreacionPersonaje
                        {
                            DataContext = vmc
                        };

                    //Creacion de una habilidad
                    case ViewModelCrearHabilidad vmc:
                        return new UserControlCreacionHabilidad
                        {
                            DataContext = vmc
                        };
                }
            }

            return null;
        }
    }
}
