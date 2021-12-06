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
	}
}