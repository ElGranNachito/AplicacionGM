using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AppGM.Core.Delegados;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// <see cref="ViewModel"/> para representar un control de tipo ComboBox
	/// </summary>
	/// <typeparam name="TValor"><see cref="Type"/> del valor que almacenan sus opciones</typeparam>
	public class ViewModelComboBox<TValor> : ViewModel
	{
		#region Eventos

		/// <summary>
		/// Evento disparado cuando el <see cref="ValorSeleccionado"/> cambia
		/// </summary>
		public event DVariableCambio<ViewModelItemComboBoxBase<TValor>> OnValorSeleccionadoCambio = delegate { }; 

		#endregion

		#region Campos & Propiedades

		/// <summary>
		/// Contiene el valor de <see cref="ValorSeleccionado"/>
		/// </summary>
		private ViewModelItemComboBoxBase<TValor> mValorSeleccionado;

		/// <summary>
		/// Descripcion de este combo box
		/// </summary>
		public string Descripcion { get; set; }

		/// <summary>
		/// Indica si esta combobox esta habilitada
		/// </summary>
		public bool EstaHabilitada { get; set; } = true;

		/// <summary>
		/// <see cref="List{T}"/> de <see cref="ViewModelItemComboBoxBase{TipoValor}"/> que puede
		/// seleccionar el usuario
		/// </summary>
		public ViewModelListaDeElementos<ViewModelItemComboBoxBase<TValor>> ValoresPosibles { get; set; } = new ViewModelListaDeElementos<ViewModelItemComboBoxBase<TValor>>();

		/// <summary>
		/// <see cref="ViewModelItemComboBoxBase{TipoValor}"/> seleccionado
		/// </summary>
		public ViewModelItemComboBoxBase<TValor> ValorSeleccionado
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
		/// <see cref="TValor"/> seleccionado por el usuario
		/// </summary>
		public TValor Valor
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

		public ViewModelComboBox(string descripcion, List<TValor> _valoresPosibles = null, ViewModelItemComboBoxBase<TValor> _valorPorDefecto = null)
			:this(_valoresPosibles, _valorPorDefecto)
		{
			Descripcion = descripcion;
		}

		/// <summary>
		/// Constructor por defecto
		/// </summary>
		/// <param name="_valoresPosibles">Coleccion con los <see cref="ValoresPosibles"/></param>
		/// <param name="_valorPorDefecto">Valor que se asignara por defecto a <see cref="ValorSeleccionado"/></param>
		public ViewModelComboBox(List<TValor> _valoresPosibles = null, ViewModelItemComboBoxBase<TValor> _valorPorDefecto = null)
		{
			ActualizarValoresPosibles(_valoresPosibles);

			ValorSeleccionado = _valorPorDefecto;
		}

		/// <summary>
		/// Constructor que inicializa los <see cref="ValoresPosibles"/> utilizando una coleccion de <see cref="ViewModelItemComboBoxBase{TipoValor}"/>
		/// </summary>
		/// <param name="_valoresPosibles">Coleccion con los <see cref="ValoresPosibles"/></param>
		/// <param name="_valorPorDefecto">Valor que se asignara por defecto a <see cref="ValorSeleccionado"/></param>
		public ViewModelComboBox(List<ViewModelItemComboBoxBase<TValor>> _valoresPosibles = null, ViewModelItemComboBoxBase<TValor> _valorPorDefecto = null)
		{
			ValoresPosibles.Elementos = new ObservableCollection<ViewModelItemComboBoxBase<TValor>>(_valoresPosibles);

			ValorSeleccionado = _valorPorDefecto;
		}

		#endregion

		#region Metodos

		/// <summary>
		/// Actualiza los valores en <see cref="ValoresPosibles"/>
		/// </summary>
		/// <param name="nuevosValoresPosibles">Coleccion con los nuevos <see cref="ValoresPosibles"/></param>
		public void ActualizarValoresPosibles(List<TValor> nuevosValoresPosibles)
		{
			IEnumerable<ViewModelItemComboBoxBase<TValor>> nuevosItemsComboBox = null;

			if (nuevosValoresPosibles is not null)
			{
				nuevosItemsComboBox = nuevosValoresPosibles.Select(valor => new ViewModelItemComboBoxBase<TValor> { Texto = valor.ToString(), valor = valor });
			}

			ValoresPosibles.Elementos = new ObservableCollection<ViewModelItemComboBoxBase<TValor>>(nuevosItemsComboBox);
		}

		/// <summary>
		/// Cambia el valor seleccionado
		/// </summary>
		/// <param name="nuevoValor">Valor que seleccionar</param>
		public void SeleccionarValor(TValor nuevoValor)
		{
			var vmNuevoValor = ValoresPosibles.FirstOrDefault(vm => EqualityComparer<TValor>.Default.Equals(vm.valor, nuevoValor));

			if(vmNuevoValor == null)
				SistemaPrincipal.LoggerGlobal.Log($"No se encontro una opcion con el {nameof(nuevoValor)}({nuevoValor})", ESeveridad.Error);

			ValorSeleccionado = vmNuevoValor;
		}

		#endregion
	}
}