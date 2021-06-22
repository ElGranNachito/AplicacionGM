using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AppGM.Core
{
	/// <summary>
	/// <see cref="ViewModel"/> para representar un control de tipo ComboBox
	/// </summary>
	/// <typeparam name="TipoValor"><see cref="Type"/> del valor que almacenan sus opciones</typeparam>
	public class ViewModelComboBox<TipoValor>
	{
		/// <summary>
		/// <see cref="List{T}"/> de <see cref="ViewModelItemComboBoxBase{TipoValor}"/> que puede
		/// seleccionar el usuario
		/// </summary>
		public ViewModelListaDeElementos<ViewModelItemComboBoxBase<TipoValor>> ValoresPosibles { get; set; } = new ViewModelListaDeElementos<ViewModelItemComboBoxBase<TipoValor>>();

		/// <summary>
		/// <see cref="ViewModelItemComboBoxBase{TipoValor}"/> seleccionado
		/// </summary>
		public ViewModelItemComboBoxBase<TipoValor> ValorSeleccionado { get; set; }

		/// <summary>
		/// <see cref="TipoValor"/> seleccionado por el usuario
		/// </summary>
		[MaybeNull]
		public TipoValor Valor => ValorSeleccionado.valor;

		public void ActualizarValoresPosibles(List<TipoValor> nuevosValoresPosibles)
		{
			var nuevosItemsComboBox = 
				nuevosValoresPosibles.Select(valor => new ViewModelItemComboBoxBase<TipoValor> {Texto = valor.ToString(), valor = valor});

			ValoresPosibles.Elementos = new ObservableCollection<ViewModelItemComboBoxBase<TipoValor>>(nuevosItemsComboBox);
		}
	}
}
