using System;
using System.Collections.Generic;
using System.Linq;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Clase abstracta base de todos los controladores
	/// </summary>
	public abstract class ControladorBase
	{
		public abstract ControladorVariableBase ObtenerControladorVariable(int idVariable);

		/// <summary>
		/// Obtiene un <see cref="ControladorVariableBase"/> guardada en el modelo representado por este controlador
		/// a partir de su <paramref name="idVariable"/>
		/// </summary>
		/// <param name="idVariable">Id de la variable que se quiere obtener</param>
		/// <returns>Controlador de la variable hallada o null si no se encontro la variable</returns>
		[AccesibleEnGuraScratch(nameof(ObtenerValorVariable))]
		public abstract object ObtenerValorVariable(int idVariable);

		/// <summary>
		/// Asigna un <paramref name="valor"/> a la variable con la <paramref name="idVariable"/> especificada
		/// </summary>
		/// <param name="idVariable">Id de la variable a la que se quiere asignar el <paramref name="valor"/></param>
		/// <param name="valor">Valor que se le asignara a la variable</param>
		[AccesibleEnGuraScratch("Asignar a variable")]
		public abstract void AsignarAVariable(int idVariable, object valor);

		/// <summary>
		/// Obtiene una <see cref="IControladorTiradaBase"/> guardada en el modelo representado por este controlador
		/// a partir de su <paramref name="idTirada"/>
		/// </summary>
		/// <param name="idTirada">Id de la tirada que se quiere obtener</param>
		/// <returns>Tirada hallada o null</returns>
		[AccesibleEnGuraScratch(nameof(ObtenerTirada))]
		public abstract IControladorTiradaBase ObtenerTirada(int idTirada);
	}

	/// <summary>
	/// Clase base de todos los controladores
	/// </summary>
	/// <typeparam name="TipoModelo">Tipo del modelo que este controlador representa</typeparam>
    public class Controlador<TipoModelo> : ControladorBase
        where TipoModelo: ModeloBaseSK, new()
    {
		#region Eventos

		/// <summary>
		/// Evento que se dispara cuando este controlador es eliminado
		/// </summary>
		public event Action OnControladorEliminado = delegate { }; 

		#endregion

		#region Campos & Propiedades

		/// <summary>
		/// Instancia del modelo que este controlador representa
		/// </summary>
		public TipoModelo modelo;

		/// <summary>
		/// Contiene todas los <see cref="ControladorVariableBase"/> de las variables persistentes guardados en el <see cref="modelo"/>
		/// </summary>
		protected Dictionary<int, ControladorVariableBase> mVariablesPersistenes;

		/// <summary>
		/// Contiene todas las <see cref="IControladorTiradaBase"/> guardadas en el <see cref="modelo"/>
		/// </summary>
		protected Dictionary<int, IControladorTiradaBase> mTiradas;

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
			}
		}

		public override ControladorVariableBase ObtenerControladorVariable(int idVariable)
		{
			if (mVariablesPersistenes.ContainsKey(idVariable))
				return mVariablesPersistenes[idVariable];

			SistemaPrincipal.LoggerGlobal.Log($"Se intento obtener una variable con id: {idVariable}, pero no se encuentra en {nameof(mVariablesPersistenes)}", ESeveridad.Error);

			return null;
		}

		public override object ObtenerValorVariable(int idVariable)
		{
			return ObtenerControladorVariable(idVariable)?.ObtenerValorVariable();
		}

		public override void AsignarAVariable(int idVariable, object valor)
		{
			ObtenerControladorVariable(idVariable)?.GuardarValorVariable(valor);
		}

		public override IControladorTiradaBase ObtenerTirada(int idTirada)
		{
			if (mTiradas.ContainsKey(idTirada))
				return mTiradas[idTirada];

			return null;
		}

		/// <summary>
		/// <para>
		///		Carga las variables persistentes guardadas en el <see cref="modelo"/> a <see cref="mVariablesPersistenes"/>
		/// </para>
		/// <para>
		///		El modelo del controlador debe ser de tipo <see cref="ModeloConVariablesYTiradas"/>. De lo contrario esta funcion no tendra efecto alguno
		///	</para>
		/// </summary>
		protected void CargarVariablesYTiradas()
		{
			if (modelo is ModeloConVariablesYTiradas modeloConVariables)
			{
				mVariablesPersistenes = new Dictionary<int, ControladorVariableBase>(modeloConVariables.Variables.Select(var =>
				{
					return new KeyValuePair<int, ControladorVariableBase>(var.Variable.IDVariable, ControladorVariableBase.CrearControladorCorrespondiente(var.Variable));
				}));

				mTiradas = new Dictionary<int, IControladorTiradaBase>(modeloConVariables.Tiradas.Select(var =>
				{
					return new KeyValuePair<int, IControladorTiradaBase>(var.Tirada.Id, IControladorTiradaBase.CrearControladorDeTiradaCorrespondiente(var.Tirada));
				}));
			}
			else
			{
				SistemaPrincipal.LoggerGlobal.Log($@"Se intentaron cargar variables persistentes desde un controlador cuyo modelo no es {nameof(ModeloConVariablesYTiradas)}
														Controlador: {this}", ESeveridad.Error);
			}
		}

		#endregion
	}
}