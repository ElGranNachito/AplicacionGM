using System;
using System.Globalization;
using System.Threading.Tasks;

namespace AppGM.Core
{
	/// <summary>
	/// Controlador de tirada base
	/// </summary>
	public abstract class ControladorTiradaBase : Controlador<ModeloTiradaBase>
	{
		#region Propiedades

		/// <summary>
		/// Resultado de la ultima tirada
		/// </summary>
		[AccesibleEnGuraScratch(nameof(Resultado))]
		public ResultadoTirada Resultado { get; protected set; }

		/// <summary>
		/// Ultimo personaje que realizo esta tirada
		/// </summary>
		[AccesibleEnGuraScratch(nameof(UltimoUsuario))]
		public ModeloConVariablesYTiradas UltimoUsuario { get; protected set; }
		#endregion

		#region Constructor

		public ControladorTiradaBase(ModeloTiradaBase _modelo)
			:base(_modelo) {} 

		#endregion

		#region Metodos

		/// <summary>
		/// Realiza la tirada
		/// </summary>
		/// <param name="args">Argumentos con los que realizar la tirada</param>
		/// <returns>Resultado de la tirada</returns>
		[AccesibleEnGuraScratch(nameof(RealizarTiradaAsync))]
		public abstract ResultadoTirada RealizarTirada(ArgumentosTirada args);

		/// <summary>
		/// Reliza la tirada asincronicamente
		/// </summary>
		/// <param name="args">Argumentos con los que realizar la tirada</param>
		public abstract Task<ResultadoTirada> RealizarTiradaAsync(ArgumentosTiradaPersonalizada args);

		/// <summary>
		/// Intenta parsear la tirada
		/// </summary>
		/// <param name="usuario">Modelo que realiza esta tirada</param>
		/// <returns><see cref="bool"/> indicando si se pudo parsear la tirada</returns>
		[AccesibleEnGuraScratch(nameof(TryParse))]
		public abstract bool TryParse(ModeloConVariablesYTiradas usuario);

		/// <summary>
		/// Intenta parsear la tirada asincronicamente
		/// </summary>
		/// <param name="usuario">Modelo que realiza esta tirada</param>
		/// <returns><see cref="bool"/> indicando si se pudo parsear la tirada</returns>
		public abstract Task<bool> TryParseAsync(ModeloConVariablesYTiradas usuario);

		/// <summary>
		/// Averigua si un <paramref name="modeloContenedor"/> puede realizar esta tirada
		/// </summary>
		/// <param name="modeloContenedor">Modelo que contiene la tirada</param>
		/// <returns><see cref="bool"/> indicando si un <paramref name="modeloContenedor"/> puede realizar esta tirada</returns>
		public bool PuedeRealizarTirada(ModeloConVariablesYTiradas modeloContenedor)
		{
			var resultadoVerificacion = ParserTiradas.VerificarValidezTirada(modelo.Tirada, modeloContenedor.ObtenerVariablesDisponibles(), modelo.TipoTirada, EStat.STR);

			return resultadoVerificacion.esValida;
		}

		/// <summary>
		/// Averigua si un <paramref name="modeloContenedor"/> puede realizar esta tirada
		/// </summary>
		/// <param name="modeloContenedor">Modelo que contiene la tirada</param>
		/// <param name="motivo">En caso de que no se pueda realizar la tirada, el motivo se indicara por esta parametro</param>
		/// <returns><see cref="bool"/> indicando si un <paramref name="modeloContenedor"/> puede realizar esta tirada</returns>
		public bool PuedeRealizarTirada(ModeloConVariablesYTiradas modeloContenedor, out string motivo)
		{
			var resultadoVerificacion = ParserTiradas.VerificarValidezTirada(modelo.Tirada, modeloContenedor.ObtenerVariablesDisponibles(), modelo.TipoTirada, EStat.STR);

			motivo = resultadoVerificacion.error;

			return resultadoVerificacion.esValida;
		}

		public override ViewModelItemListaBase CrearViewModelItem()
		{
			return new ViewModelTiradaItem(this);
		}

		/// <summary>
		/// Crea el <see cref="AppGM.Core.ControladorTiradaBase"/> correspondiente para el <paramref name="modeloTirada"/> especificado
		/// </summary>
		/// <param name="modeloTirada">Modelo de la tirada para la que se quiere crear el controlador</param>
		/// <returns>Controlador para <paramref name="modeloTirada"/></returns>
		public static ControladorTiradaBase CrearControladorDeTiradaCorrespondiente(ModeloTiradaBase modeloTirada)
		{
			if (modeloTirada is ModeloTiradaDeDaño tiradaDaño)
				return new ControladorTiradaDaño(tiradaDaño);

			return new ControladorTirada(modeloTirada);
		}

		#endregion
	}
}
