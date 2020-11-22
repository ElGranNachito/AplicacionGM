using AppGM.Core;

namespace AppGM
{
    public class ViewModelMensajeCrearRol_DatosRol : BaseViewModel
    {
        public ModeloRol ModeloRol { get; set; }
        public ViewModelMensajeCrearRol_DatosRol(ModeloRol _modeloRol)
        {
            ModeloRol = _modeloRol;
        }
    }
}
