using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Representa a una funcion en un <see cref="ViewModelCrearHabilidad"/>
	/// </summary>
	/// <typeparam name="TControlador">Tipo del <see cref="ControladorFuncionBase"/></typeparam>
	public class ViewModelFuncionItem<TControlador> : ViewModelItemLista
		where TControlador: ControladorFuncionBase
	{
		/// <summary>
		/// Controlador de la funcion
		/// </summary>
		public TControlador ControladorFuncion { get; private set; }

		/// <summary>
		/// Nombre de la funcion
		/// </summary>
		public string NombreFuncion => ControladorFuncion.NombreFuncion;

		//TODO: Hacer que de verdad indique el tipo de la funcion
		/// <summary>
		/// Tipo de la funcion
		/// </summary>
		public EPropositoFuncion TipoFuncion => EPropositoFuncion.NINGUNO;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_controladorFuncion"></param>
		public ViewModelFuncionItem(TControlador _controladorFuncion)
		{
			ControladorFuncion = _controladorFuncion;

			if (ControladorFuncion == null)
			{
				SistemaPrincipal.LoggerGlobal.Log($"{nameof(_controladorFuncion)} pasado es null!", ESeveridad.Error);

				return;
			}

			CaracteristicasItem.AddRange(new ViewModelCaracteristicaItem[]
			{
				new ViewModelCaracteristicaItem
				{
					Titulo = "Nombre",
					Valor = NombreFuncion
				},

				new ViewModelCaracteristicaItem
				{
					Titulo = "Tipo",
					Valor = TipoFuncion.ToString()
				}
			});

			ComandoEditar = new Comando(() =>
			{
				//Obtenemos el vm actual de la ventana
				var dataContextActual = SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido;

				//Creamos el vm para editar la funcion
				SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido =
					ControladorFuncion.CrearVMParaEditar(vm =>
						{
							SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = dataContextActual;
						});
			});

			ComandoEliminar = new Comando(() =>
			{
				//TODO: Mostrar mensaje de confirmacion
				ControladorFuncion.Eliminar();
			});
		}
	}
}
