namespace AppGM.Core
{
    /// <summary>
    /// VM que representa el contenido de un globo
    /// </summary>
    /// <typeparam name="TipoViewModel">Tipo del <see cref="ViewModel"/> con informacion personalizada para este globo</typeparam>
    public class ViewModelGlobo<TipoViewModel> : ViewModel
        where TipoViewModel: ViewModel
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
        public string ColorFondo { get; set; } = "b4e1d6"; 

        #endregion
    }
}
