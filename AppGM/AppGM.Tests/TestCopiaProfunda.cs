using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using AppGM.Core;

namespace AppGM.Tests
{
	/// <summary>
	/// Tests de la copia profunda de modelos
	/// </summary>
	public class TestCopiaProfunda
	{
		/// <summary>
		/// Crea un modelo de personaje para utilizar dentro de las pruebas
		/// </summary>
		/// <returns><see cref="ModeloPersonaje"/></returns>
		private static ModeloPersonaje CrearPersonajeDePrueba()
		{
			var pj = new ModeloPersonaje
			{
				Nombre = "Nachito",

				MaxHp = 20,
				Hp = 20,
				Str = 15,
				Agi = 10,
				End = 15,
				Int = 10,
				Lck = 15,

				Alianzas = new List<ModeloAlianza>
				{
					new ModeloAlianza
					{
						Id = 5,
						Descripcion = "Descripcion",
						Nombre = "Alianza de choripaneros",
						EIconoAlianza = EIconoAlianza.Team_Hetero
					}
				},
				Especialidades = new List<ModeloEspecialidad>
				{
					new ModeloEspecialidad
					{
						Id = 1,
						Nombre = "nada"
					}
				}
			};

			pj.Alianzas[0].PersonajesAfectados.Add(pj);

			return pj;
		}

		/// <summary>
		/// Comprueba que los valores copiados al nuevo modelo sean iguales al original
		/// </summary>
		[Fact]
		public void ValoresSonIgualesEnNuevoModelo()
		{
			var pj = CrearPersonajeDePrueba();

			var copia = pj.CrearCopiaProfundaEnSubtipo<ModeloPersonaje, ModeloPersonaje>();

			Assert.True(pj.Nombre == copia.resultado.Nombre, "Nombre de los personajes no coinciden");
			Assert.True(copia.resultado.Alianzas.Count > 0, "No se copiaron alianzas");
			Assert.True(pj.Alianzas.First().Nombre == copia.resultado.Alianzas.First().Nombre, "Nombres de las alianzas no coinciden");
			Assert.True(pj.Especialidades.First().Nombre == copia.resultado.Especialidades.First().Nombre, "Nombres de las especialidades no coinciden");
		}

		/// <summary>
		/// Comprueba que se hayan creado nuevas instancias de cada modelo dentro del modelo original
		/// </summary>
		[Fact]
		public void ReferenciasDeModelosSonDistintas()
		{
			var pj = CrearPersonajeDePrueba();

			var copia = pj.CrearCopiaProfundaEnSubtipo<ModeloPersonaje, ModeloPersonaje>();

			Assert.True(pj.Especialidades != copia.resultado.Especialidades, "Las listas de especialidades no son referencias distintas");

			copia.resultado.Alianzas.First().Nombre = "Alianza poronga";

			Assert.True(pj.Alianzas.First().Nombre != copia.resultado.Alianzas.First().Nombre, "Nombre de la alianza original fue modificado junto con el de la copia");
		}

		[Fact]
		public void ReferenciaDeModelosReptedosEsLaMisma()
		{
			var pj = CrearPersonajeDePrueba();

			var copia = pj.CrearCopiaProfundaEnSubtipo<ModeloPersonaje, ModeloPersonaje>();

			Assert.True(copia.resultado.Alianzas[0].PersonajesAfectados[0] == copia.resultado);
		}

		[Fact]
		public async void PruebaVersionAsincronica()
		{
			var pj = CrearPersonajeDePrueba();

			var copia = (await pj.CrearCopiaProfundaEnSubtipoAsync<ModeloPersonaje, ModeloPersonaje>()).resultado;

			Assert.True(pj.Nombre == copia.Nombre, "Nombres no son iguales");

			pj.Nombre = "nadie";

			Assert.True(pj.Nombre != copia.Nombre, "Nombre fue modificado en ambos modelos");
			Assert.True(copia.Alianzas[0].PersonajesAfectados[0] == copia, "Se generaron dos copias del modelo");
		}

		[Fact]
		public async void PruebaCopiaAModeloExistente()
		{
			var pj  = CrearPersonajeDePrueba();
			var pj2 = CrearPersonajeDePrueba();

			pj2.Nombre = "Pancho";
			pj2.Alianzas.Clear();

			await pj.CrearCopiaProfundaEnSubtipoAsync<ModeloPersonaje, ModeloPersonaje>(pj2);

			Assert.True(pj.Nombre == pj2.Nombre, "Los nombre no coinciden");
			Assert.True(pj2.Alianzas[0].PersonajesAfectados[0].Nombre == pj.Nombre, "El personaje de la alianza no es el pj1");
		}

