using System.Threading.Tasks;
using Ninject;

namespace AppGM.Core
{

    /// <summary>
    /// Controlador del programa, se encarga de lidiar con la carga de datos y view models
    /// Es un intermediario entre la UI y los datos
    /// </summary>
    
    //TODO: Extender esta clase cuando tengamos el programa mas desarrollado
    public static class SistemaPrincipal
    {
        #region Propiedades
        public static IKernel Kernel { get; set; } = new StandardKernel();

        public static IControladorDeArchivos ControladorDeArchivos        => ObtenerInstancia<IControladorDeArchivos>();

        public static ViewModelAplicacion           Aplicacion            => ObtenerInstancia<ViewModelAplicacion>();
        public static ViewModelPaginaPrincipalRol   RolSeleccionado       => ObtenerInstancia<ViewModelPaginaPrincipalRol>();
        public static ViewModelMenuSeleccionCombate MenuSeleccionCombate  => ObtenerInstancia<ViewModelMenuSeleccionCombate>();
        public static ViewModelCombate              CombateActual         => ObtenerInstancia<ViewModelCombate>();
        public static DatosRol                      DatosRolSeleccionado  => RolSeleccionado.ControladorRol.datosRol;

        #endregion

        #region Funciones

        /// <summary>
        /// Funcion que se llama antes de que se inicie la primera ventana. Se encarga de la carga
        /// de datos y creacion de view models
        /// </summary>
        public static void Inicializar()
        {
            CrearViewModels();
        }
        public static void CrearControladorDeArchivos(IControladorDeArchivos controladorDeArchivos)
        {
            Kernel.Bind<IControladorDeArchivos>().ToConstant(controladorDeArchivos);
        }
        public static async Task CargarRol(ModeloRol modelo)
        {
            //TODO: Cargar base da datos en base al nombre/id del rol
            Kernel.Bind<ViewModelPaginaPrincipalRol>().ToConstant(new ViewModelPaginaPrincipalRol(modelo));

            await CargarDatosRol();

            CrearViewModelsRol(modelo);

            Aplicacion.VentanaPrincipal.TituloVentana = modelo.Nombre;
        }
        public static T ObtenerInstancia<T>()
        {
            return Kernel.Get<T>();
        }

        public static async Task GuardarDatosRolAsync()
        {
            await DatosRolSeleccionado.GuardarDatosAsync();
        }
        public static void GuardarDatosRol()
        {
            DatosRolSeleccionado.GuardarDatos();
        }

        public static void GuardarModelo(ModeloBaseSK modelo)
        {
            if (modelo == null)
                return;

            DatosRolSeleccionado.GuardarModelo(modelo);
        }

        public static void EliminarModelo(ModeloBaseSK modelo)
        {
            if (modelo == null)
                return;

            DatosRolSeleccionado.EliminarModelo(modelo);
        }

        public static void CerrarConexion()
        {
            DatosRolSeleccionado.CerrarConexion();
        }
        private static async Task CargarDatosRol()
        {
            await DatosRolSeleccionado.CargarDatos();
        }

        private static void CrearViewModels()
        {
            Kernel.Bind<ViewModelAplicacion>()     .ToConstant(new ViewModelAplicacion());
            Kernel.Bind<ViewModelPaginaPrincipal>().ToConstant(new ViewModelPaginaPrincipal());
        }

        private static void CrearViewModelsRol(ModeloRol modelo)
        {
            Kernel.Bind<ViewModelMenuSeleccionTipoFicha>().ToConstant(new ViewModelMenuSeleccionTipoFicha());
            Kernel.Bind<ViewModelListaFichasVistaFichas>().ToConstant(new ViewModelListaFichasVistaFichas());
            Kernel.Bind<ViewModelMapaPrincipal>()         .ToConstant(new ViewModelMapaPrincipal(DatosRolSeleccionado.Mapas[0]));
            Kernel.Bind<ViewModelMenuSeleccionCombate>()  .ToConstant(new ViewModelMenuSeleccionCombate(DatosRolSeleccionado.CombatesActivos));
            Kernel.Bind<ViewModelCombate>()               .ToConstant(new ViewModelCombate());
        }
        #endregion
    }
}
