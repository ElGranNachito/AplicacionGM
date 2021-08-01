using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un bloque de codigo
	/// </summary>
	public abstract class BloqueBase
	{
		/// <summary>
		/// ID del bloque
		/// </summary>
		public int IDBloque { get; protected set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_idBLoque">ID que tendra el bloque</param>
		public BloqueBase(int _idBLoque)
		{
			IDBloque = _idBLoque;
		}

		/// <summary>
		/// Obtiene la <see cref="Expression"/> que equivale a este bloque
		/// </summary>
		/// <returns><see cref="Expression"/> equivalente al bloque</returns>
		public abstract Expression ObtenerExpresion(Compilador compilador);

		/// <summary>
		/// Obtiene el XML que equivale a esta expresion
		/// </summary>
		/// <param name="writer">Elemento al que escribir</param>
		public abstract void ConvertirHaciaXML(XmlWriter writer);

		/// <summary>
		/// Crea una instancia de este bloque desde un elemento XML
		/// </summary>
		/// <param name="reader"></param>
		public abstract BloqueBase ConvertirDesdeXML(XmlReader reader);

		/// <summary>
		/// Obtiene un bloque a partir de un elemento XML
		/// </summary>
		/// <param name="reader"><see cref="XmlReader"/> del que convertir</param>
		/// <returns><see cref="BloqueBase"/> equivalente a <paramref name="reader"/></returns>
		public static List<BloqueBase> DesdeXml(XmlReader reader)
		{
			throw new NotImplementedException();
		}
	}
}
