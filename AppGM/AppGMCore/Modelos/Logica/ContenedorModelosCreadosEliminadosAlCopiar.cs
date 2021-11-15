using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppGM.Core
{
	/// <summary>
	/// Registro que contiene listas con los <see cref="ModeloBase"/> creados y eliminados al copiar de un modelo a otro
	/// </summary>
	public record ContenedorModelosCreadosEliminadosAlCopiar
	{
		#region Propiedades

		/// <summary>
		/// Modelos creados
		/// </summary>
		private List<ModeloBase> mModelosCreados { get; set; }

		/// <summary>
		/// Modelos eliminados
		/// </summary>
		private List<ModeloBase> mModelosEliminados { get; set; }

		/// <summary>
		/// Obtiene una lista de solo lectura de los <see cref="ModeloBase"/> creados al copiar
		/// </summary>
		public IReadOnlyList<ModeloBase> ModelosCreados => mModelosCreados.AsReadOnly();

		/// <summary>
		/// Obtiene una lista de solo lectura de los <see cref="ModeloBase"/> eliminados al copiar
		/// </summary>
		public IReadOnlyList<ModeloBase> ModelosEliminados => mModelosEliminados.AsReadOnly(); 

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_modelosCreados"></param>
		/// <param name="_modelosEliminados"></param>
		public ContenedorModelosCreadosEliminadosAlCopiar(List<ModeloBase> _modelosCreados, List<ModeloBase> _modelosEliminados)
		{
			mModelosCreados = _modelosCreados;
			mModelosEliminados = _modelosEliminados;
		} 

		#endregion

		#region Metodos

		/// <summary>
		/// Guarda los <see cref="ModelosCreados"/> a la base de datos
		/// </summary>
		/// <param name="actualizarBaseDeDatos"><see cref="bool"/> indicando si se deben guardar los cambios a la base de datos</param>
		public async Task GuardarModelosCreadosAsync(bool actualizarBaseDeDatos = true)
		{
			foreach (var modelo in mModelosCreados)
			{
				await modelo.GuardarAsync();
			}

			if (actualizarBaseDeDatos)
				await SistemaPrincipal.GuardarDatosAsync();
		}

		/// <summary>
		/// Quita los <see cref="ModelosEliminados"/> de la base de datos
		/// </summary>
		/// <param name="actualizarBaseDeDatos"><see cref="bool"/> indicando si se deben guardar los cambios a la base de datos</param>
		public async Task EliminarModelosQuitadosAsync(bool actualizarBaseDeDatos = true)
		{
			foreach (var modelo in mModelosEliminados)
			{
				await modelo.Eliminar();
			}

			if (actualizarBaseDeDatos)
				await SistemaPrincipal.GuardarDatosAsync();
		}

		/// <summary>
		/// Llama a <see cref="GuardarModelosCreadosAsync"/> y <see cref="EliminarModelosQuitadosAsync"/>, luego
		/// guarda los cambios si <paramref name="actualizarBaseDeDatos"/> es verdadero
		/// </summary>
		/// <param name="actualizarBaseDeDatos">Indica si se deben guardar los cambios a la base de datos al finalizar todas las operaciones</param>
		public async Task GuardarYEliminarModelosAsync(bool actualizarBaseDeDatos = true)
		{
            if (ModelosCreados.Count == 0 && ModelosEliminados.Count == 0)
                return;

			await GuardarModelosCreadosAsync(false);
			await EliminarModelosQuitadosAsync(false);

			if(actualizarBaseDeDatos)
				await SistemaPrincipal.GuardarDatosAsync();
		}

		#endregion
	}
}