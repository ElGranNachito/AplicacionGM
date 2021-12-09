using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Logica de <see cref="ModeloInfligidorDaño"/>
	/// </summary>
	public partial class ModeloInfligidorDaño
	{
		public ModeloInfligidorDaño() {}

		public ModeloInfligidorDaño(
			ModeloArgumentosDaño _argsDaño,
			ModeloPersonaje _personaje,
			ModeloHabilidad _habilidad,
			ModeloItem _item)
		{
			ArgumentosDaño = _argsDaño;
			Personaje = _personaje;
			Habilidad = _habilidad;
			Item = _item;

			Tipo |= ETipoInfligidorDaño.Personaje;

			if (Habilidad is not null)
			{
				Tipo |= ETipoInfligidorDaño.Habilidad;
			}
			else if (Item is not null)
			{
				Tipo |= ETipoInfligidorDaño.Item;
			}
		}

		/// <summary>
		/// Obtiene el <see cref="IInfligidorDaño"/> representado por este modelo
		/// </summary>
		/// <returns><see cref="IInfligidorDaño"/> representado por este modelo</returns>
		public IInfligidorDaño ObtenerInfligidorDaño()
		{
			ModeloBase modelo = Personaje;
			modelo ??= Item;
			modelo ??= Habilidad;

			if (modelo is null)
			{
				SistemaPrincipal.LoggerGlobal.Log($"Se intento obtener el controlador para un {nameof(ModeloInfligidorDaño)} al cual no se le ha asignado un modelo", ESeveridad.Error);

				return null;
			}

			return SistemaPrincipal.ObtenerControlador(modelo, modelo.GetType().ObtenerTipoControladorParaModelo()) as IInfligidorDaño;
		}
	}
}