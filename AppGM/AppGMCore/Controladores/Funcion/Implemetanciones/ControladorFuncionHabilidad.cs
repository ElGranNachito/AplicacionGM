using System;

using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Controlador para una funcion de una habilidad
	/// </summary>
	public class ControladorFuncion_Habilidad : ControladorFuncion<Action<ControladorHabilidad, ControladorPersonaje, ControladorPersonaje, ControladorFuncionBase, object[]>>
	{
		public ControladorFuncion_Habilidad(ModeloFuncion _modelo)
			: base(_modelo)
		{ }

		public override ViewModelCreacionDeFuncionBase CrearVMParaEditar(Action<ViewModelCreacionDeFuncionBase> accionSalir)
		{
			return new ViewModelCreacionDeFuncionHabilidad(accionSalir, this);
		}

		/// <summary>
		/// Wrapper para llamar a la funcion subyacente de manera segura
		/// </summary>
		/// <param name="controladorEfecto">Controlador del efecto</param>
		/// <param name="instigador">Personaje responsable de aplicar el efecto</param>
		/// <param name="objetivo">Personaje al que se le aplica el efecto</param>
		/// <param name="parametrosExtra">Parametros extra que toma la funcion</param>
		/// <returns><see cref="bool"/> indicando si se la funcion se ejecuto con extio</returns>
		public bool EjecutarFuncion(
			ControladorHabilidad controladorhabilidad,
			ControladorPersonaje instigador,
			ControladorPersonaje objetivo,
			params object[] parametrosExtra)
		{
			try
			{
				Funcion(controladorhabilidad, instigador, objetivo, this, parametrosExtra);
			}
			catch (Exception ex)
			{
				SistemaPrincipal.LoggerGlobal.Log($"Error al intentar ejecutar funcion {this}.{Environment.NewLine}{ex.Message}", ESeveridad.Error);

				return false;
			}

			return true;
		}
	}
}
