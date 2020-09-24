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
        //DatosRolActual DatorRolActual
        //ControladorRol ControladorRolActual

        public static IKernel Kernel { get; set; } = new StandardKernel();

        /// <summary>
        /// Funcion que se llama antes de que se inicie la primera ventana. Se encarga de la carga
        /// de datos y creacion de view models
        /// </summary>
        public static void Inicializar()
        {
            CrearViewModels();
        }

        private static void CrearViewModels()
        {
            Kernel.Bind<ViewModelPaginaPrincipal>().ToConstant(new ViewModelPaginaPrincipal());
        }

        public static T ObtenerInstancia<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
