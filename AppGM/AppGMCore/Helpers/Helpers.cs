using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CoolLogs;
using Ninject.Infrastructure.Language;

namespace AppGM.Core
{
	/// <summary>
	/// Funciones helpers generales
	/// </summary>
	public static class Helpers
	{
		/// <summary>
		/// Devuelve el <see cref="Type"/> que retorno de un <see cref="MemberInfo"/>
		/// </summary>
		/// <param name="miembro"><see cref="MemberInfo"/> del que obtener el <see cref="Type"/> que retorna</param>
		/// <returns><see cref="Type"/> que retorna</returns>
		public static Type ObtenerTipoRetorno(this MemberInfo miembro)
		{
			switch (miembro)
			{
				//Si es un metodo...
				case MethodInfo mi:
					return mi.ReturnType;
					break;
				//Si es una propiedad...
				case PropertyInfo pi:
					return pi.PropertyType;
					break;
				//Si es un campo...
				case FieldInfo fi:
					return fi.FieldType;
					break;
				default:
					return null;
			}
		}

		public static bool EsFuncionConParametros(this MemberInfo miembro)
		{
			if (miembro is MethodInfo mi)
				return mi.GetParameters().Length > 0;

			return false;
		}

		public static Type ObtenerTipoCompatible(this Type t)
		{
			if (t.IsValueType)
				return t;
			else
				return typeof(object);
		}

		public static bool EsAccesibleEnGuraScratch(this MemberInfo miembro) =>
			miembro.HasAttribute(typeof(AccesibleEnGuraScratch));

		public static bool EsAsignableDesdeOA(this Type primero, Type segundo) => primero.IsAssignableFrom(segundo) || primero.IsAssignableTo(segundo);

		public static bool EsAsignableA(this Type primero, Type segundo, bool esParaExpresion = false)
		{
			if (!esParaExpresion)
				return primero.IsAssignableTo(segundo);
			else
			{
				if (primero.IsValueType)
					return primero == segundo;
				else
					return primero.IsAssignableTo(segundo);
			}
		}

		public static bool EsComparableA(this Type primero, Type segundo)
		{
			if (primero.IsClass && segundo.IsClass && primero.EsAsignableDesdeOA(segundo))
				return true;

			return primero.IsValueType && segundo.IsValueType && segundo == primero;
		}

		public static bool SePuedeConvertirDesde(this Type tipo, object valorDesdeElQueConvertir) => TypeDescriptor.GetConverter(tipo).IsValid(valorDesdeElQueConvertir);

