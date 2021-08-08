
using System.Windows.Input;

namespace AppGM.Core
{
	public class ViewModelFuncionItem<TControlador> : ViewModel
		where TControlador: ControladorFuncionBase
	{
		public TControlador ControladorFuncion { get; private set; }

		public string NombreFuncion => ControladorFuncion.NombreFuncion;

		public ICommand ComandoEditar { get; private set; }

		public ViewModelFuncionItem(TControlador _controladorFuncion)
		{
			ControladorFuncion = _controladorFuncion;

			ComandoEditar = new Comando(() =>
			{
				var dataContextActual = SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido;

				SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido =
					ControladorFuncion.CrearVMParaEditar(vm =>
						{
							SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = dataContextActual;
						});
			});
		}
	}
}
