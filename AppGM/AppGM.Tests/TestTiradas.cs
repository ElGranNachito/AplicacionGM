using System;
using AppGM.Core;
using Xunit;

namespace AppGM.Tests
{
	public class TestTiradas
	{
		[Theory]
		[InlineData(5, 2, 500)]
		[InlineData(1, 1, 500)]
		[InlineData(3, 80, 1000)]
		public void Prueba_TiradasDentroDeSusLimites(int numeroDados, int numeroCaras, int numeroRepeticiones)
		{
			//Preparacion
			int minimo = 1 * numeroDados;
			int maximo = numeroCaras * numeroDados;
			
			//Testeo
			for (int i = 0; i < numeroRepeticiones; ++i)
			{
				Assert.InRange(ParserTiradas.RealizarTirada(numeroDados, numeroCaras).resultado, minimo, maximo);
			}
		}

		[Theory]
		[InlineData(0, 0, 5000)]
		[InlineData(1, 0, 5000)]
		[InlineData(0, 5, 5000)]
		[InlineData(6, 1, 5000)]
		public void Prueba_3d6DentroDeSusLimites(int especialidad, int mod, int numeroRepeticiones)
		{
			//Preparacion
			int minimo = 3 + mod - especialidad * Core.Constantes.BonoEspecialidad;
			int maximo = minimo + 15;

			ArgumentosTirada argsTirada = new ArgumentosTirada
			{
				modificador = mod,
				multiplicadorEspecialidad = especialidad
			};

			//Testeo
			for (int i = 0; i < numeroRepeticiones; ++i)
			{
				Assert.InRange(ParserTiradas.RealizarTiradaStat(argsTirada).resultado, minimo, maximo);
			}
		}

		[Theory]
		[InlineData("1d1*1d1+1d1", 0, 0, 1, EStat.AGI, EManoUtilizada.Dominante, 10, 0, 2, 100)]
		[InlineData("1d1*2*3", 1, 0, 1, EStat.AGI, EManoUtilizada.Dominante, 10, 0, 6, 200)]
		[InlineData("1d1+3", 0, 2, 1, EStat.AGI, EManoUtilizada.Dominante, 10, 0, 4, 200)]
		[InlineData("1d1+2-2", 0, 2, 1.5, EStat.AGI, EManoUtilizada.Dominante, 10, 0, 1, 500)]
		[InlineData("1d1+2*5", 2, 2, 1.5, EStat.AGI, EManoUtilizada.Dominante, 10, 0, 15, 500)]
		[InlineData("1d1+2*5", 2, 2, 1.5, EStat.AGI, EManoUtilizada.NoDominante, 10, 0, 15, 500)]
		[InlineData("1d1", 2, 2, 1.5, EStat.AGI, EManoUtilizada.NoDominante, 15, 0, 1, 500)]
		[InlineData("1d1+@ParametroExtra", 2, 2, 1, EStat.AGI, EManoUtilizada.AmbasManos, 12, 0, 1, 1000, "25")]
		[InlineData("1d1", 2, 2, 2, EStat.AGI, EManoUtilizada.AmbasManos, 12, 2, 1, 1000)]
		[InlineData("1d1", 2, 2, 2, EStat.AGI, EManoUtilizada.Dominante, 12, 2, 1, 1000)]
		public async void Prueba_TiradaVariableDañoConResultadoConocido(
			string tirada, 
			int especialidad,
			int mod,
			float multiplicador, 
			EStat stat,
			EManoUtilizada manoUtilizada,
			int valorStat,
			int bonoStat,
			int resultadoTiradas,
			int repeticiones,
			string parametroExtra ="0")
		{
			//Preparacion
			ModeloPersonaje pj = new ModeloPersonaje
			{
				Nombre = "LePibe"
			};

			ControladorPersonaje controlador = new ControladorPersonaje(pj);

			controlador.EstablecerValorStat(stat, valorStat);
			controlador.EstablecerValorBonoStat(stat, bonoStat);

			int resultadoEsperado = (int) Math.Floor((resultadoTiradas + int.Parse(parametroExtra) + mod + especialidad * Core.Constantes.BonoEspecialidad + Math.Floor(controlador.ObtenerModificadorStat(stat) * Helpers.Juego.ObtenerMultiplicadorManoUsada(manoUtilizada))) * multiplicador);

			var resultado = await ParserTiradas.TryParseAsync(tirada, controlador.modelo, ETipoTirada.Daño, stat);

			ArgumentosTiradaDaño args = new ArgumentosTiradaDaño
			{
				controlador = controlador,
				multiplicadorEspecialidad = especialidad,
				modificador = mod,
				multiplicador = multiplicador,
				stat = stat,
				manoUtilizada = manoUtilizada,
				parametroExtra = parametroExtra
			};

			//Prueba
			for (int i = 0; i < repeticiones; ++i)
			{
				Assert.Equal(resultadoEsperado, resultado.funcion(args).resultado);
			}
		}

