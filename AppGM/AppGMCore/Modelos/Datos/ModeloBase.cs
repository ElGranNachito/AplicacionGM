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
		public virtual bool EsValido { get; set; }

        //Id
        [Key]
        public int Id
        {
	        get => mId;
            set
            {
                if(value == 0)
                    return;

                //Intentamos quitar el modelo del sistema principal en caso de que ya existiera
                SistemaPrincipal.QuitarModelo(this);

				mId = value;

                //Añadimos el modelo al sistema principal
	            SistemaPrincipal.AñadirModelo(this);
            }
        }
	}
}
