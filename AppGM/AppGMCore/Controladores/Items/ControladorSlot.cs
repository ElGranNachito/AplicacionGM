using System.Collections.Generic;
using System.Threading.Tasks;
using CoolLogs;

namespace AppGM.Core
{
    /// <summary>
    /// Controlador de un <see cref="ModeloSlot"/>
    /// </summary>
    public class ControladorSlot : Controlador<ModeloSlot>
    {
	    #region Eventos

	    public delegate void dItemAlmacenado(ControladorItem item);

	    public delegate void dParteDelCuerpoAlmacenada(ControladorParteDelCuerpo parteDelCuerpo);

	    public delegate void dElementoAlmacenado(IModeloConSlots elemento, ControladorBase controladorElemento);

	    public event dItemAlmacenado OnItemAlmacenado = delegate { };
	    public event dItemAlmacenado OnItemQuitado = delegate { };
        public event dParteDelCuerpoAlmacenada OnParteDelCuerpoAlmacenada = delegate { };
        public event dParteDelCuerpoAlmacenada OnParteDelCuerpoQuitada = delegate { };
        public event dElementoAlmacenado OnElementoAlmacenado = delegate { };
        public event dElementoAlmacenado OnElementoQuitado = delegate { };

        #endregion

        #region Campos & Propiedades

        /// <summary>
        /// Espacio disponible en este slot
        /// </summary>
        public decimal EspacioDisponible => modelo.EspacioDisponible;

        /// <summary>
        /// Indica si este slot se encuentra vacio
        /// </summary>
        public bool EstaVacio => !ContieneItems && !ContieneParteDelCuerpo;

        /// <summary>
        /// Indica si este slot contiene items
        /// </summary>
        public bool ContieneItems => ControladoresItemsAlmacenados.Count > 0;

        /// <summary>
        /// Indica si este slot contiene una parte del cuerpo
        /// </summary>
        public bool ContieneParteDelCuerpo => ParteDelCuerpoAlmacenada != null;

        /// <summary>
        /// Lista con todos los <see cref="ControladorItem"/> guardados
        /// </summary>
        public List<ControladorItem> ControladoresItemsAlmacenados { get; set; }

        /// <summary>
        /// Controlador de la parte del cuerpo almacenada en este slot
        /// </summary>
        public ControladorParteDelCuerpo ParteDelCuerpoAlmacenada { get; set; }

        /// <summary>
        /// Personaje que contiene este slot
        /// </summary>
        public ControladorPersonaje PersonajeContenedor { get; set; }

