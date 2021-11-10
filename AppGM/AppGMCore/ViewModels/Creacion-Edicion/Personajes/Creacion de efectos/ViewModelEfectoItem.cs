using System;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un <see cref="ControladorEfecto"/> en un <see cref="ViewModelCrearHabilidad"/>
	/// </summary>
	public class ViewModelEfectoItem : ViewModelItemListaControlador<ViewModelEfectoItem, ControladorEfecto>
	{
		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_controladorEfecto">Controlador del efecto que representara esta instancia</param>
		public ViewModelEfectoItem(ControladorEfecto _controladorEfecto)
			: base(_controladorEfecto)
		{
			ControladorGenerico = _controladorEfecto;
		} 

		#endregion

		#region Metodos

		protected override void ActualizarCaracteristicas()
		{
			CaracteristicasItem.AddRange(new ViewModelCaracteristicaItem[]
			{
				new ViewModelCaracteristicaItem
				{
					Titulo = "Nombre",
					Valor = ControladorGenerico.NombreEfecto
				},

				new ViewModelCaracteristicaItem
				{
					Titulo = "Tipo",
					Valor = ControladorGenerico.TipoEfecto.ToString()
				}
			});
		}

		protected override void ActualizarGruposDeBotones()
		{
			Action accionEditar = () =>
			{
				SistemaPrincipal.MostrarViewModelCreacionEdicion<ViewModelCreacionEdicionEfecto, ModeloEfecto, ControladorEfecto>(
					new ViewModelCreacionEdicionEfecto(async vm =>
					{
						if (vm.Resultado.EsAceptarOFinalizar())
						{
							var modelosCreadosEliminados = (await vm.CrearModelo().CrearCopiaProfundaEnSubtipoAsync<ModeloEfecto, ModeloEfecto>(ControladorGenerico.modelo)).modelosCreadosEliminados;

							await modelosCreadosEliminados.GuardarYEliminarModelosAsync();

							await ControladorGenerico.Recargar();

							ActualizarCaracteristicas();
						}

					}, ControladorGenerico.modelo.ObtenerPersonajeContenedor(), ControladorGenerico.modelo.ObtenerModeloContenedor().GetType().ObtenerTipoControladorParaModelo(), ControladorGenerico));
			};

			Action accionEliminar = async () =>
			{
				await ControladorGenerico.EliminarAsync();
			};

			CrearBotonesParaEditarYEliminar(accionEditar, accionEliminar);
		}

		#endregion
	}
}
