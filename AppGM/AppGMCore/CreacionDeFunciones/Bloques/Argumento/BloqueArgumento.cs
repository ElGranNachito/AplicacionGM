using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml;

namespace AppGM.Core
{
	/// <summary>
	/// Bloque que representa un argumento.
	/// </summary>
	public class BloqueArgumento : BloqueBase
	{
		/// <summary>
		/// Secciones de este argumento
		/// </summary>
		private List<SeccionArgumentoBase> mSeccionesArgumento;

		/// <summary>
		/// <see cref="Type"/> de este argumento
		/// </summary>
		public Type TipoArgumento { get; private set; }

		/// <summary>
		/// Indica si el <see cref="TipoArgumento"/> puede ser detectado de manera automatica
		/// </summary>
		public bool DetectarTipoAutomaticamente { get; private set; }

		/// <summary>
		/// Indica si el argumetno puede quedar vacio
		/// </summary>
		public bool PuedeQuedarVacio { get; private set; }

		/// <summary>
		/// Nombre de este argumento
		/// </summary>
		public string Nombre { get; private set; }

		/// <summary>
		/// Texto actual ingresado por el usuario
		/// </summary>
		public string TextoActual { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_seccionesArgumento"><see cref="List{T}"/> de <see cref="SeccionArgumentoBase"/> con la que
		/// inicializar <see cref="mSeccionesArgumento"/></param>
		public BloqueArgumento(
			int _idBloque,
			List<SeccionArgumentoBase> _seccionesArgumento,
			Type _tipoArgumento,
			bool _detectarTipoAutomaticamente,
			string _nombre,
			string _textoActual) :base(_idBloque)
		{
			mSeccionesArgumento = _seccionesArgumento;
			TipoArgumento       = _tipoArgumento;

			DetectarTipoAutomaticamente = _detectarTipoAutomaticamente;
			Nombre                      = _nombre;
			TextoActual                 = _textoActual;
		}

		/// <summary>
		/// Crea el bloque a partir de un elemento XML
		/// </summary>
		/// <param name="_reader">Archivo XML del cual obtener el elemento</param>
		public BloqueArgumento(XmlReader _reader)
			: base(_reader){}

		public override Expression ObtenerExpresion(Compilador compilador)
		{
			//Ultima expresion obtenida.
			//La utilizamos para alimentar las llamadas de GenerarExpresion con las expresiones anteriores
			Expression expresion = null;

			for (int i = 0; i < mSeccionesArgumento.Count; ++i)
			{
				//Obtenemos la expresion del bloque actual
				expresion = mSeccionesArgumento[i].GenerarExpresion(compilador, expresion);
			}

			return expresion;
		}

		public ParametrosInicializarArgumentoDesdeBloque ObtenerParametrosInicializarVM()
		{
			var parametros = new ParametrosInicializarArgumentoDesdeBloque
			{
				idBloque = IDBloque,
				puedeQuedarVacio = PuedeQuedarVacio,
				detectarTipoAutomaticamente = DetectarTipoAutomaticamente,
				nombre = Nombre,
				textoActual = TextoActual,
				tipoArgumento = TipoArgumento
			};

			foreach (var seccion in mSeccionesArgumento)
			{
				if(seccion is SeccionArgumentoMetodo metodo)
					parametros.metodos.Add(metodo.BloqueFuncion);
			}

			return parametros;
		}

		public override void ConvertirHaciaXML(XmlWriter writer)
		{
			writer.WriteStartElement(nameof(BloqueArgumento));

			writer.WriteElementString(nameof(TipoArgumento), TipoArgumento.AssemblyQualifiedName);
			writer.WriteElementString(nameof(DetectarTipoAutomaticamente), DetectarTipoAutomaticamente.ToString().ToLower());
			writer.WriteElementString(nameof(PuedeQuedarVacio), PuedeQuedarVacio.ToString().ToLower());
			writer.WriteElementString(nameof(Nombre), Nombre);
			writer.WriteElementString(nameof(TextoActual), TextoActual);

			writer.WriteStartElement("Secciones");
			writer.WriteAttributeString("NumeroDeSecciones", mSeccionesArgumento.Count.ToString());

			foreach (var seccion in mSeccionesArgumento)
				seccion.ConvertirHaciaXML(writer);

			writer.WriteEndElement();
			
			writer.WriteEndElement();
		}

		protected override void ConvertirDesdeXML(XmlReader reader)
		{
			if (reader.Name != nameof(BloqueArgumento))
				return;

			reader.ReadToFollowing(nameof(TipoArgumento));

			TipoArgumento = Type.GetType(reader.ReadElementContentAsString());

			reader.ReadToFollowing(nameof(DetectarTipoAutomaticamente));

			DetectarTipoAutomaticamente = reader.ReadElementContentAsBoolean();

			reader.ReadToFollowing(nameof(PuedeQuedarVacio));

			PuedeQuedarVacio = reader.ReadElementContentAsBoolean();

			reader.ReadToFollowing(nameof(Nombre));

			Nombre = reader.ReadElementContentAsString();

			reader.ReadToFollowing(nameof(TextoActual));

			TextoActual = reader.ReadElementContentAsString();

			reader.ReadToFollowing("Secciones");

			//Inicializamos la lista de secciones y reservamos espacio para todas las secciones necesarias
			mSeccionesArgumento = new List<SeccionArgumentoBase>(int.Parse(reader.GetAttribute("NumeroDeSecciones")));

			for (int i = 0; i < mSeccionesArgumento.Capacity; ++i)
			{
				//Ignoramos todo hasta encontrar el proximo elemento
				while (!reader.Name.StartsWith("SeccionArgumento") && reader.NodeType != XmlNodeType.Element)
					reader.Read();

				//Nos fijamos que tipo de seccion es la actual e instanciamos el tipo correspondiente
				switch (reader.Name)
				{
					case nameof(SeccionArgumentoMiembro):
						mSeccionesArgumento.Add(new SeccionArgumentoMiembro(reader));
						break;
					case nameof(SeccionArgumentoMetodo):
						mSeccionesArgumento.Add(new SeccionArgumentoMetodo(reader));
						break;
					case nameof(SeccionArgumentoVariable):
						mSeccionesArgumento.Add(new SeccionArgumentoVariable(reader));
						break;
					default:
						SistemaPrincipal.LoggerGlobal.Log($"Tipo de seccion ({reader.Name}) desconocido en BloqueArgumento: {Nombre}");
						break;
				}
			}
		}
	}
}