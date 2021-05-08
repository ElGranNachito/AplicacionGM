using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    /// <summary>
    /// Clase base de todos los modelos, el sufijo SK indica que no tiene Key
    /// </summary>
    public abstract class ModeloBaseSK{}

    /// <summary>
    /// Clase base de todos los modelos con una key
    /// </summary>
    public class ModeloBase : ModeloBaseSK
    {   
        //Id
        [Key]
        public int Id { get; set; }
    }
}
