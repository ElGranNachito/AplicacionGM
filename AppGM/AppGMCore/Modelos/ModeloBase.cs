using System.Collections.Generic;
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
        /// <summary>
        /// Contiene el valor de <see cref="Id"/>
        /// </summary>
	    private int mId;

        //Id
        [Key]
        public int Id
        {
	        get => mId;
            set
            {
                //Si cambia la id y la anterior no es cero entonces intentamos quitar el modelo ya existente del sistema principal
	            if (value != 0 && mId != 0)
		            SistemaPrincipal.QuitarModelo(this);

				mId = value;

                //Añadimos el modelo al sistema principal
	            SistemaPrincipal.AñadirModelo(this);
            }
        }
    }

    /// <summary>
    /// Clase base para todos los modelos que deban almacenar <see cref="ModeloVariableBase"/> y <see cref="ModeloTiradaBase"/>
    /// </summary>
    public abstract class ModeloConVariablesYTiradas : ModeloBase
    {
	    /// <summary>
	    /// Variables persistentes que contiene este modelo
	    /// </summary>
	    public virtual List<ModeloVariableBase> Variables { get; set; } = new List<ModeloVariableBase>();

	    /// <summary>
	    /// Tiradas que contiene este modelo
	    /// </summary>
	    public virtual List<ModeloTiradaBase> Tiradas { get; set; } = new List<ModeloTiradaBase>();
    }
}
