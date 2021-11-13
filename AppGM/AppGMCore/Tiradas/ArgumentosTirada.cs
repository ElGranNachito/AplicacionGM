namespace AppGM.Core
{
	public class ArgumentosTirada
	{
		public int multiplicadorEspecialidad;
		public int modificador;
	}

	public class ArgumentosTiradaPersonalizada : ArgumentosTirada
	{
		public string parametroExtra;

		public ControladorPersonaje controlador;

		public EStat stat;
	}

	public class ArgumentosTiradaDaño : ArgumentosTiradaPersonalizada
	{
		public float multiplicador;

		public EManoUtilizada manoUtilizada;
	}
}