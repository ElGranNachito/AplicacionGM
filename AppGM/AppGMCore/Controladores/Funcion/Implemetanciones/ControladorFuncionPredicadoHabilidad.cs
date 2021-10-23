using System;

using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Controlador para un predicado de un efecto
	/// </summary>
	public class ControladorFuncion_PredicadoHabilidad : ControladorFuncion<Func<ControladorHabilidad, ControladorPersonaje, ControladorPersonaje, ControladorFuncionBase, object[], bool>>
	{
		public ControladorFuncion_PredicadoHabilidad(ModeloFuncion _modelo)
			: base(_modelo)
		{ }

		public override ViewModelCreacionDeFuncionBase CrearVMParaEditar(Action<ViewModelCreacionDeFuncionBase> accionSalir)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Wrapper para llamar a la funcion subyacente de manera segura
		/// </summary>
		/// <param name="controladorHabilidad">Controlador de la habilidad que llama esta funcion</param>
		/// <param name="instigador">Personaje que utilizo la habilidad</param>
		/// <param name="objetivo">Personaje objetivo de la habilidad</param>
		/// <param name="parametrosExtra">Parametros extra que toma la funcion</param>
		/// <returns>Tupla con dos <see cref="bool"/>, el primero indica si se pudo ejecutar la funcion en su totalidad y el segundo contiene su resultado</returns>
		public (bool funcionEjecutadaConExito, bool resultadoFuncion) EjecutarFuncion(
			ControladorHabilidad controladorHabilidad,
			ControladorPersonaje instigador,
			ControladorPersonaje objetivo,
			params object[] parametrosExtra)
		{
			bool res = false;

			try
			{
				res = Funcion(controladorHabilidad, instigador, objetivo, this, parametrosExtra);
			}
			catch (Exception ex)
			{
				SistemaPrincipal.LoggerGlobal.Log($"Error al intentar ejecutar funcion {this}.{Environment.NewLine}{ex.Message}", ESeveridad.Error);

				return (false, res);
			}

			return (true, res);
		}
	}
}
