using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    /// <summary>
    /// Modelo de una accion que realiza un personaje durante un combate
    /// </summary>
    public class ModeloAccion : ModeloBase
    {
        [StringLength(2000)]
        public string Descripcion { get; set; }
    }
}