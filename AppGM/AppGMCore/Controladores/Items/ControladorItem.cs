using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppGM.Core
{
	public class ControladorItem : Controlador<ModeloItem>, IInfligidorDaño
    {
	    #region Eventos

	    public delegate void dUtilizarItem(ControladorItem item, ControladorPersonaje usuario, ControladorPersonaje objetivo);

	    public delegate void dEstadoPortacionCambio(ControladorItem item, ControladorPersonaje portador, EEstadoPortacion estadoAnterior, EEstadoPortacion estadoActual);

	    public delegate void dItemEliminado(ControladorItem item, ControladorPersonaje portador);

	    public event dUtilizarItem OnUtilizado = delegate { };

	    public event dEstadoPortacionCambio OnEstadoDePortacionCambio = delegate { };

	    public event dItemEliminado OnItemEliminado = delegate { };

	    public event IInfligidorDaño.dInfligirDaño OnInfligirDaño;

        #endregion

        #region Propiedades

        /// <summary>
        /// Nombre del item
        /// </summary>
        public string Nombre
        {
	        get => modelo.Nombre;
	        set => modelo.Nombre = value;
        }

        /// <summary>
        /// Descripcion del item
        /// </summary>
        public string Descripcion
        {
	        get => modelo.Descripcion;
	        set => modelo.Descripcion = value;
        }

        /// <summary>
        /// Espacio que ocupa el item
        /// </summary>
        public decimal EspacioQueOcupa => modelo.EspacioQueOcupa;

        /// <summary>
        /// Tipo del item
        /// </summary>
        public ETipoItem TipoItem
        {
	        get => modelo.TipoItem;
	        set => modelo.TipoItem = value;
        }

        /// <summary>
        /// Estado de portacion del item
        /// </summary>
        public EEstadoPortacion EstadoPortacion
        {
	        get => modelo.EstadoPortacion;
	        set
	        {
                if(value == modelo.EstadoPortacion)
                    return;

                OnEstadoDePortacionCambio(this, Portador, modelo.EstadoPortacion, value);

                modelo.EstadoPortacion = value;
	        }
        }

        public List<ControladorSlot> Slots { get; set; } = new List<ControladorSlot>();

        /// <summary>
        /// Controlador del personaje que porta el item
        /// </summary>
        public ControladorPersonaje Portador { get; set; }

        /// <summary>
        /// Controlador de denfensa del item
        /// </summary>
        public ControladorDefensa Defensa { get; set; }

        #endregion

        #region Constructores

        public ControladorItem(ModeloItem _modeloItem)
	        : base(_modeloItem)
        {
	        Portador = SistemaPrincipal.ObtenerControlador<ControladorPersonaje, ModeloPersonaje>(modelo.PersonajePortador, false);

	        if (modelo.DatosDefensa is not null)
	        {
		        Defensa = SistemaPrincipal.ObtenerControlador<ControladorDefensa, ModeloDatosDefensa>(modelo.DatosDefensa, true);
	        }

	        if (Portador == null)
	        {
                modelo.PersonajePortador.AñadirHandlerSoloUsoControladorCreado((modelo, controlador) =>
                {
	                Portador = (ControladorPersonaje) controlador;
                });
	        }

	        modelo.AñadirHandlerSoloUsoModeloEliminado(m =>
            {
	            OnItemEliminado(this, Portador);
            });

            CargarVariablesYTiradas();
        }

        #endregion

        #region Metodos

        public virtual void Utilizar(ControladorPersonaje usuario, ControladorPersonaje[] objetivos, object parametroExtra, object segundoParametroExtra)
        {
            
        }

        public virtual void Utilizar(ControladorPersonaje usuario, object parametroExtra, object segundoParametroExtra)
        {
           
        }

        public virtual bool PuedeUtilizar(ControladorPersonaje usuario, ControladorPersonaje[] objetivos)
        {
	        return false;
        }

        public void InfligirDaño(IDañable objetivo, ModeloArgumentosDaño argsDaño, SortedList<int, IDañable> subObjetivos = null)
        {
	        throw new System.NotImplementedException();
        }

        public override async Task Recargar()
        {
	        await base.Recargar();

	        foreach (var slot in modelo.Slots)
	        {
		        
	        }
        }

        public override ViewModelItemListaBase CrearViewModelItem()
        {
	        return new ViewModelItemListaItems(this);
        }

        public override string ToString()
        {
	        return $"{Nombre} ({TipoItem.FlagsActivasEnumToString()}). Portado por: {modelo.PersonajePortador}";
        }

        #endregion
    }
}
