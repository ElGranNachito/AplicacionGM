using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
	/// <summary>
	/// Clase base de todos los modelos, el sufijo SK indica que no tiene Key
	/// </summary>
	public abstract class ModeloBaseSK
	{
        /// <summary>
        /// Guarda el modelo a la base de datos
        /// </summary>
		public virtual void Guardar() => SistemaPrincipal.GuardarModelo(this);

        /// <summary>
        /// Elimina el modelo de la base de datos
        /// </summary>
		public virtual void Eliminar() => SistemaPrincipal.EliminarModelo(this);

        /// <summary>
        /// Crea una copia superficial de este modelo
        /// </summary>
        /// <returns></returns>
        public ModeloBase Clonar() => (ModeloBase)MemberwiseClone();

	}

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
