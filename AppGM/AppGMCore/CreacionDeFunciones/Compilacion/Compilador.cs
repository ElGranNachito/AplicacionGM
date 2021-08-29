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

		private List<BloqueVariable> mBloquesVariables = new List<BloqueVariable>();

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

			SistemaPrincipal.LoggerGlobal.Log($"Iniciando Compilacion... (PropositoFuncion: {typeof(TipoFuncion)})", ESeveridad.Info);

			//Label que se encuentra al final de la funcion. Esta etiqueta es utilizada por los return
			var labelFinalFuncion = Expression.Label(typeof(void), "FinalFuncion");

			//Lista de expresiones que componen la funcion
			List<Expression> expresiones = new List<Expression>();

			try
			{
				//Inicialzamos las variables
				foreach (var var in mBloquesVariables)
				{
					if (var.ObtenerExpresionInicializacion(this) is { } exp)
					{
						expresiones.Add(Expression.Assign(var.ObtenerExpresion(this), exp));
					}
				}

				var parametrosCreadosPorElUsuario =
					mBloquesVariables.FindAll(b =>
						b.tipoVariable == ETipoVariable.ParametroCreadoPorElUsuario);

				//Asignamos los valores correspondientes a los parametros creados por el usuario
				for (int i = 0; i < parametrosCreadosPorElUsuario.Count; ++i)
				{
					expresiones.Add(Expression.Assign(parametrosCreadosPorElUsuario[i].ObtenerExpresion(this), Expression.Convert(Expression.ArrayIndex(mVariables[Variables.ParametrosCreados], Expression.Constant(i)), parametrosCreadosPorElUsuario[i].tipo)));
				}
				
				//Luego añadimos el resto de bloques en orden
				foreach (var bloque in mBloques)
					expresiones.Add(bloque.ObtenerExpresion(this));

				//Colocamos la label al final de funcion
				expresiones.Add(Expression.Label(labelFinalFuncion));

				var metodoAsignarValorAVariable = typeof(ControladorBase).GetMethod(nameof(ControladorBase.AsignarAVariable), new[] {typeof(object)});

				if (metodoAsignarValorAVariable == null)
					throw new Exception($"No se pudo hallar el metodo {nameof(ControladorBase.AsignarAVariable)}!");

				//Guardamos el valor de las variables persistentes
				expresiones.AddRange(mBloquesVariables.FindAll(b => b.tipoVariable == ETipoVariable.Persistente).Select(b =>
				{
					return Expression.Call(
						mVariables[Compilador.Variables.ControladorFuncion],
						metodoAsignarValorAVariable, 
						Expression.Constant(b.IDBloque),
						Expression.Convert(mVariables[b.IDBloque], typeof(object)));
				}));

				//Expresion final representando toda la funcion
				var expresionFinal = Expression.Block(mBloquesVariables.Select(b => b.ObtenerExpresion(this)).Cast<ParameterExpression>(), expresiones);

				//Generamos la lambda y la compilamos
				resultado.Funcion = Expression.Lambda<TipoFuncion>(expresionFinal, mParametros).Compile();
				
				SistemaPrincipal.LoggerGlobal.Log("Compilacion Finalizada!", ESeveridad.Info);

				resultado.FueExitosa = true;
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
			mBloquesVariables.Clear();

			foreach (var bloque in bloques)
			{
				if (bloque is BloqueVariable var)
				{
					var expresionVaraibleActual = (ParameterExpression) var.ObtenerExpresion(this);

					mVariables.Add(var.IDBloque, expresionVaraibleActual);

					if(var.tipoVariable != ETipoVariable.Parametro)
						mBloquesVariables.Add(var);
					else
						mParametros.Add(expresionVaraibleActual);
					/*if (var.tipoVariable == ETipoVariable.Parametro)
					{
						
					}
					else if(var.Argumento == null)
					{
						mBloquesVariables.Add(new VariableCompilador(
							var.ObtenerExpresion(this), 
							var.ObtenerExpresionInicializacion(this),
							var.tipo,
							var.EsPersistente, 
							var.tipoVariable == ETipoVariable.ParametroCreadoPorElUsuario));

						if (var.tipoVariable == ETipoVariable.ParametroCreadoPorElUsuario)
						{
							mParametrosCreadosPorElUsuario.Add(new ValueTuple<ParameterExpression, Type>(expresionVariableActual, var.tipo));
						}
						else if (var.EsPersistente)
						{
							mVariablesPersistentes.Add(new ValueTuple<ParameterExpression, Type>(expresionVariableActual, var.tipo));
						}

						mVars.Add(expresionVariableActual);
					}*/
					
					continue;
				}

				mBloques.Add(bloque);
			}

			mParametros.Add(Expression.Parameter(typeof(ControladorFuncionBase), "ControladorFuncion"));
			mVariables.Add(Compilador.Variables.ControladorFuncion, mParametros.Last());

			mVariables.Add(Variables.ParametrosCreados, Expression.Parameter(typeof(object[]), "ParametrosCreados"));
			mParametros.Add((ParameterExpression) mVariables.Last().Value);

			mBloques.TrimExcess();
			mParametros.TrimExcess();
			mVariables.TrimExcess();
			mBloquesVariables.TrimExcess();
		}
	}

	class VariableCompilador
	{
		public Expression expresionVariable;

		public Expression expresionValorDefault;

		public Type tipoVariable;

		public bool esPersistente;

		public bool esParametro;

		public VariableCompilador(Expression _expresionVariable, Expression _expresionValorDefault, Type _tipoVariable, bool _esPersistente, bool _esParametro)
		{
			expresionVariable     = _expresionVariable;
			expresionValorDefault = _expresionValorDefault;
			tipoVariable          = _tipoVariable;
			esPersistente         = _esPersistente;
			esParametro           = _esParametro;
		}
	}
}