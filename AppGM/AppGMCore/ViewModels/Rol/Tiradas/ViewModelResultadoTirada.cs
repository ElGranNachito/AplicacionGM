namespace AppGM.Core
{
	/// <summary>
	/// <see cref="ViewModel"/> que representa a un <see cref="ResultadoTirada"/>
	/// </summary>
	public class ViewModelResultadoTirada
	{
		#region Campos & Propiedades

		/// <summary>
		/// Resultado representado por esta instancia
		/// </summary>
		public readonly ResultadoTirada resultadoTirada;

		/// <summary>
		/// Tirada a la que pertenece este resultado
		/// </summary>
		public readonly ControladorTiradaBase tirada;

		/// <summary>
		/// Argumentos con los que se realizo la tirada
		/// </summary>
		public readonly ArgumentosTirada argsTirada;

		/// <summary>
		/// Resultado de la tirada
		/// </summary>
		public int Resultado => resultadoTirada.resultado;

		/// <summary>
		/// Resultado detallado de la tirada
		/// </summary>
		public string ResultadoDetallado => resultadoTirada.resultadoDetallado;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_resultadoTirada">Resultado de tirada que sera representado por esta instancia</param>
		public ViewModelResultadoTirada(ResultadoTirada _resultadoTirada, ArgumentosTirada _argsTirada, ControladorTiradaBase _tirada)
		{
			resultadoTirada = _resultadoTirada;
			argsTirada = _argsTirada;
			tirada = _tirada;
		}

		#endregion

		#region Metodos

		public override string ToString() => resultadoTirada.ToString(); 

		#endregion
	}
}