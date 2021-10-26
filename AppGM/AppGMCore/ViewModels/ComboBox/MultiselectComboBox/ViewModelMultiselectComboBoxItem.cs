using System;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa un elemento en un <see cref="ViewModelMultiselectComboBox{TItems}"/>
	/// </summary>
	/// <typeparam name="TValor"></typeparam>
	public class ViewModelMultiselectComboBoxItem<TValor>
	{
		#region Eventos

		/// <summary>
		/// Evento que se dispara cuando el valor de <see cref="EstaSeleccionado"/> es modificado
		/// </summary>
		public event Action<ViewModelMultiselectComboBoxItem<TValor>> OnSeleccionadoCambio = delegate { };

		#endregion

		#region Campos & Propiedades

		/// <summary>
		/// Contiene el valor de <see cref="EstaSeleccionado"/>
		/// </summary>
		private bool mEstaSeleccionado;

		/// <summary>
		/// Contenido de este item
		/// </summary>
		public string Contenido { get; set; }

		/// <summary>
		/// Indica si este item esta seleccionado actualmente
		/// </summary>
		public bool EstaSeleccionado
		{
			get => mEstaSeleccionado;
			set
			{
				if (value == mEstaSeleccionado)
					return;

				mEstaSeleccionado = value;

				OnSeleccionadoCambio(this);
			}
		}

		/// <summary>
		/// Valor almacenado en este item
		/// </summary>
		public TValor Valor { get; init; } 

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_valor">Valor que contiene este item</param>
		/// <param name="_estaSelccionado"></param>
		public ViewModelMultiselectComboBoxItem(TValor _valor, string _contenido, bool _estaSelccionado = false)
		{
			Valor = _valor;
			Contenido = _contenido;

			EstaSeleccionado = _estaSelccionado;
		} 

		#endregion
	}
}