		public static List<(MethodInfo metodo, string nombre)> ObtenerMetodosAccesiblesEnGuraScratch(this Type tipo)
		{
			List<(MethodInfo metodo, string nombre)> resultado = new List<(MethodInfo metodo, string nombre)>();

			foreach (var metodo in tipo.GetMethods())
			{
				if (metodo.GetCustomAttribute<AccesibleEnGuraScratch>() is {} att)
					resultado.Add(new ValueTuple<MethodInfo, string>(metodo, att.nombreQueMostrar));
			}

			return resultado;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TElementos"></typeparam>
		/// <param name="propiedad"></param>
		/// <param name="instancia"></param>
		/// <param name="resultado"></param>
		/// <param name="incluirSubtipos"></param>
		/// <returns></returns>
		public static bool EsListaDe<TElementos>(this PropertyInfo propiedad, object instancia, out IList<TElementos> resultado, bool incluirSubtipos = true)
		{
			resultado = null;

			if (incluirSubtipos)
			{
				var argumentosGenericos = propiedad.PropertyType.GetGenericArguments();

				if (argumentosGenericos.Length > 0 && 
					typeof(IList).IsAssignableFrom(propiedad.PropertyType) &&
					typeof(TElementos).IsAssignableFrom(argumentosGenericos[0]))
				{
					resultado = propiedad.ObtenerValorComoLista<TElementos>(instancia);

					return true;
				}			

				return false;
			}

			if(typeof(IList<TElementos>).IsAssignableFrom(propiedad.PropertyType))
			{
				resultado = propiedad.ObtenerValorComoLista<TElementos>(instancia);

				return true;
			}

			return false;
		}

		/// <summary>
		/// Obtiene el valor de <paramref name="propiedad"/> para una determinada <paramref name="instancia"/> y lo devuelve como una <see cref="IList"/> de <typeparamref name="TElementos"/>
		/// </summary>
		/// <typeparam name="TElementos">Tipo de elementos que contiene la lista</typeparam>
		/// <param name="propiedad">Propiedad de la cual se obtendra el valor</param>
		/// <param name="instancia">Instancia de la cual se obtendra el valor de la <paramref name="propiedad"/></param>
		/// <returns><see cref="IList"/> de <typeparamref name="TElementos"/></returns>
		public static IList<TElementos> ObtenerValorComoLista<TElementos>(this PropertyInfo propiedad, object instancia)
		{
			if(!typeof(IList).IsAssignableFrom(propiedad.PropertyType))
			{
				SistemaPrincipal.LoggerGlobal.LogCrash($"{nameof(propiedad)}({propiedad}) no es de tipo {typeof(IList)}");

				return null;
			}

			try
			{
				return (propiedad.GetValue(instancia) as IList).Cast<TElementos>().ToList();
			}
			catch(Exception ex)
			{
				SistemaPrincipal.LoggerGlobal.LogCrash($"No se pudo obtener el valor de {nameof(propiedad)} o castearlo a una {nameof(IList)} de {typeof(TElementos)}{Environment.NewLine}{ex.Message}");

				return null;
			}
		}

		/// <summary>
		/// Obtiene el <see cref="Type"/> de un <see cref="ModeloPersonaje"/> representado por <paramref name="tipoPersonaje"/>
		/// </summary>
		/// <param name="tipoPersonaje"></param>
		/// <returns></returns>
		public static Type ObtenerTipoPersonaje (this ETipoPersonaje tipoPersonaje)
		{
			switch(tipoPersonaje)
			{
				case ETipoPersonaje.Master:
					return typeof(ModeloMaster);
				case ETipoPersonaje.Servant:
					return typeof(ModeloServant);
				case ETipoPersonaje.Invocacion:
					return typeof(ModeloInvocacion);
				case ETipoPersonaje.NPC:
					return typeof(ModeloPersonaje);
				default:
					SistemaPrincipal.LoggerGlobal.Log($"{nameof(tipoPersonaje)}({tipoPersonaje}) no soportado", ESeveridad.Error);
					return null;
			}
		}

		public static class Juego
		{
			/// <summary>
			/// Obtiene el modificador para un <paramref name="valorStat"/>
			/// </summary>
			/// <param name="valorStat">Valor de la stat para la cual obtener el modificador</param>
			/// <returns>Modificador correspondiente el <paramref name="valorStat"/></returns>
			public static int ObtenerModificadorStat(int valorStat)
			{
				//Si es diez devolvemos 0 directamente porque sino nos va a tirar una excepcion por dividir entre 0
				if (valorStat == 10)
					return 0;

				return (int)Math.Floor((valorStat - 10.0f) / 2.0f);
			}

			/// <summary>
			/// Obtiene el multiplicador para la <paramref name="mano"/> que utilizo un personaje
			/// </summary>
			/// <param name="mano">Mano que utilizo el personaje</param>
			/// <returns>Multiplicador correspondiente a la mano utilizada</returns>
			public static float ObtenerMultiplicadorManoUsada(EManoUtilizada mano)
			{
				switch (mano)
				{
					case EManoUtilizada.Dominante:
						return 1;
					case EManoUtilizada.NoDominante:
						return 0.5f;
					case EManoUtilizada.AmbasManos:
						return 2.5f;

					default:
						{
							SistemaPrincipal.LoggerGlobal.Log($"Valor de {nameof(mano)}({mano}) no soportado", ESeveridad.Error);

							return 0;
						}
				}
			}
		}

		/// <summary>
		/// Provee funciones para facilitar operaciones asincronicas
		/// </summary>
		public static class Threading
		{
			/// <summary>
			/// Ejecuta una <see cref="accion"/> asincronicamente dentro de un bloque trycatch
			/// </summary>
			/// <param name="lck">Lock que se utilizara para la sincronizacion</param>
			/// <param name="accion">Accion que se ejecutara dentro del trycatch si se pudo obtener posesion del <paramref name="lck"/></param>
			/// <param name="mensajeEnCasoDeError">Mensaje que se mostrara en caso de que ocurra un error dentro del try</param>
			/// <param name="timeout">Tiempo maximo de espera para obtener el <paramref name="lck"/> en milisegundos</param>
			/// <returns>Tupla conteniendo el resultado de ejecutar <paramref name="accion"/> y <see cref="bool"/> indicando si se ejecuto <paramref name="accion"/></returns>
			public static (TResultado resultado, bool accionEjecutada) ThreadSafeTryCatch<TResultado>(object lck, Func<TResultado> accion, string mensajeEnCasoDeError = "", int timeout = Int32.MaxValue)
			{
				bool lockObtenido = false;

				try
				{
					//Intentamos obtener posesion del lock
					Monitor.TryEnter(lck, timeout, ref lockObtenido);

					//Si pudimos obtenerla entonces ejecutamos la funcion
					if(lockObtenido)
						return (accion(), true);

					return (default, false);
				}
				catch (Exception ex)
				{
					SistemaPrincipal.LoggerGlobal.Log(mensajeEnCasoDeError.IsNullOrWhiteSpace()
						? $"{ex.Message}"
						: $"{mensajeEnCasoDeError}{Environment.NewLine}{ex.Message}");

					return (default, false);
				}
				finally
				{
					//Si obtuvimos el lock lo liberamos y avisamos a cualquier otro hilo que este esperando su liberacion
					if (lockObtenido)
					{
						Monitor.PulseAll(lck);
						Monitor.Exit(lck);
					}
				}
			}

			/// <summary>
			/// Ejecuta una <see cref="accion"/> asincronicamente dentro de un bloque trycatch
			/// </summary>
			/// <param name="lck">Lock que se utilizara para la sincronizacion</param>
			/// <param name="accion">Accion que se ejecutara dentro del trycatch si se pudo obtener posesion del <paramref name="lck"/></param>
			/// <param name="mensajeEnCasoDeError">Mensaje que se mostrara en caso de que ocurra un error dentro del try</param>
			/// <param name="timeout">Tiempo maximo de espera para obtener el <paramref name="lck"/> en milisegundos</param>
			/// <returns><see cref="bool"/> indicando si se ejecuto <paramref name="accion"/></returns>
			public static bool ThreadSafeTryCatch(object lck, Action accion, string mensajeEnCasoDeError = "", int timeout = Int32.MaxValue)
			{
				bool lockObtenido = false;

				try
				{
					//Intentamos obtener posesion del lock
					Monitor.TryEnter(lck, timeout, ref lockObtenido);

					//Si pudimos obtenerla entonces ejecutamos la funcion
					if (lockObtenido)
						accion();

					return lockObtenido;
				}
				catch (Exception ex)
				{
					SistemaPrincipal.LoggerGlobal.Log(mensajeEnCasoDeError.IsNullOrWhiteSpace()
						? $"{ex.Message}"
						: $"{mensajeEnCasoDeError}{Environment.NewLine}{ex.Message}");

					return false;
				}
				finally
				{
					//Si obtuvimos el lock lo liberamos y avisamos a cualquier otro hilo que este esperando su liberacion
					if (lockObtenido)
					{
						Monitor.PulseAll(lck);
						Monitor.Exit(lck);
					}
				}
			}
		}
	}
}
