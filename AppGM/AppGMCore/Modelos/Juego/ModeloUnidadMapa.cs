using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo utilizado para representar la posicion de un elemento en el mapa.
    /// </summary>
	public class ModeloUnidadMapa : ModeloBase
    {
        /// <summary>
        /// Controlador
        /// </summary>
        public ControladorUnidadMapa controladorUnidadMapa;

        /// <summary>
        /// Nombre de la unidad
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Tipo de la unidad
        /// </summary>
        public ETipoUnidad ETipoUnidad { get; set; }

        /// <summary>
        /// Posicion de la unidad sobre el mapa
        /// </summary>
        public virtual TIUnidadMapaVector2   Posicion { get; set; }

        /// <summary>
        /// Personaje que representa la unidad
        /// </summary>
        public virtual TIPersonajeUnidadMapa Personaje { get; set; }
    }

    /// <summary>
    /// Modelo utilizado para representar a un <see cref="ModeloMaster"/> o un <see cref="ModeloServant"/> en el mapa
    /// </summary>
    public class ModeloUnidadMapaMasterServant : ModeloUnidadMapa
    {
        /// <summary>
        /// Clase del servant. En caso de ser un master esto se refiere a la clase del servant que controla
        /// </summary>
        public EClaseServant EClaseServant { get; set; }
    }

    /// <summary>
    /// Modelo utilizado para repsentar a una <see cref="ModeloInvocacion"/> en el mapa
    /// </summary>
    public class ModeloUnidadMapaInvocacionTrampa : ModeloUnidadMapaMasterServant
    {
        [StringLength(1)]
        public string Inicial { get; set; }

        /// <summary>
        /// Cantidad de trampas o invocaciones sobre esa unidad
        /// </summary>
        public int Cantidad { get; set; }
        
        /// <summary>
        /// Siendo que puede ser de un master o un servant, su imagen imagen cambiara independientemente de la clase del servant
        /// </summary>
        public bool EsDeMaster { get; set; }
    }
}
