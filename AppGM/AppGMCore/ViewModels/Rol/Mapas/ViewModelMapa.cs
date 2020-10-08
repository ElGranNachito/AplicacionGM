using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace AppGM.Core
{
    public class ViewModelMapa : BaseViewModel
    {
        #region Miembros

        private ControladorMapa mControladorMapa;

        #endregion

        #region Propiedades
        public string PathAImagen { get; set; }
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
            mControladorMapa = new ControladorMapa
            {
                modelo = new ModeloMapa
                {
                    EFormatoImagen = EFormatoImagen.Png,
                    NombreMapa = "Seoul",
                    PosicionesElementos = new List<TIMapaVector2>
                    {
                        new TIMapaVector2
                        {
                            Posicion = new ModeloVector2
                            {
                                X = 50,
                                Y = 150
                            }
                        }
                    }
                }
            };
            if (mControladorMapa.modelo.PosicionesElementos.Count != 0)
            {
                PosicionIglesia = new ViewModelIngresoPosicion(this, new Vector2(mControladorMapa.modelo.PosicionesElementos.First().Posicion.X, mControladorMapa.modelo.PosicionesElementos.First().Posicion.Y));
            }

            PathAImagen = "../../../Media/Imagenes/Mapas/" + 
                mControladorMapa.modelo.NombreMapa + mControladorMapa.ObtenerExtension();
        }
    }
}
