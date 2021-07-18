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
		/// Constructor
		/// </summary>
		/// <param name="_seccionesArgumento"><see cref="List{T}"/> de <see cref="SeccionArgumentoBase"/> con la que
		/// inicializar <see cref="mSeccionesArgumento"/></param>
		public BloqueArgumento(List<SeccionArgumentoBase> _seccionesArgumento, Type _tipoArgumento)
		{
			mSeccionesArgumento = _seccionesArgumento;
			TipoArgumento       = _tipoArgumento;
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

		protected override void ConvertirHaciaXML(XmlWriter writer)
		{
			throw new System.NotImplementedException();
		}
	}
}