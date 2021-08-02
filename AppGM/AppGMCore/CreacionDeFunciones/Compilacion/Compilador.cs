using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Clase encargada de compilar funciones de GuraScratch
	/// </summary>
	public sealed partial class Compilador
	{
		/// <summary>
		/// Variables existentes dentro de la funcion siendo compilada.
		/// </summary>
		private Dictionary<int, Expression> mVariables = new Dictionary<int, Expression>();

		/// <summary>
		/// Parametros de la funcion siendo compilada
		/// </summary>
		private List<ParameterExpression> mParametros = new List<ParameterExpression>();

		/// <summary>
		/// Parametros de la funcion siendo compilada que fueron añadidos por el usuario
		/// </summary>
		private List<(ParameterExpression expresion, Type tipo)> mParametrosCreadosPorElUsuario = new List<(ParameterExpression expresion, Type tipo)>();

		/// <summary>
		/// Variables del afuncion siendo compilada
		/// </summary>
		private List<ParameterExpression> mVars = new List<ParameterExpression>();

		/// <summary>
		/// Bloques que componen la funcion, sin contar la variables
		/// </summary>
		private List<BloqueBase> mBloques = new List<BloqueBase>();

		/// <summary>
		/// Indica si el compilador es valido, de no serlo, no se puede iniciar la compilacion
		/// </summary>
		public bool EsValido { get; private set; }

		/// <summary>
		/// Constructor que toma <paramref name="bloques"/>
		/// </summary>
		/// <param name="bloques"><see cref="List{T}"/> de <see cref="BloqueBase"/> que componen la funcion</param>
		public Compilador(List<BloqueBase> bloques)
		{
			AñadirBloques(bloques);

			EsValido = true;
		}

		/// <summary>
		/// Constructor que carga los bloques de la funcion desde un arhivo XML.
		/// </summary>
		/// <param name="nombreFuncion">Nombre de la funcion, debe ser igual al del archivo que la contiene</param>
		/// <param name="id">ID de la funcion, debe ser igual al del archivo que la contiene</param>
		public Compilador(string nombreFuncion, int id)
		{
			string pathArchivoFuncion = Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioFunciones, $"{nombreFuncion}-{id}.xml");

			if (!File.Exists(pathArchivoFuncion))
			{
				SistemaPrincipal.LoggerGlobal.Log($"'{nombreFuncion}-{id}.xml' --- Archivo no encontrado en {SistemaPrincipal.ControladorDeArchivos.DirectorioFunciones}");

				return;
			}

			//Abrimos el archivo XML para lectura
			using XmlReader reader = new XmlTextReader(new StreamReader(pathArchivoFuncion, Encoding.UTF8));
			
			//Obtenemos la lista de bloques que componen la funcion a traves de la funcion estatica de BloqueBase
			//var resultado = BloqueBase.DesdeXml(reader);

			//AñadirBloques(resultado);

			EsValido = true;
		}

		/// <summary>
		/// Compila la funcion
		/// </summary>
		/// <typeparam name="TipoFuncion">Tipo de la funcion que resultara de la compilacion, debe ser exacto, de otra manera habra un error</typeparam>
		/// <returns></returns>
		public ResultadoCompilacion<TipoFuncion> Compilar<TipoFuncion>()
		{
			var resultado = new ResultadoCompilacion<TipoFuncion>();

			if (!EsValido)
			{
				resultado.Mensaje = "Compilador no es valido!";

				return resultado;
			}

			SistemaPrincipal.LoggerGlobal.Log($"Iniciando Compilacion... (TipoFuncion: {typeof(TipoFuncion)})", ESeveridad.Info);

			//Lista de expresiones que componen la funcion
			List<Expression> expresiones = new List<Expression>();

			try
			{
				//Asignamos los valores correspondientes a los parametros creados por el usuario
				for (int i = 0; i < mParametrosCreadosPorElUsuario.Count; ++i)
				{
					expresiones.Add(Expression.Assign(mParametrosCreadosPorElUsuario[i].expresion, Expression.Convert(Expression.ArrayIndex(mVariables[Variables.ParametrosCreados], Expression.Constant(i)), mParametrosCreadosPorElUsuario[i].tipo)));
				}
				
				mParametrosCreadosPorElUsuario = null;

				//Luego añadimos el resto de bloques en orden
				foreach (var bloque in mBloques)
					expresiones.Add(bloque.ObtenerExpresion(this));

				//Expresion final representando toda la funcion
				var expresionFinal = Expression.Block(mVars, expresiones);

				//Generamos la lambda y la compilamos
				resultado.Funcion = Expression.Lambda<TipoFuncion>(expresionFinal, mParametros).Compile();
				
				SistemaPrincipal.LoggerGlobal.Log("Compilacion Finalizada!", ESeveridad.Info);
			}
			catch (Exception ex)
			{
				SistemaPrincipal.LoggerGlobal.Log(ex.Message, ESeveridad.Error);

				resultado.Mensaje += ex.Message;

				return resultado;
			}

			return resultado;
		}

		public Expression this[int idVariable]
		{
			get
			{
				if (mVariables.ContainsKey(idVariable))
					return mVariables[idVariable];

				return null;
			}
		}

		/// <summary>
		/// Inicializa <see cref="mVariables"/>, <see cref="mParametros"/> y <see cref="mBloques"/> a partir de <param name="bloques"></param>
		/// </summary>
		/// <param name="bloques"><see cref="List{T}"/> de <see cref="BloqueBase"/> con la que se inicializara</param>
		private void AñadirBloques(List<BloqueBase> bloques)
		{
			mBloques.Clear();
			mParametros.Clear();
			mVariables.Clear();
			mVars.Clear();

			foreach (var bloque in bloques)
			{
				if (bloque is BloqueVariable var)
				{
					ParameterExpression expresionVariableActual = (ParameterExpression)var.ObtenerExpresion(this);

					mVariables.Add(var.IDBloque, expresionVariableActual);

					if (var.tipoVariable == ETipoVariable.Parametro)
					{
						mParametros.Add(expresionVariableActual);
					}
					else 
					{
						if (var.tipoVariable == ETipoVariable.ParametroCreadoPorElUsuario)
						{
							mParametrosCreadosPorElUsuario.Add(new ValueTuple<ParameterExpression, Type>(expresionVariableActual, var.tipo));
						}

						mVars.Add(expresionVariableActual);
					}
					
					continue;
				}

				mBloques.Add(bloque);
			}

			mVariables.Add(Variables.ParametrosCreados, Expression.Parameter(typeof(object[]), "ParametrosCreados"));
			mParametros.Add((ParameterExpression) mVariables.Last().Value);

			mBloques.TrimExcess();
			mParametros.TrimExcess();
			mParametrosCreadosPorElUsuario.TrimExcess();
			mVariables.TrimExcess();
			mVars.TrimExcess();
		}
	}
}