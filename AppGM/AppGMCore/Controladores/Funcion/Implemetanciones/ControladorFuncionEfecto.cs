using System;

using CoolLogs;

namespace AppGM.Core
{

	/// <summary>
	/// Controlador para una funcion de un efecto
	/// </summary>
	public class ControladorFuncion_Efecto : ControladorFuncion<Action<ControladorEfectoSiendoAplicado, ControladorEfecto, ControladorPersonaje, ControladorPersonaje, ControladorFuncionBase, object[]>>
	{
		public ControladorFuncion_Efecto(ModeloFuncion _modelo)
			: base(_modelo)
		{ }

		public override ViewModelCreacionDeFuncionBase CrearVMParaEditar(Action<ViewModelCreacionDeFuncionBase> accionSalir)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Wrapper para llamar a la funcion subyacente
		/// </summary>
		/// <param name="controladorAplicacionEfecto">Controlador de la aplicacion del efecto que llama esta funcion</param>
		/// <param name="controladorEfecto">Controlador del efecto</param>
		/// <param name="instigador">Personaje responsable de aplicar el efecto</param>
		/// <param name="objetivo">Personaje al que se le aplica el efecto</param>
		/// <param name="parametrosExtra">Parametros extra que toma la funcion</param>
		/// <returns><see cref="bool"/> indicando si se la funcion se ejecuto con extio</returns>
		public bool EjecutarFuncion(
			ControladorEfectoSiendoAplicado controladorAplicacionEfecto,
			ControladorEfecto controladorEfecto,
			ControladorPersonaje instigador,
			ControladorPersonaje objetivo,
			params object[] parametrosExtra)
		{
			try
			{
				Funcion(controladorAplicacionEfecto, controladorEfecto, instigador, objetivo, this, parametrosExtra);
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