		[Theory]
		[InlineData("1d5*1d10+1d15", 0, 0,          1,   EStat.AGI, EManoUtilizada.Dominante,   10, 0, 3,  20,  3000)]
		[InlineData("2d20*2*3", 1, 0,               1,   EStat.AGI, EManoUtilizada.Dominante,   10, 0, 12, 120, 2000)]
		[InlineData("1d6+8", 2, 2,                  2,   EStat.AGI, EManoUtilizada.Dominante,   12, 2, 9,  14,  2000)]
		[InlineData("2d2+3", 0, 2,                  1,   EStat.AGI, EManoUtilizada.Dominante,   10, 0, 5,  7,   2000)]
		[InlineData("1d100+2-2", 0, 2,              1.5, EStat.AGI, EManoUtilizada.Dominante,   10, 0, 1,  100, 2000)]
		[InlineData("1d5+2*5", 2, 2,				1.5, EStat.AGI, EManoUtilizada.Dominante,   10, 0, 11, 15,  2000)]
		[InlineData("8d8+2*5", 2, 2,				1.5, EStat.AGI, EManoUtilizada.NoDominante, 10, 0, 18, 74,  2000)]
		[InlineData("1d5*1d5", 2, 2,				1.5, EStat.AGI, EManoUtilizada.NoDominante, 15, 0, 1,  25,  2000)]
		[InlineData("1d6", 2, 2,                    2,   EStat.AGI, EManoUtilizada.AmbasManos,  12, 2, 1,  6,	2000)]
		[InlineData("1d6", 2, 2,					2,   EStat.AGI, EManoUtilizada.AmbasManos,  12, 2, 1,  6,	2000)]
		public async void Prueba_TiradaVariableDañoDentroDelRangoEsperado(
			string tirada,
			int especialidad,
			int mod,
			float multiplicador,
			EStat stat,
			EManoUtilizada manoUtilizada,
			int valorStat,
			int bonoStat,
			int resultadoMinimoTiradas,
			int resultadoMaximoTiradas,
			int repeticiones,
			string parametroExtra = "0")
		{
			/*//Preparacion
			ModeloPersonaje pj = new ModeloPersonaje
			{
				Nombre = "LePibe"
			};

			ControladorPersonaje controlador = new ControladorPersonaje(pj);

			controlador.EstablecerValorStat(stat, valorStat);
			controlador.EstablecerValorBonoStat(stat, bonoStat);

			int resultadoMinimo = (int)Math.Floor((resultadoMinimoTiradas 
			                                       + especialidad * Constantes.BonoEspecialidad 
			                                       + Math.Floor(controlador.ObtenerModificadorStat(stat) * Helpers.Juego.ObtenerMultiplicadorManoUsada(manoUtilizada))
			                                       + mod
												   + int.Parse(parametroExtra)) * multiplicador);

			int resultadoMaximo = (int)Math.Floor((resultadoMaximoTiradas + especialidad * Constantes.BonoEspecialidad + Math.Floor(controlador.ObtenerModificadorStat(stat) * Helpers.Juego.ObtenerMultiplicadorManoUsada(manoUtilizada)) + int.Parse(parametroExtra) + mod) * multiplicador);

			var resultado = await ParserTiradas.TryParseAsync(tirada, controlador, ETipoTirada.Daño, stat);

			ArgumentosTiradaDaño args = new ArgumentosTiradaDaño
			{
				controlador = controlador,
				multiplicadorEspecialidad = especialidad,
				modificador = mod,
				multiplicador = multiplicador,
				stat = stat,
				manoUtilizada = manoUtilizada,
				parametroExtra = parametroExtra
			};

			CoolLogs.Globales.Inicializar<CoolFactory>(ESeveridad.TODOS, "%t-T [%a>%f:%l %s-u]: %m", "LoggerPrincipal", "log");
			CoolLogs.Globales.LoggerGlobal.Log(resultadoMinimoTiradas.ToString());

			//Prueba
			for (int i = 0; i < repeticiones; ++i)
			{
				Assert.InRange(resultado.funcion(args).resultado, resultadoMinimo, resultadoMaximo);
			}*/
		}
	}
}
