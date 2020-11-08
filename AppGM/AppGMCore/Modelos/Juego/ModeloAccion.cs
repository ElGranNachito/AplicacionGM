using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloAccion : ModeloBase
    {
        [StringLength(2000)]
        public string Descripcion { get; set; }
    }
}