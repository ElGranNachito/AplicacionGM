namespace AppGM.Core
{
	public abstract class ViewModelItemAutocompletadoBase : ViewModel
	{
		/// <summary>
		/// Contiene una cadena con datos para representar este item
		/// </summary>
		public string RepresentacionTextual { get; set; }

		/// <summary>
		/// Contiene una cadena con mas datos de este item para mostrar aparte
		/// de <see cref="RepresentacionTextual"/>
		/// </summary>
		public string DatosExtra { get; set; }

		/// <summary>
		/// Indica si el usuario actualmente tiene este valor seleccionado
		/// </summary>
		public bool EstaSeleccionado { get; set; }

		/// <summary>
		/// Obtiene una cadena que representa este <see cref="ViewModelItemAutocompletadoBase"/>
		/// y actualiza los valores de <see cref="RepresentacionTextual"/> y posiblemente <see cref="DatosExtra"/>
		/// </summary>
		protected abstract void ActualizarRepresentacionTextual();

		/// <summary>
		/// Comprueba si este item es abarcado por una cadena
		/// </summary>
		/// <param name="cadena">Cadena contra la que comparar</param>
		/// <param name="comparacionExacta">Indica si la <paramref name="cadena"/> tiene que ser exactamente igual a la comparada</param>
		/// <returns><see cref="bool"/> resultante</returns>
		public abstract bool Comparar(string cadena, bool comparacionExacta = false);
	}
}
