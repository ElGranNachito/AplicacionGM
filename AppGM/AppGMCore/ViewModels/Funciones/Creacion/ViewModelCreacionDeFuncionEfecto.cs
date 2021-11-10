using System;
using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel para la creacion de una funcion que representa aplicar un efecto
	/// </summary>
	public class ViewModelCreacionDeFuncionEfecto : ViewModelCreacionDeFuncion
		
		<Action<ControladorEfecto, ControladorEfectoSiendoAplicado, ControladorPersonaje, ControladorPersonaje, ControladorFuncionBase, object[]>>
	{
		public ViewModelCreacionDeFuncionEfecto(
			Action<ViewModelCreacionDeFuncionBase> _accionSalir, 
			ControladorFuncion<Action<ControladorEfecto, ControladorEfectoSiendoAplicado, ControladorPersonaje, ControladorPersonaje, ControladorFuncionBase, object[]>> _controladorFuncion) 
			
			: base(_accionSalir, _controladorFuncion, EPropositoFuncion.Efecto)
		{

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
			VariablesBase = new List<BloqueVariable>
			{
				new BloqueVariable(Compilador.Variables.VariableDueña, "Efecto", typeof(ControladorHabilidad), ETipoVariable.Parametro),
				new BloqueVariable(ObtenerID(), "AplicacionEfecto", typeof(ControladorEfectoSiendoAplicado), ETipoVariable.Parametro),
				new BloqueVariable(ObtenerID(), "Instigador", typeof(ControladorPersonaje), ETipoVariable.Parametro),
				new BloqueVariable(ObtenerID(), "Objetivo", typeof(ControladorPersonaje), ETipoVariable.Parametro)
			};
		}
	}
}
