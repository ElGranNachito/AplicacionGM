﻿using System;
using System.Threading;
using System.Threading.Tasks;
using CoolLogs;
using Ninject;

namespace AppGM.Core
{
    /// <summary>
    /// Controlador del programa, se encarga de lidiar con la carga de datos y view models.
    /// Permite la facil comunicacion entre varias partes de la aplicacion
    /// </summary>
    public static class SistemaPrincipal
    {
        #region Propiedades

        /// <summary>
        /// Instancia de <see cref="StandardKernel"/> de Ninject.
        /// </summary>
        public static IKernel Kernel { get; set; } = new StandardKernel();

        /// <summary>
        /// Instancia global de <see cref="IControladorDeArchivos"/>
        /// </summary>
        public static IControladorDeArchivos ControladorDeArchivos          => ObtenerInstancia<IControladorDeArchivos>();

        /// <summary>
        /// View model de la aplicacion
        /// </summary>
        public static ViewModelAplicacion           Aplicacion              => ObtenerInstancia<ViewModelAplicacion>();

        /// <summary>
        /// Viewm model de la pagina principal del rol
        /// </summary>
        public static ViewModelRol        RolSeleccionado         => ObtenerInstancia<ViewModelRol>();

        /// <summary>
        /// View model del menu de seleccion de combate
        /// </summary>
        public static ViewModelMenuSeleccionCombate MenuSeleccionCombate    => ObtenerInstancia<ViewModelMenuSeleccionCombate>();
                                                                            
        /// <summary>                                                       
        /// View model del controlador de combate                          
        /// </summary>                                                      
        public static ViewModelCombate              CombateActual           => ObtenerInstancia<ViewModelCombate>();

        /// <summary>
        /// View model del menu de creacion de funciones actualmente abierto
        /// </summary>
        public static ViewModelCreacionDeFuncionBase VMCreacionDeFuncionActual => ObtenerInstancia<ViewModelCreacionDeFuncionBase>();
                                                                            
        /// <summary>                                                       
        /// Modelo del rol actualmente abierto                              
        /// </summary>                                                      
        public static ModeloRol                     ModeloRolActual         => ObtenerInstancia<ModeloRol>();

        /// <summary>
        /// Controladores de personajes, habilidades, etc. Del rol actualmente seleccionado
        /// </summary>
        public static DatosRol                      DatosRolSeleccionado    => RolSeleccionado.ControladorRol.datosRol;
                                                                            
        /// <summary>                                                       
        /// Logger global                                                   
        /// </summary>                                                      
        public static LoggerFactory                 LoggerFactoryGlobal     => ObtenerInstancia<LoggerFactory>();
                                                                            
        /// <summary>                                                       
        /// Logger global                                                   
        /// </summary>                                                      
        public static Logger                        LoggerGlobal            => ObtenerInstancia<Logger>();
                                                                            
        public static Drag                          Drag                    => ObtenerInstancia<Drag>();

        /// <summary>
        /// Contexto del thread principal
        /// </summary>
        public static SynchronizationContext ThreadUISyncContext;

        #endregion

        #region Funciones

        /// <summary>
        /// Funcion que se llama antes de que se inicie la primera ventana. Se encarga de la carga
        /// de datos y creacion de view models
        /// </summary>
        /// <param name="controladorDeArchivos">Instancia de un <see cref="IControladorDeArchivos"/> para atar al IoC</param>
        public static void Inicializar(IControladorDeArchivos controladorDeArchivos)
        {
            //Obtenemos el contexto del hilo que nos llamo
            ThreadUISyncContext = SynchronizationContext.Current;
  
            Kernel.Bind<LoggerFactory>().ToConstant(new CoolFactory(ESeveridad.TODOS, "%t-T [%a>%f:%l %s-u]: %m"));
            Kernel.Bind<Logger>().ToConstant(new CoolLogger(LoggerFactoryGlobal, "LoggerPrincipal", "log"));

            LoggerGlobal.Log("Inicializando sistema", ESeveridad.Info);

            Kernel.Bind<IControladorDeArchivos>().ToConstant(controladorDeArchivos);

            //Atamos los view models basicos, es decir que necesitamos independientemente del rol que se seleccione
            Kernel.Bind<ViewModelAplicacion>().ToConstant(new ViewModelAplicacion());
            Kernel.Bind<ViewModelPaginaInicio>().ToConstant(new ViewModelPaginaInicio());
            Kernel.Bind<Drag>().ToConstant(new Drag());

            Aplicacion.OnPaginaActualCambio += PaginaActualCambioHandler;
        }

        /// <summary>
        /// Carga los datos del rol asincronicamente
        /// </summary>
        /// <param name="modelo">Modelo cuyos datos seran cargados</param>
        /// <returns></returns>
        public static async Task CargarRolAsincronicamente(ModeloRol modelo)
        {
            //Atamos primero estos das clases al IoC porque son necesarias para la carga
	        Kernel.Bind<ModeloRol>().ToConstant(modelo);
            Kernel.Bind<ViewModelRol>().ToConstant(new ViewModelRol());

            //Cargamos los datos
            await DatosRolSeleccionado.CargarDatos();

            //Ahora creamos los view models que necesitan de los datos que acabamos de cargar
            CrearViewModelsRol();

            //Le damos a la ventana el nombre del rol
            Aplicacion.VentanaPrincipal.TituloVentana = modelo.Nombre;
        }

