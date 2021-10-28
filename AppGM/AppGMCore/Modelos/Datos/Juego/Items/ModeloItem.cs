using System.Collections.Generic;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para el utilizable
    /// </summary>
    public partial class ModeloItem : ModeloConVariablesYTiradas, IModeloConSlots
    {
	    /// <summary>
        /// Peso del utilizable
        /// </summary>
        public decimal Peso { get; set; }

	    /// <summary>
	    /// Slots que ocupa el item
	    /// </summary>
	    public decimal EspacioQueOcupa { get; set; }

        /// <summary>
        /// Nombre del utilizable
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Descripcion del utilizable
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Condicion porcentual del item
        /// </summary>
        public int Estado { get; set; }

        /// <summary>
        /// Personaje que porta este utilizable
        /// </summary>
        public virtual ModeloPersonaje PersonajePortador { get; set; }

        /// <summary>
        /// Handlers vinculados a eventos del controlador de este utilizable
        /// </summary>
        public virtual List<TIFuncionHandlerEvento<ModeloItem>> HandlersEventos { get; set; } = new List<TIFuncionHandlerEvento<ModeloItem>>();

        /// <summary>
        /// Funciones de este efecto
        /// </summary>
        public virtual List<TIFuncionItem> Funciones { get; set; } = new List<TIFuncionItem>();

        /// <summary>
        /// Slots que ocupa este item
        /// </summary>
        public virtual List<ModeloSlot> SlotsQueOcupa { get; set; } = new List<ModeloSlot>();

        /// <summary>
        /// Slots que contiene este item
        /// </summary>
        public virtual List<ModeloSlot> Slots { get; set; } = new List<ModeloSlot>();
    }
}