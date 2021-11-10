using System;
using System.Collections.Generic;
using System.Linq;
using AppGM.Core;

using Xunit;

namespace AppGM.Tests
{
	public class TestsEnumHelpers
	{
		[Fact]
		public static void PruebaObtenerValoresDeDeteccionDeDañoDisponibles()
		{
			EEstrategiaDeDeteccionDeDaño estrategiaDeteccionDeDaño = EEstrategiaDeDeteccionDeDaño.Nivel;

			var tiposDisponibles = estrategiaDeteccionDeDaño.ObtenerValoresDeDeteccionDeDañoDisponibles();

			ComprobarValoresIguales(tiposDisponibles.Cast<ENivelMagia>().ToList(), EnumHelpers.NivelesDeMagiaDisponibles);

			estrategiaDeteccionDeDaño = EEstrategiaDeDeteccionDeDaño.Rango;

			tiposDisponibles = estrategiaDeteccionDeDaño.ObtenerValoresDeDeteccionDeDañoDisponibles();

			ComprobarValoresIguales(tiposDisponibles.Cast<ERango>().ToList(), EnumHelpers.RangosDisponibles);

			estrategiaDeteccionDeDaño = EEstrategiaDeDeteccionDeDaño.TipoDeDaño;

			tiposDisponibles = estrategiaDeteccionDeDaño.ObtenerValoresDeDeteccionDeDañoDisponibles();

			ComprobarValoresIguales(tiposDisponibles.Cast<ETipoDeDaño>().ToList(), EnumHelpers.TiposDeDañoDisponibles);
		}

		private static void ComprobarValoresIguales<TValor>(List<TValor> coleccionA, List<TValor> coleccionB)
			where TValor : struct, Enum
		{
			Assert.Equal(coleccionA.Count, coleccionB.Count);

			for (int i = 0; i < coleccionA.Count; ++i)
			{
				Assert.Equal(coleccionA[i], coleccionB[i]);
			}
		}
	}
}
