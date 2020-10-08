using System.ComponentModel;

namespace AppGM.Core
{
    public class ViewModelMapa : BaseViewModel
    {
        #region Propiedades

        public double TamañoCanvasX
        {
            get => TamañoCanvas.X.Round(1);
            set
            {
                TamañoCanvas.X = value;

                MitadTamañoCanvas.DispararPropertyChanged(new PropertyChangedEventArgs(nameof(ViewModelVector2.X)));
            }
        }

        public double TamañoCanvasY
        {
            get => TamañoCanvas.Y.Round(1);
            set
            {
                TamañoCanvas.Y = value;

                MitadTamañoCanvas.DispararPropertyChanged(new PropertyChangedEventArgs(nameof(ViewModelVector2.Y)));
            }
        }

        public ViewModelVector2 TamañoImagenesPosicion { get; set; } = new ViewModelVector2(100, 120);

        public ViewModelVector2 MitadTamañoImagenesPosicion => new ViewModelVector2(
            -(TamañoImagenesPosicion.X / 2.0f).Round(1),
            -(TamañoImagenesPosicion.Y / 2.0f).Round(1));

        public ViewModelVector2 TamañoCanvas { get; set; } = new ViewModelVector2();

        public ViewModelVector2 MitadTamañoCanvas => new ViewModelVector2(
            (TamañoCanvas.X / 2.0f).Round(1),
            (TamañoCanvas.Y / 2.0f).Round(1));

        public ViewModelIngresoPosicion PosicionIglesia { get; set; } 

        #endregion

        public ViewModelMapa()
        {
            PosicionIglesia = new ViewModelIngresoPosicion
            {
                TextoPosicionX = "50",
                TextoPosicionY = "150",

                mapa = this
            };
        }
    }
}
