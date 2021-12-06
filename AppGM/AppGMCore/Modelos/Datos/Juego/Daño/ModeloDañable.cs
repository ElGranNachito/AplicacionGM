namespace AppGM.Core
{
	/// <summary>
	/// Modelo que representa a un <see cref="IDañable"/> en un <see cref="ModeloArgumentosDaño"/>
	/// </summary>
	public class ModeloDañable : ModeloBase
	{
		/// <summary>
		/// Indice de este dañable en el orden de aplicacion de daño. Un valor de -1 indica que se trata del objetivo principal
		/// </summary>
		public int Indice { get; set; }

		/// <summary>
		/// Argumentos del daño causado a este objeto
		/// </summary>
		public virtual ModeloArgumentosDaño ArgumentosDaño { get; set; }

		/// <summary>
		/// Personaje representado
		/// </summary>
		[CopiarSuperficialmente]
		public virtual ModeloPersonaje Personaje { get; set; }

		/// <summary>
		/// Slot representado
		/// </summary>
		[CopiarSuperficialmente]
		public virtual ModeloSlot Slot { get; set; }

		/// <summary>
		/// Parte del cuerpo representada
		/// </summary>
		[CopiarSuperficialmente]
		public virtual ModeloParteDelCuerpo ParteDelCuerpo { get; set; }

		/// <summary>
		/// Item representado
		/// </summary>
		[CopiarSuperficialmente]
		public virtual ModeloItem Item { get; set; }
	}
}