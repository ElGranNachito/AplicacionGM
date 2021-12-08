namespace AppGM.Core
{
	/// <summary>
	/// Representa el tipo de contenido por un <see cref="ModeloInfligidorDaño"/>
	/// </summary>
	public enum ETipoInfligidorDaño
	{
		/// <summary>
		/// <see cref="ControladorPersonaje"/>
		/// </summary>
		Personaje = 1<<0,

		/// <summary>
		/// <see cref="ControladorItem"/>
		/// </summary>
		Item = 1<<1,

		/// <summary>
		/// <see cref="ControladorHabilidad"/>
		/// </summary>
		Habilidad = 1<<2
	}
}
