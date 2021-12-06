using System.Linq;

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

		public ViewModelCreacionEdicionDeSlot ViewModelEdicionSlotActual { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_modeloPersonaje">Modelo del personaje a quein pertenece este inventario</param>
		/// <param name="_controladorPersonaje">Controlador del personaje a quien pertenece este incentario</param>
		public ViewModelInventario(ModeloPersonaje _modeloPersonaje, ControladorPersonaje _controladorPersonaje, ETipoItem tiposDeItemQueMostrar = ETipoItem.TODOS)
		{
			ModeloPersonaje      = _modeloPersonaje;
			ControladorPersonaje = _controladorPersonaje;

			ViewModelVistaInventario = new ViewModelVistaArbol<ViewModelElementoArbol<ControladorSlot>, ControladorSlot>(null);

			var elementosBaseArbol = ModeloPersonaje.SlotsBase.Where(s => s.ObtenerProfundidad() == 0).Select(s =>
			{
				var controladorSlot = SistemaPrincipal.ObtenerControlador<ControladorSlot, ModeloSlot>(s, true);

				return new ViewModelElementoArbolItemInventario(ViewModelVistaInventario, null, controladorSlot, tiposDeItemQueMostrar);
			}).Cast<ViewModelElementoArbol<ControladorSlot>>().ToList();
			
			ViewModelVistaInventario.Hijos.AddRange(elementosBaseArbol);

			ViewModelVistaInventario.OnElementoSeleccionadoCambio += async (arbol, item) =>
			{
				if(arbol.ElementosSeleccionados.Count > 1)
					return;

				var vmAnterior = ViewModelEdicionSlotActual;

				ViewModelEdicionSlotActual = await new ViewModelCreacionEdicionDeSlot(async vm =>
				{
					if (vm.Resultado.EsAceptarOFinalizar())
					{
						var resultadoCopia = await vm.ModeloCreado.CrearCopiaProfundaEnSubtipoAsync<ModeloSlot, ModeloSlot>(item.Contenido.modelo);

						await resultadoCopia.modelosCreadosEliminados.GuardarYEliminarModelosAsync();

						await item.Contenido.Recargar();

						item.Actualizar();
					}

				}, item.Contenido).Inicializar();

				ViewModelEdicionSlotActual.OnSlotModificado += slot =>
				{
					item.Actualizar();
				};

				if (vmAnterior == null)
					return;

				if(vmAnterior.MostrarSlot)
					ViewModelEdicionSlotActual.ComandoVerSlot.Execute(null);

				if(vmAnterior.MostrarContenido)
					ViewModelEdicionSlotActual.ComandoVerContenidoSlot.Execute(null);

				vmAnterior = null;
			};
		}
	}
}
