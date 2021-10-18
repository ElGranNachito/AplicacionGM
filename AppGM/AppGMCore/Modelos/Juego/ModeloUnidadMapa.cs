using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo utilizado para representar la posicion de un elemento en el mapa.
    /// </summary>
	public class ModeloUnidadMapa : ModeloBase
    {
        /// <summary>
        /// Nombre de la unidad
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string Nombre { get; set; }

        /// <summary>
        /// Tipo de la unidad
        /// </summary>
        public ETipoUnidad ETipoUnidad { get; set; }

        /// <summary>
        /// Posicion de la unidad sobre el mapa
        /// </summary>
        public virtual ModeloVector2 Posicion { get; set; }

        /// <summary>
        /// Personaje que representa la unidad
        /// </summary>
        public virtual ModeloPersonaje Personaje { get; set; }

        /// <summary>
        /// Mapa al que pertenece esta unidad
        /// </summary>
        public virtual ModeloMapa Mapa { get; set; }
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
        [Column(TypeName = "varchar(1)")]
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
