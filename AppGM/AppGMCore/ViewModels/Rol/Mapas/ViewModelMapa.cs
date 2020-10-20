using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace AppGM.Core
{
    public class ViewModelMapa : BaseViewModel
    {
        #region Miembros

        protected ControladorMapa mControladorMapa;

        #endregion

        #region Propiedades
        public string PathAImagen { get; set; }
        public double TamañoCanvasX
        {
            get => TamañoCanvas.X.Round(1);
            set
            {
                TamañoCanvas.X = value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(MitadTamañoCanvasX)));
            }
        }

        public double TamañoCanvasY
        {
            get => TamañoCanvas.Y.Round(1);
            set
            {
                TamañoCanvas.Y = value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(MitadTamañoCanvasY)));
            }
        }

        public double MitadTamañoCanvasX => (TamañoCanvas.X / 2.0f).Round(1);
        public double MitadTamañoCanvasY => (TamañoCanvas.Y / 2.0f).Round(1);

        public ViewModelVector2 TamañoCanvas { get; set; } = new ViewModelVector2();

        public ViewModelVector2 TamañoImagenesPosicion { get; set; } = new ViewModelVector2(100, 120);

        public ViewModelVector2 MitadTamañoImagenesPosicion => new ViewModelVector2(
            -(TamañoImagenesPosicion.X / 2.0f).Round(1),
            -(TamañoImagenesPosicion.Y / 2.0f).Round(1));

        #endregion

        #region Constructores

        public ViewModelMapa(ControladorMapa _controlador)
        {
            mControladorMapa = _controlador;

            PathAImagen = "../../../Media/Imagenes/Mapas/" +
                          mControladorMapa.modelo.NombreMapa + mControladorMapa.ObtenerExtension();
        }
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

            PathAImagen = "../../../Media/Imagenes/Mapas/" +
                          mControladorMapa.modelo.NombreMapa + mControladorMapa.ObtenerExtension();
        } 
        #endregion
    }
}
