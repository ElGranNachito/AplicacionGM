using System.Linq;

namespace AppGM.Core
{
    public class ViewModelMapaPrincipal : ViewModelMapa
    {
        public ViewModelIngresoPosicion PosicionIglesia { get; set; }

        public ViewModelMapaPrincipal()
        {
            if (mControladorMapa.modelo.PosicionesElementos.Count != 0)
            {
                PosicionIglesia = new ViewModelIngresoPosicion(
                    this,
                    new Vector2(mControladorMapa.modelo.PosicionesElementos.First().Posicion.X, mControladorMapa.modelo.PosicionesElementos.First().Posicion.Y));
            }
        }
    }
}
