using System;
using System.Globalization;
using AppGM.Core;

namespace AppGM
{
    public class ViewModelToContenidoConverter : BaseConverter<ViewModelToContenidoConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value.GetType() == typeof(ViewModelContenidoGloboInfoRol))
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

            return new UserControlInfoCombateGlobo
            {
                DataContext = value
            };
        }
    }
}
