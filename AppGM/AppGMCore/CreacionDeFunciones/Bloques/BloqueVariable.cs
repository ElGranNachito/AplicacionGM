using System;
using System.Linq.Expressions;
using System.Xml;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Bloque que representa una variable
	/// </summary>
	public class BloqueVariable : BloqueBase
	{
		public ETipoVariable tipoVariable { get; private set; }

		/// <summary>
		/// Indica si el valor de esta variable se mantiene entre las distintas llamadas a la funcion
		/// </summary>
		public bool EsPersistente => tipoVariable == ETipoVariable.Persistente;

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
		public BloqueArgumento Argumento { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_idBloque">ID de este bloque</param>
		/// <param name="_nombre">Nombre de la variable</param>
		/// <param name="_tipo"><see cref="Type"/> de la variable</param>
		/// <param name="_tipoVariable"><see cref="ETipoVariable"/> de la variable</param>
		public BloqueVariable(int _idBloque, string _nombre, Type _tipo, ETipoVariable _tipoVariable)
			:base(_idBloque)
		{
			nombre       = _nombre;
			tipo         = _tipo;
			tipoVariable = _tipoVariable;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_nombre">Nombre que se le asignara a la variable</param>
		/// <param name="_tipo"><see cref="Type"/> de la variable</param>
		/// <param name="_tipoVariable"><see cref="ETipoVariable"/> de la variable</param>
		/// <param name="_argumento"><see cref="BloqueArgumento"/> para asignarle un valor por defecto a la variable</param>
		public BloqueVariable(int _idBloque, string _nombre, Type _tipo, ETipoVariable _tipoVariable, BloqueArgumento _argumento)
			:base(_idBloque)
		{
			Actualizar(_idBloque, _nombre, _tipo, _tipoVariable, _argumento);
		}

		/// <summary>
		/// Inicializa este bloque a partir de un elemento XML
		/// </summary>
		/// <param name="_reader">Documento que contiene el elemento</param>
		public BloqueVariable(XmlReader _reader)
			: base(_reader) { }

		public override Expression ObtenerExpresion(Compilador compilador)
		{
			//Si la variable ya fue añadida al compilador entonces simplemente devolvemos la que ya esta ahi
			if (compilador[IDBloque] is {} exp)
				return exp;

			if(tipoVariable == ETipoVariable.Parametro)
				return Expression.Parameter(tipo, nombre);

			return Expression.Variable(tipo, nombre);
		}

		public Expression ObtenerExpresionInicializacion(Compilador compilador)
		{
			switch (tipoVariable)
			{
				case ETipoVariable.Normal:
					return Argumento != null ? Argumento.ObtenerExpresion(compilador) : Expression.Default(tipo);
				case ETipoVariable.ParametroCreadoPorElUsuario:
				case ETipoVariable.Parametro:
					return null;
				case ETipoVariable.Persistente:
				{
					return Expression.Convert(
							Expression.Call(compilador[Compilador.Variables.ControladorFuncion], typeof(ControladorBase).GetMethod(nameof(ControladorBase.ObtenerValorVariable), new []{typeof(int)}) , Expression.Constant(IDBloque)), 
							tipo);
				}

				default:
					SistemaPrincipal.LoggerGlobal.Log($"{tipoVariable} no soportado!", ESeveridad.Error);
					return null;
			}
		}

		public void Actualizar(int _idBloque, string _nombre, Type _tipo, ETipoVariable _tipoVariable, BloqueArgumento _argumento)
		{
			nombre       = _nombre;
			tipo         = _tipo;
			tipoVariable = _tipoVariable;
			Argumento    = _argumento;
		}

		public override ViewModelBloqueFuncionBase ObtenerViewModel(IContenedorDeBloques _padre = null)
		{
			return new ViewModelBloqueDeclaracionVariable(
				IDBloque,
				tipo,
				tipoVariable,
				nombre, 
				Argumento.ObtenerParametrosInicializarVM(),
				_padre ?? SistemaPrincipal.VMCreacionDeFuncionActual);
		}

		public override void ConvertirHaciaXML(XmlWriter writer)
		{
			writer.WriteStartElement(nameof(BloqueVariable));

			writer.WriteElementString("Nombre", nombre);
			writer.WriteElementString("ID", IDBloque.ToString());
			writer.WriteElementString("Tipo", tipo.AssemblyQualifiedName);
			writer.WriteElementString("TipoVariable", tipoVariable.ToString());

			writer.WriteComment("Valor por defecto");

			Argumento?.ConvertirHaciaXML(writer);

			writer.WriteEndElement();
		}

		protected override void ConvertirDesdeXML(XmlReader reader)
		{
			if (reader.Name != nameof(BloqueVariable))
				return;

			//Iteramos mientras podamos continuar leyendo y no estemos en el final de este elemento
			while (reader.Read() && (reader.Name != nameof(BloqueVariable) && reader.NodeType != XmlNodeType.EndElement))
			{
				//Si el nodo actual no es un elemento saltamos a la siguiente iteracion
				if(reader.NodeType != XmlNodeType.Element)
					continue;

				switch (reader.Name)
				{
					case "Nombre":
						nombre = reader.ReadElementContentAsString();
						break;
					case "ID":
						IDBloque = reader.ReadElementContentAsInt();
						break;
					case "Tipo":
						tipo = Type.GetType(reader.ReadElementContentAsString());
						break;
					case "TipoVariable":
						tipoVariable = Enum.Parse<ETipoVariable>(reader.ReadElementContentAsString());
						break;
					case nameof(BloqueArgumento):
						Argumento = new BloqueArgumento(reader);
						break;
					default:
						SistemaPrincipal.LoggerGlobal.Log($"Elemento {reader.Name} desconocido hallado al cargar {nameof(BloqueVariable)}({this})", ESeveridad.Advertencia);
						break;
				}
			}
		}
	}
}