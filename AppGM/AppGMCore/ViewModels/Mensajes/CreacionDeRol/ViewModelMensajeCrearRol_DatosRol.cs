using System;

namespace AppGM.Core
{
    public class ViewModelMensajeCrearRol_DatosRol : ViewModelPaso<ViewModelMensajeCrearRol>
    {
        #region Propiedades

        private ModeloRol mModeloRol; 

        #endregion

        #region Propiedades

        public string NombreRol      { get; set; } = string.Empty;
        public string DescripcionRol { get; set; } = string.Empty;
        public string TextoLetrasRestantes => 1000 - DescripcionRol.Length + "/1000";

        #endregion

        #region Constructor
        /// <summary>
        /// No usar para construir instancia de la clase!
        /// </summary>
        public ViewModelMensajeCrearRol_DatosRol(){}
        public ViewModelMensajeCrearRol_DatosRol(ModeloRol _modeloRol)
        {
            mModeloRol = _modeloRol;
        }

        #endregion

        #region Funciones

        public override void Desactivar(ViewModelMensajeCrearRol vm)
        {
            mModeloRol.Nombre = NombreRol;
            mModeloRol.Descripcion = DescripcionRol;
        }

        public override bool PuedeAvanzar() => !(string.IsNullOrEmpty(NombreRol) || string.IsNullOrEmpty(DescripcionRol));

        #endregion
    }
}
