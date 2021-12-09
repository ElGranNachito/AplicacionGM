using System;
using System.Linq;

using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Controlador para el <see cref="ModeloDatosDefensa"/>
	/// </summary>
	public class ControladorDefensa : Controlador<ModeloDatosDefensa>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_datosDefensa"><see cref="ModeloDatosDefensa"/> representado por este controlador</param>
		public ControladorDefensa(ModeloDatosDefensa _datosDefensa)
			:base(_datosDefensa){}

		/// <summary>
		/// Reduce un <see cref="ModeloArgumentosDaño"/> utilizando los <see cref="ModeloDatosReduccionDeDaño"/> contenido que sean aplicables
		/// </summary>
		/// <param name="argsDaño"><see cref="ModeloArgumentosDaño"/> que reducir</param>
		public void ReducirDaño(ModeloArgumentosDaño argsDaño)
		{
			//Por cada reduccion de daño
			modelo.ReduccionesDeDaños.ForEach(reduccionActual =>
			{
				if(!reduccionActual.EstaHabilitada)
					return;

				switch (reduccionActual.EstrategiaDeDeteccionDeDaño)
				{
					case EEstrategiaDeDeteccionDeDaño.TipoDeDaño:
					{
						if(argsDaño.TipoDeDaño is null)
							return;

						if ((argsDaño.TipoDeDaño & reduccionActual.TiposDeDañoQueReduce) == 0)
							return;

						argsDaño.DañoFinal = ReducirDaño(argsDaño.DañoFinal, reduccionActual);

						break;
					}

					case EEstrategiaDeDeteccionDeDaño.Rango:
					{
						if (argsDaño.Rango is null)
							return;

						if (!reduccionActual.RangosDelDañoQueReduce.TieneFlagRango(argsDaño.Rango.Value))
							return;

						argsDaño.DañoFinal = ReducirDaño(argsDaño.DañoFinal, reduccionActual);

						break;
					}

					case EEstrategiaDeDeteccionDeDaño.Nivel:
					{
						if(argsDaño.NivelMagia is null)
							return;

						if (!reduccionActual.NivelesDeLasMagiasCuyosDañosReduce.TieneFlagMagia(argsDaño.NivelMagia.Value))
							return;

						argsDaño.DañoFinal = ReducirDaño(argsDaño.DañoFinal, reduccionActual);

						break;
					}

					case EEstrategiaDeDeteccionDeDaño.FuenteDelDaño:
					{ 
						if (argsDaño.FuentesDeDañoAbarcadas.Count <= 0)
							return;

						if (!reduccionActual.FuentesDeDañoQueReduce.Any(f => argsDaño.FuentesDeDañoAbarcadas.Contains(f)))
							return;

						argsDaño.DañoFinal = ReducirDaño(argsDaño.DañoFinal, reduccionActual);

						break;
					}
				}
			});
		}

		/// <summary>
		/// Activa o desactiva un <see cref="ModeloDatosReduccionDeDaño"/>
		/// </summary>
		/// <param name="nombreReduccion">Nombre del <see cref="ModeloDatosReduccionDeDaño"/></param>
		/// <param name="estaHabilitada">Nuevo estado de habilitacion</param>
		public void ToggleReduccionActiva(string nombreReduccion, bool estaHabilitada)
		{
			var reduccionEncontrada = modelo.ReduccionesDeDaños.Find(r => r.Nombre == nombreReduccion);

			reduccionEncontrada.EstaHabilitada = estaHabilitada;
		}

		/// <summary>
		/// Reduce el valor de un <paramref name="daño"/> utilizando una <paramref name="reduccion"/>
		/// </summary>
		/// <param name="daño">Valor del daño que reducir</param>
		/// <param name="reduccion">Reduccion que utilizar</param>
		/// <returns>Daño tras aplicar la reduccion</returns>
		private int ReducirDaño(int daño, ModeloDatosReduccionDeDaño reduccion)
		{
			if(reduccion is null)
				SistemaPrincipal.LoggerGlobal.LogCrash($"{nameof(reduccion)} no puede ser null");

			switch (reduccion.MetodoDeReduccionDeDaño)
			{
				case EMetodoDeReduccionDeDaño.Porcentual:
				{
					var multiplicador = reduccion.ValorReduccion / 100;

					return Convert.ToInt32(Math.Floor(daño * (1 - multiplicador)));
				}

				case EMetodoDeReduccionDeDaño.ReduccionCompleta:
				{
					return 0;
				}

				case EMetodoDeReduccionDeDaño.ValorFijo:
				{
					return Math.Clamp(daño - Convert.ToInt32(reduccion.ValorReduccion), 0, int.MaxValue);
				}

				default:
				{
					SistemaPrincipal.LoggerGlobal.Log($"{reduccion.MetodoDeReduccionDeDaño} no es valido o no esta actualmente soportado", ESeveridad.Error);
					
					return daño;
				}
			}
		}
	}
}