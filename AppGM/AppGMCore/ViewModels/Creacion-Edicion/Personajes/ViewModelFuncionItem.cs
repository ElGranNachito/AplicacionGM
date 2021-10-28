using System;
using System.Collections.Generic;

using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Representa a una funcion en un <see cref="ViewModelCrearHabilidad"/>
	/// </summary>
	/// <typeparam name="TControlador">Tipo del <see cref="ControladorFuncionBase"/></typeparam>
	public class ViewModelFuncionItem<TControlador> : ViewModelItemListaControlador<ViewModelFuncionItem<TControlador>, ControladorFuncionBase> 
		where TControlador: ControladorFuncionBase
	{
		/// <summary>
		/// Nombre de la funcion
		/// </summary>
		public string NombreFuncion => ControladorGenerico?.NombreFuncion;

		//TODO: Hacer que de verdad indique el tipo de la funcion
		/// <summary>
		/// Tipo de la funcion
		/// </summary>
		public EPropositoFuncion TipoFuncion => EPropositoFuncion.NINGUNO;

		/// <summary>
		/// Constructor por defecto
		/// </summary>
		public ViewModelFuncionItem()
		{
			ActualizarGruposDeBotones();
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_controladorFuncion">Controlador de la funcion representada por este item</param>
		/// <param name="_titulo">Titulo que tendra este item</param>
		public ViewModelFuncionItem(TControlador _controladorFuncion, string _titulo = "")
			:base(_controladorFuncion, _titulo) {}

		protected override void ActualizarCaracteristicas()
		{
			CaracteristicasItem.AddRange(new ViewModelCaracteristicaItem[]
			{
				new ViewModelCaracteristicaItem
				{
					Titulo = "Nombre",
					Valor = NombreFuncion
				},

				new ViewModelCaracteristicaItem
				{
					Titulo = "Tipo",
					Valor = TipoFuncion.ToString()
				}
			});
		}

		protected override void ActualizarGruposDeBotones()
		{
			Action accionEditar = () =>
			{
				//Obtenemos el vm actual de la ventana
				var dataContextActual = SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido;

				//Creamos el vm para editar la funcion
				SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido =
					ControladorGenerico.CrearVMParaEditar(vm =>
					{
						SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = dataContextActual;
					});
			};

			Action accionEliminar = () =>
			{
				//TODO: Mostrar mensaje de confirmacion
				ControladorGenerico.Eliminar();
			};

			CrearBotonesParaEditarYEliminar(accionEditar, accionEliminar);

			GruposDeBotones.Add(new ViewModelGrupoBotones(new List<ViewModelBoton>
			{
				new ViewModelBoton(ObtenerAccionCrear(), ViewModelBoton.NombresComunes.Crear, ViewModelBoton.NombresComunes.Crear, this)
			}));

			if (Controlador == null)
				IndiceGrupoDeBotonesActivo = 1;
		}

		/// <summary>
		/// Obtiene un <see cref="Action"/> que ejecuta la logica necesaria para crear una nueva funcion
		/// </summary>
		/// <returns><see cref="Action"/> que ejecuta la logica necesaria para crear una nueva funcion</returns>
		protected virtual Action ObtenerAccionCrear()
		{
			return () =>
			{
				var dataContextActual = SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido;

				var nuevoVmCreacionFuncion = ControladorFuncionBase.CrearVMParaCrear(typeof(TControlador), vm =>
				{
					if (vm.Resultado.EsAceptarOFinalizar())
						ControladorGenerico = vm.CrearControlador();

					SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = dataContextActual;
				});

				SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = nuevoVmCreacionFuncion;
			};
		}
	}
}