namespace AppGM.Core
{
	/// <summary>
	/// Representa a un <see cref="IInfligidorDaño"/>
	/// </summary>
	public partial class ModeloInfligidorDaño : ModeloBase
	{
		/// <summary>
		/// Tipo del infligidor de daño representado
		/// </summary>
		public ETipoInfligidorDaño Tipo { get; set; }

		/// <summary>
		/// Argumentos del daño infligido
		/// </summary>
		public virtual ModeloArgumentosDaño ArgumentosDaño { get; set; }

		/// <summary>
		/// Modelo del personaje representado
		/// </summary>
		public virtual ModeloPersonaje Personaje { get; set; }

		/// <summary>
		/// Modelo del item representado
		/// </summary>
		public virtual ModeloItem Item { get; set; }

		/// <summary>
		/// Modelo de la habilidad representada
		/// </summary>
		public virtual ModeloHabilidad Habilidad { get; set; }
	}
}