		[Fact]
		public void PruebaModelosEliminados()
		{
			var pj = CrearPersonajeDePrueba();

			var alianza = pj.Alianzas[0];

			var copiaPj = pj.CrearCopiaProfundaEnSubtipo<ModeloPersonaje, ModeloPersonaje>();

			copiaPj.resultado.Alianzas.Clear();

			var resultadoCopia = copiaPj.resultado.CrearCopiaProfundaEnSubtipo(pj.GetType(), pj, null, null);

			var modelosEliminados = resultadoCopia.modelosCreadosEliminados.ModelosEliminados;

			Assert.True(modelosEliminados.Count > 0, "No se eliminaron modelos");
			Assert.Contains(modelosEliminados, m => m == alianza);
		}

		[Fact]
		public void PruebaModelosAñadidos()
		{
			var pj = CrearPersonajeDePrueba();

			var nuevoContrato = new ModeloContrato
			{
				Id = 1,

				Nombre = "Contrato piola",
				Descripcion = "Contrato entre pibes piola"
			};

			var nuevaAlianza = new ModeloAlianza 
			{ 
				ContratoDeAlianza = nuevoContrato,
				Id = 1,
				
				Nombre = "Cooler alianza",
				Descripcion = "Cooler"
			};

			var resultadoCopia =  pj.CrearCopiaProfundaEnSubtipo<ModeloPersonaje, ModeloPersonaje>();

			var copiaPj = resultadoCopia.resultado;

			copiaPj.Alianzas.Add(nuevaAlianza);
			copiaPj.Contratos.Add(nuevoContrato);

			var resultadoSegundaCopia = copiaPj.CrearCopiaProfundaEnSubtipo<ModeloPersonaje, ModeloPersonaje>(pj, null, null);

			Assert.Contains(resultadoSegundaCopia.modelosCreadosEliminados.ModelosCreados, m => m is ModeloContrato && m.guid == nuevoContrato.guid);
			Assert.Contains(resultadoSegundaCopia.modelosCreadosEliminados.ModelosCreados, m => m is ModeloAlianza && m.guid == nuevaAlianza.guid);
			Assert.True(resultadoSegundaCopia.modelosCreadosEliminados.ModelosCreados.Count == 2);
		}

		[Fact]
		public void PruebaReemplazoDeReferencias()
		{
			var pj = CrearPersonajeDePrueba();
			var copiaPj = pj.CrearCopiaProfundaEnSubtipo<ModeloPersonaje, ModeloPersonaje>();

			var nuevoContrato = new ModeloContrato
			{
				Id = 1,

				PersonajesAfectados = new List<ModeloPersonaje>{copiaPj.resultado},

				Nombre = "Contrato piola",
				Descripcion = "Contrato entre pibes piola"
			};

			var nuevaAlianza = new ModeloAlianza
			{
				ContratoDeAlianza = nuevoContrato,
				Id = 1,
				
				PersonajesAfectados = new List<ModeloPersonaje>{copiaPj.resultado},

				Nombre = "Cooler alianza",
				Descripcion = "Cooler"
			};

			copiaPj.resultado.Alianzas.Add(nuevaAlianza);
			copiaPj.resultado.Contratos.Add(nuevoContrato);

			copiaPj.resultado.CrearCopiaProfundaEnSubtipo<ModeloPersonaje, ModeloPersonaje>(pj, null, null);

			Assert.True(pj.Contratos[0].PersonajesAfectados[0] == pj, "No se reemplazo la referencia del personaje");
		}

		[Fact]
		public async void PruebaAtributoCopiarSuperficialmente()
		{
			var pj = CrearPersonajeDePrueba();

			pj.Rol = new ModeloRol
			{
				Nombre = "Rolsito",
				ClimaHorarioGlobal = new ModeloClimaHorario
				{
					Clima = EClima.Granizo,
					DiaSemana = EDiaSemana.Lunes,
					Humedad = EHumedad.MuchaHumedad,
					Temperatura = ETemperatura.Frio
				}
			};

			var copia = (await pj.CrearCopiaProfundaEnSubtipoAsync<ModeloPersonaje, ModeloPersonaje>()).resultado;

			Assert.Equal(pj.Rol, copia.Rol);
		}
	}
}