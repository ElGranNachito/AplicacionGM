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

        public static ViewModelPaginaPrincipalRol RolSeleccionado => ObtenerInstancia<ViewModelPaginaPrincipalRol>();

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
        public static async Task CargarRol(ModeloRol modelo)
        {
            //TODO: Cargar base da datos en base al nombre/id del rol

            await CargarDatosRol();

            Kernel.Bind<ViewModelPaginaPrincipalRol>()    .ToConstant(new ViewModelPaginaPrincipalRol(modelo));
            Kernel.Bind<ViewModelMenuSeleccionTipoFicha>().ToConstant(new ViewModelMenuSeleccionTipoFicha());
            Kernel.Bind<ViewModelListaFichasVistaFichas>().ToConstant(new ViewModelListaFichasVistaFichas());
            Kernel.Bind<ViewModelMapa>()                  .ToConstant(new ViewModelMapa());

            ObtenerInstancia<ViewModelAplicacion>().TituloVentana = modelo.Nombre;
        }
        public static T ObtenerInstancia<T>()
        {
            return Kernel.Get<T>();
        }

        private static async Task CargarDatosRol()
        {
            await RolSeleccionado.ControladorRol.datosRol.CargarDatos();
            RolSeleccionado.ControladorRol.datosRol.CerrarConexion();
        }
        private static void CrearViewModels()
        {
            Kernel.Bind<ViewModelAplicacion>().ToConstant(new ViewModelAplicacion());
            Kernel.Bind<ViewModelPaginaPrincipal>().ToConstant(new ViewModelPaginaPrincipal());
        }
        #endregion
    }
}
