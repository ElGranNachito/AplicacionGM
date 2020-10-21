using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloAccion
    {
        //Id
        [Key]
        public int IdAccion { get; set; }

        [StringLength(2000)]
        public string Descripcion { get; set; }
    }
}