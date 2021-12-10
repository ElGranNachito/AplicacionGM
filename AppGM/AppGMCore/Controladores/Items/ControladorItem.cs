using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoolLogs;

namespace AppGM.Core
{
	public class ControladorItem : Controlador<ModeloItem>, IInfligidorDaño, IDañable
    {
	    #region Eventos

	    public delegate void dUtilizarItem(ControladorItem item, ControladorPersonaje usuario, ControladorPersonaje objetivo);

	    public delegate void dEstadoPortacionCambio(ControladorItem item, ControladorPersonaje portador, EEstadoPortacion estadoAnterior, EEstadoPortacion estadoActual);

	    public delegate void dItemEliminado(ControladorItem item, ControladorPersonaje portador);

	    public event dUtilizarItem OnUtilizado = delegate { };

	    public event dEstadoPortacionCambio OnEstadoDePortacionCambio = delegate { };

	    public event dItemEliminado OnItemEliminado = delegate { };

	    public event IInfligidorDaño.dInfligirDaño OnInfligioDaño = delegate{};

	    public event IDañable.dDañado OnDañado = delegate{};

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
        public ControladorDefensa ControladorDefensa { get; set; }

        /// <summary>
        /// Controlador de la funcion para utilizar el item
        /// </summary>
        public ControladorFuncion_Item ControladorFuncionUtilizar { get; set; }

        #endregion

        #region Constructores

        public ControladorItem(ModeloItem _modeloItem)
	        : base(_modeloItem)
        {
	        Portador = SistemaPrincipal.ObtenerControlador<ControladorPersonaje, ModeloPersonaje>(modelo.PersonajePortador, false);

	        if (modelo.DatosDefensa is not null)
	        {
		        ControladorDefensa = SistemaPrincipal.ObtenerControlador<ControladorDefensa, ModeloDatosDefensa>(modelo.DatosDefensa, true);
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

        public async Task<bool> CompilarFunciones()
        {
	        foreach (var funcion in modelo.Funciones)
	        {
		        switch (funcion.PropositoFuncionRelacion)
		        {
			        case EPropositoFuncionRelacion.Uso:
			        {
				        ControladorFuncionUtilizar = new ControladorFuncion_Item(funcion.Funcion);

				        await ControladorFuncionUtilizar.CargarBloquesAsync();

				        await ControladorFuncionUtilizar.CompilarAsync();

				        if (!ControladorFuncionUtilizar.ResultadoCompilacion.FueExitosa)
				        {
                            SistemaPrincipal.LoggerGlobal.Log($"Error al compilar {ControladorFuncionUtilizar}", ESeveridad.Error);

					        return false;
				        }

				        break;
			        }
		        }
	        }

	        return true;
        }

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

        public void Dañar(ModeloArgumentosDaño argsDaño, SortedList<int, SubobjetivoDaño> subObjetivos = null, SubobjetivoDaño subobjetivoActual = null)
        {
	        ControladorDefensa?.ReducirDaño(argsDaño);

	        var objetivoQueRepresentaAEsteItem = argsDaño.Objetivos.Find(o => o.Item == modelo);

	        if (objetivoQueRepresentaAEsteItem is null)
	        {
		        objetivoQueRepresentaAEsteItem = new ModeloDañable
		        {
			        Item = modelo,
			        ArgumentosDaño = argsDaño
		        };

                argsDaño.AñadirObjetivo(objetivoQueRepresentaAEsteItem);
	        }

            modelo.HistorialDañoRecibido.Add(objetivoQueRepresentaAEsteItem);

	        OnDañado(argsDaño, subObjetivos);
        }

        public void InfligirDaño(IDañable objetivo, ModeloArgumentosDaño argsDaño, SortedList<int, SubobjetivoDaño> subObjetivos = null)
        {
	        if (Portador is not null)
	        {
                Portador.InfligirDaño(objetivo, argsDaño, subObjetivos);
	        }
	        else
	        {
		        objetivo.Dañar(argsDaño, subObjetivos);
            }

	        var infligidorDañoQueRepresentaAEsteItem = argsDaño.InfligidoresDaño.Find(i => i.Item == modelo) ?? argsDaño.AñadirInfligidorDaño(this);

	        modelo.HistorialDañoInfligido.Add(infligidorDañoQueRepresentaAEsteItem);

	        OnInfligioDaño(objetivo, argsDaño, subObjetivos);
        }

        public override ControladorVariableBase ObtenerControladorVariable(string nombreVariable)
        {
	        return base.ObtenerControladorVariable(nombreVariable) ?? Portador?.ObtenerControladorVariable(nombreVariable);
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
