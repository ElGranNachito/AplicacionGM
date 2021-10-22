using System.Collections.ObjectModel;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un <see cref="ModeloTiradaBase"/> en una lista
	/// </summary>
	public class ViewModelTiradaItem : ViewModelItemListaControlador<ViewModelTiradaItem, ControladorTiradaVariable>
	{
		public ViewModelTiradaItem(ControladorTiradaVariable _controladorTirada, bool _mostrarBotones = true)
			:base(_controladorTirada, _mostrarBotones) 
		{
			ControladorGenerico = _controladorTirada;

			mAccionBotonSuperior = () =>
			{
				var dataContextActual = SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido;

				var vmEdicion = new ViewModelCrearTirada(vm =>
				{
				}, ControladorGenerico.modelo.ObtenerModeloContenedor(), ControladorGenerico);

				SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = vmEdicion;
			};

			mAccionBotonInferior = () =>
			{
				ControladorGenerico?.Eliminar();
			};
		}

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
					Valor = ((ModeloTiradaVariable)ControladorGenerico.modelo).Tirada
				},

				new ViewModelCaracteristicaItem
				{
					Titulo = "Tipo",
					Valor = ControladorGenerico.modelo.TipoTirada.ToString()
				}
			};
		}
	}
}
