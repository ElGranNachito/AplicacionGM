using System;
using System.Collections.Generic;
using System.Linq;

namespace AppGM.Core.ViewModels.CreacionDeHabilidades
{
	/// <summary>
	/// <see cref="ViewModelCreacionDeFuncionBase"/> para la creacion de una habilidad
	/// </summary>
	public class ViewModelCreacionDeFuncionHabilidad : 
		ViewModelCreacionDeFuncion<Action<ControladorPersonaje, List<ControladorPersonaje>, List<object>>>
	{
		public override void CrearFuncion()
		{
			//TODO: Implementar de verdad
			Compilador.IniciarCompilacion(
				(from bloque in BloquesColocados.Elementos
					select bloque.GenerarBloque()).ToList());
		}

		protected override List<ViewModelBloqueFuncionBase> AsignarListaDeBloques()
		{
			return new List<ViewModelBloqueFuncionBase>
			{
				new ViewModelBloqueDeclaracionVariable(this),
				new ViewModelBloqueLlamarFuncion(this)
			};
		}

		protected override List<BloqueVariable> AsignarVariablesBase()
		{
			return new List<BloqueVariable>
			{
				new BloqueVariable(  "Combate",  typeof(ControladorAdministradorDeCombate)),
				new BloqueVariable ("Usuario", typeof(ControladorPersonaje)),
				new BloqueVariable( "Objetivos", typeof(List<ControladorPersonaje>))
			};
		}
	}
}
