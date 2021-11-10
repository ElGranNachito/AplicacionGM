namespace AppGM.Core
{
	/// <summary>
	/// Especifica la forma en la que se reduce el daño
	/// </summary>
	public enum EMetodoDeReduccionDeDaño
	{
		/// <summary>
		/// Reduce el daño por un valor fijo
		/// </summary>
		ValorFijo,

		/// <summary>
		/// Reduce el daño en un porcentaje fijo
		/// </summary>
		Porcentual,

		/// <summary>
		/// Elimina el multiplicador de daño
		/// </summary>
		EliminacionDeMultiplicador,

		/// <summary>
		/// Reduce el daño completamente
		/// </summary>
		ReduccionCompleta
	}
}
