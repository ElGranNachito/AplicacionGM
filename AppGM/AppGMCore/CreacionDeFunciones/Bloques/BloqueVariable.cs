using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml;

namespace AppGM.Core
{
	/// <summary>
	/// Bloque que representa una variable
	/// </summary>
	public class BloqueVariable : BloqueBase
	{
		/// <summary>
		/// Nombre de la variable
		/// </summary>
		public string nombre;

		/// <summary>
		/// Tipo de la variable
		/// </summary>
		public Type tipo;

		/// <summary>
		/// Valor por defecto de la variable de la variable
		/// </summary>
		public BloqueArgumento argumento;

		public BloqueVariable(string _nombre, Type _tipo)
		{
			nombre = _nombre;
			tipo   = _tipo;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_nombre">Nombre que se le asignara a la variable</param>
		/// <param name="_tipo"><see cref="Type"/> de la variable</param>
		/// <param name="argumento"><see cref="BloqueArgumento"/> para asignarle un valor por defecto a la variable</param>
		public BloqueVariable(string _nombre, Type _tipo, BloqueArgumento _argumento)
		{
			nombre    = _nombre;
			tipo      = _tipo;
			argumento = _argumento;
		}

		public override Expression ObtenerExpresion(Compilador compilador)
		{
			throw new NotImplementedException();
		}

		protected override void ConvertirHaciaXML(XmlWriter writer)
		{
			throw new NotImplementedException();
		}
	}
}
