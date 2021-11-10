using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppGM.Core
{
	public class ControladorItem : Controlador<ModeloItem>
    {
	    #region Eventos

	    public delegate void dUtilizarItem(ControladorItem item, ControladorPersonaje usuario, ControladorPersonaje objetivo);

	    public delegate void dEstadoPortacionCambio(ControladorItem item, ControladorPersonaje portador, EEstadoPortacion estadoAnterior, EEstadoPortacion estadoActual);

	    public delegate void dItemEliminado(ControladorItem item, ControladorPersonaje portador);

	    public event dUtilizarItem OnUtilizado = delegate { };

	    public event dEstadoPortacionCambio OnEstadoDePortacionCambio = delegate { };

	    public event dItemEliminado OnItemEliminado = delegate { };

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

        #endregion

        #region Constructores

        public ControladorItem(ModeloItem _modeloItem)
	        : base(_modeloItem)
        {
	        Portador = SistemaPrincipal.ObtenerControlador<ControladorPersonaje, ModeloPersonaje>(modelo.PersonajePortador, false);

            if(Portador == null)
                SistemaPrincipal.LoggerGlobal.LogCrash($"{Portador} no puede ser null!");

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

        public override async Task Recargar()
        {
	        await base.Recargar();

	        foreach (var slot in modelo.Slots)
	        {
		        
	        }
        }

        #endregion
    }
}
