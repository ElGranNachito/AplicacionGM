namespace AppGM.Core
{
	public partial class ModeloParteDelCuerpo
	{
		/// <summary>
		/// Contiene el valor de <see cref="PersonajeContenedor"/>
		/// </summary>
		private ModeloPersonaje mPersonajeContenedor;

		public int ObtenerProfundidad(int profundidadActual = 0)
		{
			return SlotContenedor?.ObtenerProfundidad(profundidadActual) ?? profundidadActual;
		}
	}
}
