using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public abstract class ModeloBaseSK{}
    public class ModeloBase : ModeloBaseSK
    {   
        //Id
        [Key]
        public int Id { get; set; }
    }
}
