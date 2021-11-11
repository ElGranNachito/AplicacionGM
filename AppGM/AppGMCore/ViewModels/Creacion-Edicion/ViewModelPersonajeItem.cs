using System;
using System.Collections.ObjectModel;

namespace AppGM.Core
{
	/// <summary>
	/// Representa a un <see cref="ControladorPersonaje"/> en una lista
	/// </summary>
	public sealed class ViewModelPersonajeItem : ViewModelItemListaControlador<ViewModelPersonajeItem, ControladorPersonaje>
	{
		public readonly ModeloPersonaje modeloPersonaje;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_controladorPersonaje">Controlador del personaje que sera representado por este vm</param>
		public ViewModelPersonajeItem(ControladorPersonaje _controladorPersonaje, bool _mostrarBotonesLaterales = true)
			: base(_controladorPersonaje)
		{
			modeloPersonaje = _controladorPersonaje.modelo;

			ActualizarCaracteristicas();
		}

		/// <summary>
		/// Constructor utilizado cuando no contamos con el <see cref="ControladorPersonaje"/>
		/// </summary>
		/// <param name="_modeloPersonaje">Modelo del personaje</param>
		public ViewModelPersonajeItem(ModeloPersonaje _modeloPersonaje)
		{
			modeloPersonaje = _modeloPersonaje;

			if(modeloPersonaje == null)
				SistemaPrincipal.LoggerGlobal.LogCrash($"{nameof(modeloPersonaje)} no puede ser null");

			ActualizarCaracteristicas();
		}

		protected override void ActualizarCaracteristicas()
		{
			if(modeloPersonaje == null)
				return;

			CaracteristicasItem.Elementos = new ObservableCollection<ViewModelCaracteristicaItem>
			{
				new ViewModelCaracteristicaItem
				{
					Titulo = "Nombre",
					Valor = modeloPersonaje.Nombre
				},

				new ViewModelCaracteristicaItem
				{
					Titulo = "Tipo",
					Valor = modeloPersonaje.TipoPersonaje.ToString()
				}
			};

			//Si el personaje es un master o un servant entonces añadimos la clase del servant a las caracteristicas
			if (modeloPersonaje is ModeloPersonajeJugable p)
			{
				CaracteristicasItem.Elementos.Add(new ViewModelCaracteristicaItem
				{
					Titulo = "Clase Servant",
					Valor = p.ClaseServant.ToString()
				});
			}

			PathImagen = modeloPersonaje.PathImagenAbsoluto;
		}

		protected override void ActualizarGruposDeBotones()
		{
			Action accionEditar = async () =>
			{
				var dataContextActual = SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido;

				SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = await new ViewModelCreacionEdicionPersonaje(async vm =>
				{
					if (vm.Resultado.EsAceptarOFinalizar())
					{
						var modelo = vm.CrearModelo();

						var resultado = await modelo.CrearCopiaProfundaEnSubtipoAsync(modeloPersonaje.GetType(), modeloPersonaje);

						await resultado.modelosCreadosEliminados.GuardarYEliminarModelosAsync();
					}

					SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = dataContextActual;
				}).Inicializar();
			};

			Action accionEliminar = async () =>
			{
				if (ControladorGenerico != null)
				{
					await ControladorGenerico.EliminarAsync();

					return;
				}

				await modeloPersonaje.Eliminar(true);
			};

			CrearBotonesParaEditarYEliminar(accionEditar, accionEliminar);

			IndiceGrupoDeBotonesActivo = 0;
		}
	}
}