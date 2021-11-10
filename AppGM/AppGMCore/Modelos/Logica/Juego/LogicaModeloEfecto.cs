namespace AppGM.Core
{
	/// <summary>
	/// Contiene la logica para <see cref="ModeloEfecto"/>
	/// </summary>
	public partial class ModeloEfecto
	{
		/// <summary>
		/// Obtiene el <see cref="ModeloPersonaje"/> que contiene este efecto
		/// </summary>
		/// <returns><see cref="ModeloPersonaje"/> que contiene este efecto</returns>
		public ModeloPersonaje ObtenerPersonajeContenedor() =>
			HabilidadContenedora?.Dueño ?? ItemContenedor?.PersonajePortador;

		/// <summary>
		/// Obtiene el <see cref="ModeloBase"/> que contiene este efecto
		/// </summary>
		/// <returns><see cref="ModeloBase"/> que contiene este efecto</returns>
		public ModeloBase ObtenerModeloContenedor()
			=> HabilidadContenedora != null ? HabilidadContenedora : ItemContenedor;
	}
}
