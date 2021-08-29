using System;
using System.Collections.Generic;
using System.Linq;
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
        #region Propiedades & Campos

        //---------------------------------CAMPOS---------------------------------

        private static Dictionary<ModeloBaseSK, ControladorBase> mControladores = new Dictionary<ModeloBaseSK, ControladorBase>();

        //------------------------------PROPIEDADES-------------------------------

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
			
            CoolLogs.Globales.Inicializar<CoolFactory>(ESeveridad.TODOS, "%t-T [%a>%f:%l %s-u]: %m", "LoggerPrincipal", "log");
            
            Kernel.Bind<Logger>().ToConstant(CoolLogs.Globales.LoggerGlobal);
            Kernel.Bind<LoggerFactory>().ToConstant(CoolLogs.Globales.Factory);

            LoggerGlobal.Log("Inicializando sistema...", ESeveridad.Info);

            Kernel.Bind<IControladorDeArchivos>().ToConstant(controladorDeArchivos);

            //Atamos los view models basicos, es decir que necesitamos independientemente del rol que se seleccione
            Kernel.Bind<ViewModelAplicacion>().ToConstant(new ViewModelAplicacion());
            Kernel.Bind<ViewModelPaginaInicio>().ToConstant(new ViewModelPaginaInicio());
            Kernel.Bind<Drag>().ToConstant(new Drag());

            Aplicacion.OnPaginaActualCambio += PaginaActualCambioHandler;
        }

        /// <summary>
        /// Funcion llamada antes de salir de la aplicacion
        /// </summary>
        /// <param name="codigoSalida">Codigo de salida de la aplicacion <para/> Para ver los posibles codigos y sus significados: <seealso cref="https://docs.microsoft.com/es-es/windows/win32/debug/system-error-codes?redirectedfrom=MSDN"/></param>
        public static void Apagar(int codigoSalida)
        {
            LoggerGlobal.Log($"Saliendo de la app... Codigo de salida: ({codigoSalida})", ESeveridad.Info);

            CoolLogs.Globales.Finalizar();
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
		/// Intenta obtener el <see cref="Controlador{TipoModelo}"/> para <paramref name="modelo"/>
		/// </summary>
		/// <typeparam name="TControlador">Tipo del controlador</typeparam>
		/// <typeparam name="TModelo">Tipo del modelo</typeparam>
		/// <param name="modelo">Modelo del que se quiere obtener el controlador</param>
		/// <param name="intenarCrearSiNoExiste">Indica si se debe intentar crear el controlador en caso de que no se encuentre en el diccionario</param>
		/// <returns>El <see cref="TControlador"/> obtenido o null</returns>
		public static TControlador ObtenerControlador<TControlador, TModelo>(TModelo modelo, bool intenarCrearSiNoExiste = false)
            where TModelo : ModeloBaseSK
            where TControlador : Controlador<TModelo>
        {
            if (mControladores.ContainsKey(modelo))
                return mControladores[modelo] as TControlador;

            if (!intenarCrearSiNoExiste)
                return null;

            try
            {
                var nuevoControlador = Activator.CreateInstance(typeof(TControlador), modelo) as TControlador;

                mControladores.Add(modelo, nuevoControlador);

                return nuevoControlador;
            }
            catch (Exception ex)
            {
                SistemaPrincipal.LoggerGlobal.Log($"Error la intentar crear controlador de tipo {typeof(TControlador)}.{Environment.NewLine}Exception:{ex.Message}", ESeveridad.Error);

                return null;
            }
        }

        /// <summary>
        /// Intenta obtener el <see cref="Controlador{TipoModelo}"/> para <paramref name="modelo"/>
        /// </summary>
        /// <param name="modelo">Modelo del que se quiere obtener el controlador</param>
        /// <param name="intenarCrearSiNoExiste">Indica si se debe intentar crear el controlador en caso de que no se encuentre en el diccionario</param>
        /// <returns>El controlador encontrado o null</returns>
        public static ControladorBase ObtenerControlador(ModeloBase modelo, Type tipoControlador = null, bool intenarCrearSiNoExiste = false)
        {
	        if (mControladores.ContainsKey(modelo))
		        return mControladores[modelo];

	        if (tipoControlador == null || !intenarCrearSiNoExiste || tipoControlador.IsSubclassOf(typeof(ControladorBase)))
		        return null;

	        try
	        {
		        var nuevoControlador = Activator.CreateInstance(tipoControlador, modelo) as ControladorBase;

		        mControladores.Add(modelo, nuevoControlador);

		        return nuevoControlador;
	        }
	        catch (Exception ex)
	        {
		        SistemaPrincipal.LoggerGlobal.Log($"Error la intentar crear controlador para el modelo de tipo {modelo.GetType()}.{Environment.NewLine}Exception:{ex.Message}", ESeveridad.Error);

		        return null;
	        }
        }

        /// <summary>
        /// Intenta obtener el <see cref="Controlador{TipoModelo}"/> para <paramref name="modelo"/>
        /// </summary>
        /// <param name="modelo">Modelo del que se quiere obtener el controlador</param>
        /// <param name="intenarCrearSiNoExiste">Indica si se debe intentar crear el controlador en caso de que no se encuentre en el diccionario</param>
        /// <returns>El controlador encontrado o null</returns>
        public static List<ControladorBase> ObtenerControladores(Type tipoControladores, bool incluirSubclases)
        {
	        var controladoresExistente = mControladores.Values.ToList();

	        return controladoresExistente.FindAll(controlador =>
	        {
		        Type tipoControladorActual = controlador.GetType();

		        if (tipoControladorActual == tipoControladores ||
		            (incluirSubclases && tipoControladorActual.IsSubclassOf(tipoControladores)))
		        {
			        return true;
		        }

		        return false;
	        });
        }

        /// <summary>
        /// Añade el <paramref name="controlador"/> al diccionario
        /// </summary>
        /// <typeparam name="TControlador">Tipo del controlador</typeparam>
        /// <typeparam name="TModelo">Tipo del modelo</typeparam>
        /// <param name="modelo"><see cref="ModeloBaseSK"/> del controlador</param>
        /// <param name="controlador"><see cref="ControladorBase"/>que sera añadido</param>
        public static void AñadirControlador<TModelo, TControlador>(TModelo modelo, TControlador controlador)
            where TModelo : ModeloBaseSK
            where TControlador : Controlador<TModelo>
        {
            try
            {
                mControladores.Add(modelo, controlador);
            }
            catch (Exception ex)
            {
                SistemaPrincipal.LoggerGlobal.Log($"Error añadir controlador ({controlador}) al diccionario.{Environment.NewLine}Exception:{ex.Message}", ESeveridad.Error);
            }
        }

        /// <summary>
        /// Quita el <see cref="Controlador{TipoModelo}"/> asociado a <paramref name="modelo"/>
        /// </summary>
        /// <param name="modelo"><see cref="ModeloBaseSK"/> al que esta asociado el
        /// <see cref="Controlador{TipoModelo}"/> que se queire quitar</param>
        public static void QuitarControlador(ModeloBaseSK modelo)
            => mControladores.Remove(modelo);

        /// <summary>
        /// Muestra un mensaje sobre la ventana actualmente activa
        /// </summary>
        /// <param name="vm">View model que se le pasara a la ventana del mensaje</param>
        /// <param name="titulo">Titulo del mensaje</param>
        /// <param name="esperarCierre">Indica si la ventana actual debe esperar al cierre del mensaje</param>
        /// <param name="alto">Alto de la ventana del mensaje</param>
        /// <param name="ancho">Ancho de la ventana del mensaje</param>
        /// <returns></returns>
        public static async Task<EResultadoViewModel> MostrarMensaje(
	        ViewModelConResultadoBase vm, 
	        string titulo,
	        bool esperarCierre, 
	        int alto = -1,
	        int ancho = -1)
        {
	        return await Aplicacion.VentanaActual.MostrarMensaje(vm, titulo, esperarCierre, alto, ancho);
        }

        /// <summary>
        /// Funcion que se encarga de lidiar con el evento de cambio de pagina actual
        /// </summary>
        /// <param name="paginaAnterior">Pagina anterior</param>
        /// <param name="paginaActual">Pagina actual</param>
        private static void PaginaActualCambioHandler(EPagina paginaAnterior, EPagina paginaActual)
        {
            //Si volvimos a la pagina principal descargamos los datos del rol
            if (paginaActual == EPagina.PaginaPrincipal &&
                paginaAnterior == EPagina.PaginaPrincipalRol)
	            DescargarRol();
        }

        #endregion
    }
}