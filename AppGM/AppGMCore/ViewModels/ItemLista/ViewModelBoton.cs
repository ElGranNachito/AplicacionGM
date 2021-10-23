using System;
using System.Windows.Input;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa a un boton en la interfaz grafica
	/// </summary>
	public class ViewModelBoton : ViewModel
	{
		#region Eventos

		/// <summary>
		/// Evento que se disparara cuando el boton sea presionado
		/// </summary>
		public event Action<ViewModelBoton> OnClick = delegate { }; 

		#endregion

		#region Campos & Propiedades

		/// <summary>
		/// Contiene el metodo que se ejecutara cuando el boton sea presionado
		/// </summary>
		private readonly Action mAccionBotonPresionado;

		/// <summary>
		/// Nombre del boton
		/// </summary>
		public string Nombre { get; set; }

		/// <summary>
		/// Contenido del boton
		/// </summary>
		public string Contenido { get; set; }

		/// <summary>
		/// Indica si este boton esta habilitado
		/// </summary>
		public bool EstaHabilitado { get; set; } = true;

		/// <summary>
		/// Indica si este boton es visible
		/// </summary>
		public bool EsVisible { get; set; } = true;

		/// <summary>
		/// Viewmodel del elemento que contiene este boton
		/// </summary>
		public ViewModel ViewModelElementoContenedor { get; init; }

		/// <summary>
		/// Comando que se ejecutara cuando el boton sea presionado
		/// </summary>
		public ICommand Comando { get; private set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_acccionBotonPresionado">Delegado que se ejecutara cuando el boton sea presionado</param>
		/// <param name="_nombre">Nombre que tendra el boton</param>
		/// <param name="_contenido">Contenido del boton</param>
		public ViewModelBoton(Action _acccionBotonPresionado, string _nombre, string _contenido, ViewModel _elementoContenedor)
		{
			mAccionBotonPresionado = _acccionBotonPresionado;
			Nombre = _nombre;
			Contenido = _contenido;

			ViewModelElementoContenedor = _elementoContenedor;

			Comando = new Comando(() =>
			{
				mAccionBotonPresionado?.Invoke();

				OnClick(this);
			});
		} 

		#endregion

		/// <summary>
		/// Nombres y contenidos comunes 
		/// </summary>
		public static class NombresComunes
		{
			public const string Editar = "Editar";
			public const string Eliminar = "Elimar";
			public const string Crear = "Crear";
		}
	}
}
