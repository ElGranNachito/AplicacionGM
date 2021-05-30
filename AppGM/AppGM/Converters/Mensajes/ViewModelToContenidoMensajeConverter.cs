using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Convierte de un <see cref="BaseViewModel"/> a un <see cref="UserControl"/> para mostrar
    /// como contenido de un mensaje (popup)
    /// </summary>
    [ValueConversion(sourceType: typeof(BaseViewModel), targetType: typeof(UserControl))]
    public class ViewModelToContenidoMensajeConverter : BaseConverter<ViewModelToContenidoMensajeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is BaseViewModel vm)
            {
                switch (vm)
                {
                    //Creacion de una unidad para un mapa
                    case ViewModelMensajeCrearUnidadMapa vcu:
                        return new UserControlMensajeCrearUnidadMapa
                        {
                            DataContext = vcu
                        };

                    //Creacion de un rol
                    case ViewModelMensajeCrearRol vmc:
                        return new UserControlMensajeConPasos
                        {
                            DataContext = vmc
                        };

                    //Creacion de un personaje
                    case ViewModelMensajeCrearRol_CrearPersonaje vmc:
                        return new UserControlCreacionRol_CreacionPersonaje
                        {
                            DataContext = vmc
                        };

                    //Creacion de una habilidad
                    case ViewModelMensajeCrearRol_CrearHabilidad vmc:
                        return new UserControlCreacionRol_CreacionHabilidad
                        {
                            DataContext = vmc
                        };
                }
            }

            return null;
        }
    }
}
