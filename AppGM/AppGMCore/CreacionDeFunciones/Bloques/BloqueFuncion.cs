using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;

namespace AppGM.Core
{
	/// <summary>
	/// Bloque que representa llamar a una funcion
	/// </summary>
	public abstract class BloqueFuncion : BloqueBase
	{
		/// <summary>
		/// Parametros que se le pasaran a la funcion en caso de que necesite
		/// </summary>
		public List<BloqueVariable> parametrosFuncion;

		/// <summary>
		/// Metodo que llama
		/// </summary>
		public MethodInfo metodo;

		/// <summary>
		/// Tipo del valor que retorna el metodo
		/// </summary>
		public Type TipoDeRetorno => metodo.ReturnType;

		/// <summary>
		/// Parametros que necesita el metodo
		/// </summary>
		public ParameterInfo[] Parametros => metodo.GetParameters();

		/// <summary>
		/// Nombre del metodo
		/// </summary>
		public string Nombre => metodo.Name;
	}

	/// <summary>
	/// Bloque que representa llamar a una funcion estatica
	/// </summary>
	public class BloqueFuncionTipo : BloqueFuncion
	{
		/// <summary>
		/// Tipo que contiene la funcion
		/// </summary>
		public Type tipo;

		public override Expression ObtenerExpresion(Compilador compilador)
		{
			throw new System.NotImplementedException();
		}

		protected override void ConvertirHaciaXML(XmlWriter writer)
		{
			throw new System.NotImplementedException();
		}
	}

	/// <summary>
	/// Bloque que representa llamar a una funcion sobre una variable
	/// </summary>
	public class BloqueFuncionVariable : BloqueFuncion
	{
		/// <summary>
		/// Variable sobre la cual se llamara la funcion
		/// </summary>
		public BloqueVariable variable;

		public override Expression ObtenerExpresion(Compilador compilador)
		{
			throw new System.NotImplementedException();
		}

		protected override void ConvertirHaciaXML(XmlWriter writer)
		{
			throw new System.NotImplementedException();
		}
	}
}