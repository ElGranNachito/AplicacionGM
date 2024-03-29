﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
	public partial class ModeloArgumentosDaño
	{
		[NotMapped] 
		public bool FuenteEsMagia => NivelMagia != ENivelMagia.NINGUNO;

		/// <summary>
		/// Añade un <see cref="IInfligidorDaño"/> a los <see cref="InfligidoresDaño"/> de este modelo
		/// </summary>
		/// <param name="infligidor"><see cref="IInfligidorDaño"/> que añadir</param>
		public void AñadirInfligidorDaño(
			IInfligidorDaño infligidor)
		{
			if(infligidor is null)
				SistemaPrincipal.LoggerGlobal.LogCrash($"{nameof(infligidor)} no puede ser null");

			InfligidoresDaño.Add(new ModeloInfligidorDaño(
				this,
				(infligidor as ControladorPersonaje)?.modelo,
				(infligidor as ControladorHabilidad)?.modelo,
				(infligidor as ControladorItem)?.modelo));
		}

		/// <summary>
		/// Añade el <paramref name="objetivoPrincipal"/> y <paramref name="subobjetivos"/> a los <see cref="Objetivos"/>
		/// </summary>
		/// <param name="objetivoPrincipal">Objetivo principal del daño</param>
		/// <param name="subobjetivos">Subobjetivos del daño</param>
		public void AñadirObjetivos(
			IDañable objetivoPrincipal,
			SortedList<int, SubobjetivoDaño> subobjetivos)
		{
			if(objetivoPrincipal is null)
				SistemaPrincipal.LoggerGlobal.LogCrash($"{nameof(objetivoPrincipal)} no puede ser null");

			Objetivos.Add(new ModeloDañable
			{
				ArgumentosDaño = this,

				Personaje = (objetivoPrincipal as ControladorPersonaje)?.modelo,

				Indice = subobjetivos?.Count ?? 0
			});

			if(subobjetivos is null)
				return;

			//Añadimos los objetivos del daño
			foreach (var objetivo in subobjetivos)
			{
				var slotActual = (objetivo.Value.objetivo as ControladorSlot)?.modelo;

				if (slotActual is null)
					SistemaPrincipal.LoggerGlobal.LogCrash($"Todos los subobjetivos deben ser un slot");

				Objetivos.Add(new ModeloDañable
				{
					ArgumentosDaño = this,

					Slot = slotActual,

					Indice = objetivo.Key
				});

				if (objetivo.Value.dañarContenido || objetivo.Value.profundidadMaxima >= 0)
				{
					var itemsSlot = slotActual.ObtenerItems(objetivo.Value.dañarContenido, objetivo.Value.profundidadMaxima);
					var partesDelCuerpoSlot = slotActual.ObtenerPartesDelCuerpo(objetivo.Value.dañarContenido, objetivo.Value.profundidadMaxima);

					foreach (var item in itemsSlot)
					{
						Objetivos.Add(new ModeloDañable
						{
							ArgumentosDaño = this,

							Item = item,

							Indice = objetivo.Key
						});
					}

					foreach (var parteDelCuerpo in partesDelCuerpoSlot)
					{
						Objetivos.Add(new ModeloDañable
						{
							ArgumentosDaño = this,

							ParteDelCuerpo = parteDelCuerpo,

							Indice = objetivo.Key
						});
					}
				}
			}
		}
	}
}
