using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un <see cref="ControladorVariableBase"/> en un autocompletado
	/// </summary>
	public sealed class ViewModelItemAutocompletadoVariablePersistente : ViewModelItemAutocompletadoBase
	{
		#region Campos

		/// <summary>
		/// Controlador de la variable representada por este item
		/// </summary>
		public readonly ControladorVariableBase controladorVariable; 

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_controladorVariable">Controlador de la variable representada por este item</param>
		public ViewModelItemAutocompletadoVariablePersistente(ControladorVariableBase _controladorVariable)
		{
			//Nos aseguramos de que el controlador que nos pasaron no sea null
			if (_controladorVariable == null)
			{
				SistemaPrincipal.LoggerGlobal.Log($"{nameof(_controladorVariable)} no puede ser null", ESeveridad.Error);
			}

			controladorVariable = _controladorVariable;

			ActualizarRepresentacionTextual();
		}
		
		#endregion

		#region Metodos

		protected override void ActualizarRepresentacionTextual()
		{
			RepresentacionTextual = controladorVariable.NombreVariable;

			DatosExtra = controladorVariable.TipoVariable.Name;
		}

		public override bool Comparar(string cadena, bool comparacionExacta = false)
		{
			if (!comparacionExacta)
				return controladorVariable.NombreVariable.Contains(cadena);

			return controladorVariable.NombreVariable == cadena;
		} 

		#endregion
	}
}