        #endregion
        
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_modeloSlot"><see cref="ModeloSlot"/> representado por este controlador</param>
        public ControladorSlot(ModeloSlot _modeloSlot)
	        : base(_modeloSlot)
        {
	        ControladoresItemsAlmacenados = new List<ControladorItem>(modelo.ItemsAlmacenados.Count);

	        foreach (var item in modelo.ItemsAlmacenados)
	        {
                var controladorItem = SistemaPrincipal.ObtenerControlador<ControladorItem, ModeloItem>(item, true);

                AlmacenarItem(controladorItem);
	        }

	        if (modelo.ParteDelCuerpoAlmacenada != null)
	        {
		        ParteDelCuerpoAlmacenada = SistemaPrincipal.ObtenerControlador<ControladorParteDelCuerpo, ModeloParteDelCuerpo>(modelo.ParteDelCuerpoAlmacenada, true);
			        
		        AlmacenarParteDelCuerpo(ParteDelCuerpoAlmacenada);
	        }

	        PersonajeContenedor = SistemaPrincipal.ObtenerControlador<ControladorPersonaje, ModeloPersonaje>(modelo.PersonajeContenedor);

	        if (PersonajeContenedor == null)
	        {
                modelo.PersonajeContenedor.AñadirHandlerSoloUsoControladorCreado((modelo, controlador) =>
                {
	                PersonajeContenedor = SistemaPrincipal.ObtenerControlador<ControladorPersonaje, ModeloPersonaje>((ModeloPersonaje)modelo);
                });
	        }
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Indica si puede cierto <see cref="ControladorItem"/> puede ser almacenado por este slot
        /// </summary>
        /// <param name="item">Item que ver si puede ser almacenado</param>
        /// <returns><see cref="bool"/> indicando si este <paramref name="item"/> puede ser almacenado</returns>
        public bool PuedeAlmacenarItem(ControladorItem item)
        {
	        return modelo.EspacioTotal - EspacioDisponible >= item.modelo.EspacioQueOcupa;
        }

        /// <summary>
        /// Almacena un item
        /// </summary>
        /// <param name="item">Item a almacenar</param>
        /// <returns><see cref="bool"/> indicando si el item fue almacenado exitosamente</returns>
        public bool AlmacenarItem(ControladorItem item)
        {
	        if (!PuedeAlmacenarItem(item))
		        return false;

            ControladoresItemsAlmacenados.Add(item);

            if (!modelo.ItemsAlmacenados.Contains(item.modelo))
            {
	            modelo.ItemsAlmacenados.Add(item.modelo);

	            OnItemAlmacenado(item);
	            OnElementoAlmacenado(item.modelo, item);
            }

            item.OnItemEliminado += ItemEliminadoHandler;

            SistemaPrincipal.LoggerGlobal.Log($"{item} almacenado con exito en {this}", ESeveridad.Debug);

            return true;
        }

        /// <summary>
        /// Almacena un item utilizando su modelo. Si se sabe de la existencia de su <see cref="ControladorItem"/>
        /// es una mejor idea utilizar la sobrecarga que toma el controlador directamente
        /// </summary>
        /// <param name="item">Modelo del item que se quiere almacenar</param>
        public void AlmacenarItem(ModeloItem item)
        {
	        var controladorItem = SistemaPrincipal.ObtenerControlador<ControladorItem, ModeloItem>(item);

	        if (controladorItem != null)
	        {
		        AlmacenarItem(controladorItem);

                return;
	        }

	        item.OnControladorCreado += ControladorElementoCreadoHandler;
        }

        /// <summary>
        /// Almacena una parte del cuerpo utilizando su modelo. Si se sabe de la existencia de su <see cref="ControladorParteDelCuerpo"/>
        /// es una mejor idea utilizar la sobrecarga que toma el controlador directamente
        /// </summary>
        /// <param name="parteDelCuerpo">Modelo de la parte del cuerpo que se queire almacenar</param>
        public void AlmacenarParteDelCuerpo(ModeloParteDelCuerpo parteDelCuerpo)
        {
	        var controladorParteDelCuerpo = SistemaPrincipal.ObtenerControlador<ControladorParteDelCuerpo, ModeloParteDelCuerpo>(parteDelCuerpo);

	        if (controladorParteDelCuerpo != null)
	        {
		        AlmacenarParteDelCuerpo(controladorParteDelCuerpo);

		        return;
	        }

	        parteDelCuerpo.OnControladorCreado += ControladorElementoCreadoHandler;
        }

        /// <summary>
        /// Quita un item del slot
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool QuitarItem(ControladorItem item)
        {
	        if (!ControladoresItemsAlmacenados.Remove(item))
		        return false;

	        if (!modelo.ItemsAlmacenados.Remove(item.modelo))
	        {
		        ControladoresItemsAlmacenados.Add(item);

                return false;
            }

	        OnItemQuitado(item);
	        OnElementoQuitado(item.modelo, item);

	        item.OnItemEliminado -= ItemEliminadoHandler;

            SistemaPrincipal.LoggerGlobal.Log($"{item} quitado de {this} con exito", ESeveridad.Debug);

            return true;
        }

        /// <summary>
        /// Almacena una parte del cuerpo en este slot
        /// </summary>
        /// <param name="parteDelCuerpo">Controlador de la parte del cuerpo que se quiere almacenar</param>
        public void AlmacenarParteDelCuerpo(ControladorParteDelCuerpo parteDelCuerpo)
        {
	        ParteDelCuerpoAlmacenada = parteDelCuerpo;

	        if (modelo.ParteDelCuerpoAlmacenada != parteDelCuerpo.modelo)
	        {
		        modelo.ParteDelCuerpoAlmacenada = parteDelCuerpo.modelo;

		        OnParteDelCuerpoAlmacenada(parteDelCuerpo);
		        OnElementoAlmacenado(parteDelCuerpo.modelo, parteDelCuerpo);
	        }

	        ParteDelCuerpoAlmacenada.OnParteDelCuerpoEliminada += ParteDelCuerpoEliminadaHandler;

            SistemaPrincipal.LoggerGlobal.Log($"{parteDelCuerpo} almacenada con exito en {this}", ESeveridad.Debug);
        }

        /// <summary>
        /// Quita la parte del cuerpo almacenada en este slot
        /// </summary>
        public void QuitarParteDelCuerpo()
        {
            if(ParteDelCuerpoAlmacenada == null)
                return;

	        OnParteDelCuerpoQuitada(ParteDelCuerpoAlmacenada);
	        OnElementoQuitado(ParteDelCuerpoAlmacenada.modelo, ParteDelCuerpoAlmacenada);

	        ParteDelCuerpoAlmacenada.OnParteDelCuerpoEliminada -= ParteDelCuerpoEliminadaHandler;

	        ParteDelCuerpoAlmacenada = null;
	        modelo.ParteDelCuerpoAlmacenada = null;

            SistemaPrincipal.LoggerGlobal.Log($"Parte del cuerpo almacenada en {this} quitada con exito", ESeveridad.Debug);
        }

        public override async Task Recargar()
        { 
	        await base.Recargar();

            //Solo nos interesa lidiar con el caso de que se hayan añadido mas items puesto que estamos subscritos al evento
            //de item eliminado asi que si ocurre el caso opuesto nos encargaremos automaticamente
	        if (modelo.ItemsAlmacenados.Count > ControladoresItemsAlmacenados.Count)
	        {
		        var diferencia = modelo.ItemsAlmacenados.Count - ControladoresItemsAlmacenados.Count;

		        for (int i = ControladoresItemsAlmacenados.Count - 1; i < diferencia; ++i)
			        AlmacenarItem(modelo.ItemsAlmacenados[i]);
	        }

            SistemaPrincipal.LoggerGlobal.Log($"{this} recargado", ESeveridad.Debug);
        }

        /// <summary>
        /// Metodo encargado de almacenar elementos al slot cuando son cargados
        /// </summary>
        /// <param name="elemento"><see cref="ModeloBase"/> cuyo controlador fue creado</param>
        /// <param name="controlador"><see cref="ControladorBase"/> controlador del <paramref name="elemento"/></param>
        private void ControladorElementoCreadoHandler(ModeloBase elemento, ControladorBase controlador)
        {
	        elemento.OnControladorCreado -= ControladorElementoCreadoHandler;

	        if (controlador is ControladorItem item)
	        {
		        AlmacenarItem(item);
	        }
	        else if (controlador is ControladorParteDelCuerpo parteDelCuerpo)
	        {
                AlmacenarParteDelCuerpo(parteDelCuerpo);
	        }
	        else
	        {
                SistemaPrincipal.LoggerGlobal.Log($"{nameof(controlador)} no es de un tipo aceptado", ESeveridad.Error);
	        }
        }

        /// <summary>
        /// Metodo encargado de lidiar con el evento de que un <see cref="ControladorItem"/> almacenado sea eliminado
        /// </summary>
        /// <param name="item">Controlador del item que fue eliminado</param>
        /// <param name="portador">Controlador del personaje al que pertenece este item</param>
        private void ItemEliminadoHandler(ControladorItem item, ControladorPersonaje portador)
        {
	        SistemaPrincipal.LoggerGlobal.Log($"quitando {item} porque fue eliminado", ESeveridad.Debug);

	        QuitarItem(item);
        }

        /// <summary>
        /// Metodo encargado de lidiar con el evento de que un <see cref="ControladorParteDelCuerpo"/> almacenado sea eliminado
        /// </summary>
        /// <param name="partedelCuerpo">Controlador de la parte del cuerpo que fue eliminada</param>
        /// <param name="dueño">Controlador del personaje al que pertenece esta parte del cuerpo</param>
        private void ParteDelCuerpoEliminadaHandler(ControladorParteDelCuerpo partedelCuerpo, ControladorPersonaje dueño)
        {
	        SistemaPrincipal.LoggerGlobal.Log($"quitando {partedelCuerpo} porque fue eliminado", ESeveridad.Debug);

	        QuitarParteDelCuerpo();
        }

        public override string ToString()
        {
	        if (ControladoresItemsAlmacenados.Count > 0)
	        {
		        if (ControladoresItemsAlmacenados.Count == 1)
		        {
			        return $"{ControladoresItemsAlmacenados[0].modelo.Nombre}";
		        }
		        else
		        {
			        return $"{ControladoresItemsAlmacenados.Count} items almacenados";
		        }
	        }
            else if (ParteDelCuerpoAlmacenada != null)
	        {
		        return ParteDelCuerpoAlmacenada.modelo.Nombre;
	        }
	        else
	        {
		        return modelo.NombreSlot.IsNullOrWhiteSpace() ? string.Intern("Slot vacio") : modelo.NombreSlot;
	        }
        }

        #endregion
    }
}