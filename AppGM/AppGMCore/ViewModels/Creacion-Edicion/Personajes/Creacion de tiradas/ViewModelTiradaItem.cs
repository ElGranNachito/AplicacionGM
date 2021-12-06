using System;
using System.Collections.ObjectModel;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un <see cref="ModeloTiradaBase"/> en una lista
	/// </summary>
	public class ViewModelTiradaItem : ViewModelItemListaControlador<ViewModelTiradaItem, ControladorTiradaBase>
	{
		public ViewModelTiradaItem(ControladorTiradaBase _controladorTirada)
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
					Valor = ControladorGenerico.modelo.Tirada
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

				var vmEdicion = await new ViewModelCreacionEdicionDeTirada(async vm =>
				{
					if (vm.Resultado.EsAceptarOFinalizar())
					{
						var modeloTiradaEditada = vm.CrearModelo();

						//No nos interesa el resultado de la copia puesto que el modelo tirada no contiene referencias a modelos que se puedan modificar durante su edicion
						await modeloTiradaEditada.CrearCopiaProfundaEnSubtipoAsync(ControladorGenerico.modelo.GetType(), ControladorGenerico.modelo);

						await SistemaPrincipal.GuardarDatosAsync();

						await ControladorGenerico.Recargar();

						ActualizarCaracteristicas();
					}

					SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = dataContextActual;

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
