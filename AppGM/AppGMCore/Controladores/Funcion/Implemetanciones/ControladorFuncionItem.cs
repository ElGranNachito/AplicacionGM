using System;
using CoolLogs;

namespace AppGM.Core
{
	public class ControladorFuncion_Item : ControladorFuncion<Action<ControladorItem, ControladorPersonaje, ControladorPersonaje, ControladorFuncionBase, object[]>>
	{
		public ControladorFuncion_Item(ModeloFuncion _modelo) : base(_modelo)
		{
		}

		public bool EjecutarFuncion(
			ControladorItem item, 
			ControladorPersonaje usuario,
			ControladorPersonaje objetivo,
			params object[] parametrosExtra)
		{
			try
			{
				Funcion(item, usuario, objetivo, this, parametrosExtra);
			}
			catch (Exception ex)
			{
				SistemaPrincipal.LoggerGlobal.Log($"Error al intentar ejecutar funcion {this}.{Environment.NewLine}{ex.Message}", ESeveridad.Error);

				return false;
			}

			return true;
		}

		public override ViewModelCreacionDeFuncionBase CrearVMParaEditar(Action<ViewModelCreacionDeFuncionBase> accionSalir)
		{
			throw new NotImplementedException();
		}
	}
}
