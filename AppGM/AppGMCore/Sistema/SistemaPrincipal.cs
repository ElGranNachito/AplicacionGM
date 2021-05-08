using System.Threading;
using System.Threading.Tasks;
using Ninject;
using AppGM.Core.Delegados;

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

        private static ServiceContainer mServicios;
        public static IKernel Kernel { get; set; } = new StandardKernel();

        public static IControladorDeArchivos ControladorDeArchivos        => ObtenerInstancia<IControladorDeArchivos>();

        public static ViewModelAplicacion           Aplicacion            => ObtenerInstancia<ViewModelAplicacion>();
        public static ViewModelPaginaPrincipalRol   RolSeleccionado       => ObtenerInstancia<ViewModelPaginaPrincipalRol>();
        public static ViewModelMenuSeleccionCombate MenuSeleccionCombate  => ObtenerInstancia<ViewModelMenuSeleccionCombate>();
        public static ViewModelCombate              CombateActual         => ObtenerInstancia<ViewModelCombate>();
        public static ModeloRol                     ModeloRolActual       => ObtenerInstancia<ModeloRol>();
        public static DatosRol                      DatosRolSeleccionado  => RolSeleccionado.ControladorRol.datosRol;

        public static SynchronizationContext ThreadUISyncContext;

        #endregion

        #region Funciones

        /// <summary>
        /// Funcion que se llama antes de que se inicie la primera ventana. Se encarga de la carga
        /// de datos y creacion de view models
        /// </summary>
        public static void Inicializar()
        {
            ThreadUISyncContext = SynchronizationContext.Current;

            CrearViewModels();

            Aplicacion.OnPaginaActualCambio += PaginaActualCambioHandler;
        }
        public static void CrearControladorDeArchivos(IControladorDeArchivos controladorDeArchivos)
        {
            Kernel.Bind<IControladorDeArchivos>().ToConstant(controladorDeArchivos);
        }
        public static async Task CargarRol(ModeloRol modelo)
        {
            await CrearViewModelsRol(modelo);

            Aplicacion.VentanaPrincipal.TituloVentana = modelo.Nombre;
        }

        private static void DescargarRol()
        {
            DatosRolSeleccionado.CerrarConexion();

            EliminarViewModelsRol();

            Aplicacion.VentanaPrincipal.TituloVentana = "AppGM";
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

        private static void CrearViewModels()
        {
            Kernel.Bind<ViewModelAplicacion>()     .ToConstant(new ViewModelAplicacion());
            Kernel.Bind<ViewModelPaginaPrincipal>().ToConstant(new ViewModelPaginaPrincipal());
        }

        private static async Task CrearViewModelsRol(ModeloRol modelo)
        {
            Kernel.Bind<ViewModelPaginaPrincipalRol>().ToConstant(new ViewModelPaginaPrincipalRol(modelo));

            await DatosRolSeleccionado.CargarDatos();

            Kernel.Bind<ModeloRol>()                      .ToConstant(modelo);
            Kernel.Bind<ViewModelMenuSeleccionTipoFicha>().ToConstant(new ViewModelMenuSeleccionTipoFicha());
            Kernel.Bind<ViewModelListaFichasVistaFichas>().ToConstant(new ViewModelListaFichasVistaFichas());
            Kernel.Bind<ViewModelMapaPrincipal>()         .ToConstant(new ViewModelMapaPrincipal(DatosRolSeleccionado.Mapas[0]));
            Kernel.Bind<ViewModelMenuSeleccionCombate>()  .ToConstant(new ViewModelMenuSeleccionCombate(DatosRolSeleccionado.CombatesActivos));
            Kernel.Bind<ViewModelCombate>()               .ToConstant(new ViewModelCombate());
        }

        private static void EliminarViewModelsRol()
        {
            Kernel.Unbind<ViewModelMenuSeleccionTipoFicha>();
            Kernel.Unbind<ViewModelListaFichasVistaFichas>();
            Kernel.Unbind<ViewModelMapaPrincipal>();
            Kernel.Unbind<ViewModelMenuSeleccionCombate>();
            Kernel.Unbind<ViewModelCombate>();
            Kernel.Unbind<ViewModelPaginaPrincipalRol>();
            Kernel.Unbind<ModeloRol>();
        }

        private static void PaginaActualCambioHandler(EPagina paginaAnterior, EPagina paginaActual)
        {
            if (paginaActual == EPagina.PaginaPrincipal)
                DescargarRol();
        }

        #endregion
    }
}
