using System;
using System.Collections.ObjectModel;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un <see cref="ModeloTiradaBase"/> en una lista
	/// </summary>
	public class ViewModelTiradaItem : ViewModelItemListaControlador<ViewModelTiradaItem, ControladorTiradaPersonalizada>
	{
		public ViewModelTiradaItem(ControladorTiradaPersonalizada _controladorTirada)
			:base(_controladorTirada) {}

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
					Titulo = "Tirada",
					Valor = ((ModeloTiradaPersonalizada)ControladorGenerico.modelo).Tirada
				},

				new ViewModelCaracteristicaItem
				{
					Titulo = "Tipo",
					Valor = ControladorGenerico.modelo.TipoTirada.ToString()
				}
			};
		}

		protected override void ActualizarGruposDeBotones()
		{
			Action accionEditar = async () =>
			{
				var dataContextActual = SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido;

				var vmEdicion = await new ViewModelCreacionEdicionDeTirada(vm =>
				{
				}, ControladorGenerico.modelo.ObtenerModeloContenedor(), ControladorGenerico).Inicializar();

				SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = vmEdicion;
			};

			Action accionEliminar = () =>
			{
				ControladorGenerico?.Eliminar(true);
			};

			CrearBotonesParaEditarYEliminar(accionEditar, accionEliminar);

			IndiceGrupoDeBotonesActivo = 0;
		}
	}
}
