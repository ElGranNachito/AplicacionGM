using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa un control para visualizar y modificar el inventario de un <see cref="ModeloPersonaje"/>
	/// </summary>
	public class ViewModelInventario : ViewModel
	{
		public ModeloPersonaje ModeloPersonaje { get; init; }

		public ControladorPersonaje ControladorPersonaje { get; init; }

		public ViewModelVistaArbol<ViewModelElementoArbol<ControladorSlot>, ControladorSlot> ViewModelVistaInventario { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="modeloPersonaje">Modelo del personaje a quein pertenece este inventario</param>
		/// <param name="controladorPersonaje">Controlador del personaje a quien pertenece este incentario</param>
		public ViewModelInventario(ModeloPersonaje _modeloPersonaje, ControladorPersonaje _controladorPersonaje)
		{
			ModeloPersonaje      = _modeloPersonaje;
			ControladorPersonaje = _controladorPersonaje;

			ViewModelVistaInventario = new ViewModelVistaArbol<ViewModelElementoArbol<ControladorSlot>, ControladorSlot>(null);

			var elementosBaseArbol = ModeloPersonaje.SlotsBase.Select(s =>
			{
				var controladorSlot = SistemaPrincipal.ObtenerControlador<ControladorSlot, ModeloSlot>(s, true);

				return new ViewModelElementoArbolItemInventario(ViewModelVistaInventario, null, controladorSlot);
			}).Cast<ViewModelElementoArbol<ControladorSlot>>().ToList();
			
			ViewModelVistaInventario.Hijos.AddRange(elementosBaseArbol);

			ViewModelVistaInventario.OnElementoSeleccionadoCambio += (arbol, item) =>
			{
				
			};
		}
	}
}
