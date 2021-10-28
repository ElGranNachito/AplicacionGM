using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Clase que contiene la logica para <see cref="ModeloItem"/>
	/// </summary>
	public partial class ModeloItem
	{
		public override ModeloPersonaje ObtenerPersonajeContenedor() => PersonajePortador;

		public override IReadOnlyList<ModeloVariableBase> ObtenerVariablesDisponibles()
		{
			var variablesDisponibles = new List<ModeloVariableBase>(PersonajePortador.Variables);

			variablesDisponibles.AddRange(Variables);

			return variablesDisponibles.AsReadOnly();
		}
	}
}
