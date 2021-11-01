using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppGM.Core
{
	/// <summary>
	/// Clase abstracta base de todos los controladores
	/// </summary>
	public abstract class ControladorBase
	{
		/// <summary>
		/// <para>
		///		Devuelve el modelo representado por este controlador
		/// </para>
		/// <para>
		///		Solo utilizar para acceder al modelo desde un <see cref="ControladorBase"/>
		///	</para>
		/// </summary>
		public abstract ModeloBase Modelo { get; protected set; }

		/// <summary>
		/// Obtiene el controlador de la variable con la <paramref name="idVariable"/> especificada
		/// </summary>
		/// <param name="idVariable">Id de la variable que se queire obtener</param>
		/// <returns><see cref="ControladorVariableBase"/> de la variable</returns>
		public abstract ControladorVariableBase ObtenerControladorVariable(int idVariable);

		/// <summary>
		/// Obtiene el controlador de la variable con el <paramref name="nombreVariable"/> especificado
		/// </summary>
		/// <param name="nombreVariable">nombre de la variable que se queire obtener</param>
		/// <returns><see cref="ControladorVariableBase"/> de la variable</returns>
		public abstract ControladorVariableBase ObtenerControladorVariable(string nombreVariable);

		/// <summary>
		/// Obtiene un <see cref="ControladorVariableBase"/> guardada en el modelo representado por este controlador
		/// a partir de su <paramref name="idVariable"/>
		/// </summary>
		/// <param name="idVariable">Id de la variable que se quiere obtener</param>
		/// <returns>Controlador de la variable hallada o null si no se encontro la variable</returns>
		[AccesibleEnGuraScratch(nameof(ObtenerValorVariable))]
		public abstract object ObtenerValorVariable(int idVariable);

		/// <summary>
		/// Obtiene un <see cref="ControladorVariableBase"/> guardada en el modelo representado por este controlador
		/// a partir de su <paramref name="nombreVariable"/>
		/// </summary>
		/// <param name="nombreVariable">Nombre de la variable que se quiere obtener</param>
		/// <returns>Controlador de la variable hallada o null si no se encontro la variable</returns>
		[AccesibleEnGuraScratch(nameof(ObtenerValorVariable))]
		public abstract object ObtenerValorVariable(string nombreVariable);

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
		public abstract IControladorTiradaBase ObtenerTirada(int idTirada);

		/// <summary>
		/// Obtiene una <see cref="IControladorTiradaBase"/> guardada en el modelo representado por este controlador
		/// a partir de su <paramref name="nombreTirada"/>
		/// </summary>
		/// <param name="nombreTirada">nombre de la tirada que se quiere obtener</param>
		/// <returns>Tirada hallada o null</returns>
		[AccesibleEnGuraScratch(nameof(ObtenerTirada))]
		public abstract IControladorTiradaBase ObtenerTirada(string nombreTirada);

		/// <summary>
		/// Compara este controlador con una <paramref name="cadena"/> y determina si la cadena lo representa
		/// </summary>
		/// <param name="cadena">Cadena con la que comparar</param>
		/// <returns><see cref="bool"/> que indica si este controlador es representado por la cadena</returns>
		public virtual bool CompararConCadena(string cadena) => false;

		/// <summary>
		/// Crea un viewmodel para representar a este controlador en una lista
		/// </summary>
		/// <returns>Instancia del viewmodel creado o null</returns>
		public virtual ViewModelItemListaBase CrearViewModelItem() => null;

		/// <summary>
		/// Guarda el <see cref="Modelo"/>
		/// </summary>
		public virtual void Guardar() => Modelo.Guardar();

		/// <summary>
		/// Guarda el <see cref="Modelo"/>
		/// </summary>
		public virtual async Task GuardarAsync() => await Modelo.GuardarAsync();

		/// <summary>
		/// Metodo encargado de eliminar el modelo de la base de datos
		/// </summary>
		public virtual async void Eliminar(bool mostrarMensajeConfirmacion = false) => await Modelo.Eliminar(mostrarMensajeConfirmacion);

		/// <summary>
		/// Metodo encargado de eliminar el modelo de la base de datos
		/// </summary>
		public virtual async Task EliminarAsync(bool mostrarMensajeConfirmacion = true) => await Modelo.Eliminar(mostrarMensajeConfirmacion);

		public virtual (ControladorBase controlador, List<EventInfo> eventos) ObtenerEventosDisponibles()
		{
			return (this, GetType().GetEvents().ToList());
		}

		/// <summary>
		/// Metodo utilizado cuando el modelo representado por este controlador es actualizado y hace falta reconstruir ciertas propiedades
		/// </summary>
		/// <returns></returns>
		public virtual async Task Recargar()
		{
			await Task.FromResult(0);
		}
	}
}