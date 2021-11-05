using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppGM.Core
{
	/// <summary>
	/// Representa a un <see cref="ModeloItem"/> en una lista
	/// </summary>
	public class ViewModelItemListaItems : ViewModelItemListaControlador<ViewModelItemListaItems, ControladorItem>
	{
		public ViewModelItemListaItems(ControladorItem _item, string _titulo = "")
			:base(_item, _titulo)
		{
			
		}

		protected override void ActualizarCaracteristicas()
		{
			CaracteristicasItem.AddRange(new []
			{
				new ViewModelCaracteristicaItem
				{
					Titulo = string.Intern("Nombre"),
					Valor = ControladorGenerico.Nombre
				},

				new ViewModelCaracteristicaItem
				{
					Titulo = string.Intern("Tipo"),
					Valor = ControladorGenerico.TipoItem.ToStringTipoItem()
				}
			});
		}

		protected override void ActualizarGruposDeBotones()
		{
			Func<bool, Task<ViewModelCreacionEdicionItem>> accionCrearVmCreacionEdicion = async b =>
			{
				return await new ViewModelCreacionEdicionItem(async vm =>
				{
					if (vm.Resultado.EsAceptarOFinalizar())
					{
						var modeloCreado = vm.CrearModelo();

						var resultadoCopia =
							await modeloCreado.CrearCopiaProfundaEnSubtipoAsync(ControladorGenerico.modelo.GetType(),
								ControladorGenerico.modelo);

						await resultadoCopia.modelosCreadosEliminados.GuardarYEliminarModelosAsync();

						await SistemaPrincipal.GuardarDatosAsync();
					}
				}, ControladorGenerico.Portador.modelo, ControladorGenerico).Inicializar();
			};

			Action accionVer = async () =>
			{
				await SistemaPrincipal.MostrarMensajeAsync(await accionCrearVmCreacionEdicion(false), $"Editar {ControladorGenerico.Nombre}", true);
			};

			Action accionEditar = async () =>
			{
				await SistemaPrincipal.MostrarMensajeAsync(await accionCrearVmCreacionEdicion(true), $"Editar {ControladorGenerico.Nombre}", true);
			};

			Action accionEliminar = () =>
			{

			};

			GruposDeBotones.Add(new ViewModelGrupoBotones(new List<ViewModelBoton>
			{
				new ViewModelBoton(accionVer, ViewModelBoton.NombresComunes.Ver, ViewModelBoton.NombresComunes.Ver, this),
				new ViewModelBoton(accionEditar, ViewModelBoton.NombresComunes.Editar, ViewModelBoton.NombresComunes.Editar, this),
				new ViewModelBoton(accionEliminar, ViewModelBoton.NombresComunes.Eliminar, ViewModelBoton.NombresComunes.Eliminar, this)
			}));
		}
	}
}
