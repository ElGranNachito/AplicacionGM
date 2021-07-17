using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CoolLogs;

namespace AppGM.Core
{
	public abstract class ControladorFuncionBase : Controlador<ModeloFuncion>
	{
		private Dictionary<string, ModeloVariableFuncionBase> mVariablesPersistenes;

		public static ControladorFuncionBase CrearControladorCorrespondiente()
		{
			return null;
		}

		public ControladorFuncionBase(ModeloFuncion _modelo)
			: base(_modelo)
		{
		}

		[IndexerName("Variables")]
		public ModeloVariableFuncionBase this[string nombreVariable]
		{
			get
			{
				if (mVariablesPersistenes.ContainsKey(nombreVariable))
					return mVariablesPersistenes[nombreVariable];

				SistemaPrincipal.LoggerGlobal.Log($"Se intento obtener una variable ({nombreVariable}) pero no se encuentra en {nameof(mVariablesPersistenes)}", ESeveridad.Error);

				return null;
			}
		}
	}

	public abstract class ControladorFuncion<TipoFuncion> : ControladorFuncionBase
	{
		public TipoFuncion funcion;

		public ControladorFuncion(ModeloFuncion _modelo)
			: base(_modelo)
		{
			Compilador compilador = new Compilador(modelo.NombreFuncion, modelo.Id);

			funcion = compilador.Compilar<TipoFuncion>().Funcion;
		}
	}

	public class ControladorFuncion_Efecto : ControladorFuncion<Func<ControladorEfecto, ControladorPersonaje, List<ControladorPersonaje>, ControladorFuncionBase, bool>>
	{
		public ControladorFuncion_Efecto(ModeloFuncion _modelo)
			: base(_modelo)
		{ }


	}

	public class ControladorFuncion_Habilidad : ControladorFuncion<Func<ControladorHabilidad, ControladorPersonaje, List<ControladorPersonaje>, ControladorFuncionBase, bool>>
	{
		public ControladorFuncion_Habilidad(ModeloFuncion _modelo)
			: base(_modelo)
		{}


	}
}