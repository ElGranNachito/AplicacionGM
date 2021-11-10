using System;
using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa a un grupo de botones
	/// </summary>
	public class ViewModelGrupoBotones : ViewModel
	{
		#region Eventos

		/// <summary>
		/// Evento que se dispara cuando cualquiera de los botones contenenidos en este grupo es presionado
		/// </summary>
		public Action<ViewModelBoton> OnAlgunBotonPresionado = delegate { }; 

		#endregion

		#region Propiedades

		/// <summary>
		/// Direccion en la que se ordenaran los botones
		/// </summary>
		public EDireccionItems Direccion { get; set; }

		/// <summary>
		/// Indica si este grupo esta habilitado
		/// </summary>
		public bool EstaHabilitado { get; set; } = true;

		/// <summary>
		/// Indica si este grupo es visible
		/// </summary>
		public bool EsVisible { get; set; }

		/// <summary>
		/// Botones contenidos en este grupo
		/// </summary>
		public ViewModelListaDeElementos<ViewModelBoton> Botones { get; set; } 

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_botones">Botones que contendra este grupo</param>
		public ViewModelGrupoBotones(List<ViewModelBoton> _botones)
		{
			Botones = new ViewModelListaDeElementos<ViewModelBoton>(_botones);
		}

		#endregion

		#region Metodos

		/// <summary>
		/// Añade un <paramref name="handler"/> al evento <see cref="ViewModelBoton.OnClick"/> de un <see cref="ViewModelBoton"/>
		/// </summary>
		/// <param name="nombreBoton">Nombre del boton al que añadir el <paramref name="handler"/></param>
		/// <param name="handler">Handler que añadir al evento</param>
		/// <returns><see cref="bool"/> indicando si se pudo añadir el handler</returns>
		public bool AñadirHandlerClick(string nombreBoton, Action<ViewModelBoton> handler)
		{
			//Buscamos el boton con el nombre indicado y si lo encontramos añadimos el handler
			foreach (var btn in Botones)
			{
				if (btn.Nombre == nombreBoton)
				{
					btn.OnClick += handler;

					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Quita un <paramref name="handler"/> del evento <see cref="ViewModelBoton.OnClick"/> de un <see cref="ViewModelBoton"/>
		/// </summary>
		/// <param name="nombreBoton">Nombre del boton al que quitar el <paramref name="handler"/></param>
		/// <param name="handler">Handler que quitar del evento</param>
		/// <returns><see cref="bool"/> indicando si se pudo quitar el handler</returns>
		public bool QuitarHandlerClick(string nombreBoton, Action<ViewModelBoton> handler)
		{
			//lo mismo que en el metodo de arriba solo que aqui quitamos el handler
			foreach (var btn in Botones)
			{
				if (btn.Nombre == nombreBoton)
				{
					btn.OnClick -= handler;

					return true;
				}
			}

			return false;
		}

		#endregion
	}
}