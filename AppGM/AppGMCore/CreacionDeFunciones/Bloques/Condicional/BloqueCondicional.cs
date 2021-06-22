using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml;

using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// <see cref="BloqueBase"/> que representa una condicion para un if, un bucle o cualquier cosa que necesite
	/// una condicion para funcionar
	/// </summary>
	public class BloqueCondicional : BloqueBase
	{
		#region Campos

		/// <summary>
		/// <see cref="Queue{T}"/> con los <see cref="BloqueArgumento"/> que utiliza esta condicion
		/// </summary>
		private Queue<BloqueArgumento> mArgumentos;

		/// <summary>
		/// <see cref="Queue{T}"/> con las <see cref="EOperacionLogica"/> que realizar entre los <see cref="mArgumentos"/>
		/// </summary>
		private Queue<EOperacionLogica> mOperaciones;

		#endregion

		#region Constructores

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_argumentos"><see cref="List{T}"/> con los <see cref="BloqueArgumento"/></param>
		/// <param name="_operaciones"><see cref="List{T}"/> con las <see cref="EOperacionLogica"/></param>
		public BloqueCondicional(List<BloqueArgumento> _argumentos, List<EOperacionLogica> _operaciones)
		{
			mArgumentos = new Queue<BloqueArgumento>(_argumentos);
			mOperaciones = new Queue<EOperacionLogica>(_operaciones);
		}

		#endregion

		#region Metodos

		public override Expression ObtenerExpresion(Compilador compilador)
		{
			BloqueArgumento argumentoAnterior = null;
			Expression expresionAnterior = null;

			//Iteramos mientras queden argumentos en la cola...
			while (mArgumentos.Peek() != null)
			{
				BloqueArgumento argumentoActual = mArgumentos.Dequeue();

				Expression expresionActual;
				EOperacionLogica operacionARealizar;

				//Si el tipo del argumento es booleano...
				if (argumentoActual.TipoArgumento == typeof(bool))
				{
					//Obtenemos la expresion que revisa si es verdadero
					expresionActual = Expression.IsTrue(argumentoActual.ObtenerExpresion(compilador));

					//Si la expresion anterior es null entonces la asignamos a la expresion actual
					if (expresionAnterior == null)
						expresionAnterior = expresionActual;
					//Si no...
					else
					{
						//Obtenemos la operacion a realizar con la expresion anterior
						operacionARealizar = mOperaciones.Dequeue();

						//Obtenemos la operacion a realizar y la asignamos a operacion anterior
						expresionAnterior = ObtenerExpresionOperacion(operacionARealizar, expresionAnterior, expresionActual);
					}

					//Pasamos a la proxima iteracion
					continue;
				}

				//Si estamos aqui significa que el argumento no es booleano entonces necesitamos
				//conocer el segundo argumento para poder realizar la operacion debida entre ellos
				if (argumentoAnterior == null)
				{
					//Guardamos el argumento actual en argumento anterior
					argumentoAnterior = argumentoActual;

					//Pasamos a la proxima iteracion
					continue;
				}

				//Obtenemos la operacion a realizar con el argumento anterior
				operacionARealizar = mOperaciones.Dequeue();

				expresionActual = ObtenerExpresionOperacion(
					operacionARealizar,
					argumentoAnterior.ObtenerExpresion(compilador),
					argumentoActual.ObtenerExpresion(compilador));

				//Hacemos lo mismo que mas arriba con el booleano
				if (expresionAnterior == null)
					expresionAnterior = expresionActual;
				else
				{
					operacionARealizar = mOperaciones.Dequeue();

					expresionAnterior =
						ObtenerExpresionOperacion(operacionARealizar, expresionAnterior, expresionActual);
				}

				//Igualamos el argumento anterior a null para no generar problemas en las proximas iteraciones
				argumentoAnterior = null;
			}

			return expresionAnterior;
		}

		/// <summary>
		/// Obtiene la <see cref="Expression"/> correspondiente a la <paramref name="operacion"/>
		/// </summary>
		/// <param name="operacion"><see cref="EOperacionLogica"/> que realizar</param>
		/// <param name="exp1"><see cref="Expression"/> que ira en el lado izquierdo de la <paramref name="operacion"/></param>
		/// <param name="exp2"><see cref="Expression"/> que ira en el lado derecho de la <paramref name="operacion"/></param>
		/// <returns><see cref="Expression"/> correspondiente a la <paramref name="operacion"/></returns>
		private Expression ObtenerExpresionOperacion(EOperacionLogica operacion, Expression exp1, Expression exp2)
		{
			switch (operacion)
			{
				case EOperacionLogica.Mayor:
					return Expression.GreaterThan(exp1, exp2);
				case EOperacionLogica.MayorIgual:
					return Expression.GreaterThanOrEqual(exp1, exp2);
				case EOperacionLogica.Menor:
					return Expression.LessThan(exp1, exp2);
				case EOperacionLogica.MenorIgual:
					return Expression.LessThanOrEqual(exp1, exp2);
				case EOperacionLogica.Igual:
					return Expression.Equal(exp1, exp2);
				case EOperacionLogica.NoIgual:
					return Expression.NotEqual(exp1, exp2);
				case EOperacionLogica.Y:
					return Expression.And(exp1, exp2);
				case EOperacionLogica.O:
					return Expression.Or(exp1, exp2);
				case EOperacionLogica.No:
					return Expression.Not(exp1);
				default:
					SistemaPrincipal.LoggerGlobal.Log($"{operacion} no contemplado en switch!", ESeveridad.Advertencia);
					return null;
			}
		}

		protected override void ConvertirHaciaXML(XmlWriter writer)
		{
			throw new System.NotImplementedException();
		} 

		#endregion
	}
}