using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AppGM.Core
{
	public class ViewModelCreacionDeFuncionHandlerEvento : ViewModelCreacionDeFuncion<Action<ControladorFuncionBase, object[]>>
	{
		public Type TipoHandler { get; init; }

		public ViewModelCreacionDeFuncionHandlerEvento(
			Action<ViewModelCreacionDeFuncionBase> _accionSalir,
			Type _tipoHandler,
			ControladorFuncion<Action<ControladorFuncionBase, object[]>> _controladorFuncion = null) 

			: base(_accionSalir,
				_controladorFuncion,
				EPropositoFuncion.HandlerEvento)
		{
			TipoHandler = _tipoHandler;
			
			if (TipoHandler == null)
			{
				SistemaPrincipal.LoggerGlobal.LogCrash($"{nameof(TipoHandler)} fue null");
			}

			if (!TipoHandler.IsSubclassOf(typeof(MulticastDelegate)))
			{
				SistemaPrincipal.LoggerGlobal.LogCrash($"{nameof(TipoHandler)} debe ser un subtipo de {nameof(MulticastDelegate)}");
			}
		}

		protected override void AsignarListaDeBloques()
		{
			BloquesDisponibles = new ViewModelListaDeElementos<ViewModelBloqueMuestra>(new[]
			{
				new ViewModelBloqueMuestra(this, typeof(ViewModelBloqueDeclaracionVariable)),
				new ViewModelBloqueMuestra(this, typeof(ViewModelBloqueLlamarFuncion)),
				new ViewModelBloqueMuestra(this, typeof(ViewModelBloqueCondicionalCompleto))
			});
		}

		protected override void AsignarVariablesBase()
		{
			var parametrosMetodoInvoke =  TipoHandler.GetMethod("Invoke").GetParameters();

			VariablesBase = parametrosMetodoInvoke.Select(p =>
					new BloqueVariable(ObtenerID(), p.Name, p.ParameterType, ETipoVariable.ParametroCreadoPorElUsuario)).ToList();
		}
	}
}