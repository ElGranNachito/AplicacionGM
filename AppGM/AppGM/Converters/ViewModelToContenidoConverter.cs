using System;
using System.Globalization;
using AppGM.Core;

namespace AppGM
{
    public class ViewModelToContenidoConverter : BaseConverter<ViewModelToContenidoConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new UserControlContenidoGloboInfoRol
            {
                DataContext = value ?? new ViewModelContenidoGloboInfoRol
                {
                    ModeloRol = new ModeloRol
                    {
                        Nombre = "Hola",
                        Descripcion = "Caca"
                    }
                }
            };
        }
    }
}
