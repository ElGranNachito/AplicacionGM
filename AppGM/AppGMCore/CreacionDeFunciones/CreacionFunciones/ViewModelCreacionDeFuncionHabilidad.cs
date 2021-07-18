using System;
using System.Collections.Generic;
using System.Linq;

namespace AppGM.Core
{
	/// <summary>
	/// <see cref="ViewModelCreacionDeFuncionBase"/> para la creacion de una habilidad
	/// </summary>
	public class ViewModelCreacionDeFuncionHabilidad : 
		ViewModelCreacionDeFuncion<Action<ControladorPersonaje, List<ControladorPersonaje>, List<object>>>
	{
		public override void CrearFuncion()
		{
			Compilador compilador = new Compilador(
				((from bloque in Bloques
				select bloque.GenerarBloque())
				.Concat(ObtenerVariables(null)))
				.ToList()
				);

			var resultado = compilador.Compilar<Action<ControladorPersonaje, ControladorPersonaje[], ControladorHabilidad>>();

			var controladorHabilidad = new ControladorHabilidad(new ModeloHabilidad {Nombre = "Ultra destructor de nada"});

			resultado.Funcion(null, null, controladorHabilidad);
		}

		protected override List<Type> AsignarListaDeBloques()
		{
			return new List<Type>
			{
				typeof(ViewModelBloqueDeclaracionVariable),
				typeof(ViewModelBloqueLlamarFuncion),
				typeof(ViewModelBloqueCondicionalCompleto)
			};
		}

		protected override List<BloqueVariable> AsignarVariablesBase()
		{
			return new List<BloqueVariable>
			{
				new BloqueVariable(  "Combate",  typeof(ControladorAdministradorDeCombate), ETipoVariable.Normal),
				new BloqueVariable( "Usuario", typeof(ControladorPersonaje), ETipoVariable.Parametro),
				new BloqueVariable( "Objetivos", typeof(ControladorPersonaje[]), ETipoVariable.Parametro),
				new BloqueVariable(Compilador.NombreVariableDueña, typeof(ControladorHabilidad), ETipoVariable.Parametro)
			};
		}
	}
}
