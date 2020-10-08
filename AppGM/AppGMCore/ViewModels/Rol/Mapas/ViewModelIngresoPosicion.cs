using System.ComponentModel;
using System.Linq;

namespace AppGM.Core
{
    public class ViewModelIngresoPosicion : BaseViewModel
    {
        public ViewModelMapa mapa; 
        public double PosicionX { get; set; }
        public double PosicionY { get; set; }
        public string TextoPosicionX
        {
            get => PosicionX == 0 ? "" : PosicionX.Round(1).ToString();
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
                        PosicionX = tmp;
                    else
                        PosicionX = mapa.TamañoCanvasX;

                    return;
                }

                if (value.Length == 0)
                    PosicionX = 0;
            }
        }
        public string TextoPosicionY
        {
            get => PosicionY.Round(1).ToString();
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
                        PosicionY = tmp;
                    else
                        PosicionY = mapa.TamañoCanvasY;

                    return;
                }

                if (value.Length == 0)
                    PosicionX = 0;
            }
        }
    }
}
