using System.Collections.ObjectModel;

namespace AppGM.Core
{
	/// <summary>
	/// Representa a un <see cref="ControladorPersonaje"/> en una lista
	/// </summary>
	class ViewModelPersonajeItem : ViewModelItemListaControlador
	{
		/// <summary>
		/// Controlador del personaje siendo representado por este vm
		/// </summary>
		public ControladorPersonaje ControladorPersonaje { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_controladorPersonaje">Controlador del personaje que sera representado por este vm</param>
		public ViewModelPersonajeItem(ControladorPersonaje _controladorPersonaje, bool _mostrarBotonesLaterales = true)
			:base(_controladorPersonaje, _mostrarBotonesLaterales)
		{
			ControladorPersonaje = _controladorPersonaje;

			PathImagen = ControladorPersonaje.modelo.PathImagenAbsoluto;

			CaracteristicasItem.Elementos = new ObservableCollection<ViewModelCaracteristicaItem>
			{
				new ViewModelCaracteristicaItem
				{
					Titulo = "Nombre",
					Valor = ControladorPersonaje.modelo.Nombre
				},

				new ViewModelCaracteristicaItem
				{
					Titulo = "Tipo",
					Valor = ControladorPersonaje.modelo.TipoPersonaje.ToString()
				}
			};

			//Si el personaje es un master o un servant entonces añadimos la clase del servant a las caracteristicas
			if (ControladorPersonaje.modelo is ModeloPersonajeJugable p)
			{
				CaracteristicasItem.Elementos.Add(new ViewModelCaracteristicaItem
				{
					Titulo = "Clase Servant",
					Valor = p.EClaseServant.ToString()
				});
			}
		}
	}
}