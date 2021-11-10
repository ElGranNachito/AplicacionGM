using System;
using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa un control para la creacion de una funcion de uso de un <see cref="ModeloItem"/>
	/// </summary>
	public class ViewModelCreacionDeFuncionItem : ViewModelCreacionDeFuncion<Action<ControladorItem, ControladorPersonaje, ControladorPersonaje, ControladorFuncionBase, object[]>>
	{
		public ViewModelCreacionDeFuncionItem(
			Action<ViewModelCreacionDeFuncionBase> _accionSalir, 
			ControladorFuncion<Action<ControladorItem, ControladorPersonaje, ControladorPersonaje, ControladorFuncionBase, object[]>> _controladorFuncion = null) 
			
			: base(_accionSalir, _controladorFuncion, EPropositoFuncion.UsoItem)
		{}

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
				new BloqueVariable(Compilador.Variables.VariableDueña, "ControladorItem", typeof(ControladorItem), ETipoVariable.Parametro),
				new BloqueVariable(ObtenerID(), "Usuario", typeof(ControladorPersonaje), ETipoVariable.Parametro),
				new BloqueVariable(ObtenerID(), "Objetivo", typeof(ControladorPersonaje), ETipoVariable.Parametro),
			};
		}
	}
}
