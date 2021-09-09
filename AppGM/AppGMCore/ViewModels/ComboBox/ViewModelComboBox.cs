using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AppGM.Core.Delegados;

namespace AppGM.Core
{
	/// <summary>
	/// <see cref="ViewModel"/> para representar un control de tipo ComboBox
	/// </summary>
	/// <typeparam name="TipoValor"><see cref="Type"/> del valor que almacenan sus opciones</typeparam>
	public class ViewModelComboBox<TipoValor> : ViewModel
	{
		#region Campos & Propiedades

		private ViewModelItemComboBoxBase<TipoValor> mValorSeleccionado;

		public DVariableCambio<ViewModelItemComboBoxBase<TipoValor>> OnValorSeleccionadoCambio = delegate { };

		/// <summary>
		/// <see cref="List{T}"/> de <see cref="ViewModelItemComboBoxBase{TipoValor}"/> que puede
		/// seleccionar el usuario
		/// </summary>
		public ViewModelListaDeElementos<ViewModelItemComboBoxBase<TipoValor>> ValoresPosibles { get; set; } = new ViewModelListaDeElementos<ViewModelItemComboBoxBase<TipoValor>>();

		/// <summary>
		/// <see cref="ViewModelItemComboBoxBase{TipoValor}"/> seleccionado
		/// </summary>
		public ViewModelItemComboBoxBase<TipoValor> ValorSeleccionado
		{
			get => mValorSeleccionado;
			set
			{
				if (value == mValorSeleccionado)
					return;

				var valorAnterior = mValorSeleccionado;

				mValorSeleccionado = value;

				OnValorSeleccionadoCambio(valorAnterior, mValorSeleccionado);
			}
		}

		/// <summary>
		/// <see cref="TipoValor"/> seleccionado por el usuario
		/// </summary>
		public TipoValor Valor
		{
			get
			{
				if (ValorSeleccionado != null)
					return ValorSeleccionado.valor;

				return default;
			}
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor por defecto
		/// </summary>
		/// <param name="_valoresPosibles">Coleccion con los <see cref="ValoresPosibles"/></param>
		/// <param name="_valorPorDefecto">Valor que se asignara por defecto a <see cref="ValorSeleccionado"/></param>
		public ViewModelComboBox(List<TipoValor> _valoresPosibles = null, ViewModelItemComboBoxBase<TipoValor> _valorPorDefecto = null)
		{
			ActualizarValoresPosibles(_valoresPosibles);

			ValorSeleccionado = _valorPorDefecto;
		}

		/// <summary>
		/// Constructor que inicializa los <see cref="ValoresPosibles"/> utilizando una coleccion de <see cref="ViewModelItemComboBoxBase{TipoValor}"/>
		/// </summary>
		/// <param name="_valoresPosibles">Coleccion con los <see cref="ValoresPosibles"/></param>
		/// <param name="_valorPorDefecto">Valor que se asignara por defecto a <see cref="ValorSeleccionado"/></param>
		public ViewModelComboBox(List<ViewModelItemComboBoxBase<TipoValor>> _valoresPosibles = null, ViewModelItemComboBoxBase<TipoValor> _valorPorDefecto = null)
		{
			ValoresPosibles.Elementos = new ObservableCollection<ViewModelItemComboBoxBase<TipoValor>>(_valoresPosibles);

			ValorSeleccionado = _valorPorDefecto;
		}

		#endregion

		#region Metodos

		/// <summary>
		/// Actualiza los valores en <see cref="ValoresPosibles"/>
		/// </summary>
		/// <param name="nuevosValoresPosibles">Coleccion con los nuevos <see cref="ValoresPosibles"/></param>
		public void ActualizarValoresPosibles(List<TipoValor> nuevosValoresPosibles)
		{
			var nuevosItemsComboBox =
				nuevosValoresPosibles.Select(valor => new ViewModelItemComboBoxBase<TipoValor> { Texto = valor.ToString(), valor = valor });

			ValoresPosibles.Elementos = new ObservableCollection<ViewModelItemComboBoxBase<TipoValor>>(nuevosItemsComboBox);
		} 

		#endregion
	}
}
