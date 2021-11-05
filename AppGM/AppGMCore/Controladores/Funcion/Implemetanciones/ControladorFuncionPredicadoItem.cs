using System;

using CoolLogs;

namespace AppGM.Core
{
	public class ControladorFuncion_PredicadoItem : ControladorFuncion<Func<ControladorItem, ControladorPersonaje, ControladorPersonaje, ControladorFuncionBase, object[], bool>>
	{
		public ControladorFuncion_PredicadoItem(ModeloFuncion _modelo) : base(_modelo)
		{
		}

		public (bool funcionEjecutadaConExito, bool resultadoFuncion) EjecutarFuncion(
			ControladorItem item,
			ControladorPersonaje usuario, 
			ControladorPersonaje objetivo,
			params object[] parametrosExtra)
		{
			bool res = false;

			try
			{
				res = Funcion(item, usuario, objetivo, this, parametrosExtra);
			}
			catch (Exception ex)
			{
				SistemaPrincipal.LoggerGlobal.Log($"Error al intentar ejecutar funcion {this}.{Environment.NewLine}{ex.Message}", ESeveridad.Error);

				return (false, res);
			}

			return (true, res);
		}

		public override ViewModelCreacionDeFuncionBase CrearVMParaEditar(Action<ViewModelCreacionDeFuncionBase> accionSalir)
		{
			throw new NotImplementedException();
		}
	}
}
