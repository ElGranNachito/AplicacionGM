using AppGM.Core;

namespace AppGM
{
    public class ViewModelMensajeCrearRol_DatosRol : ViewModelPaso<ViewModelMensajeCrearRol>
    {
        #region Propiedades

        private ModeloRol mModeloRol; 

        #endregion

        #region Propiedades
        public string NombreRol { get; set; }
        public string DescripcionRol { get; set; }

        #endregion

        #region Constructor
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
