using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Clase base de todos los controladores
	/// </summary>
	/// <typeparam name="TipoModelo">Tipo del modelo que este controlador representa</typeparam>
    public class Controlador<TipoModelo> : ControladorBase
        where TipoModelo: ModeloBase
    {
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
		/// Contiene todas las <see cref="AppGM.Core.ControladorTiradaBase"/> guardadas en el <see cref="modelo"/>
		/// </summary>
		protected Dictionary<int, ControladorTiradaBase> mTiradas;

		public IReadOnlyList<ControladorVariableBase> Variables => mVariablesPersistenes.Values.ToList();

		public override ModeloBase Modelo
		{
			get => modelo;
			protected set {}
		}

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

		public override ControladorVariableBase ObtenerControladorVariable(string nombreVariable)
		{
			return null;
		}

		public override object ObtenerValorVariable(int idVariable)
		{
			return ObtenerControladorVariable(idVariable)?.ObtenerValorVariable();
		}

		public override object ObtenerValorVariable(string nombreVariable)
		{
			//TODO: Implementar
			return null;
		}

		public override void AsignarAVariable(int idVariable, object valor)
		{
			ObtenerControladorVariable(idVariable)?.GuardarValorVariable(valor);
		}

		public override ControladorTiradaBase ObtenerTirada(int idTirada)
		{
			if (mTiradas.ContainsKey(idTirada))
				return mTiradas[idTirada];

			return null;
		}

		public override ControladorTiradaBase ObtenerTirada(string nombreTirada)
		{
			throw new System.NotImplementedException();
		}

		public override List<ControladorTiradaBase> ObtenerTiradas()
		{
			return mTiradas.Values.ToList();
		}

		/// <summary>
		/// Metodo destinado a lidia con el evento de que se cree el <see cref="ControladorBase"/> que necesitamos
		/// </summary>
		/// <param name="modelo"><see cref="ModeloBase"/> para el que se creo el <paramref name="controlador"/></param>
		/// <param name="controlador"><see cref="ControladorBase"/> que se creo para el <paramref name="modelo"/></param>
		protected virtual void ControladorParaModeloCreadoHandler(ModeloBase modelo, ControladorBase controlador)
		{
			modelo.OnControladorCreado -= ControladorParaModeloCreadoHandler;
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
					return new KeyValuePair<int, ControladorVariableBase>(var.IDVariable, ControladorVariableBase.CrearControladorCorrespondiente(var));
				}));

				mTiradas = new Dictionary<int, ControladorTiradaBase>(modeloConVariables.Tiradas.Select(var =>
				{
					return new KeyValuePair<int, ControladorTiradaBase>(var.Id, ControladorTiradaBase.CrearControladorDeTiradaCorrespondiente(var));
				}));
			}
			else
			{
				SistemaPrincipal.LoggerGlobal.Log($@"Se intentaron cargar variables persistentes desde un controlador cuyo modelo no es {nameof(ModeloConVariablesYTiradas)}
														Controlador: {this}", ESeveridad.Error);
			}
		}

		public override async Task Recargar()
		{
			if (modelo is ModeloConVariablesYTiradas modeloConVariables)
			{
				//Obtenemos todas las variables del modelo
				var variablesNuevas = new List<ModeloVariableBase>(modeloConVariables.Variables);

				//Recargamos las variables previamente cargadas o las quitamos si ya no se encuentran en el modelo
				foreach (var var in mVariablesPersistenes)
				{
					if (!variablesNuevas.Contains(var.Value.modelo))
					{
						mVariablesPersistenes.Remove(var.Key);

						continue;
					}

					await var.Value.Recargar();

					variablesNuevas.Remove(var.Value.modelo);
				}

				//Creamos un controlador para las nuevas
				foreach (var var in variablesNuevas)
				{
					mVariablesPersistenes.Add(var.IDVariable,
						ControladorVariableBase.CrearControladorCorrespondiente(var));
				}

				//Obtenemos todas las tiradas del modelo
				var tiradasNuevas = new List<ModeloTiradaBase>(modeloConVariables.Tiradas);

				//Recargamos las tiradas previamente cargadas o las quitamos si ya no se encuentran en el modelo
				foreach (var var in mTiradas)
				{
					var controladorTirada = var.Value;

					if (!tiradasNuevas.Contains(controladorTirada.modelo))
					{
						mVariablesPersistenes.Remove(var.Key);

						continue;
					}

					await controladorTirada.Recargar();

					tiradasNuevas.Remove(controladorTirada.modelo);
				}

				//Creamos un controlador para las nuevas tiradas
				foreach (var var in tiradasNuevas)
				{
					mTiradas.Add(var.Id, ControladorTiradaBase.CrearControladorDeTiradaCorrespondiente(var));
				}
			}
		}

		#endregion
	}
}