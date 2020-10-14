using System;
using System.Globalization;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Toma un enum y devuelve una nueva instancia de una pagina correspondiente al valor del enum dado
    /// Como segundo parametro toma un numero indicando el tipo de enum que fue pasado en el primer parametro
    /// </summary>
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
                        case EMenuActualRol.Mapas:
                            return new UserControlMapa
                            {
                                ViewModel = SistemaPrincipal.ObtenerInstancia<ViewModelMapaPrincipal>()
                            };
                        case EMenuActualRol.AdministrarCombates:
                            return new UserControlMenuSeleccionCombate();
                        case EMenuActualRol.Combate:
                            return new UserControlCombate();
                    }
                    break;
            }
            

            return null;
        }
    }
}
