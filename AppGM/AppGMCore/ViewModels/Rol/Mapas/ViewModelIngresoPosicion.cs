using System.ComponentModel;
using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// View model que representa el control para la modificacion de la posicion de un personaje en un mapa
    /// </summary>
    public class ViewModelIngresoPosicion : BaseViewModel
    {
        #region Campos & Propiedades


        // -------------------------CAMPOS----------------------------


        /// <summary>
        /// Viewmodel del mapa
        /// </summary>
        public ViewModelMapa         mapa;

        /// <summary>
        /// Controlador de la unidad
        /// </summary>
        public ControladorUnidadMapa unidad;


        // -----------------------PROPIEDADES----------------------------------


        /// <summary>
        /// Comando que se ejecuta al presionar el boton de eliminar unidad
        /// </summary>
        public ICommand ComandoEliminarUnidad { get; set; }

        /// <summary>
        /// Posicion de la unidad
        /// </summary>
        public ViewModelVector2 Posicion { get; set; }

        /// <summary>
        /// Indica si debe mostrar la seccion de datos extra en el control de ingreso de posicion
        /// </summary>
        public bool DebeMostrarDatosExtra { get; set; } = false;

        /// <summary>
        /// Indica si el indicador en el mapa debe ser visible o no
        /// </summary>
        public bool ImagenPosicionEsVisible { get; set; } = true;

        /// <summary>
        /// Ruta de la imagen de la unidad
        /// </summary>
        public string PathImagen => unidad.Path;

        /// <summary>
        /// Nombre de la unidad
        /// </summary>
        public string Nombre => EsInvocacionOTrampa ? string.Format($"{unidad.Nombre} ({Cantidad})") : unidad.Nombre;

        /// <summary>
        /// Devuelve verdadero si esta unidad es una invocacion o una trampa
        /// </summary>
        public bool EsInvocacionOTrampa => (unidad.TipoUnidad & (ETipoUnidad.Invocacion | ETipoUnidad.Trampa)) != 0;

        /// <summary>
        /// Cantidad de elementos que representa
        /// </summary>
        public int Cantidad => EsInvocacionOTrampa ? unidad.Cantidad : 0;

        /// <summary>
        /// Tamaño de la imagen
        /// </summary>
        public ViewModelVector2 TamañoImagenesPosicion => mapa.TamañoImagenesPosicion;

        /// <summary>
        /// Mitad del tamaño de la imagen
        /// </summary>
        public ViewModelVector2 MitadTamañoImagenesPosicion => mapa.MitadTamañoImagenesPosicion;

        /// <summary>
        /// Posicion de la imagen en el mapa
        /// </summary>
        public Grosor PosicionImg => new Grosor(Posicion.X, Posicion.Y, 0, 0);

        /// <summary>
        /// Posicion del texto de cantidad de unidades
        /// </summary>
        public Grosor PosicionCantidadUnidades => EsInvocacionOTrampa ? new Grosor(Posicion.X - Cantidad.Length() * 2.66, Posicion.Y + TamañoImagenesPosicion.Y * 0.25, 0, 0) : new Grosor(0);

        /// <summary>
        /// Texto del campo de texto que representa la posicion en el eje x
        /// </summary>
        public string TextoPosicionX
        {
	        get => Posicion.X == 0 ? "" : Posicion.X.Round(1).ToString();
	        set
	        {
		        if (mapa == null)
			        return;

		        double tmp;

		        //Intentamos parsear el nuevo valor
                if (double.TryParse(value, out tmp))
		        {
			        if (tmp < 0)
				        return;

			        //Si el valor esta dentro de los limites del mapa actualizamos
                    if (tmp < mapa.TamañoCanvasX)
				        Posicion.X = tmp;
                    //Si no lo esta simplemente ponemos como posicion el final del mapa
                    else
                        Posicion.X = mapa.TamañoCanvasX;

                    //Le avisamos a la UI que la posicion de la imagen cambio
                    DispararPropertyChangedPosImgPosCantUnidades();

			        return;
		        }

		        if (value.IsNullOrWhiteSpace())
			        Posicion.X = 0;

		        DispararPropertyChangedPosImgPosCantUnidades();
	        }
        }

        /// <summary>
        /// Texto del campo de texto que representa la posicion en el eje y
        /// </summary>
        public string TextoPosicionY
        {
	        get => Posicion.Y.Round(1).ToString();
	        set
	        {
		        if (mapa == null)
			        return;

		        double tmp;

                //Intentamos parsear el nuevo valor
		        if (double.TryParse(value, out tmp))
		        {
			        if (tmp < 0)
				        return;

                    //Si el valor esta dentro de los limites del mapa actualizamos
			        if (tmp < mapa.TamañoCanvasY)
				        Posicion.Y = tmp;
                    //Si no lo esta simplemente ponemos como posicion el final del mapa
			        else
				        Posicion.Y = mapa.TamañoCanvasY;

                    //Le avisamos a la UI que la posicion de la imagen cambio
			        DispararPropertyChangedPosImgPosCantUnidades();

			        return;
		        }

		        if (value.IsNullOrWhiteSpace())
			        Posicion.Y = 0;

                DispararPropertyChangedPosImgPosCantUnidades();
	        }
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Constrcutor
        /// </summary>
        /// <param name="_mapa">Viewmodel del mapa</param>
        /// <param name="_unidad">Contralador de la unidad</param>
        public ViewModelIngresoPosicion(ViewModelMapa _mapa, ControladorUnidadMapa _unidad)
        {
            mapa   = _mapa;
            unidad = _unidad;

            Posicion = new ViewModelVector2(unidad.posicion);

            ComandoEliminarUnidad = new Comando(EliminarUnidad);
        }

        #endregion

        #region Funciones

        /// <summary>
        /// Dispara el evento property changed para las propiedades <see cref="PosicionImg" y <see cref="PosicionCantidadUnidades"/>/>
        /// </summary>
        public void DispararPropertyChangedPosImgPosCantUnidades()
        {
            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PosicionImg)));
            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PosicionCantidadUnidades)));
        }

        /// <summary>
        /// Elimina esta unidad del mapa y de la base de datos
        /// </summary>
        private void EliminarUnidad()
        {
            mapa.Posiciones.Remove(this);

            unidad.Eliminar();

            SistemaPrincipal.GuardarDatosRolAsincronicamente();
        }

        #endregion
    }
}