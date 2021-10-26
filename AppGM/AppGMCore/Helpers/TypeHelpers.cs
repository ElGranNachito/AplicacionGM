using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using CoolLogs;
using Ninject.Infrastructure.Language;

namespace AppGM.Core
{
	public static class TypeHelpers
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
				if (metodo.GetCustomAttribute<AccesibleEnGuraScratch>() is { } att)
					resultado.Add(new ValueTuple<MethodInfo, string>(metodo, att.nombreQueMostrar));
			}

			return resultado;
		}

		/// <summary>
		/// Obtiene el <see cref="Type"/> de un <see cref="ModeloPersonaje"/> representado por <paramref name="tipoPersonaje"/>
		/// </summary>
		/// <param name="tipoPersonaje"></param>
		/// <returns></returns>
		public static Type ObtenerTipoPersonaje(this ETipoPersonaje tipoPersonaje)
		{
			switch (tipoPersonaje)
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

		/// <summary>
		/// Obtiene la lista de eventos disponibles para cierto controlador.
		/// <para>
		///		Las combinaciones existentes de tipos son las siguientes:
		///		<list type="bullet">
		///			<item>
		///				Efecto -> Habilidad -> Personaje
		///			</item>
		///			<item>
		///				Efecto -> Item -> Personaje
		///			</item>
		///			<item>
		///				Habilidad -> Personaje
		///			</item>
		///			<item>
		///				Efecto -> Personaje
		///			</item>
		///			<item>
		///				Personaje
		///			</item>
		///		</list>
		/// </para>
		/// </summary>
		/// <param name="tipoControlador">Tipo del <see cref="ControladorBase"/> para el que queremos obtener los eventos disponibles</param>
		/// <param name="tiposExtra">Tipos extra necesitados por el controlador de los cuales obtendra mas eventos</param>
		/// <returns>Lista de eventos disponibles</returns>
		public static List<EventInfo> ObtenerEventosDisponibles(Type tipoControlador, params Type[] tiposExtra)
		{
			//Nos aseguramos de que el tipo pasado sea un controlador
			if (!tipoControlador.IsSubclassOf(typeof(ControladorBase)))
			{
				SistemaPrincipal.LoggerGlobal.LogCrash(
					$"{nameof(tipoControlador)} debe ser un subtipo de {nameof(ControladorBase)}");
			}

			//Convertimos el arreglo de tipos extra en una lista para poder acceder a metodos utiles
			var listaTiposExtra = tiposExtra.ToList();

			//Si el tipo actual no es un personaje...
			if (!typeof(ControladorPersonaje).IsAssignableFrom(tipoControlador))
			{
				//Si es un efecto vamos a necesitar que la lista de tipos extra contenga al menos dos elementos
				if (tipoControlador == typeof(ControladorEfecto))
				{
					if (listaTiposExtra.Count < 2)
					{
						SistemaPrincipal.LoggerGlobal.LogCrash(
							$"Cantidad de {nameof(tiposExtra)} provista es insuficiente para este tipo");
					}
				}
				//Si es una habilidad o un utilizable vamos a necesitar que la lista de tipos extra contenga al menos un elemento
				else if (tipoControlador == typeof(ControladorHabilidad) ||
						 tipoControlador == typeof(ControladorUtilizable))
				{
					if (listaTiposExtra.Count < 1)
					{
						SistemaPrincipal.LoggerGlobal.LogCrash(
							$"Cantidad de {nameof(tiposExtra)} provista es insuficiente para este tipo");
					}
				}

				var tipoExtraActual = tiposExtra[0];

				//Quitamos el primer elemento
				listaTiposExtra.RemoveAt(0);

				//Obtenemos los eventos disponibles para el primer tipo extra
				var eventosDisponibles = ObtenerEventosDisponibles(tipoExtraActual, listaTiposExtra.ToArray());

				//Añadimos los eventos del tipo a los que obtuvimos del tipo extra
				eventosDisponibles.AddRange(tipoControlador.GetEvents());

				return eventosDisponibles;
			}

			//Si es un personaje simplemente devolvemos los eventos del tipo
			return tipoControlador.GetEvents().ToList();
		}

		/// <summary>
		/// Obtiene el nombre amigable de un delegado
		/// </summary>
		/// <param name="tipoDelegado"></param>
		/// <returns></returns>
		public static string ObtenerNombreAmigableDelegado(this Type tipoDelegado)
		{
			if (!typeof(MulticastDelegate).IsAssignableFrom(tipoDelegado))
			{
				SistemaPrincipal.LoggerGlobal.Log($"{nameof(tipoDelegado)} debe ser un {nameof(MulticastDelegate)}", ESeveridad.Error);

				return "Error";
			}

			return tipoDelegado.Name.Remove(0, 1);
		}

		/// <summary>
		/// Obtiene el nombre amigable de un <paramref name="miembro"/>
		/// </summary>
		/// <param name="miembro">Miembro del que obtener el nombre</param>
		/// <returns>Nombre amigble del <paramref name="miembro"/></returns>
		public static string ObtenerNombreAmigableMiembro(this MemberInfo miembro) => Regex.Replace(miembro.Name, "[A-Z]", " $0");
	}
}