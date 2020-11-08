using System;
using System.Globalization;
using AppGM.Core;

namespace AppGM
{
    public class ViewModelToContenidoConverter : BaseConverter<ViewModelToContenidoConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case ViewModelContenidoGloboInfoRol vm:
                    return new UserControlContenidoGloboInfoRol {DataContext = vm};

                case ViewModelInfoCombateGlobo vm:
                    return new UserControlInfoCombateGlobo {DataContext = vm};

                default:
                    return null;
            }
        }
    }
}
