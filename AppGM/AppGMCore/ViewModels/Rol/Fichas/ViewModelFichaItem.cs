using AppGM.Core;

namespace AppGM
{

    /// <summary>
    /// View model para un item en un ItemControl que contiene datos basicos de una ficha de un personaje
    /// </summary>
    public class ViewModelFichaItem : ViewModel, IViewModelConBotonSeleccionado
    {
        #region Propiedades

        //TODO: Considerar cambiar esto de un Modelo a un Controlador
        /// <summary>
        /// Personaje que representa esta ficha
        /// </summary>
        public ModeloPersonaje Personaje { get; set; }

        /// <summary>
        /// Indica si este personaje es un <see cref="ModeloServant"/>
        /// </summary>
        public bool EsServant    => Personaje.GetType() == typeof(ModeloServant);

        /// <summary>
        /// Indica si este personaje es un <see cref="ModeloMaster"/>
        /// </summary>
        public bool EsMaster     => Personaje.GetType() == typeof(ModeloMaster);

        /// <summary>
        /// Indica si este personaje es un <see cref="ModeloInvocacion"/>
        /// </summary>
        public bool EsInvocacion => Personaje.GetType() == typeof(ModeloInvocacion);

        /// <summary>
        /// Indica si este personaje es un <see cref="ModeloPersonaje"/>
        /// </summary>
        public bool EsNPC        => Personaje.GetType() == typeof(ModeloPersonaje);

        /// <summary>
        /// Indica si este personaje es <see cref="ModeloServant"/> o <see cref="ModeloMaster"/>
        /// </summary>
        public bool EsServantOMaster => EsServant || EsMaster;

        public ViewModel ViewModelConBotonSeleccionado { get; set; }

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_personaje">Personaje que representa esta ficha</param>
        public ViewModelFichaItem(ModeloPersonaje _personaje)
        {
            Personaje = _personaje;
            ViewModelConBotonSeleccionado = SistemaPrincipal.ObtenerInstancia<ViewModelListaFichasVistaFichas>();
        }

        #endregion
    }
}
