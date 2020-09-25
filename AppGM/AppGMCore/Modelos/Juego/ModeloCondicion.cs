using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloCondicion
    {
        //Id
        [Key]
        public int IdCondicion { get; set; }

        public string NombrePropiedad { get; set; }
        public string OperacionARealizar { get; set; }
        public string ValorContraElQueComparar { get; set; }
    }
}