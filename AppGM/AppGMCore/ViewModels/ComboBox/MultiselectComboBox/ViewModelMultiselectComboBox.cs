using System;
using System.Collections.Generic;
using System.Linq;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa un combobox que permite seleccionar multiples elementos
	/// </summary>
	/// <typeparam name="TItems"></typeparam>
	public class ViewModelMultiselectComboBox<TItems> : ViewModel
	{
		#region Eventos

		/// <summary>
		/// Evento que se dispara cuando <see cref="ViewModelMultiselectComboBoxItem{TValor}.EstaSeleccionado"/> de alguno de los items cambia
		/// </summary>
		public event Action<ViewModelMultiselectComboBoxItem<TItems>> OnEstadoSeleccionItemCambio = delegate { };

		#endregion

		#region Propiedades

		/// <summary>
		/// Items contenidos por este combo box
		/// </summary>
		public ViewModelListaDeElementos<ViewModelMultiselectComboBoxItem<TItems>> Items { get; set; } = new ViewModelListaDeElementos<ViewModelMultiselectComboBoxItem<TItems>>();

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="items"></param>
		public ViewModelMultiselectComboBox(List<ViewModelMultiselectComboBoxItem<TItems>> items)
		{
			Items.AddRange(items);

			Items.Elementos.CollectionChanged += (sender, args) =>
			{
				if (args.NewItems is not null)
				{
					//Nos subscribimos al evento se esta de seleccion modificado de cada nuevo item
					foreach (var nuevoItem in args.NewItems.Cast<ViewModelMultiselectComboBoxItem<TItems>>())
					{
						nuevoItem.OnSeleccionadoCambio += EstadoSeleccionItemCambioHandler;
					}
				}

				if(args.OldItems is null)
					return;

				//Nos desubscribimos del evento de esta de seleccion cambio de cada item removido
				foreach (var antiguoItem in args.OldItems.Cast<ViewModelMultiselectComboBoxItem<TItems>>())
				{
					if (!Items.Contiene(antiguoItem))
						antiguoItem.OnSeleccionadoCambio -= EstadoSeleccionItemCambioHandler;
				}
			};
		} 

		#endregion

		#region Metodos

		/// <summary>
		/// Selecciona o deselecciona un <paramref name="itemQueSeleccionar"/>
		/// </summary>
		/// <param name="itemQueSeleccionar">Item cuyo estado de seleccion se desea modificar</param>
		/// <param name="estaSeleccionado">Nuevo estado de seleccion que se le dara al item</param>
		public void ModificarEstadoSeleccionItem(TItems itemQueSeleccionar, bool estaSeleccionado)
		{
			if (Items.Find(i => EqualityComparer<TItems>.Default.Equals(i.Valor, itemQueSeleccionar)) is { } itemEncontrado)
			{
				itemEncontrado.EstaSeleccionado = estaSeleccionado;
			}

			SistemaPrincipal.LoggerGlobal.Log($"No se encontro {nameof(itemQueSeleccionar)}({itemQueSeleccionar})", ESeveridad.Error);
		}

		/// <summary>
		/// Metodo encargado con lidiar con eventos de estado de seleccion cambio de los items de la coleccion
		/// </summary>
		/// <param name="item">Item cuyo estado de seleccion cambio</param>
		private void EstadoSeleccionItemCambioHandler(ViewModelMultiselectComboBoxItem<TItems> item) => OnEstadoSeleccionItemCambio(item); 

		#endregion
	}
}