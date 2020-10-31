using System;
using System.Globalization;
using AppGM.Core;

namespace AppGM
{
    public class ViewModelToContenidoMensajeConverter : BaseConverter<ViewModelToContenidoConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is BaseViewModel vm)
            {
                switch (vm)
                {
                    case ViewModelMensajeCrearUnidadMapa vcu:
                        return new UserControlMensajeCrearUnidadMapa
                        {
                            DataContext = vcu
                        };
                }
            }

            return null;
        }
    }
}
