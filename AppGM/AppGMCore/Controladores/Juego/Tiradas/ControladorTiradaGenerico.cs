using System;
using System.Threading.Tasks;

using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Controlador generico para un <see cref="ModeloTiradaBase"/>
	/// </summary>
	/// <typeparam name="TModelo">Tipo del <see cref="ModeloTiradaBase"/> contenido por esta clase</typeparam>
	/// <typeparam name="TArgs">Tipo de los <see cref="ArgumentosTiradaPersonalizada"/> requeridos por el <see cref="ModeloTiradaBase"/> contenido por esta clase</typeparam>
	public abstract class ControladorTiradaGenerico<TModelo, TArgs> : ControladorTiradaBase
		
		where TModelo: ModeloTiradaBase
		where TArgs: ArgumentosTiradaPersonalizada
	{
		#region Campos & Propiedades

		/// <summary>
		/// <typeparamref name="TModelo"/> contenido por este controlador
		/// </summary>
		public readonly TModelo modeloGenerico;

		/// <summary>
		/// Funcion que ejecuta la tirada
		/// </summary>
		public Func<TArgs, ResultadoTirada> FuncionTirada { get; private set; } 

		/// <summary>
		/// Argumentos utilizados la ultima vez que se realizo esta tirada
		/// </summary>
		public TArgs ArgsUltimaTirada { get; private set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_modelo">Modelo contenido por este controlador</param>
		public ControladorTiradaGenerico(TModelo _modelo)
			: base(_modelo)
		{
			modeloGenerico = _modelo;
		}

		#endregion

		#region Metodos

		public override ResultadoTirada RealizarTirada(ArgumentosTirada args)
		{
			if (args is TArgs argsTipoRequerido)
				return RealizarTirada(argsTipoRequerido);

			SistemaPrincipal.LoggerGlobal.LogCrash($"{nameof(args)} debe ser de tipo {typeof(TArgs)}");

			return new ResultadoTirada(Constantes.ResultadoTiradaInvalido, string.Empty);
		}

		/// <summary>
		/// Version generica del metodo <see cref="AppGM.Core.ControladorTiradaBase.RealizarTirada"/>
		/// </summary>
		/// <param name="argsTirada">Argumentos con los que realizar la tirada</param>
		/// <returns>Resultado de la tirada</returns>
		public virtual ResultadoTirada RealizarTirada(TArgs argsTirada)
		{
			if (FuncionTirada is not null)
			{
				if (argsTirada.personaje.modelo == UltimoUsuario)
				{
					ArgsUltimaTirada = argsTirada;

					Resultado = FuncionTirada(argsTirada);

					return Resultado;
				}

				if (!PuedeRealizarTirada((ModeloConVariablesYTiradas)argsTirada.controlador.Modelo))
				{
					SistemaPrincipal.LoggerGlobal.Log($"No se puede realizar la tirada con el controlador especificado", ESeveridad.Error);

					return new ResultadoTirada(Constantes.ResultadoTiradaInvalido, string.Empty);
				}

				UltimoUsuario = argsTirada.personaje.modelo;

				ArgsUltimaTirada = argsTirada;

				Resultado = FuncionTirada(argsTirada);

				return Resultado;
			}

			var resultadoParse = ParserTiradas.TryParse(
				modelo.Tirada,
				(ModeloConVariablesYTiradas)argsTirada.controlador.Modelo,
				modelo.TipoTirada,
				argsTirada.stat);
			
			FuncionTirada = resultadoParse.funcion;

			if (resultadoParse.exito)
			{
				ArgsUltimaTirada = argsTirada;

				Resultado = FuncionTirada(argsTirada);

				return Resultado;
			}

			return new ResultadoTirada(Constantes.ResultadoTiradaInvalido, string.Empty);
		}

		public override async Task<ResultadoTirada> RealizarTiradaAsync(ArgumentosTiradaPersonalizada args)
		{
			if (args is TArgs argsTipoRequerido)
				return await RealizarTiradaAsync(argsTipoRequerido);

			SistemaPrincipal.LoggerGlobal.LogCrash($"{nameof(args)} debe ser de tipo {typeof(TArgs)}");

			return new ResultadoTirada(Constantes.ResultadoTiradaInvalido, string.Empty);
		}

		/// <summary>
		/// Version generica del metodo <see cref="AppGM.Core.ControladorTiradaBase.RealizarTiradaAsync"/>
		/// </summary>
		/// <param name="argsTirada">Argumentos con los que realizar la tirada</param>
		/// <returns>Resultado de la tirada</returns>
		public virtual async Task<ResultadoTirada> RealizarTiradaAsync(TArgs argsTirada)
		{
			if (FuncionTirada is not null)
			{
				if (UltimoUsuario == argsTirada.personaje.modelo)
				{
					ArgsUltimaTirada = argsTirada;

					Resultado = FuncionTirada(argsTirada);

					return Resultado;
				}

				if (!PuedeRealizarTirada((ModeloConVariablesYTiradas) argsTirada.controlador.Modelo))
				{
					SistemaPrincipal.LoggerGlobal.Log($"No se puede realizar la tirada con el controlador especificado", ESeveridad.Error);

					return new ResultadoTirada(Constantes.ResultadoTiradaInvalido, string.Empty);
				}

				UltimoUsuario = argsTirada.personaje.modelo;

				ArgsUltimaTirada = argsTirada;

				Resultado = FuncionTirada(argsTirada);

				return Resultado;
			}

			var resultadoParse = await ParserTiradas.TryParseAsync(
				modelo.Tirada,
				(ModeloConVariablesYTiradas) argsTirada.controlador.Modelo, 
				modelo.TipoTirada,
				argsTirada.stat);

			FuncionTirada = resultadoParse.funcion;

			if (resultadoParse.exito)
			{
				ArgsUltimaTirada = argsTirada;

				Resultado = FuncionTirada(argsTirada);

				return Resultado;
			}

			return new ResultadoTirada(Constantes.ResultadoTiradaInvalido, string.Empty);
		}

		public override bool TryParse(ModeloConVariablesYTiradas contenedor)
		{
			return TryParse(contenedor);
		}

		public override async Task<bool> TryParseAsync(ModeloConVariablesYTiradas contenedor)
		{
			return await TryParseAsync(contenedor);
		}

		private bool TryParse(ModeloConVariablesYTiradas contenedor, EStat stat = EStat.NINGUNA)
		{
			if (FuncionTirada is not null)
				SistemaPrincipal.LoggerGlobal.Log($"Volviendo a parsear una tirada ({modelo.Nombre})", ESeveridad.Advertencia);

			var resultadoParse = ParserTiradas.TryParse(modelo.Tirada, contenedor, modelo.TipoTirada, stat);

			FuncionTirada = resultadoParse.funcion;

			if (!resultadoParse.exito)
				SistemaPrincipal.LoggerGlobal.Log($"Error al parsear tirada ({modelo.Nombre}). Error: {resultadoParse.error}", ESeveridad.Error);

			return resultadoParse.exito;
		}

		private async Task<bool> TryParseAsync(ModeloConVariablesYTiradas contenedor, EStat stat = EStat.NINGUNA)
		{
			if (FuncionTirada is not null)
				SistemaPrincipal.LoggerGlobal.Log($"Volviendo a parsear una tirada ({modelo.Nombre})", ESeveridad.Advertencia);

			var resultadoParse = await ParserTiradas.TryParseAsync(modelo.Tirada, contenedor, modelo.TipoTirada, stat);

			FuncionTirada = resultadoParse.funcion;

			if (!resultadoParse.exito)
				SistemaPrincipal.LoggerGlobal.Log($"Error al parsear tirada ({modelo.Nombre}). Error: {resultadoParse.error}", ESeveridad.Error);

			return resultadoParse.exito;
		} 

		#endregion
	}
}