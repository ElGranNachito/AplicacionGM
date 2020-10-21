using System.ComponentModel;
using System.Linq;

namespace AppGM.Core
{
    public class ViewModelIngresoPosicion : BaseViewModel
    {
        #region Miembros

        public ViewModelMapa mapa;

        #endregion

        #region Propiedades
        public ViewModelVector2 Posicion { get; set; }
        public string TextoPosicionX
        {
            get => Posicion.X == 0 ? "" : Posicion.X.Round(1).ToString();
            set
            {
                if (mapa == null)
                    return;

                double tmp;

                if (double.TryParse(value, out tmp))
                {
                    if (tmp < 0)
                        return;

                    if (tmp < mapa.TamañoCanvasX)
                        Posicion.X = tmp;
                    else
                        Posicion.X = mapa.TamañoCanvasX;

                    return;
                }

                if (value.EstaVacio())
                    Posicion.X = 0;
            }
        }
        public string TextoPosicionY
        {
            get => Posicion.Y.Round(1).ToString();
            set
            {
                if (mapa == null)
                    return;

                double tmp;

                if (double.TryParse(value, out tmp))
                {
                    if (tmp < 0)
                        return;

                    if (tmp < mapa.TamañoCanvasY)
                        Posicion.Y = tmp;
                    else
                        Posicion.Y = mapa.TamañoCanvasY;

                    return;
                }

                if (value.EstaVacio())
                    Posicion.X = 0;
            }
        }
        #endregion

        #region Constructores
        public ViewModelIngresoPosicion(ViewModelMapa _mapa, Vector2 _posicionInicial)
        {
            mapa = _mapa;

            Posicion = new ViewModelVector2(_posicionInicial);
        }

        public ViewModelIngresoPosicion(ViewModelMapa _mapa)
        {
            mapa = _mapa;

            Posicion = new ViewModelVector2(new Vector2(0, 0));
        }
        #endregion
    }
}
