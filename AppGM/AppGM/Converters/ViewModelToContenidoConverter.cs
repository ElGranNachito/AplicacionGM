using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Convierte un <see cref="ViewModel"/> a un <see cref="UserControl"/>
    /// </summary>
    [ValueConversion(sourceType: typeof(ViewModel), targetType: typeof(UserControl))]
    public class ViewModelToContenidoConverter : BaseConverter<ViewModelToContenidoConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                //Globo para mostrar informacion de un rol
                case ViewModelContenidoGloboInfoRol vm:
                    return new UserControlContenidoGloboInfoRol {DataContext = vm};

                //Globo para mostrar informacion de un combate
                case ViewModelInfoCombateGlobo vm:
                    return new UserControlInfoCombateGlobo {DataContext = vm};

                default:
                    return null;
            }
        }
    }
}
