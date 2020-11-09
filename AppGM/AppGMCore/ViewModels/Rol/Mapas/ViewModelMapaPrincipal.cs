namespace AppGM.Core
{
    public class ViewModelMapaPrincipal : ViewModelMapa
    {
        public ViewModelIngresoPosicion PosicionIglesia { get; set; }

        public ViewModelMapaPrincipal(ControladorMapa _controlador) : base(_controlador){}
    }
}
