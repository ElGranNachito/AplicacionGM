using System;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Controlador para un predicado de un efecto
	/// </summary>
	public class ControladorFuncion_PredicadoEfecto : ControladorFuncion<Func<ControladorEfectoSiendoAplicado, ControladorEfecto, ControladorPersonaje, ControladorPersonaje, ControladorFuncionBase, object[], bool>>
	{
		public ControladorFuncion_PredicadoEfecto(ModeloFuncion _modelo)
			: base(_modelo)
		{ }

		public override ViewModelCreacionDeFuncionBase CrearVMParaEditar(Action<ViewModelCreacionDeFuncionBase> accionSalir)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Wrapper para llamar a la funcion subyacente de manera segura
		/// </summary>
		/// <param name="controladorAplicacionEfecto">Controlador de la aplicacion del efecto que llama esta funcion</param>
		/// <param name="controladorEfecto">Controlador del efecto</param>
		/// <param name="instigador">Personaje responsable de aplicar el efecto</param>
		/// <param name="objetivo">Personaje al que se le aplica el efecto</param>
		/// <param name="parametrosExtra">Parametros extra que toma la funcion</param>
		/// <returns>Tupla con dos <see cref="bool"/>, el primero indica si se pudo ejecutar la funcion en su totalidad y el segundo contiene su resultado</returns>
		public (bool funcionEjecutadaConExito, bool resultadoFuncion) EjecutarFuncion(
			ControladorEfectoSiendoAplicado controladorAplicacionEfecto,
			ControladorEfecto controladorEfecto,
			ControladorPersonaje instigador,
			ControladorPersonaje objetivo,
			params object[] parametrosExtra)
		{
			bool res = false;

			try
			{
				res = Funcion(controladorAplicacionEfecto, controladorEfecto, instigador, objetivo, this, parametrosExtra);
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
