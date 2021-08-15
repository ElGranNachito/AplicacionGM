using System;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Clase abstracta base de todos los controladores
	/// </summary>
	public abstract class ControladorBase
	{}

	/// <summary>
	/// Clase base de todos los controladores
	/// </summary>
	/// <typeparam name="TipoModelo">Tipo del modelo que este controlador representa</typeparam>
    public class Controlador<TipoModelo> : ControladorBase
        where TipoModelo: ModeloBaseSK, new()
    {
		#region Eventos

		public event Action OnControladorEliminado = delegate { }; 

		#endregion

		#region Campos

		/// <summary>
		/// Instancia del modelo que este controlador representa
		/// </summary>
		public TipoModelo modelo;

		#endregion

		#region Constructores

		/// <summary>
		/// Constructor default
		/// </summary>
		public Controlador()
		{
			SistemaPrincipal.LoggerGlobal.Log("Creado un controlador a traves del constructor sin parametros", ESeveridad.Advertencia);
		}

		/// <summary>
		/// Constructor default
		/// </summary>
		/// <param name="_modelo"></param>
		public Controlador(TipoModelo _modelo)
		{
			modelo = _modelo;

			SistemaPrincipal.AñadirControlador(modelo, this);
		} 

		#endregion

		#region Metodos

		/// <summary>
		/// Funcion encargada de eliminar el modelo de la base de datos
		/// </summary>
		public virtual void Eliminar()
		{
			SistemaPrincipal.QuitarControlador(modelo);

			OnControladorEliminado();
		}

		/// <summary>
		/// Guarda el <see cref="modelo"/>
		/// </summary>
		public virtual void Guardar(){}

		/// <summary>
		/// Actualiza las propiedades de <see cref="modelo"/> con las de <paramref name="nuevoModelo"/>.
		/// </summary>
		/// <param name="nuevoModelo"><see cref="TipoModelo"/> con el que actualizar el <see cref="modelo"/></param>
		/// <param name="eliminarSiNuevoModeloEsNull">Indica si se debe eliminar el controlador cuando <paramref name="nuevoModelo"/> sea null</param>
		public virtual void ActulizarModelo(TipoModelo nuevoModelo, bool eliminarSiNuevoModeloEsNull = false)
		{
			if (nuevoModelo == null)
			{
				if (eliminarSiNuevoModeloEsNull)
					Eliminar();
				else
					SistemaPrincipal.LoggerGlobal.Log($"{nameof(nuevoModelo)} fue null!", ESeveridad.Error);

				return;
			}
		}

		#endregion
    }
}
