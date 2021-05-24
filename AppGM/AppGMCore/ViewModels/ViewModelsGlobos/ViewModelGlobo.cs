namespace AppGM.Core
{
    public class ViewModelGlobo<TipoViewModel> : BaseViewModel
        where TipoViewModel: BaseViewModel
    {
        #region Propiedades

        /// <summary>
        /// View model del contenido del globo
        /// </summary>
        public TipoViewModel ViewModelContenido { get; set; }

        /// <summary>
        /// ¿Es visible el globo?
        /// </summary>
        public bool GloboVisible { get; set; }

        /// <summary>
        /// ¿Es visible la cola del globo?
        /// </summary>
        public bool ColaGloboVisible { get; set; }

        /// <summary>
        /// Color del fondo del globo
        /// </summary>
        public string ColorFondo { get; set; } = "FFFFFF"; 
        #endregion
    }
}
