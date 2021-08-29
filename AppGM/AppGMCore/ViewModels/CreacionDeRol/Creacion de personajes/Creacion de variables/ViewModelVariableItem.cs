using System.Collections.ObjectModel;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un <see cref="ModeloVariable{TipoVariable}"/> en una lista
	/// </summary>
	public class ViewModelVariableItem : ViewModelItemLista
	{
		/// <summary>
		/// Variable representada
		/// </summary>
		public ModeloVariableBase Variable { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_variable">Controlador de la variable que sera representada por este vm</param>
		public ViewModelVariableItem(ModeloVariableBase _variable)
		{
			Variable = _variable;

			//Intentamos obtener el controlador de la variable para obtener su valor actual
			var controladorVariable = SistemaPrincipal.ObtenerControlador<ControladorVariableBase, ModeloVariableBase>(Variable);

			CaracteristicasItem.Elementos = new ObservableCollection<ViewModelCaracteristicaItem>(new[]
			{
				new ViewModelCaracteristicaItem
				{
					Titulo = "Nombre variable",
					Valor = Variable.NombreVariable
				},

				new ViewModelCaracteristicaItem
				{
					Titulo = "Tipo variable",
					Valor = Variable.TipoVariable.ToString(),
				},

				new ViewModelCaracteristicaItem
				{
					Titulo = "Valor actual",
					Valor = controladorVariable?.ObtenerValorVariable()?.ToString() ?? "No disponible"
				}
			});
		}
	}
}
