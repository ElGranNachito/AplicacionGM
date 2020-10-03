namespace AppGM.Core
{
    class ViewModelMapaPrincipal : BaseViewModel
    {
        public ViewModelIngresoPosicion PosicionIglesia { get; set; }

        public ViewModelMapaPrincipal()
        {
            PosicionIglesia = new ViewModelIngresoPosicion
            {
                TextoPosicionX = "50",
                TextoPosicionY = "150"
            };
        }
    }
}
