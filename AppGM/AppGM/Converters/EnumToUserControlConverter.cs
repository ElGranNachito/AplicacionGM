using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Toma un enum y devuelve una nueva instancia de una pagina correspondiente al valor del enum dado
    /// Como segundo parametro toma un numero indicando el tipo de enum que fue pasado en el primer parametro
    /// </summary>
    [ValueConversion(sourceType: typeof(EPagina), targetType: typeof(UserControl), ParameterType = typeof(int))]
    public class EnumToUserControlConverter : BaseConverter<EnumToUserControlConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //El parametro no puede ser null
            if (parameter == null)
                throw new ArgumentNullException(" 'parameter' No puede ser null");

            switch (int.Parse(parameter.ToString()))
            {
                //Si el valor del parametro es 1 entonces el tipo de value tiene que ser de tipo EPagina
                case 1:
                    switch ((EPagina)value)
                    {
                        case EPagina.PaginaPrincipal:
                            return new UserControlPaginaInicio();
                        case EPagina.PaginaPrincipalRol:
                            return new UserControlPaginaPrincipalRol();
                    }
                    break;

                //Si el valor del parametro es 2 entonces el tipo de value tiene que ser de tipo EMenuRol
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
