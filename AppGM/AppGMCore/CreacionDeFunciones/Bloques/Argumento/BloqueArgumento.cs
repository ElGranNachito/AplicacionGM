using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mime;
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

		public override void ConvertirHaciaXML(XmlWriter writer)
		{
			writer.WriteStartElement(nameof(BloqueArgumento));

			writer.WriteElementString(nameof(TipoArgumento), TipoArgumento.AssemblyQualifiedName);
			writer.WriteElementString(nameof(DetectarTipoAutomaticamente), DetectarTipoAutomaticamente.ToString());
			writer.WriteElementString(nameof(PuedeQuedarVacio), PuedeQuedarVacio.ToString());
			writer.WriteElementString(nameof(Nombre), Nombre);
			writer.WriteElementString(nameof(TextoActual), TextoActual);
			
			writer.WriteEndElement();
		}

		public override BloqueBase ConvertirDesdeXML(XmlReader reader)
		{
			return null;
		}
	}
}