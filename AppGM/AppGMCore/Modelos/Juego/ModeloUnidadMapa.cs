using System;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    /// <summary>
    /// Tipo de la unidad que esta posicionada sobre el mapa, se utiliza para determinar el tipo de imagen
    /// </summary>
    [Flags]
    public enum ETipoUnidad
    {
        NINGUNO    = 0,
        Master     = 1,
        Servant    = 2,
        Invocacion = 4,
        Trampa     = 8,
        Iglesia    = 16
    }
    public class ModeloUnidadMapa : ModeloBase
    {
        public ControladorUnidadMapa controladorUnidadMapa;

        /// <summary>
        /// Nombre de la unidad
        /// </summary>
        public string Nombre { get; set; }

        public ETipoUnidad ETipoUnidad { get; set; }

        /// <summary>
        /// Posicion de la unidad sobre el mapa
        /// </summary>
        public TIUnidadMapaVector2   Posicion { get; set; }
        /// <summary>
        /// Personaje que representa la unidad
        /// </summary>
        public TIPersonajeUnidadMapa Personaje { get; set; }
    }
    public class ModeloUnidadMapaMasterServant : ModeloUnidadMapa
    {
        public EClaseServant EClaseServant { get; set; }
    }

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
