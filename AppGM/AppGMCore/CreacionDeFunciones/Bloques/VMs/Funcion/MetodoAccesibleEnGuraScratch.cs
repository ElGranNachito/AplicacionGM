using System.Collections.Generic;
using System.Reflection;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un metodo accesible desde GuraScratch
	/// </summary>
	public class MetodoAccesibleEnGuraScratch
	{
		/// <summary>
		/// VM del bloque que contiene esta funcion
		/// </summary>
		public ViewModelBloqueFuncionBase BloqueContenedor { get; private set; }

		/// <summary>
		/// <see cref="MethodInfo"/> que representa
		/// </summary>
		public MethodInfo Metodo { get; private set; }

		/// <summary>
		/// Atributo <see cref="AccesibleEnGuraScratch"/> del <see cref="Metodo"/>
		/// </summary>
		public AccesibleEnGuraScratch AtributoAccesibleEnGuraScratch { get; private set; }

		/// <summary>
		/// Parametros del <see cref="Metodo"/>
		/// </summary>
		public ParameterInfo[] Parametros { get; private set; }

		/// <summary>
		/// Argumentos para llamar al <see cref="Metodo"/>
		/// </summary>
		public ViewModelBloqueArgumentosFuncion ArgumentosFuncion { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_metodo"><see cref="MethodInfo"/> que sera representado por esta instancia</param>
		/// <param name="_argsFuncion">Parametros para incilizar <see cref="ArgumentosFuncion"/> de la funcion. Solo se utilizar al cargar datos de una funcion guardada</param>
		public MetodoAccesibleEnGuraScratch(ViewModelBloqueFuncionBase _contenedor, MethodInfo _metodo, List<ParametrosInicializarArgumentoDesdeBloque> _argsFuncion = null)
		{
			if(!Actualizar(_metodo, _contenedor, _argsFuncion))
				SistemaPrincipal.LoggerGlobal.Log($"{nameof(_metodo)}(Valor: {_metodo}) no contiene atributo {nameof(AccesibleEnGuraScratch)}!", ESeveridad.Error);
		}

		/// <summary>
		/// Actualiza el <see cref="Metodo"/> que representa esta instancia
		/// </summary>
		/// <param name="nuevoMetodo">nuevo <see cref="MethodInfo"/></param>
		/// <param name="nuevoContenedor">nuevo <see cref="ViewModelBloqueContenedor{TipoBloque}"/>. Si se deja en null no se actualizara</param>
		/// <param name="argsFuncion">Parametros para incilizar <see cref="ArgumentosFuncion"/> de la funcion. Solo se utilizar al cargar datos de una funcion guardada</param>
		/// <returns><see cref="bool"/> indicando si se actualizo el <see cref="Metodo"/></returns>
		public bool Actualizar(MethodInfo nuevoMetodo, ViewModelBloqueFuncionBase nuevoContenedor = null, List<ParametrosInicializarArgumentoDesdeBloque> argsFuncion = null)
		{
			//Revisamos que el metodo contenga el atributo 'AccesibleEnGuraScratch' y que no sea el mismo que el metodo actual
			if (nuevoMetodo != Metodo && nuevoMetodo.GetCustomAttribute<AccesibleEnGuraScratch>() is { } att)
			{
				Metodo                         = nuevoMetodo;
				AtributoAccesibleEnGuraScratch = att;
				BloqueContenedor               = nuevoContenedor ?? BloqueContenedor;

				Parametros = Metodo.GetParameters();

				ArgumentosFuncion = RequiereParametros()
					? new ViewModelBloqueArgumentosFuncion(BloqueContenedor, this, argsFuncion)
					: null;

				return true;
			}

			return false;
		}

		/// <summary>
		/// Comprueba que esta instancia sea valida
		/// </summary>
		/// <returns>true si el <see cref="Metodo"/> contiene el atributo <see cref="AccesibleEnGuraScratch"/></returns>
		public bool VerificarValidez()
		{
			if (!RequiereParametros())
				return true;

			if (ArgumentosFuncion != null && ArgumentosFuncion.EsValidoPara(Metodo) && ArgumentosFuncion.VerificarValidez())
				return true;

			return false;
		}

		/// <summary>
		/// Obtiene el nombre con el que se debe mostrar al metodo en GuraScratch
		/// </summary>
		/// <returns><see cref="string"/> con le nombre que se bebera usar para mostrar al metodo</returns>
		public string ObtenerNombre() => AtributoAccesibleEnGuraScratch.nombreQueMostrar.IsNullOrWhiteSpace()
			? Metodo.Name
			: AtributoAccesibleEnGuraScratch.nombreQueMostrar;

		/// <summary>
		/// Obtiene el nombre con el que se debe mostrar un parametro de este metodo
		/// </summary>
		/// <param name="indiceParametro">Indice del paramtro</param>
		/// <returns>Nombre con el que se debe mostrar el parametro en <paramref name="indiceParametro"/></returns>
		public string ObtenerNombreParametro(int indice)
		{
			if (indice >= Parametros.Length || indice < 0)
				return string.Empty;

			if (Parametros[indice].GetCustomAttribute<NombreParametroGuraScratch>() is { } att)
				return att.nombre;

			return Parametros[indice].Name;
		}

		/// <summary>
		/// Genera el <see cref="BloqueFuncion"/> que representa llamar a este <see cref="Metodo"/>.
		/// Esta funcion esta destinada para el uso de <see cref="ViewModelBloqueLlamarFuncion"/>
		/// </summary>
		/// <param name="caller"><see cref="BloqueArgumento"/> desde el que se llama a la funcion.
		/// Puede ser null en caso de que <see cref="Metodo"/> sea estatico</param>
		/// <returns><see cref="BloqueFuncion"/> que representa llamar a este <see cref="Metodo"/></returns>
		public BloqueFuncion GenerarBloque(BloqueArgumento caller)
		{
			return new BloqueFuncion(BloqueContenedor.IDBloque, Metodo, ArgumentosFuncion.ObtenerArgumentosFuncion(), caller);
		}

		/// <summary>
		/// Genera la <see cref="SeccionArgumentoMetodo"/> que representa llamar a este <see cref="Metodo"/>,
		/// Esta funcion esta destinada para el uso de <see cref="ViewModelArgumento"/>.
		/// </summary>
		/// <returns></returns>
		public SeccionArgumentoMetodo GenerarSeccion()
		{
			return new SeccionArgumentoMetodo(BloqueContenedor.IDBloque, Metodo, ArgumentosFuncion.ObtenerArgumentosFuncion());
		}

		/// <summary>
		/// Indica si esta funcion requiere que se le pasen parametros
		/// </summary>
		/// <returns><see cref="bool"/> indicando si esta funcion requiere parametros</returns>
		public bool RequiereParametros() => Parametros.Length > 0;

		/// <summary>
		/// Indica si esta funcion es estatica
		/// </summary>
		/// <returns><see cref="bool"/> indicando si esta funcion es estatica</returns>
		public bool EsFuncionEstatica() => Metodo.IsStatic;
	}
}