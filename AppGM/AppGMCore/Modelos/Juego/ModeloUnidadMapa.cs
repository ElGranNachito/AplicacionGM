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
    public class ModeloUnidadMapa
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }

        public ETipoUnidad ETipoUnidad { get; set; }

        public TIUnidadMapaVector2   Posicion { get; set; }
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
        public int Cantidad { get; set; }
        public bool EsDeMaster { get; set; }
    }
}
