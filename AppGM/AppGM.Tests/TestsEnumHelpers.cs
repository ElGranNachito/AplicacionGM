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

			ComprobarValoresIguales(tiposDisponibles.Cast<ERangoFlags>().ToList(), EnumHelpers.ObtenerValoresEnum<ERangoFlags>());

			estrategiaDeteccionDeDaño = EEstrategiaDeDeteccionDeDaño.TipoDeDaño;

			tiposDisponibles = estrategiaDeteccionDeDaño.ObtenerValoresDeDeteccionDeDañoDisponibles();

			ComprobarValoresIguales(tiposDisponibles.Cast<ETipoDeDaño>().ToList(), EnumHelpers.TiposDeDañoDisponibles);
		}

		[Fact]
		public static void PruebaModificadoresRangoCorrectos()
		{
			ERango rango = ERango.A;

			Assert.True(rango.ObtenerModificador() == 4);

			rango = ERango.B;

			Assert.True(rango.ObtenerModificador() == 4);

			rango = ERango.C;

			Assert.True(rango.ObtenerModificador() == 3);

			rango = ERango.Ex;

			Assert.True(rango.ObtenerModificador() == 6);
		}

		[Fact]
		public static void PruebaTieneFlagRango()
		{
			ERangoFlags flags = ERangoFlags.C | ERangoFlags.A | ERangoFlags.D;

			Assert.True(flags.TieneFlagRango(ERango.A));
			Assert.True(flags.TieneFlagRango(ERango.C));
			Assert.True(flags.TieneFlagRango(ERango.D));

			flags ^= ERangoFlags.C;

			Assert.True(!flags.TieneFlagRango(ERango.C));
		}

		[Fact]
		public static void PruebaTieneFlagMagia()
		{
			ENivelMagiaFlags flags = ENivelMagiaFlags.Uno | ENivelMagiaFlags.Tres | ENivelMagiaFlags.Ocho;

			Assert.True(flags.TieneFlagMagia(ENivelMagia.Uno));
			Assert.True(flags.TieneFlagMagia(ENivelMagia.Tres));
			Assert.True(flags.TieneFlagMagia(ENivelMagia.Ocho));

			flags ^= ENivelMagiaFlags.Tres;

			Assert.True(!flags.TieneFlagMagia(ENivelMagia.Tres));
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
