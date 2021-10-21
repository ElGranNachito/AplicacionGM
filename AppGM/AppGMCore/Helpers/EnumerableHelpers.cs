using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace AppGM.Core
{
	public static class EnumerableHelpers
	{
		public static IEnumerable<T> RemoverRango<T>(this IEnumerable<T> coleccion, IEnumerable<T> elementosARemover)
		{
			var listaElementos = coleccion.ToList();

			foreach (var elemento in elementosARemover)
				listaElementos.Remove(elemento);

			return listaElementos;
		}

		public static IEnumerable<T> RemoverPrimero<T>(this IEnumerable<T> coleccion, Predicate<T> p)
		{
			var listaElementos = coleccion.ToList();

			foreach (var elemento in listaElementos)
			{
				if (p(elemento))
				{
					listaElementos.Remove(elemento);

					break;
				}
			}

			return listaElementos;
		}

		public static IEnumerable<T> Remover<T>(this IEnumerable<T> coleccion, T elemento, bool quitarTodasLasInstancias = true)
		{
			var listaElementos = coleccion.ToList();

			for(int i = 0; i < listaElementos.Count; ++i)
			{
				if (elemento.Equals(listaElementos[i]))
				{
					listaElementos.Remove(elemento);

					if (!quitarTodasLasInstancias)
						break;
				}
			}

			return listaElementos;
		}

		public static IEnumerable<T> Remover<T>(this IEnumerable<T> coleccion, Predicate<T> predicado)
		{
			var listaElementos = coleccion.ToList();

			foreach (var elemento in listaElementos)
			{
				if (predicado(elemento))
				{
					listaElementos.Remove(elemento);
				}
			}

			return listaElementos;
		}

		public static IList Cast<T>(this IEnumerable<T> coleccion, Type tipoAlQueCastear)
		{
			var metodoCastGenerico = typeof(Enumerable).GetMethod(nameof(Enumerable.Cast), BindingFlags.Static | BindingFlags.Public, null, new[] { typeof(IEnumerable) }, null).MakeGenericMethod(tipoAlQueCastear);

			return metodoCastGenerico.Invoke(coleccion, new[] { coleccion }) as IList;
		}
	}
}