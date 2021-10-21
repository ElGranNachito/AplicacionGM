using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Clase base para todos los modelos que deban almacenar <see cref="ModeloVariableBase"/> y <see cref="ModeloTiradaBase"/>
	/// </summary>
	public abstract partial class ModeloConVariablesYTiradas : ModeloBase
	{
		/// <summary>
		/// Variables persistentes que contiene este modelo
		/// </summary>
		public virtual List<ModeloVariableBase> Variables { get; set; } = new List<ModeloVariableBase>();

		/// <summary>
		/// Tiradas que contiene este modelo
		/// </summary>
		public virtual List<ModeloTiradaBase> Tiradas { get; set; } = new List<ModeloTiradaBase>();	
	}
}