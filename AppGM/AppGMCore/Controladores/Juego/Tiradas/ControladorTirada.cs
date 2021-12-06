namespace AppGM.Core
{
	/// <summary>
	/// Controlador para <see cref="ModeloTiradaBase"/>
	/// </summary>
	public sealed class ControladorTirada : ControladorTiradaGenerico<ModeloTiradaBase, ArgumentosTiradaPersonalizada>
    {
	    #region Constructor

		public ControladorTirada(ModeloTiradaBase _modelo)
			: base(_modelo) {} 

		#endregion
    }
}