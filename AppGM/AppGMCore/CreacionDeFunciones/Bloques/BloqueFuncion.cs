using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;

namespace AppGM.Core
{
	/// <summary>
	/// Bloque que representa llamar a una funcion
	/// </summary>
	public class BloqueFuncion : BloqueBase
	{
		/// <summary>
		/// Parametros que se le pasaran a la funcion en caso de que necesite
		/// </summary>
		public List<BloqueArgumento> argumentosFuncion;

		/// <summary>
		/// Metodo que llama
		/// </summary>
		public MethodInfo metodo;

		/// <summary>
		/// Bloque que genera la expresion desde la cual se puede llamar a esta funcion
		/// </summary>
		public BloqueBase caller;

		/// <summary>
		/// Expresion desde la cual se llama a la funcion.
		/// Se utiliza en caso de que <see cref="caller"/> sea null
		/// </summary>
		public Expression expresionCaller;

		/// <summary>
		/// Tipo del valor que retorna el metodo
		/// </summary>
		public Type TipoDeRetorno => metodo.ReturnType;

		/// <summary>
		/// Parametros que necesita el metodo
		/// </summary>
		public ParameterInfo[] Parametros;

		/// <summary>
		/// Nombre del metodo
		/// </summary>
		public string Nombre => metodo.Name;

		public BloqueFuncion(int _idBloque, MethodInfo _metodo, List<BloqueArgumento> _argumentosFuncion, BloqueBase _caller)
			:base(_idBloque)
		{
			metodo            = _metodo;
			argumentosFuncion = _argumentosFuncion;
			caller            = _caller;

			Parametros = _metodo.GetParameters();
		}

		public override Expression ObtenerExpresion(Compilador compilador)
		{
			if (caller == null && expresionCaller != null && !metodo.IsStatic)
			{
				SistemaPrincipal.LoggerGlobal.Log($"{nameof(caller)} es null! (Metodo: {Nombre})");

				return Expression.Empty();
			}

			List<Expression> expresionesParametrosFuncion = new List<Expression>(argumentosFuncion.Count);

			for (int i = 0; i < argumentosFuncion.Count; ++i)
			{
				var parametroActual = argumentosFuncion[i];

				if(parametroActual == null)
					expresionesParametrosFuncion.Add(Expression.Constant(null));
				else if (parametroActual.TipoArgumento.EsAsignableA(Parametros[i].ParameterType, true))
					expresionesParametrosFuncion.Add(parametroActual.ObtenerExpresion(compilador));
				else
					expresionesParametrosFuncion.Add(Expression.Convert(parametroActual.ObtenerExpresion(compilador), Parametros[i].ParameterType));
			}

			expresionCaller ??= caller.ObtenerExpresion(compilador);

			return Expression.Call(expresionCaller, metodo, expresionesParametrosFuncion);
		}

		public override void ConvertirHaciaXML(XmlWriter writer)
		{
			writer.WriteStartElement(nameof(BloqueFuncion));

			writer.WriteElementString("DueñoMetodo", metodo.DeclaringType.AssemblyQualifiedName);
			writer.WriteElementString("Metodo", Nombre);

			writer.WriteComment("--Tipos de los parametros que requiere la funcion--");

			writer.WriteStartElement("TiposParametros");
			writer.WriteAttributeString("NumeroDeParametros", Parametros.Length.ToString());

			for (int i = 0; i < Parametros.Length; ++i)
				writer.WriteElementString($"TipoParametro-{i}", Parametros[i].ParameterType.AssemblyQualifiedName);

			writer.WriteEndElement();

			writer.WriteComment("--Argumentos con los que se llama a la funcion--");

			writer.WriteStartElement("Argumentos");
			writer.WriteAttributeString("NumeroDeArgumentos", argumentosFuncion.Count.ToString());

			for (int i = 0; i < argumentosFuncion.Count; ++i)
				argumentosFuncion[i].ConvertirHaciaXML(writer);

			writer.WriteEndElement();

			writer.WriteComment("--Caller--");

			caller.ConvertirHaciaXML(writer);

			writer.WriteEndElement();
		}

		public override BloqueBase ConvertirDesdeXML(XmlReader reader)
		{
			throw new NotImplementedException();
		}
	}
}