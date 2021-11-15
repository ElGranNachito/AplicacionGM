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
                throw new ArgumentNullException($"{nameof(parameter)} No puede ser null");

            switch (int.Parse(parameter.ToString()))
            {
                //Si el valor del parametro es 1 entonces el tipo de value tiene que ser de tipo PaginaActual
                case 1:
                    switch ((EPagina)value)
                    {
                        case EPagina.PaginaPrincipal:
                            return new UserControlPaginaInicio();

                            ModeloFuncion mf = new ModeloFuncion {Id = 0, NombreFuncion = "FuncionDeMierdaa"};

                            /*var controlador = new ControladorFuncion_Habilidad(mf);
                            var vm = new ViewModelCreacionDeFuncionHabilidad(, controlador);

                            vm.CargarBloquesFuncion();

                            return new UserControlCreacionFuncion
							{
								DataContext = vm
							};*/
						case EPagina.PaginaPrincipalRol:
                            return new UserControlPaginaPrincipalRol();

                        case EPagina.CreacionDeRol:
                        {
	                        if (SistemaPrincipal.Aplicacion.VentanaActual.DataContext is ViewModelCrearRol v)
		                        return new UserControlMensajeConPasos { DataContext = v };

	                        return new UserControlMensajeConPasos {DataContext = new ViewModelCrearRol()};
                        }

                        case EPagina.CreacionDePersonaje:
                        {
	                        if (SistemaPrincipal.Aplicacion.VentanaActual.DataContext is ViewModelCreacionEdicionPersonaje v)
		                        return new UserControlCreacionPersonaje {DataContext = v};

	                        var nuevoVmCreacionPj = new ViewModelCreacionEdicionPersonaje(
		                        vm =>
		                        {
			                        SistemaPrincipal.Aplicacion.PaginaActual =
				                        SistemaPrincipal.Aplicacion.PaginaAnterior;
		                        });

                            nuevoVmCreacionPj.Inicializar().RunSynchronously();

                            return new UserControlCreacionPersonaje {DataContext = nuevoVmCreacionPj};
                        }
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

                //Si el valor del parametro es 3 entonces el tipo de value tiene que ser de tipo ESeccionMapa
                case 3:
                    switch ((ESeccionMapa)value)
                    {
                        case ESeccionMapa.MapaPrincipal:
                            return new UserControlMapa
                            {
                                ViewModel = SistemaPrincipal.ObtenerInstancia<ViewModelMapaPrincipal>()
                            };
                        case ESeccionMapa.OpcionesMapa:
                            return new UserControlOpcionesMapa()
                            {
                                DataContext = SistemaPrincipal.ObtenerInstancia<ViewModelMapaPrincipal>()
                            };
                    }
                    break;
            }
            
            return null;
        }
    }
}
