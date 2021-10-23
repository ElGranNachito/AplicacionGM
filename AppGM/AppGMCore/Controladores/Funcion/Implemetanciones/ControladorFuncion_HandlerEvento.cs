using System;
using System.Reflection;

namespace AppGM.Core
{
	/// <summary>
	/// Controlador para una funcion encargada de lidiar con eventos
	/// </summary>
	public class ControladorFuncion_HandlerEvento : ControladorFuncion<Action<object[]>>
	{
		public EventInfo Evento { get; init; }

		public ControladorFuncion_HandlerEvento(ModeloFuncion _modelo, EventInfo _evento) : base(_modelo)
		{
			Evento = _evento;
		}

		public override ViewModelCreacionDeFuncionBase CrearVMParaEditar(Action<ViewModelCreacionDeFuncionBase> accionSalir)
		{
			throw new NotImplementedException();
		}
	}
}
