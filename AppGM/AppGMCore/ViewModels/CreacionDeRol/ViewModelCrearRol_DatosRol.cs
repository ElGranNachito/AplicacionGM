namespace AppGM.Core
{
    public class ViewModelCrearRol_DatosRol : ViewModelPaso<ViewModelCrearRol>
    {
        #region Campos & Propiedades

        //--------------------------------------CAMPOS----------------------------------------


        private ModeloRol mModeloRol;


        //------------------------------------PROPIEDADES-------------------------------------

        /// <summary>
        /// Nombre del rol
        /// </summary>
        public string NombreRol      { get; set; } = string.Empty;

        /// <summary>
        /// Descripcion del rol
        /// </summary>
        public string DescripcionRol { get; set; } = string.Empty;

        /// <summary>
        /// Texto que muestra los caracteres restantes
        /// </summary>
        public string TextoLetrasRestantes => 1000 - DescripcionRol.Length + "/1000";

		#endregion

		#region Constructores

		/// <summary>
		/// No usar para construir instancia de la clase!
		/// </summary>
		public ViewModelCrearRol_DatosRol() { }

		/// <summary>
		/// Constrcutor
		/// </summary>
		/// <param name="_modeloRol">Modelo del rol que estamos creando</param>
		public ViewModelCrearRol_DatosRol(ModeloRol _modeloRol)
		{
			mModeloRol = _modeloRol;
		} 

		#endregion

		#region Funciones

		public override void Desactivar(ViewModelCrearRol vm)
        {
            mModeloRol.Nombre = NombreRol;
            mModeloRol.Descripcion = DescripcionRol;
        }

        public override bool PuedeAvanzar() => !(string.IsNullOrEmpty(NombreRol) || string.IsNullOrEmpty(DescripcionRol));

        #endregion
    }
}
