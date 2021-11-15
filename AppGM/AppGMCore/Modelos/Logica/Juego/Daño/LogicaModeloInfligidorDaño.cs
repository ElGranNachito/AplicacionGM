using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Logica de <see cref="ModeloInfligidorDaño"/>
	/// </summary>
	public partial class ModeloInfligidorDaño
	{
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