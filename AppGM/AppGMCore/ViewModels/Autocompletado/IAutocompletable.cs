using System;
using System.Text.RegularExpressions;
using AppGM.Core.Delegados;

namespace AppGM.Core
{
	/// <summary>
	/// Interfaz para que implementen todos los <see cref="ViewModel"/> que tengan un campo autocompletable
	/// </summary>
	public interface IAutocompletable
	{
		/// <summary>
		/// Posicion del signo de intercalacion, es decir donde se esta escribiendo
		/// </summary>
		public int PosSignoIntercalacion { get; set; }

		/// <summary>
		/// Texto actualmente ingresado por el usuario
		/// </summary>
		public string TextoActual { get; set; }

		/// <summary>
		/// Texto anterior al ultimo cambio
		/// </summary>
		public string TextoAnterior { get; set; }

		/// <summary>
		/// Expresion regular que detecta caracteres no permitidos
		/// </summary>
		public Regex ExpresionRegularDetectarCaracteresNoPermitidos { get; set; }

		/// <summary>
		/// VM de la ventana de autocompletado
		/// </summary>
		public ViewModelVentanaAutocompletado Autocompletado { get; }

		/// <summary>
		/// Evento que se dispara cuando el <see cref="TextoActual"/> es modificado no por el usuario,
		/// sino por el programa
		/// </summary>
		public event DVariableCambio<string> OnTextoActualModificado;

		#region Metodos

		/// <summary>
		/// Funcion que actualiza las opciones posibles de <see cref="ViewModelVentanaAutocompletado"/>
		/// </summary>
		public void ActualizarPosibilidadesAutocompletado(string nuevoTexto, int nuevoIndiceIntercalacion);

		/// <summary>
		/// Funcion que se encarga
		/// </summary>
		public void ActualizarPosicionSignoDeIntercalacion(int nuevaPosicion);

		/// <summary>
		/// Funcion que lidia con el evento de que se seleccione un <see cref="ViewModelItemAutocompletado{TipoValor}"/>
		/// </summary>
		/// <param name="valorSeleccionado">Valor contenido por el <see cref="ViewModelItemAutocompletado{TipoValor}"/></param>
		public void HandlerValorSeleccionado(ViewModelItemAutocompletadoBase valorSeleccionado);

		/// <summary>
		/// Funcion que se llama cuando el textbox del control representado por este VM es seleccionado
		/// </summary>
		public void FocusObtenido();

		/// <summary>
		/// Funcion que se llama cuando el textbox del control representado por este VM es deseleccionado
		/// </summary>
		public void FocusPerdido(); 

		#endregion
	}
}
