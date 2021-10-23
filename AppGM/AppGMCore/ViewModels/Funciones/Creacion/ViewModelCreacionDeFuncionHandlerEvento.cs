using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppGM.Core
{
	public class ViewModelCreacionDeFuncionHandlerEvento : ViewModelCreacionDeFuncion<Action<object[]>>
	{
		public EventInfo Evento { get; init; }

		public ViewModelCreacionDeFuncionHandlerEvento(
			Action<ViewModelCreacionDeFuncionBase> _accionSalir,
			ControladorFuncion<Action<object[]>> _controladorFuncion,
			EventInfo _evento) 

			: base(_accionSalir,
				_controladorFuncion,
				EPropositoFuncion.HandlerEvento)
		{
			Evento = _evento;

			if (Evento == null)
			{
				SistemaPrincipal.LoggerGlobal.LogCrash($"{nameof(Evento)} fue null");

				return;
			}
		}

		protected override void AsignarListaDeBloques()
		{
			throw new NotImplementedException();
		}

		protected override void AsignarVariablesBase()
		{
			VariablesBase = new List<BloqueVariable>(Evento.AddMethod.GetParameters().Select(p =>
			{
				return new BloqueVariable(ObtenerID(), p.Name, p.ParameterType, ETipoVariable.Parametro);
			}));
		}
	}
}
