using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;
using CoolLogs;

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
		public BloqueArgumento caller;

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

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_idBloque"></param>
		/// <param name="_metodo"></param>
		/// <param name="_argumentosFuncion"></param>
		/// <param name="_caller"></param>
		public BloqueFuncion(int _idBloque, MethodInfo _metodo, List<BloqueArgumento> _argumentosFuncion, BloqueArgumento _caller)
			:base(_idBloque)
		{
			metodo            = _metodo;
			argumentosFuncion = _argumentosFuncion;
			caller            = _caller;

			Parametros = _metodo.GetParameters();
		}

		/// <summary>
		/// Inicializa este bloque a partir de un elemento XML
		/// </summary>
		/// <param name="_reader">Documento que contiene el elemento</param>
		public BloqueFuncion(XmlReader _reader)
			:base(_reader){}

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

			expresionCaller = caller.ObtenerExpresion(compilador);

			return Expression.Call(expresionCaller, metodo, expresionesParametrosFuncion);
		}

		public override ViewModelBloqueFuncionBase ObtenerViewModel(IContenedorDeBloques _padre = null)
		{
			if (caller == null)
			{
				SistemaPrincipal.LoggerGlobal.Log($"No se puede crear un {nameof(ViewModelBloqueLlamarFuncion)} a partir de {this} porque {nameof(caller)} es null!", ESeveridad.Error);

				return null;
			}

			return new ViewModelBloqueLlamarFuncion(
				IDBloque,
				this,
				caller.ObtenerParametrosInicializarVM(),
				_padre ?? SistemaPrincipal.VMCreacionDeFuncionActual
			);
		}

		/// <summary>
		/// Obtiene el <see cref="MetodoAccesibleEnGuraScratch"/> que representa a este bloque
		/// </summary>
		/// <param name="contenedor"><see cref="ViewModelBloqueFuncionBase"/> que contiene al metodo</param>
		/// <returns><see cref="MetodoAccesibleEnGuraScratch"/></returns>
		public MetodoAccesibleEnGuraScratch ObtenerMetodoAccesibleEnGuraScratch(ViewModelBloqueFuncionBase contenedor)
		{
			return new MetodoAccesibleEnGuraScratch(
				contenedor, 
				metodo,
				argumentosFuncion.Select(arg => arg.ObtenerParametrosInicializarVM()).ToList());
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

			if (caller != null)
			{
				writer.WriteStartElement("Caller");
				caller.ConvertirHaciaXML(writer);
				writer.WriteEndElement();

				writer.WriteEndElement();
			}
		}

		protected override void ConvertirDesdeXML(XmlReader reader)
		{
			if (reader.Name != nameof(BloqueFuncion))
				return;

			reader.ReadToFollowing("DueñoMetodo");

			Type dueñoFuncion = Type.GetType(reader.ReadElementContentAsString());

			reader.ReadToFollowing("Metodo");

			string nombreFuncion = reader.ReadElementContentAsString();

			reader.ReadToFollowing("TiposParametros");

			Type[] tiposDeLosParametros = new Type[int.Parse(reader.GetAttribute("NumeroDeParametros"))];

			for (int i = 0; i < tiposDeLosParametros.Length; ++i)
			{
				reader.ReadToFollowing($"TipoParametro-{i}");

				tiposDeLosParametros[i] = Type.GetType(reader.ReadElementContentAsString());
			}

			metodo = dueñoFuncion.GetMethod(nombreFuncion, tiposDeLosParametros);

			Parametros = metodo.GetParameters();

			reader.ReadToFollowing("Argumentos");

			argumentosFuncion = new List<BloqueArgumento>(int.Parse(reader.GetAttribute("NumeroDeArgumentos")));

			for (int i = 0; i < argumentosFuncion.Capacity; ++i)
			{
				reader.ReadToFollowing(nameof(BloqueArgumento));

				argumentosFuncion.Add(new BloqueArgumento(reader));
			}

			while (reader.Name != nameof(BloqueFuncion))
			{
				if (reader.Name != "Caller")
				{
					reader.Read();

					continue;
				}

				reader.ReadToFollowing(nameof(BloqueArgumento));

				caller = new BloqueArgumento(reader);

				break;
			}
		}
	}
}