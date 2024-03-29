﻿using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// <see cref="BloqueBase"/> que representa varios <see cref="BloqueCondicional"/> e acciones que ejecutar
	/// en sus respectivos casos
	/// </summary>
	public class BloqueCondicionalCompleto : BloqueBase
	{
		/// <summary>
		/// <see cref="List{T}"/> de <see cref="BloqueCondicional"/> que contiene este condicional
		/// </summary>
		private List<BloqueCondicional> mCondiciones;

		/// <summary>
		/// Lista de litas de <see cref="BloqueBase"/> que representan las acciones a realizar en caso de que su correspondiente
		/// <see cref="BloqueCondicional"/> se cumpla
		/// </summary>
		private List<List<BloqueBase>> mAcciones;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_condiciones"><see cref="List{T}"/> de <see cref="BloqueCondicional"/> que contiene este condicional</param>
		/// <param name="_acciones">Lista de litas de <see cref="BloqueBase"/> que representan las acciones a realizar</param>
		public BloqueCondicionalCompleto(int _idBloque, List<BloqueCondicional> _condiciones, List<List<BloqueBase>> _acciones)
			:base(_idBloque)
		{
			mCondiciones = _condiciones;
			mAcciones    = _acciones;
		}

		public BloqueCondicionalCompleto(XmlReader _reader)
			:base(_reader){}

		public override Expression ObtenerExpresion(Compilador compilador)
		{
			//Si el numero de condicion y acciones no es igual devolvemos una exprsion vacia
			if (mCondiciones.Count != mAcciones.Count)
			{
				SistemaPrincipal.LoggerGlobal.Log($"{nameof(mCondiciones)}.{nameof(mCondiciones.Count)} != {nameof(mAcciones)}.{nameof(mAcciones.Count)}", ESeveridad.Error);

				return Expression.Empty();
			}

			Stack<Expression> condiciones   = new Stack<Expression>(mCondiciones.Select(bloque => bloque.ObtenerExpresion(compilador)));
			Stack<BlockExpression> acciones = new Stack<BlockExpression>(mAcciones.Count);

			//Creamos un bloque de expresiones para cada accion
			foreach (var bloques in mAcciones)
			{
				BlockExpression bloqueActual = Expression.Block(bloques.Select(bloque => bloque.ObtenerExpresion(compilador)));

				acciones.Push(bloqueActual);
			}

			//Creamos la primera accion directamente aqui ya que esta representara al else final y despues de esta ya no pueden haber mas bloques
			Expression expresionAnterior = Expression.IfThen(condiciones.Pop(), acciones.Pop());

			Expression condicionActual;
			BlockExpression accionActual;

			//Mientras queden condiciones y acciones en sus respectivos stacks...
			while (condiciones.TryPop(out condicionActual) && acciones.TryPop(out accionActual))
			{
				//Creamos la expresion y la guardamos en expresion anterior para que sea usada posteriormente por la proxima condicion
				expresionAnterior = Expression.IfThenElse(condicionActual, accionActual, expresionAnterior);
			}

			return expresionAnterior;
		}

		public override ViewModelBloqueFuncionBase ObtenerViewModel(IContenedorDeBloques padre = null)
		{
			return new ViewModelBloqueCondicionalCompleto(IDBloque, mCondiciones, mAcciones, padre);
		}

		public override void ConvertirHaciaXML(XmlWriter writer)
		{
			writer.WriteStartElement(nameof(BloqueCondicionalCompleto));

			writer.WriteAttributeString("NumeroDeCondiciones", mCondiciones.Count.ToString());

			for (int i = 0; i < mAcciones.Count; ++i)
			{
				writer.WriteComment($"Condicion-{i}");
				writer.WriteStartElement("CondicionYAcciones");

				mCondiciones[i].ConvertirHaciaXML(writer);

				writer.WriteStartElement("Acciones");
				writer.WriteAttributeString("NumeroDeAcciones", mAcciones[i].Count.ToString());

				foreach (var bloque in mAcciones[i])
				{
					writer.WriteStartElement("Accion");

					bloque.ConvertirHaciaXML(writer);

					writer.WriteEndElement();
				}

				writer.WriteEndElement();

				writer.WriteEndElement();
			}

			writer.WriteEndElement();
		}

		protected override void ConvertirDesdeXML(XmlReader reader)
		{
			if (reader.Name != nameof(BloqueCondicionalCompleto))
				return;

			mCondiciones = new List<BloqueCondicional>(int.Parse(reader.GetAttribute("NumeroDeCondiciones")));
			mAcciones = new List<List<BloqueBase>>(mCondiciones.Capacity);

			//Por cada condicion...
			for (int i = 0; i < mCondiciones.Capacity; ++i)
			{
				reader.ReadToFollowing(nameof(BloqueCondicional));

				mCondiciones.Add(new BloqueCondicional(reader));

				reader.ReadToFollowing("Acciones");

				//Obtenemos el numero de acciones que se realizan en este bloque y reservamos espacio en la lista
				mAcciones.Add(new List<BloqueBase>(int.Parse(reader.GetAttribute("NumeroDeAcciones"))));

				reader.Read();

				//Por cada accion...
				for (int j = 0; j < mAcciones[i].Capacity; ++j)
				{
					//Salteamos los elementos hasta encontrar uno cuyo nombre comience con Bloque y sea el comienzo de un Elemento
					reader.ReadToFollowing("Accion");

					reader.Read();
					reader.MoveToContent();

					mAcciones[i].Add(BloqueBase.DesdeXml(reader));
				}
			}
		}
	}
}