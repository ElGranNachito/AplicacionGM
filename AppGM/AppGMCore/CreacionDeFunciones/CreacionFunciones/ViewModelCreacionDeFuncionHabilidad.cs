using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// <see cref="ViewModelCreacionDeFuncionBase"/> para la creacion de una habilidad
	/// </summary>
	public class ViewModelCreacionDeFuncionHabilidad : 
		ViewModelCreacionDeFuncion<Action<ControladorFuncionBase, ControladorPersonaje, ControladorPersonaje[], ControladorHabilidad, object[]>>
	{
		public ViewModelCreacionDeFuncionHabilidad(ControladorFuncion_Habilidad _controladorFuncion = null)
			: base(_controladorFuncion, EPropositoFuncion.Habilidad){}

		protected override void Guardar()
		{
			ControladorFuncion.ActualizarBloques(VariablesBase.Concat(
				from bloque in Bloques
				select bloque.GenerarBloque()).ToList());
		}

		protected override List<ViewModelBloqueMuestra> AsignarListaDeBloques()
		{
			return new List<ViewModelBloqueMuestra>
			{
				new ViewModelBloqueMuestra(this, typeof(ViewModelBloqueDeclaracionVariable)),
				new ViewModelBloqueMuestra(this, typeof(ViewModelBloqueLlamarFuncion)),
				new ViewModelBloqueMuestra(this, typeof(ViewModelBloqueCondicionalCompleto))
			};
		}

		protected override List<BloqueVariable> AsignarVariablesBase()
		{
			return new List<BloqueVariable>
			{
				new BloqueVariable(ObtenerID(),"Combate",  typeof(ControladorAdministradorDeCombate), ETipoVariable.Normal),
				new BloqueVariable(ObtenerID(),"Usuario", typeof(ControladorPersonaje), ETipoVariable.Parametro),
				new BloqueVariable(ObtenerID(), "Objetivos", typeof(ControladorPersonaje[]), ETipoVariable.Parametro),
				new BloqueVariable(Compilador.Variables.VariableDueña, "ControladorHabilidad", typeof(ControladorHabilidad), ETipoVariable.Parametro)
			};
		}
	}
}
