using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de datos para una alianza
    /// </summary>
    public class ModeloAlianza : ModeloBase
    {
        //Titulo de la alianza
        [StringLength(50)]
        public string Nombre { get; set; }
        //Descripcion de la alianza
        [StringLength(500)]
        public string Descripcion { get; set; }

        //Estado de la alianza
        public bool EsVigente { get; set; }

        //Contrato magico adicional a la alianza
        public TIAlianzaContrato ContratoDeAlianza { get; set; }

        //Personajes que participen en la alianza
        public List<TIPersonajeAlianza> PersonajesAfectados { get; set; } = new List<TIPersonajeAlianza>();
    }
}
