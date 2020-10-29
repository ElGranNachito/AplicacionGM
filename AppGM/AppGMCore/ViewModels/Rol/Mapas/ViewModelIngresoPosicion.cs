using System.ComponentModel;

namespace AppGM.Core
{
    public class ViewModelIngresoPosicion : BaseViewModel
    {
        #region Miembros

        public ViewModelMapa         mapa;
        public ControladorUnidadMapa unidad;

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

                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PosicionImg)));

                    return;
                }

                if (value.EstaVacio())
                    Posicion.X = 0;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PosicionImg)));
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

                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PosicionImg)));

                    return;
                }

                if (value.EstaVacio())
                    Posicion.Y = 0;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PosicionImg)));
            }
        }

        public string PathImagen => unidad.Path;
        public string Nombre     => unidad.Nombre;
        public ViewModelVector2 TamañoImagenesPosicion => mapa.TamañoImagenesPosicion;
        public ViewModelVector2 MitadTamañoImagenesPosicion => mapa.MitadTamañoImagenesPosicion;
        public Grosor           PosicionImg => new Grosor(Posicion.X, Posicion.Y, 0, 0);

        #endregion

        #region Constructores

        public ViewModelIngresoPosicion(ViewModelMapa _mapa, ControladorUnidadMapa _unidad)
        {
            mapa   = _mapa;
            unidad = _unidad;

            Posicion = new ViewModelVector2(unidad.posicion);
        }
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
