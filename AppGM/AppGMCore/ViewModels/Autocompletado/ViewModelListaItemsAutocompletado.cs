using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AppGM.Core
{
	/// <summary>
	/// Representa la lista de items elegibles de una <see cref="ViewModelVentanaAutocompletado{TipoValor}"/>
	/// </summary>
	/// <typeparam name="TipoValor">Tipo del valor que contienen <see cref="ViewModelItemAutocompletado{TipoValor}"/> contenidos por <see cref="Items"/></typeparam>
	public class ViewModelListaItemsAutocompletado : ViewModel
	{
		/// <summary>
		/// Items
		/// </summary>
		public ObservableCollection<ViewModelItemAutocompletadoBase> Items { get; set; } =
			new ObservableCollection<ViewModelItemAutocompletadoBase>();

		/// <summary>
		/// Actualiza <see cref="Items"/>
		/// </summary>
		/// <param name="nuevosValores">Lista con los nuevos <see cref="ViewModelItemAutocompletado{TipoValor}"/></param>
		public void ActualizarItems(List<ViewModelItemAutocompletadoBase> nuevosValores)
		{
			Items.Clear();

			foreach (var valor in nuevosValores)
				Items.Add(valor);
		}
	}
}