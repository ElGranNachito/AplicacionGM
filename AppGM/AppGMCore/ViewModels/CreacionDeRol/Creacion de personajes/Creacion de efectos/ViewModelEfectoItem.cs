using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un <see cref="ControladorEfecto"/> en un <see cref="ViewModelCrearHabilidad"/>
	/// </summary>
	public class ViewModelEfectoItem : ViewModelItemLista
	{
		/// <summary>
		/// Controlador del efecto representado
		/// </summary>
		public ControladorEfecto ControladorEfecto { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_controladorEfecto">Controlador del efecto que representara esta instancia</param>
		public ViewModelEfectoItem(ControladorEfecto _controladorEfecto)
		{
			ControladorEfecto = _controladorEfecto;

			if (ControladorEfecto == null)
			{
				SistemaPrincipal.LoggerGlobal.Log($"{nameof(_controladorEfecto)} pasado es null!", ESeveridad.Error);

				return;
			}

			CaracteristicasItem.AddRange(new ViewModelCaracteristicaItem[]
			{
				new ViewModelCaracteristicaItem
				{
					Titulo = "Nombre",
					Valor = ControladorEfecto.NombreEfecto
				},

				new ViewModelCaracteristicaItem
				{
					Titulo = "Tipo",
					Valor = ControladorEfecto.TipoEfecto.ToString()
				}
			});
		}
	}
}
