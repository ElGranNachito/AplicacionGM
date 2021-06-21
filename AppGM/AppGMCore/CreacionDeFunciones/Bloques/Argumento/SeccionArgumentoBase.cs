using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Clase que representa una seccion de un <see cref="BloqueArgumento"/>.
	/// Las secciones son los <see cref="BloqueVariable"/>, <see cref="MemberInfo"/> que se acceden hasta
	/// llegar al valor final de un <see cref="BloqueArgumento"/>
	/// </summary>
	public class SeccionArgumentoBase
	{
		public virtual Expression GenerarExpresion(Compilador compilador, Expression expresionAnterior) => null;
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
		public SeccionArgumentoMiembro(MemberInfo _miembro) => miembro = _miembro;

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
	}

	/// <summary>
	/// Seccion que representa llamar un <see cref="MethodInfo"/> que requiere parametros
	/// </summary>
	public class SeccionArgumentoMetodoConParametros : SeccionArgumentoBase
	{
		/// <summary>
		/// Metodo al que se llama en esta seccion
		/// </summary>
		public MethodInfo metodo;

		/// <summary>
		/// Argumentos que se utilizaran para llamar al <see cref="metodo"/>
		/// </summary>
		public List<BloqueArgumento> argumentosFuncion;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_metodo">Metodo que se llamara</param>
		/// <param name="_argumentosFuncion">Argumentos con los que se llamara al <paramref name="_metodo"/></param>
		public SeccionArgumentoMetodoConParametros(MethodInfo _metodo, List<BloqueArgumento> _argumentosFuncion)
		{
			metodo            = _metodo;
			argumentosFuncion = _argumentosFuncion;
		}

		public override Expression GenerarExpresion(Compilador compilador, Expression expresionAnterior)
		{
			List<Expression> args = new List<Expression>();

			foreach (var arg in argumentosFuncion)
			{
				args.Add(arg.ObtenerExpresion(compilador));
			}

			return Expression.Call(expresionAnterior, metodo, args);
		}
	}

	/// <summary>
	/// Seccion que representa acceder a una variable
	/// </summary>
	public class SeccionArgumentoVariable : SeccionArgumentoBase
	{
		/// <summary>
		/// Nombre de la variable
		/// </summary>
		public string nombreVariable;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_nombreVariable">nombre de la variable. Debe ser exactamente igual al
		/// nombre utilizado en su declaracion</param>
		public SeccionArgumentoVariable(string _nombreVariable) => nombreVariable = _nombreVariable;

		public override Expression GenerarExpresion(Compilador compilador, Expression expresionAnterior) => compilador[nombreVariable];
	}
}