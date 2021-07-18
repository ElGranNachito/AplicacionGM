using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

		public static IEnumerable<T> RemoverRango<T>(this IEnumerable<T> coleccion, IEnumerable<T> elementosARemover)
		{
			var listaElementos = coleccion.ToList();
			
			foreach (var elemento in elementosARemover)
				listaElementos.Remove(elemento);

			return listaElementos;
		}

		public static bool EsAccesibleEnGuraScratch(this MemberInfo miembro) =>
			miembro.HasAttribute(typeof(AccesibleEnGuraScratch));

		public static bool EsAsignableDesdeOA(this Type primero, Type segundo) => primero.IsAssignableFrom(segundo) || primero.IsAssignableTo(segundo);

		public static bool SePuedeConvertirDesde(this Type tipo, object valorDesdeElQueConvertir) => TypeDescriptor.GetConverter(tipo).IsValid(valorDesdeElQueConvertir);
	}
}
