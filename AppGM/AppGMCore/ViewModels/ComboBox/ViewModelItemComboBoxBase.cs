

using System.ComponentModel;
using System.Text;

namespace AppGM.Core
{
	/// <summary>
	/// <see cref="ViewModel"/> base para un control que aparece en las listas
	/// de las combo boxes
	/// </summary>
	/// <typeparam name="TipoValor"></typeparam>
	public class ViewModelItemComboBoxBase<TipoValor> : ViewModel
	{
		/// <summary>
		/// Valor que almacena
		/// </summary>
		public TipoValor valor;

		/// <summary>
		/// Cadena que se muestra
		/// </summary>
		public string Texto { get; set; }

		/// <summary>
		/// Cadena opcional que aparece en la parte derecha del item
		/// </summary>
		public string TextoExtra { get; set; }

		/// <summary>
		/// Tooltip que aparece cuando el mouse esta sobre este item
		/// </summary>
		public string ToolTip { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public PropertyChangedEventHandler propiedadCambiadaEnValorHandler;

		public ViewModelItemComboBoxBase(){}

		public ViewModelItemComboBoxBase(TipoValor _valor, string _texto, PropertyChangedEventHandler _propiedadCambiadaEnValorHandler = null)
		{
			Actualizar(_valor, _texto, "", "", _propiedadCambiadaEnValorHandler);
		}

		public ViewModelItemComboBoxBase(TipoValor _valor, string _texto, string _textoExra, string _tooltip = "", PropertyChangedEventHandler _propiedadCambiadaEnValorHandler = null)
		{
			Actualizar(_valor, _texto, _textoExra, _tooltip, _propiedadCambiadaEnValorHandler);
		}

		public void Actualizar(TipoValor nuevoValor, string nuevoTexto, string textoExtra = "", string nuevoToolTip = "", PropertyChangedEventHandler nuevoPropertyChangedEventHandler = null)
		{
			if (nuevoValor.Equals(valor))
				return;

			if (propiedadCambiadaEnValorHandler != null && valor is ViewModel vmAnterior)
				vmAnterior.PropertyChanged -= propiedadCambiadaEnValorHandler;

			valor = nuevoValor;
			propiedadCambiadaEnValorHandler = nuevoPropertyChangedEventHandler;

			Texto      = nuevoTexto;
			TextoExtra = TextoExtra;
			ToolTip    = nuevoToolTip;

			if (propiedadCambiadaEnValorHandler != null && valor is ViewModel vm)
				vm.PropertyChanged += propiedadCambiadaEnValorHandler;
		}
	}
}
