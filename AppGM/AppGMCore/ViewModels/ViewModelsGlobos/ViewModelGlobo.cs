namespace AppGM.Core
{
    public class ViewModelGlobo<TipoViewModel> : BaseViewModel
        where TipoViewModel: BaseViewModel
    {
        #region Propiedades
        public TipoViewModel ViewModelContenido { get; set; }
        public bool GloboVisible { get; set; }
        public bool ColaGloboVisible { get; set; }
        public string ColorFondo { get; set; } = "FFFFFF"; 
        #endregion
    }
}
