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
	            case ViewModelPaginaInicio vm:
		            return new UserControlPaginaInicio {DataContext = vm};

	            case ViewModelRol vm:
	                return new UserControlPaginaPrincipalRol {DataContext = vm};

                case ViewModelCrearRol vm:
	                return new UserControlMensajeConPasos {DataContext = vm};

                case ViewModelCrearPersonaje vm:
	                return new UserControlCreacionPersonaje {DataContext = vm};

                case ViewModelCrearHabilidad vm:
	                return new UserControlCreacionHabilidad {DataContext = vm};

	            case ViewModelCreacionDeFuncionBase vm:
		            return new UserControlCreacionFuncion { DataContext = vm };

                case ViewModelCreacionDeVariable vm:
	                return new UserControlCreacionVariable {DataContext = vm};

                case ViewModelSeleccionDeControlador vm:
	                return new UserControlSeleccionDeControlador {DataContext = vm};

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
