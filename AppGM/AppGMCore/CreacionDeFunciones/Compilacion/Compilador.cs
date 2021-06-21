using System.Collections.Generic;
using System.Linq.Expressions;

namespace AppGM.Core
{
	public sealed partial class Compilador
	{
		private Dictionary<string, Expression> mVariables = new Dictionary<string, Expression>();

		private List<BloqueBase> mBloques;

		public Compilador(List<BloqueVariable> variables, List<BloqueBase> bloques)
		{
			foreach (var var in variables)
			{
				mVariables.Add(var.nombre, var.ObtenerExpresion(this));
			}

			mBloques = bloques;
		}

		public TipoFuncion Compilar<TipoFuncion>()
		{
			//TODO:Implementar bien
			List<Expression> expresiones = new List<Expression>();

			foreach (var bloque in mBloques)
			{
				expresiones.Add(bloque.ObtenerExpresion(this));
			}

			var expresionFinal = Expression.Block(expresiones);

			return Expression.Lambda<TipoFuncion>(expresionFinal).Compile();
		}

		public Expression this[string nombreVariable]
		{
			get
			{
				if (mVariables.ContainsKey(nombreVariable))
					return mVariables[nombreVariable];

				return null;
			}
		}
	}
}
