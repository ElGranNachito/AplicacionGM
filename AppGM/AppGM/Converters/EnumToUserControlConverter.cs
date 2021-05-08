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
                    switch ((EPagina)value)
                    {
                        case EPagina.PaginaPrincipal:
                            return new UserControlPaginaInicio();
                        case EPagina.PaginaPrincipalRol:
                            return new UserControlPaginaPrincipalRol();
                    }
                    break;

                case 2:
                    switch ((EMenuRol)value)
                    {
                        case EMenuRol.NINGUNO:
                            return null;
                        case EMenuRol.SeleccionTipoFichas:
                            return new UserControlMenuSeleccionTipoFicha();
                        case EMenuRol.VistaFichas:
                            return new UserControlListaFichasViewFichas();
                        case EMenuRol.Mapas:
                            return new UserControlMapa
                            {
                                ViewModel = SistemaPrincipal.ObtenerInstancia<ViewModelMapaPrincipal>()
                            };
                        case EMenuRol.AdministrarCombates:
                            return new UserControlMenuSeleccionCombate();
                        case EMenuRol.Combate:
                            return new UserControlCombate();
                    }
                    break;
            }
            

            return null;
        }
    }
}
