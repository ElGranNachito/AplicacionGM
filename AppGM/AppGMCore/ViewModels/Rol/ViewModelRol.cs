using System.Windows.Input;
using AppGM.Core.Delegados;

namespace AppGM.Core
{
    /// <summary>
    /// VM que representa el contenido de la pagina principal del rol
    /// </summary>
    public class ViewModelRol : ViewModel, IBotonSeleccionado<object>
    {
        #region Miembros

        /// <summary>
        /// Menu actual del rol en el que se encuentra el usuario
        /// </summary>
        private EMenuRol mEMenuActual = EMenuRol.NINGUNO;

        #endregion

        #region Propiedades

        /// <summary>
        /// Controlador del rol actual
        /// </summary>
        public ControladorRol ControladorRol { get; set; }

        /// <summary>
        /// <see cref="ICommand"/> que se ejecuta cuando el usuario presiona el boton 'Fichas'
        /// </summary>
        public ICommand ComandoBotonFichas { get; set; }

        /// <summary>
        /// <see cref="ICommand"/> que se ejecuta cuando el usuario presiona el boton 'Mapas'
        /// </summary>
        public ICommand ComandoBotonMapas { get; set; }

        /// <summary>
        /// <see cref="ICommand"/> que se ejecuta cuando el usuario presiona el boton 'Registro'
        /// </summary>
        public ICommand ComandoBotonRegistro { get; set; }

        /// <summary>
        /// <see cref="ICommand"/> que se ejecuta cuando el usuario presiona el boton 'Tirada'
        /// </summary>
        public ICommand ComandoBotonTirada { get; set; }

        /// <summary>
        /// <see cref="ICommand"/> que se ejecuta cuando el usuario presiona el boton 'Combates'
        /// </summary>
        public ICommand ComandoBotonCombates { get; set; }

        /// <summary>
        /// <see cref="ICommand"/> que se ejecuta cuando el usuario presiona el boton 'Salir'
        /// </summary>
        public ICommand ComandoBotonSalir { get; set; }

        public EMenuRol EMenu
        {
            get => mEMenuActual;
            set
            {
                //Si el valor que se intenta establecer es el mismo que el actual entonces no hacemos nada
                if (value == mEMenuActual)
                    return;

                EMenuRol menuAnterior = mEMenuActual;

                mEMenuActual = value;

                //Disparamos el evento de cambio de menu
                OnMenuCambio(menuAnterior, mEMenuActual);
            }
        }

        public object BotonSeleccionado { get; set; }

        /// <summary>
        /// Viewm model de la pagina del menu mapa
        /// </summary>
        public ViewModelSolapaSeccionMapas SeccionMapaSeleccionada { get; set; } = SistemaPrincipal.ObtenerInstancia<ViewModelSolapaSeccionMapas>();

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor default
        /// </summary>
        public ViewModelRol()
        {
            ControladorRol = new ControladorRol(SistemaPrincipal.ModeloRolActual);

            ComandoBotonFichas   = new Comando(() => SistemaPrincipal.RolSeleccionado.EMenu = EMenuRol.SeleccionTipoFichas);
            ComandoBotonMapas    = new Comando(() => SistemaPrincipal.RolSeleccionado.EMenu = EMenuRol.Mapas);
            ComandoBotonCombates = new Comando(() => SistemaPrincipal.RolSeleccionado.EMenu = EMenuRol.AdministrarCombates);

            ComandoBotonSalir = new Comando(()=>
            {
	            BotonSeleccionado = null;

                SistemaPrincipal.GuardarDatos();

                SistemaPrincipal.Aplicacion.PaginaActual =
                        EPagina.PaginaPrincipal;
            });
        }
        #endregion

        #region Eventos

        /// <summary>
        /// Evento que se dispara cuando el usuario pasa a otro menu
        /// </summary>
        public event DVariableCambio<EMenuRol> OnMenuCambio = delegate { };

        #endregion
    }
}