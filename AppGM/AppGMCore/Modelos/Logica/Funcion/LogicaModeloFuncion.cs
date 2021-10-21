namespace AppGM.Core
{
	public partial class ModeloFuncion
	{
		/// <summary>
		/// Obtiene el <see cref="ModeloBase"/> que contiene a esta funcion
		/// </summary>
		/// <returns><see cref="ModeloBase"/> que contiene a esta funcion</returns>
		public ModeloBase ObtenerModeloContenedor()
		{
			if (EfectoContenedor != null)
			{
				return EfectoContenedor.Efecto;
			}
			else
			{
				return HabilidadContenedora.Habilidad;
			}
		}

		public override ModeloPersonaje ObtenerPersonajeContenedor()
		{
			if(EfectoContenedor != null)
			{
				return EfectoContenedor.Efecto.HabilidadDueña.ObtenerPersonajeContenedor();
			}
			else
			{
				return HabilidadContenedora.Habilidad.ObtenerPersonajeContenedor();
			}
		}
	}
}
