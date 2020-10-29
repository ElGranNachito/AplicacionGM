using System.Collections.Generic;
using System.ComponentModel;

namespace AppGM.Core
{
    public class ViewModelMapa : BaseViewModel
    {
        #region Miembros

        public List<ViewModelIngresoPosicion>    Posiciones { get; set; } = new List<ViewModelIngresoPosicion>();

        protected ControladorMapa                mControladorMapa;

        #endregion

        #region Propiedades
        public string PathImagen { get; set; }
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

        public object ContenedorImagenes { get; set; }

        public ViewModelVector2 TamañoCanvas { get; set; } = new ViewModelVector2();

        //Tamaño de las imagenes
        public ViewModelVector2 TamañoImagenesPosicion { get; set; } = new ViewModelVector2(101.25, 138.75);

        public ViewModelVector2 MitadTamañoImagenesPosicion => new ViewModelVector2(
            -(TamañoImagenesPosicion.X / 2.0f).Round(1),
            -(TamañoImagenesPosicion.Y / 2.0f).Round(1));

        #endregion

        #region Constructores

        public ViewModelMapa(ControladorMapa _controlador)
        {
            mControladorMapa = _controlador;

            PathImagen = "../../../Media/Imagenes/Mapas/" +
                          mControladorMapa.NombreMapa + mControladorMapa.ObtenerExtension();

            //Creamos los view models para el ingreso de las diferentes posiciones
            for(int i = 0; i < mControladorMapa.controladoresUnidadesMapa.Count; ++i)
                Posiciones.Add(new ViewModelIngresoPosicion(this, mControladorMapa.controladoresUnidadesMapa[i]));
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

            PathImagen = "../../../Media/Imagenes/Mapas/" +
                          mControladorMapa.modelo.NombreMapa + mControladorMapa.ObtenerExtension();
        } 
        #endregion
    }
}
