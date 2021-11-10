﻿using System;
using System.Threading.Tasks;

namespace AppGM.Core
{
	/// <summary>
	/// Clase que contiene helpers para la creacion de mensajes
	/// </summary>
	public class MensajeHelpers
	{
		/// <summary>
		/// Crear una mensaje para la creacion, edicion o vista de un <see cref="ModeloBase"/>
		/// </summary>
		/// <typeparam name="TModelo">Tipo del <see cref="ModeloBase"/> con el que se esta tratando</typeparam>
		/// <typeparam name="TControlador">Tipo del controlador del <typeparamref name="TModelo"/></typeparam>
		/// <typeparam name="TViewModel">Tipo del viewmodel</typeparam>
		/// <param name="viewModel">Viewmodel para la manipulacion del modelo</param>
		/// <returns>Resultado del viewmodel</returns>
		public static async Task<EResultadoViewModel> MostrarVentanaMensajeCreacionEdicionModelo<TModelo, TControlador, TViewModel>
			(ViewModelCreacionEdicionDeModelo<TModelo, TControlador, TViewModel> viewModel)

			where TModelo: ModeloBase
			where TControlador: ControladorBase
			where TViewModel: ViewModelCreacionEdicionDeModelo<TModelo, TControlador, TViewModel>
		{
			Action<TViewModel> handlerOnPuedeEditarCambio = vm =>
			{
				SistemaPrincipal.Aplicacion.VentanaActual.TituloVentana = vm.ToString();
			};

			viewModel.OnPuedeEditarCambio += handlerOnPuedeEditarCambio;

			var resultado = await SistemaPrincipal.MostrarMensajeAsync(viewModel, string.Empty, true);

			viewModel.OnPuedeEditarCambio -= handlerOnPuedeEditarCambio;

			return resultado;
		}

		/// <summary>
		/// Muestra un mensaje de confirmacion en una nueva ventana
		/// </summary>
		/// <param name="titulo">Titulo que tendra el mensaje</param>
		/// <param name="mensaje">Contenido del mensaje</param>
		/// <returns><see cref="EResultadoViewModel"/> que indica la accion seleccionada por el usuario</returns>
		public static async Task<EResultadoViewModel> MostrarMensajeConfirmacionAsync(string titulo, string mensaje)
		{
			return await SistemaPrincipal.MostrarMensajeAsync(new ViewModelMensajeConfirmacionAccion(titulo, mensaje), string.Empty, true, 375, 500);
		}
	}
}
