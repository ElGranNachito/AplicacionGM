using System;
using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa un control para la creacion de una funcion que indica si se puede utilizar un elemento
	/// </summary>
	public class ViewModelCreacionDeFuncionPredicadoItem : ViewModelCreacionDeFuncion<Func<ControladorItem, ControladorPersonaje, ControladorPersonaje, ControladorFuncionBase, object[], bool>>
	{
		public ViewModelCreacionDeFuncionPredicadoItem(
			Action<ViewModelCreacionDeFuncionBase> _accionSalir,
			ControladorFuncion<Func<ControladorItem, ControladorPersonaje, ControladorPersonaje, ControladorFuncionBase, object[], bool>> _controladorFuncion) 
			
			: base(_accionSalir, _controladorFuncion, EPropositoFuncion.UsoItem)
		{
		}

		protected override void AsignarListaDeBloques()
		{
			BloquesDisponibles = new ViewModelListaDeElementos<ViewModelBloqueMuestra>
			{
				new ViewModelBloqueMuestra(this, typeof(ViewModelBloqueDeclaracionVariable)),
				new ViewModelBloqueMuestra(this, typeof(ViewModelBloqueLlamarFuncion)),
				new ViewModelBloqueMuestra(this, typeof(ViewModelBloqueCondicionalCompleto))
			};
		}

		protected override void AsignarVariablesBase()
		{
			VariablesBase = new List<BloqueVariable>
			{
				new BloqueVariable(ObtenerID(),"Usuario", typeof(ControladorPersonaje), ETipoVariable.Parametro),
				new BloqueVariable(ObtenerID(), "Objetivo", typeof(ControladorPersonaje), ETipoVariable.Parametro),
				new BloqueVariable(Compilador.Variables.VariableDueña, "ControladorItem", typeof(ControladorItem), ETipoVariable.Parametro)
			};
		}
	}
}
