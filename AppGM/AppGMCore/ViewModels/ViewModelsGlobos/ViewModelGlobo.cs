namespace AppGM.Core
{
    public class ViewModelGlobo : BaseViewModel
    {
        public BaseViewModel ViewModelContenido { get; set; }

        public bool GloboVisible { get; set; }
        public bool ColaGloboVisible { get; set; }
        public string ColorFondo { get; set; } = "FFFFFF";
    }
}
