using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Clase que contiene la logica del <see cref="ModeloConVariablesYTiradas"/>
	/// </summary>
	public abstract partial class ModeloConVariablesYTiradas
	{
		/// <summary>
		/// Obtiene los <see cref="ModeloVariableBase"/> disponibles para el modelo
		/// </summary>
		/// <returns><see cref="IReadOnlyList{T}"/> con los <see cref="ModeloVariableBase"/> disponibles</returns>
		public virtual IReadOnlyList<ModeloVariableBase> ObtenerVariablesDisponibles() => Variables.AsReadOnly();

		/// <summary>
		/// Obtiene el <see cref="ModeloPersonaje"/> al que pertenece este modelo
		/// </summary>
		/// <returns><see cref="ModeloPersonaje"/> al que pertenece este modelo</returns>
		public abstract ModeloPersonaje ObtenerPersonajeContenedor();
	}
}
