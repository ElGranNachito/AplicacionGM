using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppGM.Core
{
    /// <summary>
    /// Controlador de un <see cref="ModeloSlot"/>
    /// </summary>
    public class ControladorSlot : Controlador<ModeloSlot>
    {
	    #region Eventos

	    public delegate void dItemAlmacenado(ModeloItem item);

	    public delegate void dElementoAlmacenado(IContenedorDeBloques elemento);

	    public event dItemAlmacenado OnItemAlmacenado = delegate { };
	    public event dElementoAlmacenado OnElementoAlmacenado = delegate { };

	    #endregion

        #region Campos & Propiedades

        /// <summary>
        /// Espacio disponible en este slot
        /// </summary>
        public decimal EspacioDisponible => modelo.EspacioDisponible;

        /// <summary>
        /// Indica si este slot se encuentra vacio
        /// </summary>
        public bool EstaVacio => ParteDelCuerpoAlmacenada == null && ControladoresItemsAlmacenados.Count > 0;

        /// <summary>
        /// Lista con todos los <see cref="ControladorItem"/> guardados
        /// </summary>
        public List<ControladorItem> ControladoresItemsAlmacenados { get; set; } = new List<ControladorItem>();

        /// <summary>
        /// Controlador de la parte del cuerpo almacenada en este slot
        /// </summary>
        public ControladorParteDelCuerpo ParteDelCuerpoAlmacenada { get; set; }

        #endregion
        
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_modeloSlot"><see cref="ModeloSlot"/> representado por este controlador</param>
        public ControladorSlot(ModeloSlot _modeloSlot)
	        : base(_modeloSlot)
        {
	        foreach (var item in modelo.ItemsAlmacenados)
	        {
				ControladoresItemsAlmacenados.Add(SistemaPrincipal.ObtenerControlador<ControladorItem, ModeloItem>(item, true));		        
	        }

	        if (modelo.ParteDelCuerpoAlmacenada != null)
	        {
		        ParteDelCuerpoAlmacenada = SistemaPrincipal.ObtenerControlador<ControladorParteDelCuerpo, ModeloParteDelCuerpo>(modelo.ParteDelCuerpoAlmacenada, true);
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

            modelo.ItemsAlmacenados.Add(item.modelo);

            return false;
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

	        return true;
        }

        public void AlmacenarParteDelCuerpo(ControladorParteDelCuerpo parteDelCuerpo)
        {
	        ParteDelCuerpoAlmacenada = parteDelCuerpo;

	        modelo.ParteDelCuerpoAlmacenada = parteDelCuerpo.modelo;
        }

        public override Task Recargar()
        {
            //TODO: Implementar
	        return base.Recargar();
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