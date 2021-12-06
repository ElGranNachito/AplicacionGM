namespace AppGM.Core
{
	/// <summary>
	/// <see cref="ViewModel"/> que representa a un <see cref="ResultadoTirada"/>
	/// </summary>
	public class ViewModelResultadoTirada
	{
		/// <summary>
		/// Resultado representado por esta instancia
		/// </summary>
		public readonly ResultadoTirada resultadoTirada;

		/// <summary>
		/// Resultado de la tirada
		/// </summary>
		public int Resultado => resultadoTirada.resultado;

		/// <summary>
		/// Resultado detallado de la tirada
		/// </summary>
		public string ResultadoDetallado => resultadoTirada.resultadoDetallado;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_resultadoTirada">Resultado de tirada que sera representado por esta instancia</param>
		public ViewModelResultadoTirada(ResultadoTirada _resultadoTirada)
		{
			resultadoTirada = _resultadoTirada;
		}

		public override string ToString() => resultadoTirada.ToString();
	}
}