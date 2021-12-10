using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Castle.Core.Internal;

namespace AppGM.Core
{
    /// <summary>
    /// View model que representa el control para la modificacion de la posicion de un personaje en un mapa
    /// </summary>
    public class ViewModelIngresoPosicion : ViewModel
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

        /// <summary>
        /// Indica si se debe mostrar la imagen de la unidad sobre el mapa.
        /// </summary>
        private bool imagenPosicionEsVisible = true;

        /// <summary>
        /// Indica si se debe mostrar la imagen de la unidad sobre el mapa.
        /// </summary>
        private bool modoPartyHabilitado = false;


        // -----------------------PROPIEDADES----------------------------------

        /// <summary>
        /// Alianzas de persoanje a las que pertenece esta unidad
        /// </summary>
        public ViewModelListaDeElementos<ViewModelPosicionAlianza> AlianzasPersonaje { get; set; } = new ViewModelListaDeElementos<ViewModelPosicionAlianza>();
        
        /// <summary>
        /// Comando que se ejecuta al presionar el boton de eliminar unidad
        /// </summary>
        public ICommand ComandoEliminarUnidad { get; set; }

        /// <summary>
        /// Comando que se ejecuta al presionar la combinacion de teclas Control + LeftClick
        /// </summary>
        public ICommand ComandoUnidadSeleccionada { get; set; }

        /// <summary>
        /// Posicion de la unidad
        /// </summary>
        public ViewModelVector2 Posicion { get; set; }

        /// <summary>
        /// Indica si debe mostrar la seccion de datos extra en el control de ingreso de posicion
        /// </summary>
        public bool DebeMostrarDatosExtra { get; set; } = false;

        /// <summary>
        /// Indica si las alianzas a las que pertenece el indicador del mapa deber ser visibles.
        /// </summary>
        public bool InsigneasAlianzasSonVisibles { get; set; } = false;

        /// <summary>
        /// Color del borde de la unidad.
        /// </summary>
        public string ColorBordeIngresoPosicion { get; set; } = "c3ccc7";

        /// <summary>
        /// Color del fondo de la unidad.
        /// </summary>
        public string ColorFondoIngresoPosicion { get; set; } = "06140d";

        /// <summary>
        /// Ruta de la imagen de la unidad
        /// </summary>
        public string PathImagen => unidad.Path;

        /// <summary>
        /// Nombre de la unidad
        /// </summary>
        public string Nombre => EsInvocacionOTrampa ? string.Format($"{unidad.Nombre} ({Cantidad})") : unidad.Nombre;

        /// <summary>
        /// Cantidad de elementos que representa
        /// </summary>
        public int Cantidad => EsInvocacionOTrampa ? unidad.Cantidad : 0;

        /// <summary>
        /// Devuelve verdadero si esta unidad es una invocacion o una trampa
        /// </summary>
        public bool EsInvocacionOTrampa => (unidad.TipoUnidad & (ETipoUnidad.Invocacion | ETipoUnidad.Trampa)) != 0;

        /// <summary>
        /// Indica si el indicador en el mapa debe ser visible o no
        /// </summary>
        public bool ImagenPosicionEsVisible
        {
            get => imagenPosicionEsVisible;
            set
            {
                imagenPosicionEsVisible = value;

                if (!UnidadParty.PersonajesParty.IsNullOrEmpty() && UnidadParty.PersonajesParty.All(a => a.ImagenPosicionEsVisible))
                {
                    UnidadParty.ImagenPosicionEsVisible = false;
                    modoPartyHabilitado = false;
                }
            }
        }

        /// <summary>
        /// Indica si el modo party de la unidad en el mapa debe ser habilitado.
        /// </summary>
        public bool ModoPartyHabilitado
        {
            get => modoPartyHabilitado;
            set
            {
                modoPartyHabilitado = value;

                if (modoPartyHabilitado)
                    ImagenPosicionEsVisible = false;
                else
                    ImagenPosicionEsVisible = true;

                UnidadParty.ImagenPosicionEsVisible = !ImagenPosicionEsVisible;

                Posicion = new ViewModelVector2(UnidadParty.Posicion.X, UnidadParty.Posicion.Y);
            }
        }

        /// <summary>
        /// Unidad party a la que corresponde esta unidad.
        /// </summary>
        public ViewModelUnidadParty UnidadParty
        {
            get
            {
                foreach (var party in mapa.PosicionesParties)
                {
                    if (party.NumeroParty == unidad.personaje.modelo.NumeroParty)
                        return party;
                }

                return null;
            }
            set
            {
                foreach (var party in mapa.PosicionesParties)
                {
                    if (party.NumeroParty == unidad.personaje.modelo.NumeroParty)
                        value = party;
                }
            }
        }

        /// <summary>
        /// Tamaño de la imagen
        /// </summary>
        public ViewModelVector2 TamañoImagenesPosicion => mapa.TamañoImagenesPosicion;

        /// <summary>
        /// Mitad del tamaño de la imagen
        /// </summary>
        public ViewModelVector2 OffsetImagenesPosicion => mapa.OffsetImagenesPosicion;

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

            ComandoEliminarUnidad     = new Comando(EliminarUnidad);
            ComandoUnidadSeleccionada = new Comando(AgregarUnidadSeleccionada);

            AlianzasPersonaje.Elementos = new ObservableCollection<ViewModelPosicionAlianza>(unidad.personaje.Alianzas.Select(a => new ViewModelPosicionAlianza(a)));
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
        /// Dispara el evento property changed para las propiedades <see cref="ColorBordeIngresoPosicion" y <see cref="ColorFondoIngresoPosicion"/>/>
        /// </summary>
        public void DispararPropertyChangedColorBordeFondoUnidad()
        {
            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(ColorBordeIngresoPosicion)));
            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(ColorFondoIngresoPosicion)));
        }

        /// <summary>
        /// Agrega esta unidad a la ObservableCollection de unidades seleccionadas.
        /// </summary>
        public void AgregarUnidadSeleccionada()
        {
            if (mapa.UnidadesSeleccionadas.Contains(this))
            {
                RemoverUnidadSeleccionada();
                return;
            }

            ColorFondoIngresoPosicion = "0e2e1e";

            mapa.UnidadesSeleccionadas.Add(this);

            DispararPropertyChangedColorBordeFondoUnidad();
        }

        /// <summary>
        /// Remueve esta unidad de la ObservableCollection de unidades seleccionadas.
        /// </summary>
        public void RemoverUnidadSeleccionada()
        {
            if (!mapa.UnidadesSeleccionadas.Contains(this))
                return;
            
            ColorFondoIngresoPosicion = "06140d";

            mapa.UnidadesSeleccionadas.Remove(this);

            DispararPropertyChangedColorBordeFondoUnidad();
        }

        /// <summary>
        /// Elimina esta unidad del mapa y de la base de datos
        /// </summary>
        private async void EliminarUnidad()
        {
            mapa.Posiciones.Remove(this);

            unidad.Eliminar();

            await SistemaPrincipal.GuardarDatosAsync();

            mapa.ActualizarUnidadesParties();
        }

        #endregion
    }
}