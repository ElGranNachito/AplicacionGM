using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa un control para la creacion/edicion/eliminacion de <see cref="ModeloFuncion_HandlerEvento"/>
	/// asociados con determinados eventos o handlers de eventos
	/// </summary>
	/// <typeparam name="TRelacionHandlerModelo">Tipo de la relacion entre el modelo siendo editado y las funciones handler</typeparam>
	/// <typeparam name="TOtro">Parametro generico requerido por <typeparamref name="TRelacionHandlerModelo"/></typeparam>
	public class ViewModelCreacionHandlersEvento<TRelacionHandlerModelo, TOtro> : ViewModel

		where TOtro : ModeloBase
		where TRelacionHandlerModelo : TIFuncionHandlerEvento<TOtro>
	{
		/// <summary>
		/// Indica si podemos vincularnos a un evento existente
		/// </summary>
		public bool PuedeVincularAEventoExistente => Evento != null;

		/// <summary>
		/// Tipo del handler del evento
		/// </summary>
		public Type TipoHandler { get; init; }

		/// <summary>
		/// Evento especifico con el que estamos lidiando
		/// </summary>
		public EventInfo Evento { get; init; }

		/// <summary>
		/// Viewmodel del boton vincular
		/// </summary>
		public ViewModelBoton ViewModelBotonVincular { get; set; }

		/// <summary>
		/// Viewmodel que muestra opciones y datos de la funcion actualmente seleccionada
		/// </summary>
		public ViewModelFuncionHandlerEventoItem ViewModelHandlerActual { get; set; }

		/// <summary>
		/// Combobox con los handlers disponibles
		/// </summary>
		public ViewModelComboBox<TRelacionHandlerModelo> ViewModelComboBoxHandlersDisponibles { get; set; }

		/// <summary>
		/// Lista de handlers del modelo que estamos editando
		/// </summary>
		public List<TRelacionHandlerModelo> ListaHandlersModelo { get; set; }

		/// <summary>
		/// Constructor que crea una instancia de este viewmodel para la creacion/edicion/eliminacion de handlers para delegados
		/// </summary>
		/// <param name="_listaHandlersModelo">Lista de handlers del modelo que estamos editando</param>
		/// <param name="_tipoHandler">Tipo del handler del evento</param>
		public ViewModelCreacionHandlersEvento(List<TRelacionHandlerModelo> _listaHandlersModelo, Type _tipoHandler)
		{
			TipoHandler = _tipoHandler;
			ListaHandlersModelo = _listaHandlersModelo;

			ViewModelHandlerActual = new ViewModelFuncionHandlerEventoItem(TipoHandler);

			ViewModelComboBoxHandlersDisponibles = new ViewModelComboBox<TRelacionHandlerModelo>(ListaHandlersModelo.Where(h => h.Funcion.TipoHandler == TipoHandler).ToList());

			Inicializar();
		}

		/// <summary>
		/// Constructor que crea una instancia de este viewmodel para la creacion/edicion/eliminacion de handlers para eventos especificos
		/// </summary>
		/// <param name="_listaHandlersModelo">Lista de handlers del modelo que estamos editando</param>
		/// <param name="_evento">Tipo del handler del evento</param>
		public ViewModelCreacionHandlersEvento(List<TRelacionHandlerModelo> _listaHandlersModelo, EventInfo _evento)
		{
			Evento = _evento;
			TipoHandler = Evento.EventHandlerType;
			ListaHandlersModelo = _listaHandlersModelo;

			ViewModelHandlerActual = new ViewModelFuncionHandlerEventoItem(Evento);

			ViewModelComboBoxHandlersDisponibles = new ViewModelComboBox<TRelacionHandlerModelo>(ListaHandlersModelo.Where(h => h.Funcion.TipoHandler == TipoHandler).ToList());

			Inicializar();
		}

		/// <summary>
		/// Inicializa propiedades y eventos
		/// </summary>
		private void Inicializar()
		{
			Action accionBotonVincular = () =>
			{
				var relacionActual = ViewModelComboBoxHandlersDisponibles.Valor;

				if (relacionActual.EstaVinculadoA(Evento.Name))
				{
					relacionActual.DesvincularDe(Evento.Name);
				}
				else
				{
					relacionActual.VincularA(Evento.Name);
				}
			};

			ViewModelBotonVincular = new(PuedeVincularAEventoExistente ? accionBotonVincular: null, "BotonVincular", string.Intern("Vincular"), this);
			ViewModelBotonVincular.EstaHabilitado = PuedeVincularAEventoExistente;

			ViewModelComboBoxHandlersDisponibles.OnValorSeleccionadoCambio += (anterior, actual) =>
			{
				if(actual.valor is not null)
					ViewModelHandlerActual.ControladorGenerico = SistemaPrincipal.ObtenerControlador(actual.valor.Funcion, typeof(ControladorFuncion_HandlerEvento)) as ControladorFuncionBase;

				ActualizarBotonVincular();
			};
		}

		/// <summary>
		/// Actualiza el texto del boton dependiendo de si el handler actualmente seleccionado esta vinculado o no
		/// </summary>
		private void ActualizarBotonVincular()
		{
			var relacionActual = ViewModelComboBoxHandlersDisponibles.Valor;

			if (relacionActual is null)
			{
				ViewModelBotonVincular.EsVisible = false;

				return;
			}

			if (relacionActual.EstaVinculadoA(Evento.Name))
			{
				ViewModelBotonVincular.Contenido = string.Intern("Desvincular");
			}
			else
			{
				ViewModelBotonVincular.Contenido = string.Intern("Vincular");
			}
		}
	}
}