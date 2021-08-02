using System;
using System.Collections.Generic;

using System.Linq.Expressions;
using System.Reflection;
using System.Xml;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Clase que representa una seccion de un <see cref="BloqueArgumento"/>.
	/// Las secciones son los <see cref="BloqueVariable"/>, <see cref="MemberInfo"/> que se acceden hasta
	/// llegar al valor final de un <see cref="BloqueArgumento"/>
	/// </summary>
	public abstract class SeccionArgumentoBase
	{
		public Type tipoRetorno;

		public virtual Expression GenerarExpresion(Compilador compilador, Expression expresionAnterior) => null;

		public SeccionArgumentoBase(){}

		public SeccionArgumentoBase(XmlReader _reader)
		{
			ConvertirDesdeXML(_reader);
		}

		/// <summary>
		/// Convierte esta seccion a un elemento XML
		/// </summary>
		/// <param name="writer">Documento XML en el que se escribira</param>
		public abstract void ConvertirHaciaXML(XmlWriter writer);

		/// <summary>
		/// Inicializa esta seccion a partir de un elemento XML
		/// </summary>
		/// <param name="reader">Documento XML en el que se encuentra el elemento</param>
		public abstract void ConvertirDesdeXML(XmlReader reader);
	}

	/// <summary>
	/// Seccion que representa acceder a un <see cref="MemberInfo"/>
	/// </summary>
	public class SeccionArgumentoMiembro : SeccionArgumentoBase
	{
		/// <summary>
		/// Miembro al que se accede
		/// </summary>
		public MemberInfo miembro;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_miembro">Miembro al que accede esta seccion</param>
		public SeccionArgumentoMiembro(MemberInfo _miembro)
		{
			miembro = _miembro;
			tipoRetorno = miembro.ObtenerTipoRetorno();
		}

		public SeccionArgumentoMiembro(XmlReader _reader)
			:base(_reader)
		{
			tipoRetorno = miembro.ObtenerTipoRetorno();
		}

		public override Expression GenerarExpresion(Compilador compilador, Expression expresionAnterior)
		{
			switch (miembro)
			{
				case MethodInfo mi:
					return Expression.Call(expresionAnterior, mi);
				case PropertyInfo pi:
					return Expression.Property(expresionAnterior, pi);
				case FieldInfo fi:
					return Expression.Field(expresionAnterior, fi);
				default:
					SistemaPrincipal.LoggerGlobal.Log($"tipo de {nameof(MemberInfo)} inesperado!", ESeveridad.Error);
					return null;
			}
		}

		public override void ConvertirHaciaXML(XmlWriter writer)
		{
			writer.WriteStartElement(nameof(SeccionArgumentoMiembro));

			writer.WriteElementString("DueñoMiembro", miembro.DeclaringType.AssemblyQualifiedName);
			writer.WriteElementString("Miembro", miembro.Name);
			writer.WriteElementString("TipoMiembro", miembro.MemberType.ToString());

			writer.WriteEndElement();
		}

		public override void ConvertirDesdeXML(XmlReader reader)
		{
			//Nos aseguramos de que el elemento actual sea del tipo correcto
			if (reader.Name != nameof(SeccionArgumentoMiembro))
				return;

			reader.ReadToFollowing("DueñoMiembro");

			Type dueñoMiembro = Type.GetType(reader.ReadElementContentAsString());

			reader.ReadToFollowing("Miembro");

			string nombreMiembro = reader.ReadElementContentAsString();

			reader.ReadToFollowing("TipoMiembro");

			MemberTypes tipoMiembro = Enum.Parse<MemberTypes>(reader.ReadElementContentAsString());

			switch (tipoMiembro)
			{
				case MemberTypes.Method:
					miembro = dueñoMiembro.GetMethod(nombreMiembro);
					break;
				case MemberTypes.Field:
					miembro = dueñoMiembro.GetField(nombreMiembro);
					break;
				case MemberTypes.Property:
					miembro = dueñoMiembro.GetProperty(nombreMiembro);
					break;
				default:
					
					SistemaPrincipal.LoggerGlobal.Log($"{tipoMiembro.ToString()}, este tipo de miembro no esta soportado!", ESeveridad.Error);

					break;
			}
		}
	}

	/// <summary>
	/// Seccion que representa llamar un <see cref="MethodInfo"/>
	/// </summary>
	public class SeccionArgumentoMetodo : SeccionArgumentoBase
	{
		public BloqueFuncion BloqueFuncion { get; private set; }

		public SeccionArgumentoMetodo(int _idBloque, MethodInfo _metodo, List<BloqueArgumento> _argumentos)
		{
			BloqueFuncion = new BloqueFuncion(_idBloque, _metodo, _argumentos, null);

			tipoRetorno = BloqueFuncion.TipoDeRetorno;
		}

		public SeccionArgumentoMetodo(XmlReader _reader)
			:base(_reader)
		{
			tipoRetorno = BloqueFuncion.TipoDeRetorno;
		}

		public override Expression GenerarExpresion(Compilador compilador, Expression expresionAnterior)
		{
			BloqueFuncion.expresionCaller = expresionAnterior;

			return BloqueFuncion.ObtenerExpresion(compilador);
		}

		public override void ConvertirHaciaXML(XmlWriter writer) => BloqueFuncion.ConvertirHaciaXML(writer);

		public override void ConvertirDesdeXML(XmlReader reader) => BloqueFuncion = new BloqueFuncion(reader);
	}

	/// <summary>
	/// Seccion que representa acceder a una variable
	/// </summary>
	public class SeccionArgumentoVariable : SeccionArgumentoBase
	{
		/// <summary>
		/// Nombre de la variable
		/// </summary>
		public int idVariable;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_nombreVariable">nombre de la variable. Debe ser exactamente igual al
		/// nombre utilizado en su declaracion</param>
		public SeccionArgumentoVariable(int _idVariable, Type _tipoVariable)
		{
			idVariable = _idVariable;
			tipoRetorno = _tipoVariable;
		}

		public SeccionArgumentoVariable(XmlReader _reader)
			:base(_reader) {}

		public override Expression GenerarExpresion(Compilador compilador, Expression expresionAnterior) => compilador[idVariable];

		public override void ConvertirHaciaXML(XmlWriter writer)
		{
			writer.WriteStartElement(nameof(SeccionArgumentoVariable));

			writer.WriteElementString("TipoRetorno", tipoRetorno.AssemblyQualifiedName);
			writer.WriteElementString("IDVariable", idVariable.ToString());

			writer.WriteEndElement();
		}

		public override void ConvertirDesdeXML(XmlReader reader)
		{
			if (reader.Name != nameof(SeccionArgumentoVariable))
				return;

			reader.ReadToFollowing("TipoRetorno");

			tipoRetorno = Type.GetType(reader.ReadElementContentAsString());

			reader.ReadToFollowing("IDVariable");

			idVariable = reader.ReadElementContentAsInt();
		}
	}
}