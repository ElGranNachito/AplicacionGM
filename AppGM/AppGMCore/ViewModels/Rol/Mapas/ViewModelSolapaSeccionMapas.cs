using System.Windows.Input;
using AppGM.Core.Delegados;
using Ninject;

namespace AppGM.Core
{
    /// <summary>
    /// VM que representa el contenido del menu de mapas.
    /// </summary>
    public class ViewModelSolapaSeccionMapas : BaseViewModel, IBotonSeleccionado<object>
    {
        #region Miembros

        /// <summary>
        /// Seccion actual del menu mapas en el rol donde se encuentra el usuario
        /// </summary>
        private ESeccionMapa mESeccionMapaActual = ESeccionMapa.MapaPrincipal;

        #endregion

        #region Propiedades

        /// <summary>
        /// Controlador del rol actual
        /// </summary>
        public ControladorRol ControladorRol { get; set; }

        /// <summary>
        /// <see cref="ICommand"/> que se ejecuta cuando el usuario presiona el boton solapa de 'Mapas'
        /// </summary>
        public ICommand ComandoBotonMapaPrincipal { get; set; }

        /// <summary>
        /// <see cref="ICommand"/> que se ejecuta cuando el usuario presiona el boton solapa'Opciones'
        /// </summary>
        public ICommand ComandoBotonOpcionesMapa { get; set; }

        public ESeccionMapa ESeccionMapa
        {
            get => mESeccionMapaActual;
            set
            {
                //Si el valor que se intenta establecer es el mismo que el actual entonces no hacemos nada
                if (value == mESeccionMapaActual)
                    return;

                ESeccionMapa seccionAnterior = mESeccionMapaActual;

                mESeccionMapaActual = value;

                //Disparamos el evento de cambio de seccion
                OnMenuCambio(seccionAnterior, mESeccionMapaActual);
            }
        }

        public object BotonSeleccionado { get; set; }

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor default
        /// </summary>
        public ViewModelSolapaSeccionMapas()
        {
            ControladorRol = new ControladorRol(SistemaPrincipal.ModeloRolActual);
            
            ComandoBotonMapaPrincipal = new Comando(() => SistemaPrincipal.RolSeleccionado.SeccionMapaSeleccionada.ESeccionMapa = ESeccionMapa.MapaPrincipal);
            ComandoBotonOpcionesMapa  = new Comando(() => SistemaPrincipal.RolSeleccionado.SeccionMapaSeleccionada.ESeccionMapa = ESeccionMapa.OpcionesMapa);
        }
        #endregion

        #region Eventos

        /// <summary>
        /// Evento que se dispara cuando el usuario pasa a otra seccion
        /// </summary>
        public event DVariableCambio<ESeccionMapa> OnMenuCambio = delegate { };

        #endregion
    }
}
