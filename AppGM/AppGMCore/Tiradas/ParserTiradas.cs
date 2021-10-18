using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Security.Cryptography;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Indica el tipo de la seccion de la tirada que se esta parseando actualmente
	/// </summary>
	internal enum ETipoSeccionActual
	{
		Tirada,
		Constante,
		Variable,
		OperacionAritmetica,
		ZonaNoAfectadaPorMultiplicador,

		NINGUNO
	}

	/// <summary>
	/// Clase que contiene los metodos necesarios para poder parsear una tirada
	/// </summary>
	public static class ParserTiradas
	{
		/// <summary>
		/// Tiradas que ya han sido parseadas
		/// </summary>
		private static Dictionary<string, Func<ArgumentosTiradaPersonalizada, ResultadoTirada>> mTiradasCacheadas =
			new Dictionary<string, Func<ArgumentosTiradaPersonalizada, ResultadoTirada>>();

		public static (bool esValida, string error, MatchCollection seccionesTirada) TiradaEsValida(
			string tirada,
			ModeloPersonaje usuario,
			ETipoTirada tipoTirada,
			EStat stat)
		{
			if (string.IsNullOrWhiteSpace(tirada))
				return (false, $"{nameof(tirada)} no puede estar vacio", null);

			if (!Regex.IsMatch(tirada, @"\d+d\d+"))
				return (false, $"{nameof(tirada)} debe tener al menos una tirada", null);

			if (usuario == null)
				return (false, $"{nameof(usuario)} no puede ser null", null);

			if (tipoTirada == ETipoTirada.Stat)
				return (false, $"Para realizar una tirada de stat utilizar el metodo {nameof(RealizarTiradaStat)}", null);

			//Revisamos que el valor de stat sea valido en caso de ser necesario
			if (tipoTirada == ETipoTirada.Daño && stat == EStat.NINGUNA)
				return (false, $"{nameof(stat)} no puede ser {EStat.NINGUNA}", null);

			//Separamos las secciones de la tirada y las clasificamos
			MatchCollection seccionesTirada = Regex.Matches(tirada, @"(?<Tirada>\d+d\d+)|(?<OperacionAritmetica>[\-+*/])|(?<Constante>\d+)|(?<Variable>@[^\d\-+*/]+)|(?<ZonaNoAfectadoPorMultiplicadores>n)", RegexOptions.IgnoreCase);

			//En este bucle nos aseguramos de que todas las variables especificadas existan en el usuario
			foreach (Match match in seccionesTirada)
			{
				//Si la seccion actual no es una tirada continuamos a la proxima seccion
				if (string.IsNullOrWhiteSpace(match.Groups["Variable"].Value))
					continue;

				//Quitamos el '@'
				string variable = match.Groups["Variable"].Value.Remove(0, 1);

				//Si es una de las variables predeterminadas continuamos a la proxima seccion
				if (variable == "ParametroExtra" || variable == "Multiplicador" || variable == "Modificador")
					continue;

				//Intentamos obtener el valor de la variable y nos aseguramos que no sea null
				if (usuario.Variables.Any(var => var.NombreVariable == variable))
					return (false, $"No se encontro una variable llamada {variable} en {usuario}", null);
			}

			return (true, string.Empty, seccionesTirada);
		}

		/// <summary>
		/// Intenta parsear una <paramref name="tirada"/>
		/// </summary>
		/// <param name="tirada">Tirada que se intentara parsear</param>
		/// <param name="usuario">Controlador que quiere realizar esta tirada</param>
		/// <param name="tipoTirada">Tipo de la tirada</param>
		/// <param name="stat">Stat de la que denpende la tirada</param>
		/// <returns>
		/// Tupla que contiene:
		/// <list type="number">
		///		<item>
		///			<see cref="bool"/> indicando si se pudo parsear la tirada con exito
		///		</item>
		///		<item>
		///			<see cref="Func{TResult}"/> de la tirada
		///		</item>
		///		<item>
		///			<see cref="string"/> en caso de que haya ocurrido un error los detalles de este se encontraran aqui
		///		</item>
		/// </list>
		/// </returns>
		public static async Task<(bool exito, Func<ArgumentosTiradaPersonalizada, ResultadoTirada> funcion, string error)> TryParseAsync(
			string tirada,
			ControladorPersonaje usuario,
			ETipoTirada tipoTirada,
			EStat stat)
		{
			//Si la tirada ya ha sido parseada entonces devolvemos la funcion existente
			if (mTiradasCacheadas.ContainsKey(tirada))
				return (true, mTiradasCacheadas[tirada], string.Empty);

			var resultadoComprobacion = ParserTiradas.TiradaEsValida(tirada, usuario.modelo, tipoTirada, stat);

			if (!resultadoComprobacion.esValida)
				return (false, null, resultadoComprobacion.error);

			//Creamos el parser
			ParserTirada parser = new ParserTirada(resultadoComprobacion.seccionesTirada, tipoTirada);

			//Parseamos la tirada
			var resultado = await parser.ParseAsync();

			//Si no hubo errores la añadimos a las tiradas cacheadas
			if(resultado.error == string.Empty && resultado.funcion != null)
				mTiradasCacheadas.Add(tirada, resultado.funcion);

			return (true, resultado.funcion, resultado.error);
		}

		/// <summary>
		/// Realiza una tirada simple
		/// </summary>
		/// <param name="numeroDados">Numero de dados</param>
		/// <param name="numeroCaras">Numero de caras de los dados</param>
		/// <returns>Resultado de la tirada</returns>
		public static ResultadoTirada RealizarTirada(int numeroDados, int numeroCaras)
		{
			//En esta variable se acumulan los resultados de todos los dados
			int acumulacion = 0;

			StringBuilder cadena = new StringBuilder(numeroDados * 3);

			cadena.Append("(");

			//Por cada dado realizamos una tirada
			for (int i = 1; i <= numeroDados; ++i)
			{
				int resultadoDadoActual = RNGCryptoServiceProvider.GetInt32(1, numeroCaras + 1);

				acumulacion += resultadoDadoActual;

				cadena.Append(i != numeroDados ? $"{resultadoDadoActual}, " : $"{resultadoDadoActual}");
			}

			cadena.Append(")");

			return new ResultadoTirada(acumulacion, cadena.ToString());
		}

		/// <summary>
		/// Realiza una tirada simple
		/// </summary>
		/// <param name="numeroDados">Numero de dados</param>
		/// <param name="numeroCaras">Numero de caras de los dados</param>
		/// <returns>Resultado de la tirada</returns>
		public static ResultadoTirada RealizarTiradaStat(ArgumentosTirada argumentos)
		{
			//En esta variable se acumulan los resultados de todos los dados
			int acumulacion = 0;

			StringBuilder cadena = new StringBuilder();

			cadena.Append("(");

			//Tiramos tres veces un dado de seis caras y acumulamos sus respectivos resultados
			for (int i = 1; i <= 3; ++i)
			{
				int resultadoDadoActual = RNGCryptoServiceProvider.GetInt32(1, 7);

				acumulacion += resultadoDadoActual;

				cadena.Append(i != 3 ? $"{resultadoDadoActual}, " : $"{resultadoDadoActual}");
			}

			cadena.Append(")");

			//Modificador

			acumulacion += argumentos.modificador;

			cadena.Append($" + Mod({argumentos.modificador})");

			//Bono por especialidad

			int bonoEspecialidad = argumentos.multiplicadorEspecialidad * Constantes.BonoEspecialidad;

			acumulacion -= bonoEspecialidad;

			cadena.Append($" + BonoEspecialidad({-bonoEspecialidad})");

			return new ResultadoTirada(acumulacion, cadena.ToString());
		}
	}

	/// <summary>
	/// Clase encargada de parsear una tirada
	/// </summary>
	internal class ParserTirada
	{
		#region Campos

		/// <summary>
		/// Contiene todas las expresiones necesarias para crear la funcion de la tirada
		/// </summary>
		private List<Expression> mExpresiones = new List<Expression>();

		/// <summary>
		/// Contiene las varaibles que se utilizan dentro de la funcion
		/// </summary>
		private List<ParameterExpression> mVariables = new List<ParameterExpression>();

		/// <summary>
		/// Contiene la expresion de la seccion siendo parseada actualmente
		/// </summary>
		private Expression mExpresionActual = null;

		/// <summary>
		/// Metodo <see cref="string.Concat(string, string)"/>
		/// </summary>
		private MethodInfo mMetodoConcadenar;

		/// <summary>
		/// Metodo <see cref="object.ToString()"/>
		/// </summary>
		private MethodInfo mMetodoToString;

		/// <summary>
		/// Metodo <see cref="ParserTiradas.RealizarTirada"/>
		/// </summary>
		private MethodInfo mMetodoRealizarTirada;

		/// <summary>
		/// Metodo <see cref="ControladorBase.ObtenerValorVariable(string)"/>
		/// </summary>
		private MethodInfo mMetodoObtenerVariable;

		private MethodInfo mMetodoFloor;

		/// <summary>
		/// Campo <see cref="ResultadoTirada.resultado"/>
		/// </summary>
		private FieldInfo mFieldResultado;

		/// <summary>
		/// Campo <see cref="ResultadoTirada.cadena"/>
		/// </summary>
		private FieldInfo mFieldCadena;

		/// <summary>
		/// Constructor de <see cref="ResultadoTirada"/>
		/// </summary>
		private ConstructorInfo mConstructorResultadoTirada;

		/// <summary>
		/// Nombre de la variable siendo parseada actualmente
		/// </summary>
		private string mNombreVariableActual = string.Empty;

		/// <summary>
		/// Error generado al intentar parsear la tirada
		/// </summary>
		private string mError = string.Empty;

		/// <summary>
		/// Indica si la zona a partir de la cual no afectan los mutiplicadores fue hallada
		/// </summary>
		private bool mZonaNoAfectadaPorMultiplicadorEncontrada = false;

		/// <summary>
		/// Tipo de la seccion siendo parseada actualmente
		/// </summary>
		private ETipoSeccionActual mTipoSeccionActual = ETipoSeccionActual.NINGUNO;

		/// <summary>
		/// Secciones de la tirada
		/// </summary>
		private readonly List<Match> mSeccionesTirada;

		/// <summary>
		/// Tipo de la tirada
		/// </summary>
		private readonly ETipoTirada mTipoTirada;

		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_seccionesTirada">Secciones de la tirada</param>
		/// <param name="_tipoTirada">Tipo de la tirada</param>
		public ParserTirada(MatchCollection _seccionesTirada, ETipoTirada _tipoTirada)
		{
			mSeccionesTirada = _seccionesTirada.ToList();

			mTipoTirada  = _tipoTirada;
		}

		/// <summary>
		/// Parsea la tirada con la que se construyo este parser
		/// </summary>
		/// <returns>
		/// Tupla que contiene:
		/// <list type="number">
		///		<item>
		///			<see cref="Func{TResult}"/> de la tirada
		///		</item>
		///		<item>
		///			<see cref="string"/> en caso de que haya ocurrido un error los detalles de este se encontraran aqui
		///		</item>
		/// </list>
		/// </returns>
		public async Task<(Func<ArgumentosTiradaPersonalizada, ResultadoTirada> funcion, string error)> ParseAsync()
		{
			//Funcion resultante
			Func<ArgumentosTiradaPersonalizada, ResultadoTirada> funcion = null;

			funcion = await Task.Run(() =>
			{
				try
				{
					//Si la incializacion falla lanzamos una excepcion
					if (!Inicializar())
					{
						throw new NullReferenceException(mError);
					}

					//Label que representa el final de la funcion
					LabelTarget labelFinal = Expression.Label(typeof(ResultadoTirada), "LabelFinal");

					//Creamos los parametros
					ParameterExpression argumentos = Expression.Parameter(typeof(ArgumentosTiradaPersonalizada), "args");

					//Creamos las variables
					ParameterExpression usuario					  = Expression.Parameter(typeof(ControladorPersonaje), "Usuario");
					ParameterExpression resultadoTirada           = Expression.Parameter(typeof(ResultadoTirada), "ResultadoTiradaActual");
					ParameterExpression multiplicador			  = Expression.Parameter(typeof(float), "Multiplicador");
					ParameterExpression modificador				  = Expression.Parameter(typeof(int), "Modificador");
					ParameterExpression multiplicadorEspecialidad = Expression.Parameter(typeof(int), "MultiplicadorEspecialidad");
					ParameterExpression resultadoActual			  = Expression.Parameter(typeof(int), "ResultadoActual");
					ParameterExpression bonoEspecialidad          = Expression.Parameter(typeof(int), "BonoEspecialidad");
					ParameterExpression parametroExtra            = Expression.Parameter(typeof(string), "ParametroExtra");
					ParameterExpression cadenaActual			  = Expression.Parameter(typeof(string), "Cadena");
					ParameterExpression manoUtilizada             = Expression.Parameter(typeof(EManoUtilizada), "ManoUtilizada");
					ParameterExpression stat                      = Expression.Parameter(typeof(EStat), "Stat");

					Expression argumentosDelTipoAdecuado = null;

					switch (mTipoTirada)
					{
						case ETipoTirada.Daño:
						{
							ParameterExpression argumentosTiradaDaño = Expression.Parameter(typeof(ArgumentosTiradaDaño), "ArgumentosTiradaDaño");

							mVariables.AddRange(new []
							{
								usuario, 
								resultadoTirada, 
								multiplicador,
								modificador, 
								bonoEspecialidad,
								multiplicadorEspecialidad,
								parametroExtra, 
								resultadoActual,
								cadenaActual, 
								manoUtilizada,
								argumentosTiradaDaño,
								stat
							});

							Expression comprobacionNull =
								Expression.IfThen(
									Expression.Equal(
										argumentosTiradaDaño,
										Expression.Constant(null)),
									Expression.Return(labelFinal, resultadoTirada, typeof(ResultadoTirada)));

							mExpresiones.AddRange(new []
							{
								Expression.Assign(argumentosTiradaDaño, Expression.Convert(argumentos, typeof(ArgumentosTiradaDaño))),
								comprobacionNull,

								Expression.Assign(manoUtilizada, Expression.Field(argumentosTiradaDaño, nameof(ArgumentosTiradaDaño.manoUtilizada))),
								Expression.Assign(multiplicador, Expression.Field(argumentosTiradaDaño, nameof(ArgumentosTiradaDaño.multiplicador))),
							});

							break;
						}
						case ETipoTirada.Personalizada:
						{
							mVariables.AddRange(new[]
							{
								usuario,
								resultadoTirada,
								modificador,
								bonoEspecialidad,
								multiplicadorEspecialidad,
								parametroExtra,
								resultadoActual,
								cadenaActual,
								stat
							});

							argumentosDelTipoAdecuado = Expression.ConvertChecked(argumentos, typeof(ArgumentosTiradaPersonalizada));

							break;
						}
					}

					mExpresiones.AddRange(new []
					{
						Expression.Assign(parametroExtra,Expression.Field(argumentos, nameof(ArgumentosTiradaPersonalizada.parametroExtra))),
						Expression.Assign(stat,Expression.Field(argumentos, nameof(ArgumentosTiradaPersonalizada.stat))),
						Expression.Assign(usuario, Expression.Field(argumentos, nameof(ArgumentosTiradaPersonalizada.controlador))),
						Expression.Assign(modificador, Expression.Field(argumentos, nameof(ArgumentosTiradaPersonalizada.modificador))),
						Expression.Assign(multiplicadorEspecialidad, Expression.Field(argumentos, nameof(ArgumentosTiradaPersonalizada.multiplicadorEspecialidad))),
					});

					//Por cada seccion de la tirada...
					for (int i = 0; i < mSeccionesTirada.Count; i += 2)
					{
						//Obtenemos la seccion actual
						Match seccionActual = mSeccionesTirada[i];

						//Si es una tirada...
						if (seccionActual.Groups["Tirada"].Value is string tirada && !string.IsNullOrWhiteSpace(tirada))
						{
							//Actualimos el tipo de seccion actual
							mTipoSeccionActual = ETipoSeccionActual.Tirada;

							//Separamos la tirada en sus dos partes
							string[] datosTirada = tirada.Split('d');

							//Obtenemos el numero de dados y de caras
							int numeroDeDados = int.Parse(datosTirada[0]);
							int carasDados    = int.Parse(datosTirada[1]);

							//Añadimos una llamada al metodo para realizar una tirada simple y guardamos su resultado en 'resultadoTirada'
							mExpresiones.Add(Expression.Assign(resultadoTirada, Expression.Call(null, mMetodoRealizarTirada, Expression.Constant(numeroDeDados), Expression.Constant(carasDados))));

							mExpresionActual = resultadoTirada;
						}

						//Si es una constante...
						else if (seccionActual.Groups["Constante"].Value is string constante && !string.IsNullOrWhiteSpace(constante))
						{
							mTipoSeccionActual = ETipoSeccionActual.Constante;

							//Igualamos la expresion actual al valor de la constante
							mExpresionActual = Expression.Constant(int.Parse(constante), typeof(int));
						}

						//Si es una variable...
						else if (seccionActual.Groups["Variable"].Value is string variable && !string.IsNullOrWhiteSpace(variable))
						{
							mTipoSeccionActual = ETipoSeccionActual.Variable;

							//Quitamos el '@' del nombre de la variable
							string nombreVariable = variable.Remove(0, 1);

							//Si es alguna de las variables por defectos asignamos la expresion actual a su valor
							if (nombreVariable.Equals("ParametroExtra"))
							{
								mExpresionActual = Expression.Call(null, typeof(int).GetMethod(nameof(int.Parse), new []{typeof(string)}), parametroExtra);
							}
							else if (nombreVariable.Equals("Multiplicador"))
							{
								mExpresionActual = multiplicador;
							}
							else if (nombreVariable.Equals("Modificador"))
							{
								mExpresionActual = modificador;
							}
							//Si no lo es entonces obtenemos el valor de la variable desde el controlador del usuario
							else
							{
								mExpresionActual = Expression.Call(usuario, mMetodoObtenerVariable, Expression.Constant(nombreVariable));
							}

							//Actualizamos el nombre de la varaible actual
							mNombreVariableActual = nombreVariable;
						}

						//Si es un indicador de zona no afectada por multiplicadores...
						else if (!string.IsNullOrWhiteSpace(seccionActual.Groups["ZonaNoAfectadoPorMultiplicadores"].Value))
						{
							mTipoSeccionActual = ETipoSeccionActual.ZonaNoAfectadaPorMultiplicador;

							//Multiplicamos el valor acumulado hasta ahora por el multiplicador
							mExpresiones.Add(Expression.Assign(resultadoActual, FloorToInt(MultiplyConTipo(resultadoActual, multiplicador, typeof(float)))));

							//Si esta no es la primera seccion, actualizamos la cadena actual
							if (i != 0)
								mExpresiones.Add(ConcatAssign(cadenaActual, Concat(Concat(Expression.Constant(" * ("), multiplicador), Expression.Constant(")"))));

							//Quitamos esta seccion de la lista y retrocedemos un indice para no romper el orden
							mSeccionesTirada.RemoveAt(i);

							--i;

							mZonaNoAfectadaPorMultiplicadorEncontrada = true;

							continue;
						}

						//Si es otra cosa...
						else
						{
							//Si es una operacion aritmetica...
							if (!string.IsNullOrWhiteSpace(seccionActual.Groups["OperacionAritmetica"].Value))
							{
								string nombreVariable = seccionActual.Groups["OperacionAritmetica"].Value;

								throw new ArgumentException($"Una variable no es valida aqui! ({nombreVariable})");
							}

							//Si no lo es...
							throw new ArgumentException($"Grupo desconocido! ({seccionActual.Value})");
						}

						//Si esta es la primera seccion...
						if (i == 0)
						{
							//Añadimos la expresion actual a la cadena
							mExpresiones.Add(ExpresionActualToString(cadenaActual));

							//Sumamos al resultado actual el valor de la expresion actual
							mExpresiones.Add(Expression.AddAssign(resultadoActual,  mTipoSeccionActual == ETipoSeccionActual.Tirada ? Expression.Field(mExpresionActual, mFieldResultado) : mExpresionActual));

							//Continuamos a la siguiente iteracion
							continue;
						}

						//Si llegamos a este punto es porque esta no es la primera seccion

						//Obtenemos la seccion anterior a la actual, la cual debe ser una operacion aritmetica
						Match seccionAnterior = mSeccionesTirada[i - 1];

						//Si es una operacion aritmetica...
						if (seccionAnterior.Groups["OperacionAritmetica"].Value is string operacion && !string.IsNullOrWhiteSpace(operacion))
						{
							//Añadimos a la cadena la operacion
							mExpresiones.Add(ConcatAssign(cadenaActual, Expression.Constant($" {operacion} ")));

							//Añadimos a la cadena la expresion actual
							mExpresiones.Add(ExpresionActualToString(cadenaActual));

							//Si la seccion actual es una tirada entonces ahora asignamos a la expresion actual el resultado de esa tirada
							if (mTipoSeccionActual == ETipoSeccionActual.Tirada)
								mExpresionActual = Expression.Field(mExpresionActual, mFieldResultado);

							//Revisamos cual es la operacion aritmetica y la realizamos
							switch (operacion[0])
							{
								case '+':
									mExpresiones.Add(Expression.AddAssign(resultadoActual, mExpresionActual));
									break;
								case '-':
									mExpresiones.Add(Expression.SubtractAssign(resultadoActual, mExpresionActual));
									break;
								case '*':
									mExpresiones.Add(Expression.MultiplyAssign(resultadoActual, mExpresionActual));
									break;
								case '/':
									mExpresiones.Add(Expression.DivideAssign(resultadoActual, mExpresionActual));
									break;
							}
						}

						//Fin del for
					}

					//Obtenemos el bono de por especialidad
					mExpresiones.Add(Expression.Assign(bonoEspecialidad, Expression.Multiply(Expression.Constant(Constantes.BonoEspecialidad), multiplicadorEspecialidad)));

					//Y se lo sumamos al resultado
					mExpresiones.Add(Expression.AddAssign(resultadoActual, bonoEspecialidad));

					//Representamos las ultimas dos operaciones en la cadena
					mExpresiones.Add(ConcatAssign(cadenaActual, Concat(Concat(Expression.Constant(" + BonoEspecialidad("), ExpresionToString(bonoEspecialidad)), Expression.Constant(")"))));

					//Si la tirada es de daño
					if (mTipoTirada == ETipoTirada.Daño)
					{
						//Metodo para obtener el modificador de stat de un personaje
						MethodInfo metodoObtenerModificadorStat            = typeof(ControladorPersonaje).GetMethod(nameof(ControladorPersonaje.ObtenerModificadorStat), new[] { typeof(EStat) });

						//Metodo para obtener el multiplicador por mano utilizada
						MethodInfo metodoObtenerMultiplicadorManoUtilizada = typeof(Helpers.Juego).GetMethod(nameof(Helpers.Juego.ObtenerMultiplicadorManoUsada), new[] {typeof(EManoUtilizada)});

						//Creamos una varaible para almacenar el valor del modificador de tirada por stat
						ParameterExpression modificadorStat = Expression.Parameter(typeof(int), "ModificadorStat");

						//Añadimos la variable a la lista
						mVariables.Add(modificadorStat);

						//Obtenemos el modificador de stat del usuario y lo guardamos en la variable creada anteriormente
						mExpresiones.Add(Expression.Assign(modificadorStat, Expression.Call(usuario, metodoObtenerModificadorStat, stat)));

						//Multiplicamos el modificador de stat por el multiplicador de mano utilizada y guardamos el resultado en la misma variable
						mExpresiones.Add(Expression.Assign(modificadorStat, FloorToInt(MultiplyConTipo(modificadorStat, Expression.Call(null, metodoObtenerMultiplicadorManoUtilizada, manoUtilizada), typeof(float)))));

						//Sumamos al resultado actual el valor de modificador stat
						mExpresiones.Add(Expression.AddAssign(resultadoActual, modificadorStat));

						//Actualizamos la cadena con la ultima operacion
						mExpresiones.Add(ConcatEnvolver(cadenaActual, Concat(ObtenerOperacionQueMostrar(modificadorStat), Expression.Constant("ModStat(")), ExpresionToString(modificadorStat), Expression.Constant(")")));
					}

					//Sumamos al resultado actual el modificador
					mExpresiones.Add(Expression.AddAssign(resultadoActual, modificador));

					//Actualizamos la candena
					mExpresiones.Add(ConcatEnvolver(cadenaActual, Concat(ObtenerOperacionQueMostrar(modificador), Expression.Constant("Mod(")), ExpresionToString(modificador), Expression.Constant(")")));

					//Si la tirada no incluia una zona no afectada por multiplicador...
					if (!mZonaNoAfectadaPorMultiplicadorEncontrada && mTipoTirada == ETipoTirada.Daño)
					{
						//Multiplicamos el resultado actual por el multiplicador
						mExpresiones.Add(Expression.Assign(resultadoActual, FloorToInt(MultiplyConTipo(resultadoActual, multiplicador, typeof(float)))));

						//Actualizamos la candena
						mExpresiones.Add(ConcatEnvolver(cadenaActual, Expression.Constant(" * Multiplicador("), ExpresionToString(multiplicador), Expression.Constant(")")));
					}

					//Construimos el resultado de la tirada con los datos generados y lo guardamos en resultadoTirada
					mExpresiones.Add(Expression.Assign(resultadoTirada, Expression.New(mConstructorResultadoTirada, resultadoActual, cadenaActual)));

					//Label de final de funcion
					mExpresiones.Add(Expression.Label(labelFinal, resultadoTirada));

					//Bloque que contiene todas las expresiones y variables creadas
					Expression expresionFinal = Expression.Block(mVariables, mExpresiones);

					//Compilamos la funcion
					return Expression.Lambda<Func<ArgumentosTiradaPersonalizada, ResultadoTirada>>(expresionFinal, argumentos).Compile();
				}
				//En caso que ocurra alguna excepcion durante el parseo...
				catch (Exception ex)
				{
					//Asignamos el valor del error a la excepcion
					mError = ex.Message;

					SistemaPrincipal.LoggerGlobal.Log($"Error parseando tirada!{Environment.NewLine}{mError}", ESeveridad.Error);

					return null;
				}
			});

			//Devolvemos la funcion creada
			return (funcion, mError);
		}

		/// <summary>
		/// Concadena <paramref name="exp1"/> y <paramref name="exp2"/>, luego asigna el resultado a <paramref name="exp1"/>
		/// </summary>
		/// <param name="exp1">Expresion a la que asignar el resultado de la concadenacion</param>
		/// <param name="exp2">Expresion que concadenar con <paramref name="exp1"/></param>
		/// <returns>Expresion que representa concadenar <paramref name="exp1"/> con <paramref name="exp1"/> y asignar el resultado a <paramref name="exp1"/></returns>
		private Expression ConcatAssign(Expression exp1, Expression exp2)
		{
			return Expression.Assign(exp1, Concat(exp1, exp2));
		}

		/// <summary>
		/// Concadena <paramref name="exp1"/> y <paramref name="exp2"/>
		/// </summary>
		/// <param name="exp1">Primera expresion</param>
		/// <param name="exp2">Segunda expresion</param>
		/// <returns>Expresion que representa concadenar <paramref name="exp1"/> con <paramref name="exp1"/></returns>
		private Expression Concat(Expression exp1, Expression exp2)
		{
			return Expression.Call(null, mMetodoConcadenar, exp1, exp2);
		}

		/// <summary>
		/// Concadena <paramref name="izq"/> con <paramref name="centro"/> y <paramref name="der"/> efectivamente dejando a <paramref name="centro"/>
		/// rodeado por <paramref name="izq"/> y <paramref name="der"/>. Luego asigna el resultado a <paramref name="var"/>
		/// </summary>
		/// <param name="var">Expresion a la que asignar el resultado de la concadenacion</param>
		/// <param name="izq">Expresion que quedara a la izquierda de <paramref name="centro"/></param>
		/// <param name="centro">Expresion que quedara rodeada por <paramref name="izq"/> y <paramref name="der"/></param>
		/// <param name="der">Expresion que quedara a la derecha de <paramref name="centro"/></param>
		/// <returns>Expresion que representa envolver a <paramref name="centro"/> con <paramref name="izq"/> y <paramref name="der"/>
		/// y luego asignar el resultado a <paramref name="var"/></returns>
		private Expression ConcatEnvolver(Expression var, Expression izq, Expression centro, Expression der)
		{
			return ConcatAssign(var, Concat(Concat(izq, centro), der));
		}

		/// <summary>
		/// Ejecuta el metodo <see cref="mMetodoToString"/> con la <see cref="mExpresionActual"/> y asigna el resultado a <see cref="cadenaActual"/>
		/// </summary>
		/// <param name="cadenaActual">Expresion a la que asignar el resultado de la concadenacion</param>
		/// <returns>Expresion que representa ejecutar el metodo <see cref="mMetodoToString"/> sobre <see cref="mExpresionActual"/> y
		/// asignar el resultado a <paramref name="cadenaActual"/></returns>
		private Expression ExpresionActualToString(Expression cadenaActual)
		{
			//Si la expresion actual es una variable tambien añadimos el nombre y unos parentesis para que se vea mas bonito
			if (mTipoSeccionActual == ETipoSeccionActual.Variable)
			{
				return ConcatAssign(cadenaActual, Expression.Call(null, mMetodoConcadenar, Expression.Call(null, mMetodoConcadenar,
					Expression.Constant($"{mNombreVariableActual} ("),
					Expression.Call(mExpresionActual, mMetodoToString)), Expression.Constant(")")));
			}
			//Si es cualquier otro tipo entonces solamente llamamos ToString
			else
			{
				return ConcatAssign(cadenaActual, ExpresionToString(mExpresionActual));
			}
		}

		/// <summary>
		/// Ejecuta el <see cref="mMetodoToString"/> sobre <paramref name="exp"/>
		/// </summary>
		/// <param name="exp">Expresion sobre la cual ejecutar el metodo</param>
		/// <returns>Expresion que representa llamar el <see cref="mMetodoToString"/> sobre <paramref name="exp"/></returns>
		private Expression ExpresionToString(Expression exp)
		{
			return Expression.Call(exp, mMetodoToString);
		}

		/// <summary>
		/// Obtiene la operacion aritmetica que mostrar frente a un digito en base a si este es negativo o positivo
		/// </summary>
		/// <param name="exp">Expresion que representa el digito</param>
		/// <returns>Expresion que representa una condicion para obtener el signo correspondiente para <paramref name="exp"/></returns>
		private Expression ObtenerOperacionQueMostrar(Expression exp)
		{
			return Expression.Condition(
				Expression.LessThan(exp, Expression.Constant(0, typeof(int))),
				Expression.Constant(" - "), Expression.Constant(" + "));
		}

		/// <summary>
		/// Ejecuta el <see cref="mMetodoFloor"/> sobre <paramref name="exp"/> y convierte el resultado a un <see cref="int"/>
		/// </summary>
		/// <param name="exp">Expresion sobre la cual ejecutar el metodo</param>
		/// <returns>Expresion que representa ejecutar el <see cref="mMetodoFloor"/> sobre <paramref name="exp"/> y
		/// convertir el resultado a un <see cref="int"/></returns>
		private Expression FloorToInt(Expression exp)
		{
			return Expression.Convert(Expression.Call(null, mMetodoFloor, Expression.Convert(exp, typeof(double))), typeof(int));
		}

		/// <summary>
		/// Multiplica dos <see cref="Expression"/> y se asegura de que sean del mismo <see cref="Type"/> a traves de una conversion
		/// </summary>
		/// <param name="exp1">Primera expresion</param>
		/// <param name="exp2">Segunda expresion</param>
		/// <param name="tipo">Tipo al que convertir las expresiones</param>
		/// <returns>Expresion que representa multiplicar <paramref name="exp1"/> con <paramref name="exp2"/></returns>
		private Expression MultiplyConTipo(Expression exp1, Expression exp2, Type tipo)
		{
			return Expression.Multiply(Expression.Convert(exp1, tipo), Expression.Convert(exp2, tipo));
		}

		/// <summary>
		/// Suma dos <see cref="Expression"/> y se asegura de que sean del mismo <see cref="Type"/> a traves de una conversion
		/// </summary>
		/// <param name="exp1">Primera expresion</param>
		/// <param name="exp2">Segunda expresion</param>
		/// <param name="tipo">Tipo al que convertir las expresiones</param>
		/// <returns>Expresion que representa sumar <paramref name="exp1"/> a <paramref name="exp2"/></returns>
		private Expression AddConTipo(Expression exp1, Expression exp2, Type tipo)
		{
			return Expression.Add(Expression.Convert(exp1, tipo), Expression.Convert(exp2, tipo));
		}

		/// <summary>
		/// Inicializa el parser (en realidad solo obtiene unos cuantos metodos)
		/// </summary>
		/// <returns><see cref="bool"/> indicando si se pudieron obtener todos los metodos</returns>
		private bool Inicializar()
		{
			mMetodoConcadenar      = typeof(string).GetMethod(nameof(string.Concat), new[] {typeof(string), typeof(string)});

			if (mMetodoConcadenar == null)
			{
				mError = $"No se pudo encontrar el metodo {nameof(string.Concat)}";

				return false;
			}

			mMetodoToString        = typeof(object).GetMethod(nameof(object.ToString), new Type[] { });

			if (mMetodoToString == null)
			{
				mError = $"No se pudo encontrar el metodo {nameof(object.ToString)}";

				return false;
			}

			mMetodoRealizarTirada  = typeof(ParserTiradas).GetMethod(nameof(ParserTiradas.RealizarTirada), new[] { typeof(int), typeof(int) });

			if (mMetodoRealizarTirada == null)
			{
				mError = $"No se pudo encontrar el metodo {nameof(ParserTiradas.RealizarTirada)}";

				return false;
			}

			mMetodoObtenerVariable = typeof(ControladorBase).GetMethod(nameof(ControladorBase.ObtenerValorVariable), new[] {typeof(string)});

			if (mMetodoObtenerVariable == null)
			{
				mError = $"No se pudo encontrar el metodo {nameof(ControladorBase.ObtenerValorVariable)}";

				return false;
			}

			mMetodoFloor = typeof(Math).GetMethod(nameof(Math.Floor), new[] { typeof(float) });

			if (mMetodoFloor == null)
			{
				mError = $"No se pudo encontrar el metodo {nameof(Math.Floor)}";

				return false;
			}

			mConstructorResultadoTirada = typeof(ResultadoTirada).GetConstructor(new[] { typeof(int), typeof(string) });

			if (mMetodoObtenerVariable == null)
			{
				mError = $"No se pudo encontrar el constructor de {nameof(ResultadoTirada)}";

				return false;
			}

			mFieldResultado = typeof(ResultadoTirada).GetField(nameof(ResultadoTirada.resultado));

			if (mFieldResultado == null)
			{
				mError = $"No se pudo encontrar el campo {nameof(ResultadoTirada.resultado)}";

				return false;
			}

			mFieldCadena = typeof(ResultadoTirada).GetField(nameof(ResultadoTirada.cadena));

			if (mFieldResultado == null)
			{
				mError = $"No se pudo encontrar el campo {nameof(ResultadoTirada.cadena)}";

				return false;
			}

			return true;
		}
	}
}