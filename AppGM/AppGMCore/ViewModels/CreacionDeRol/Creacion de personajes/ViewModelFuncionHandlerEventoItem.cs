using System;
using System.Reflection;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa a una funcion handler de evento en una lista
	/// </summary>
	public sealed class ViewModelFuncionHandlerEventoItem : ViewModelFuncionItem<ControladorFuncion_HandlerEvento>
	{
		/// <summary>
		/// Contiene el valor de <see cref="TipoHandler"/>
		/// </summary>
		private Type mTipoHandler;

		/// <summary>
		/// Tipo del del delegado del evento
		/// </summary>
		public Type TipoHandler
		{
			get => mTipoHandler;
			init
			{
				mTipoHandler = value;

				ActualizarCaracteristicas();
			}
		}

		/// <summary>
		/// Evento en especifico al que esta vinculado este handler
		/// </summary>
		public EventInfo EventoEspecifico { get; init; }

		/// <summary>
		/// Constructor utilizado cuando no existe un controlador pero conocemos
		/// el evento especifico para el que queremos crear uno
		/// </summary>
		/// <param name="_eventoEspecifo">Evento especifico para el que queremos crear un controlador</param>
		public ViewModelFuncionHandlerEventoItem(EventInfo _eventoEspecifo)
		{
			TipoHandler = _eventoEspecifo.EventHandlerType;
		}

		/// <summary>
		/// Constructor utilizado cuando no existe un controlador pero conocemos
		/// el tipo de handler de los eventos para los que queremos crear uno
		/// </summary>
		/// <param name="_tipoHandler">Tipo del handler que tienen los eventos para los que queremos crear una funcion</param>
		public ViewModelFuncionHandlerEventoItem(Type _tipoHandler)
			:base()
		{
			TipoHandler = _tipoHandler;

			if (!typeof(MulticastDelegate).IsAssignableFrom(_tipoHandler))
				SistemaPrincipal.LoggerGlobal.LogCrash($"{nameof(_tipoHandler)} debe ser un {nameof(MulticastDelegate)}");
		}

		/// <summary>
		/// Constructor utilizado cuando tenemos un controlador existente
		/// </summary>
		/// <param name="_controlador">Controlador del handler</param>
		/// <param name="_eventoEspecifico">Evento especifico al que esta vinculado este <paramref name="_controlador"/></param>
		public ViewModelFuncionHandlerEventoItem(ControladorFuncion_HandlerEvento _controlador, EventInfo _eventoEspecifico = null) 
			: base(_controlador)
		{
			TipoHandler = ((ControladorFuncion_HandlerEvento)ControladorGenerico).TipoHandler;

			EventoEspecifico = _eventoEspecifico;

			//Si se especifico un evento en especifico nos aseguramos que su handler sea compatible con la funcion
			if (EventoEspecifico is not null && EventoEspecifico.EventHandlerType != TipoHandler)
			{
				SistemaPrincipal.LoggerGlobal.LogCrash($"El {nameof(_controlador)}({TipoHandler}) no es compatible con el {nameof(_eventoEspecifico)}({_eventoEspecifico.EventHandlerType.ObtenerNombreAmigableDelegado()})");
			}
		}

		protected override void ActualizarCaracteristicas()
		{
			CaracteristicasItem.Elementos.Clear();

			if (EventoEspecifico is not null)
			{
				CaracteristicasItem.Add(new ViewModelCaracteristicaItem
				{
					Titulo = "Evento",
					Valor = EventoEspecifico.ObtenerNombreAmigableMiembro()
				});

				return;
			}

			if(TipoHandler is null)
				return;

			CaracteristicasItem.Add(new ViewModelCaracteristicaItem
			{
				Titulo = "Tipo de evento",
				Valor = TipoHandler.ObtenerNombreAmigableDelegado()
			});
		}

		protected override void ActualizarGruposDeBotones()
		{
			base.ActualizarGruposDeBotones();

			GruposDeBotones[0].Direccion = EDireccionItems.Horizontal;
		}

		protected override Action ObtenerAccionCrear()
		{
			return () =>
			{
				var dataContextActual = SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido;

				var nuevoVmCreacionFuncion = ControladorFuncionBase.CrearVMParaCrear(typeof(ControladorFuncion_HandlerEvento), vm =>
				{
					if (vm.Resultado.EsAceptarOFinalizar())
						ControladorGenerico = vm.CrearControlador();

					SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = dataContextActual;
				}, TipoHandler);

				SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = nuevoVmCreacionFuncion;
			};
		}
	}
}