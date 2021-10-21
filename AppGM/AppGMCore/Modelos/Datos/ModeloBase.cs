using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
	/// <summary>
	/// Clase base de todos los modelos, el sufijo SK indica que no tiene Key
	/// </summary>
	public abstract partial class ModeloBaseSK{}

    /// <summary>
    /// Clase base de todos los modelos con una key
    /// </summary>
    public partial class ModeloBase : ModeloBaseSK
    {
        /// <summary>
        /// Contiene el valor de <see cref="Id"/>
        /// </summary>
	    private int mId;

		/// <summary>
		/// Indica si este modelo es valido
		/// </summary>
		public bool EsValido { get; set; }

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
}
