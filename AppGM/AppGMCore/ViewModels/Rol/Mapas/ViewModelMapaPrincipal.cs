namespace AppGM.Core
{
    /// <summary>
    /// VM que representa el mapa principal del rol, es decir la ciudad donde transcurre
    /// </summary>
    public class ViewModelMapaPrincipal : ViewModelMapa
    {
		#region Propiedades y Campos

        // Propiedades ---

		/// <summary>
		/// Posicion de la iglesia
		/// </summary>
		public ViewModelIngresoPosicion PosicionIglesia { get; set; }

        #endregion

		#region Constructor

		public ViewModelMapaPrincipal(ControladorMapa _controlador) : base(_controlador) { } 

		#endregion
	}
}