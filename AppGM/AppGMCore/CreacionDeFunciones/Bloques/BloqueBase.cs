using System;
using System.Linq.Expressions;
using System.Xml;
using CoolLogs;

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
		protected BloqueBase(int _idBLoque)
		{
			IDBloque = _idBLoque;
		}

		/// <summary>
		/// Constructor que construye el bloque a partir de un elemento XML
		/// </summary>
		/// <param name="reader">Documento en el que se encuentra el elemento</param>
		protected BloqueBase(XmlReader reader)
		{
			ConvertirDesdeXML(reader);
		}

		/// <summary>
		/// Obtiene la <see cref="Expression"/> que equivale a este bloque
		/// </summary>
		/// <returns><see cref="Expression"/> equivalente al bloque</returns>
		public abstract Expression ObtenerExpresion(Compilador compilador);

		/// <summary>
		/// Obtiene un <see cref="ViewModelBloqueFuncionBase"/> a partir de este bloque
		/// </summary>
		/// <param name="vmCreacionDeFuncion">Contenedor del bloque</param>
		/// <returns><see cref="ViewModelBloqueFuncionBase"/> que representa este bloque</returns>
		public virtual ViewModelBloqueFuncionBase ObtenerViewModel(ViewModelCreacionDeFuncionBase vmCreacionDeFuncion) => null;

		/// <summary>
		/// Obtiene el XML que equivale a esta expresion
		/// </summary>
		/// <param name="writer">Elemento al que escribir</param>
		public abstract void ConvertirHaciaXML(XmlWriter writer);

		/// <summary>
		/// Crea una instancia de este bloque desde un elemento XML
		/// </summary>
		/// <param name="reader"></param>
		protected abstract void ConvertirDesdeXML(XmlReader reader);

		/// <summary>
		/// Obtiene un bloque a partir de un elemento XML
		/// </summary>
		/// <param name="reader"><see cref="XmlReader"/> del que convertir</param>
		/// <returns><see cref="BloqueBase"/> equivalente a <paramref name="reader"/></returns>
		public static BloqueBase DesdeXml(XmlReader reader)
		{
			switch (reader.Name)
			{
				case nameof(BloqueArgumento):
					return new BloqueArgumento(reader);
				case nameof(BloqueVariable):
					return new BloqueVariable(reader);
				case nameof(BloqueFuncion):
					return new BloqueFuncion(reader);
				//TODO: Terminar
				default:
					SistemaPrincipal.LoggerGlobal.Log($"Elemento actual ({reader.Name}) no representa un bloque!", ESeveridad.Error);
					return null;
			}
		}
	}
}
