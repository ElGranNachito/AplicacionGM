using System.Collections.ObjectModel;

namespace AppGM.Core
{
	/// <summary>
	/// Representa a un <see cref="ControladorPersonaje"/> en una lista
	/// </summary>
	class ViewModelPersonajeItem : ViewModelItemListaControlador<ViewModelPersonajeItem, ControladorPersonaje>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_controladorPersonaje">Controlador del personaje que sera representado por este vm</param>
		public ViewModelPersonajeItem(ControladorPersonaje _controladorPersonaje, bool _mostrarBotonesLaterales = true)
			:base(_controladorPersonaje) {}

		protected override void ActualizarCaracteristicas()
		{
			CaracteristicasItem.Elementos = new ObservableCollection<ViewModelCaracteristicaItem>
			{
				new ViewModelCaracteristicaItem
				{
					Titulo = "Nombre",
					Valor = ControladorGenerico.modelo.Nombre
				},

				new ViewModelCaracteristicaItem
				{
					Titulo = "Tipo",
					Valor = ControladorGenerico.modelo.TipoPersonaje.ToString()
				}
			};

			//Si el personaje es un master o un servant entonces añadimos la clase del servant a las caracteristicas
			if (ControladorGenerico.modelo is ModeloPersonajeJugable p)
			{
				CaracteristicasItem.Elementos.Add(new ViewModelCaracteristicaItem
				{
					Titulo = "Clase Servant",
					Valor = p.ClaseServant.ToString()
				});
			}

			PathImagen = ControladorGenerico.modelo.PathImagenAbsoluto;
		}
	}
}