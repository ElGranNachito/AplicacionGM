using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Controlador apra un <see cref="ModeloTiradaDeDaño"/>
	/// </summary>
	public class ControladorTiradaDaño : ControladorTiradaGenerico<ModeloTiradaDeDaño, ArgumentosTiradaDaño>
	{
		#region Constructor

		public ControladorTiradaDaño(ModeloTiradaDeDaño _modeloTiradaStat)
			: base(_modeloTiradaStat) { }

		#endregion

		#region Metodos

		/// <summary>
		/// Aplica el daño resultante de la ultima tirada a su objetivo
		/// </summary>
		/// <param name="subobjetivosDaño">Subobjetivos a los que aplicar el daño</param>
		[AccesibleEnGuraScratch(nameof(AplicarDaño))]
		public void AplicarDaño(SortedList<int, SubobjetivoDaño> subobjetivosDaño)
		{
			if(ArgsUltimaTirada is null)
				return;

			//Creamos una nueva entrada de historial
			var nuevaEntradaHistorialTirada = new ModeloHistorialTirada(ArgsUltimaTirada, Resultado);
			
			//Creamos los argumentos del daño
			var argsDaño = new ModeloArgumentosDaño
			{
				DañoTotal = Resultado.resultado,
				DañoFinal = Resultado.resultado,
				Tirada    = nuevaEntradaHistorialTirada,

				FuentesDeDañoAbarcadas = modeloGenerico.FuentesDeDañoAbarcadas,
				Rango                  = modeloGenerico.Rango,
				NivelMagia             = modeloGenerico.NivelMagia,
				TipoDeDaño             = modeloGenerico.TipoDeDaño
			};

			argsDaño.AñadirInfligidorDaño(ArgsUltimaTirada.personaje);
			argsDaño.AñadirInfligidorDaño(ArgsUltimaTirada.controlador as IInfligidorDaño);

			argsDaño.AñadirObjetivos(ArgsUltimaTirada.objetivo, subobjetivosDaño);

			modelo.Historial.Add(nuevaEntradaHistorialTirada);

			var infligidorDeDaño = ArgsUltimaTirada.controlador as IInfligidorDaño ?? ArgsUltimaTirada.personaje;
			
			infligidorDeDaño.InfligirDaño(ArgsUltimaTirada.objetivo, argsDaño, subobjetivosDaño);
		}

		#endregion
	}
}