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
		public ETipoVariable tipoVariable;

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

		public BloqueVariable(string _nombre, Type _tipo, ETipoVariable _tipoVariable)
		{
			nombre = _nombre;
			tipo   = _tipo;
			tipoVariable = _tipoVariable;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_nombre">Nombre que se le asignara a la variable</param>
		/// <param name="_tipo"><see cref="Type"/> de la variable</param>
		/// <param name="argumento"><see cref="BloqueArgumento"/> para asignarle un valor por defecto a la variable</param>
		public BloqueVariable(string _nombre, Type _tipo, ETipoVariable _tipoVariable, BloqueArgumento _argumento)
		{
			nombre       = _nombre;
			tipo         = _tipo;
			tipoVariable = _tipoVariable;
			argumento    = _argumento;
		}

		public override Expression ObtenerExpresion(Compilador compilador)
		{
			switch (tipoVariable)
			{
				case ETipoVariable.Normal:
					return Expression.Variable(tipo, nombre);

				case ETipoVariable.Parametro:
					return Expression.Parameter(tipo, nombre);

				case ETipoVariable.Persistente:
				{
					var controlador = compilador[Compilador.NombreVariableDueña];

					return Expression.Property(Expression.Property(controlador, "Variables", Expression.Constant(nombre)), "ValorVariable");
				}

				default:
				{
					SistemaPrincipal.LoggerGlobal.Log($"valor de tipoVariable no soportado! ({tipoVariable})", ESeveridad.Advertencia);

					return Expression.Empty();
				}
			}
		}

		protected override void ConvertirHaciaXML(XmlWriter writer)
		{
			throw new NotImplementedException();
		}
	}
}