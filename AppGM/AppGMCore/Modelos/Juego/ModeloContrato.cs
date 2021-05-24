using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para un contrato
    /// </summary>
    public class ModeloContrato : ModeloBase
    {
        //Titulo del contrato
        [StringLength(50)]
        public string Nombre { get; set; }
        //Descripcion del contrato
        [StringLength(500)]
        public string Descripcion { get; set; }

        //Estado del contrato
        public bool EsVigente { get; set; }

        //Personajes que participen en el contrato
        public List<TIPersonajeContrato> PersonajesAfectados { get; set; } = new List<TIPersonajeContrato>();
    }
}
