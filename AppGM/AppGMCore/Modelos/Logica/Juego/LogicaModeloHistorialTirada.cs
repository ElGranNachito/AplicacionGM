namespace AppGM.Core
{
	/// <summary>
	/// Clase que contiene la logica para <see cref="ModeloHistorialTirada"/>
	/// </summary>
	public partial class ModeloHistorialTirada
	{
		/// <summary>
		/// Constructor por defecto
		/// </summary>
		public ModeloHistorialTirada() {}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="argumentosTirada">Argumentos de la tirada</param>
		/// <param name="resultadoTirada">Resultado de la tirada</param>
		public ModeloHistorialTirada(ArgumentosTirada argumentosTirada, ResultadoTirada resultadoTirada)
		{
			Stat                      = argumentosTirada.stat;
			Modificador               = argumentosTirada.modificador;
			MultiplicadorEspecialidad = argumentosTirada.multiplicadorEspecialidad;

			Resultado          = resultadoTirada.resultado;
			ResultadoDetallado = resultadoTirada.resultadoDetallado;

			switch (argumentosTirada)
			{
				case ArgumentosTiradaDaño argsTiradaDaño:
				{
					TipoTirada = ETipoTirada.Daño;

					Mutliplicador	= argsTiradaDaño.multiplicador;
					ManoUtilizada	= argsTiradaDaño.manoUtilizada;
					ArgumentoExtra	= argsTiradaDaño.argumentoExtra;

					break;
				}

				case ArgumentosTiradaPersonalizada argsTiradaPersonalizada:
				{
					TipoTirada = ETipoTirada.Personalizada;

					ArgumentoExtra = argsTiradaPersonalizada.argumentoExtra;

					break;
				}
			}
		}
	}
}
