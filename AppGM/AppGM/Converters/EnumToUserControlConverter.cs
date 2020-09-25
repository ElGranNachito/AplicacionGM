using System;
using System.Globalization;
using AppGM.Core;

namespace AppGM
{
    public class EnumToUserControlConverter : BaseConverter<EnumToUserControlConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                throw new ArgumentNullException(" 'parameter' No puede ser null");

            switch (int.Parse(parameter.ToString()))
            {
                case 1:
                    switch ((EPaginaActual)value)
                    {
                        case EPaginaActual.PaginaPrincipal:
                            return new UserControlPaginaInicio();
                        case EPaginaActual.PaginaPrincipalRol:
                            return new UserControlPaginaPrincipalRol();
                    }
                    break;

                case 2:
                    switch ((EMenuActualRol)value)
                    {
                        case EMenuActualRol.NINGUNO:
                            return null;
                        case EMenuActualRol.SeleccionTipoFichas:
                            return new UserControlMenuSeleccionTipoFicha();
                        case EMenuActualRol.VistaFichas:
                            return new UserControlListaFichasViewFichas();
                    }
                    break;
            }
            

            return null;
        }
    }
}