        /// <summary>
        /// Cierra la conexion a la base de datos y desato los viewmodels del rol del IoC
        /// </summary>
        private static void DescargarRol()
        {
            //Cerramos la conexion
            DatosRolSeleccionado.CerrarConexion();

            //Desatamos los view models del IoC
            DesatarViewModelsRol();

            //Cambiamos el titulo de la ventana
            Aplicacion.VentanaPrincipal.TituloVentana = "AppGM";
        }

        /// <summary>
        /// Obtiene la instancia de una clase del IoC
        /// </summary>
        /// <typeparam name="T">Tipo de la clase cuya instancia queremos obtener</typeparam>
        /// <returns>Instancia de la clase</returns>
        public static T ObtenerInstancia<T>()
        {
            return Kernel.Get<T>();
        }

        /// <summary>
        /// Guarda todos los cambios realizados a los modelos asincronicamente
        /// </summary>
        /// <returns></returns>
        public static async Task GuardarDatosRolAsincronicamente()
        {
            await DatosRolSeleccionado.GuardarDatosAsync();
        }

        /// <summary>
        /// Guarda todos los cambios realizados a los modelos sincronicamente
        /// </summary>
        public static void GuardarDatosRol()
        {
            DatosRolSeleccionado.GuardarDatos();
        }

        /// <summary>
        /// Guarda un modelo en la base de datos
        /// </summary>
        /// <param name="modelo">Modelo a guardar</param>
        public static void GuardarModelo(ModeloBaseSK modelo)
        {
            if (modelo == null)
                return;

            DatosRolSeleccionado.GuardarModelo(modelo);
        }

        /// <summary>
        /// Elimina un modelo de la base de datos
        /// </summary>
        /// <param name="modelo">Modelo a eliminar</param>
        public static void EliminarModelo(ModeloBaseSK modelo)
        {
            if (modelo == null)
                return;

            DatosRolSeleccionado.EliminarModelo(modelo);
        }

        /// <summary>
        /// Crea los view models necesarios para el rol y los ata al IoC
        /// </summary>
        private static void CrearViewModelsRol()
        {
	        Kernel.Bind<ViewModelMenuSeleccionTipoFicha>().ToConstant(new ViewModelMenuSeleccionTipoFicha());
            Kernel.Bind<ViewModelListaFichasVistaFichas>().ToConstant(new ViewModelListaFichasVistaFichas());
            Kernel.Bind<ViewModelSolapaSeccionMapas>()    .ToConstant(new ViewModelSolapaSeccionMapas());
            Kernel.Bind<ViewModelMapaPrincipal>()         .ToConstant(new ViewModelMapaPrincipal(DatosRolSeleccionado.Mapas[0]));
            Kernel.Bind<ViewModelMenuSeleccionCombate>()  .ToConstant(new ViewModelMenuSeleccionCombate(DatosRolSeleccionado.CombatesActivos));
            Kernel.Bind<ViewModelCombate>()               .ToConstant(new ViewModelCombate());
        }

        /// <summary>
        /// Añade un <paramref name="obj"/> al <see cref="Kernel"/>
        /// </summary>
        /// <typeparam name="T">Tipo del <paramref name="obj"/> que sera atado</typeparam>
        /// <param name="obj">Objeto que sera atado</param>
        public static void Atar<T>(T obj)
        {
	        Kernel.Bind<T>().ToConstant(obj);
        }

        /// <summary>
        /// Quita el objeto de tipo <typeparamref name="T"/> actualmente atado
        /// </summary>
        /// <typeparam name="T">Tipo del objeto que sera quitado</typeparam>
        public static void Desatar<T>()
        {
            Kernel.Unbind<T>();
        }

        /// <summary>
        /// Desata los view models especificos del rol del IoC para que puedan ser recolectados por el GC
        /// </summary>
        private static void DesatarViewModelsRol()
        {
            Kernel.Unbind<ViewModelMenuSeleccionTipoFicha>();
            Kernel.Unbind<ViewModelListaFichasVistaFichas>();
            Kernel.Unbind<ViewModelMapaPrincipal>();
            Kernel.Unbind<ViewModelMenuSeleccionCombate>();
            Kernel.Unbind<ViewModelCombate>();
            Kernel.Unbind<ViewModelRol>();
            Kernel.Unbind<ModeloRol>();
        }

        /// <summary>
        /// Funcion que se encarga de lidiar con el evento de cambio de pagina actual
        /// </summary>
        /// <param name="paginaAnterior">Pagina anterior</param>
        /// <param name="paginaActual">Pagina actual</param>
        private static void PaginaActualCambioHandler(EPagina paginaAnterior, EPagina paginaActual)
        {
            //Si volvimos a la pagina principal descargamos los datos del rol
            if (paginaActual == EPagina.PaginaPrincipal)
                DescargarRol();
        }

        #endregion
    }
}