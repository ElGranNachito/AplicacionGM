namespace AppGM.Core
{
	/// <summary>
	/// Representa un item en la lista del menu de autocompletado
	/// </summary>
	public abstract class ViewModelItemAutocompletado<TipoValor> : ViewModelItemAutocompletadoBase
	{
		#region Campos & Propiedades

		/// <summary>
		/// Valor que tiene este item
		/// </summary>
		public TipoValor valorItem;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_valor"></param>
		public ViewModelItemAutocompletado(TipoValor _valor)
		{
			valorItem = _valor;
		}

		#endregion
	}
}
