using System;
using System.Collections.Generic;
using System.Reflection;

using AppGM.Core;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa a una funcion handler de evento en una lista
	/// </summary>
	public sealed class ViewModelFuncionHandlerEventoItem : ViewModelFuncionItem<ControladorFuncion_HandlerEvento>
	{
		public EventInfo Evento { get; init; }

		public ViewModelFuncionHandlerEventoItem(EventInfo _evento)
			:base()
		{
			Evento = _evento;

			
		}

		public ViewModelFuncionHandlerEventoItem(ControladorFuncion_HandlerEvento _controlador, EventInfo _evento) 
			: base(_controlador)
		{
			Evento = _evento;
		}

		protected override void ActualizarCaracteristicas()
		{
			
		}

		protected override void ActualizarGruposDeBotones()
		{
			base.ActualizarCaracteristicas();

			GruposDeBotones[0].Direccion = EDireccionItems.Horizontal;
		}

		protected override Action ObtenerAccionCrear()
		{
			return base.ObtenerAccionCrear();
		}
	}
}
