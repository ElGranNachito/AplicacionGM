using System;
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
		/// Obtiene la <see cref="Expression"/> que equivale a este bloque
		/// </summary>
		/// <returns><see cref="Expression"/> equivalente al bloque</returns>
		//TODO:Actualizar esta funcion para que tome un parametro
		public abstract Expression ObtenerExpresion(Compilador compilador);

		/// <summary>
		/// Obtiene el XML que equivale a esta expresion
		/// </summary>
		/// <param name="writer">Elemento al que escribir</param>
		protected abstract void ConvertirHaciaXML(XmlWriter writer);

		/// <summary>
		/// Obtiene un bloque a partir de un elemento XML
		/// </summary>
		/// <param name="reader"><see cref="XmlReader"/> del que convertir</param>
		/// <returns><see cref="BloqueBase"/> equivalente a <paramref name="reader"/></returns>
		public static BloqueBase DesdeXml(XmlReader reader)
		{
			throw new NotImplementedException();
		}
	}
}
