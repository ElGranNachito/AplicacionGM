namespace AppGM.Core
{
	public class ArgumentosTirada
	{
		public int multiplicadorEspecialidad;

		public int modificador;

		public EStat stat;
	}

	public class ArgumentosTiradaPersonalizada : ArgumentosTirada
	{
		public string argumentoExtra;

		public ControladorPersonaje controlador;
	}

	public class ArgumentosTiradaDaño : ArgumentosTiradaPersonalizada
	{
		public float multiplicador;

		public EManoUtilizada manoUtilizada;

		public IDañable objetivo;
	}
}