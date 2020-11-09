using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloBaseSK{}
    public class ModeloBase : ModeloBaseSK
    {   
        //Id
        [Key]
        public int Id { get; set; }
    }
}
