using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// VM para la creacion de la representacion de un elemento en un mMapa
    /// </summary>
    public class ViewModelCrearUnidadMapa : ViewModelConResultado<ViewModelCrearUnidadMapa>
    {
        #region Miembros

        // Campos ---


        /// <summary>
        /// VM del mapa en el que se añadira la unidad
        /// </summary>
        private ViewModelMapa mMapa;

        /// <summary>
        /// VM resultante de la creacion
        /// </summary>
        public ViewModelIngresoPosicion vmResultado;


        // Propiedades ---


        /// <summary>
        /// Comando que ejecutara cuando el usuario presiones el boton 'Finalizar'
        /// </summary>
        public ICommand ComandoFinalizar { get; set; }

        /// <summary>
        /// Nombre de la unidad
        /// </summary>
        public string Nombre      { get; set; } = "Mr. Sin nombre";

        /// <summary>
        /// Posicion inicial de la unidad en el eje X
        /// </summary>
        public string PosInicialX { get; set; } = "0";

        /// <summary>
        /// Posicion inicial de la unidad en el eje Y
        /// </summary>
        public string PosInicialY { get; set; } = "0";

        /// <summary>
        /// Nombre del personaje actualmente seleccionado
        /// </summary>
        public ModeloPersonaje PersonajeSeleccionado { get; set; }

        /// <summary>
        /// Cantidad de unidades en el grupo.
        /// Solamente se utiliza si el <see cref="TipoSeleccionado"/> es <see cref="ETipoUnidad.Invocacion"/> o <see cref="ETipoUnidad.Trampa"/>
        /// </summary>
        public int CantidadInicialDeUnidades { get; set; } = 1;

        /// <summary>
        /// Indica si es necesario seleccionar un <see cref="ModeloPersonaje"/>
        /// </summary>
        public bool DebeSeleccionarPersonaje      => (TipoSeleccionado & (ETipoUnidad.Master | ETipoUnidad.Servant | ETipoUnidad.Invocacion)) != 0;

        /// <summary>
        /// Indica si es necesario seleccionar una cantidad de unidades
        /// </summary>
        public bool DebeSeleccionarCantidad       => (TipoSeleccionado & (ETipoUnidad.Invocacion | ETipoUnidad.Trampa)) != 0;

        /// <summary>
        /// Tipo de la unidad que sera añadida al mapa
        /// </summary>
        public ETipoUnidad TipoSeleccionado { get; set; }

        /// <summary>
        /// Indica si se puede agregar el personaje marcado.
        /// </summary>
        public bool PuedeFinalizarCreacion
        {
            get
            {
                if (Nombre == string.Empty)
                    return false;

                if (TipoSeleccionado == ETipoUnidad.NINGUNO)
                    return false;

                if (PersonajeSeleccionado is null)
                    return false;

                if (DebeSeleccionarCantidad && CantidadInicialDeUnidades < 1)
                    return false;

                return true;
            }
        }

        /// <summary>
        /// VM del ComboBox con los distintas tipos de unidades disponibles.
        /// </summary>
        public ViewModelComboBox<ETipoUnidad> ComboBoxTiposUnidades { get; set; } = new ViewModelComboBox<ETipoUnidad>(EnumHelpers.TiposDeUnidadesDisponibles);

        /// <summary>
        /// VM del ComboBox con los distintas personajes disponibles.
        /// </summary>
        public ViewModelComboBox<ModeloPersonaje> ComboBoxPersonajesDisponibles { get; set; }

        #endregion

        #region Constrcutores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mMapa">Mapa al que se añadira la unidad</param>
        public ViewModelCrearUnidadMapa(ViewModelMapa _mapa)
        {
            mMapa = _mapa;

            ComandoFinalizar = new Comando(GenerarViewModel);

            PropertyChanged += (obj, e) =>
            {
                //Si el tipo de personaje que estamos creando cambio...
                if (e.PropertyName == nameof(TipoSeleccionado))
                {
                    //Disparamos property changed en estas propiedades para que se actualicen los campos a completar en la UI
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(DebeSeleccionarPersonaje)));
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(DebeSeleccionarCantidad)));
                }
                //Si la propiedad no es el tipo sileccionado ni si podemos finalizar la creacion...
                else if(e.PropertyName != nameof(PuedeFinalizarCreacion)) 
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuedeFinalizarCreacion)));
            };
            
            ComboBoxTiposUnidades.OnValorSeleccionadoCambio += (anterior, actual) =>
            {
                TipoSeleccionado = actual.valor;
                
                ComboBoxPersonajesDisponibles.ActualizarValoresPosibles(ObtenerPersonajesDisponibles());
            };

            ComboBoxPersonajesDisponibles = new ViewModelComboBox<ModeloPersonaje>(ObtenerPersonajesDisponibles());

            ComboBoxPersonajesDisponibles.OnValorSeleccionadoCambio += (anterior, actual) => PersonajeSeleccionado = actual.valor;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Obtiene una lista de <see cref="ModeloPersonaje"/> dependiendo del tipo de unidad seleccionado. 
        /// </summary>
        /// <returns></returns>
        public List<ModeloPersonaje> ObtenerPersonajesDisponibles()
        {
            switch (TipoSeleccionado)
            {
                case ETipoUnidad.Servant:
                    return SistemaPrincipal.ModeloRolActual.Personajes.Where(s => s.TipoPersonaje == ETipoPersonaje.Servant).Select(s => s).ToList();
                case ETipoUnidad.Master:
                    return SistemaPrincipal.ModeloRolActual.Personajes.Where(s => s.TipoPersonaje == ETipoPersonaje.Master).Select(s => s).ToList();
                case ETipoUnidad.Invocacion:
                    return SistemaPrincipal.ModeloRolActual.Personajes.Where(s => s.TipoPersonaje == ETipoPersonaje.Invocacion).Select(s => s).ToList();
                default:
                    return new List<ModeloPersonaje>(0);
            }
        }

        /// <summary>
        /// Crea el VM
        /// </summary>
        private async void GenerarViewModel()
        {
            ModeloUnidadMapa      modeloUnidad        = null;
            ModeloVector2         posicionUnidad      = new ModeloVector2();

            //Nos aseguramos que los valores ingresados queden dentro de los limites del mMapa
            double PosY = Math.Clamp(double.Parse(PosInicialX), 0, mMapa.TamañoCanvasX);
            double PosX = Math.Clamp(double.Parse(PosInicialY), 0, mMapa.TamañoCanvasY);

            posicionUnidad.X = PosX;
            posicionUnidad.Y = PosY;

            var personaje = SistemaPrincipal.ModeloRolActual.Personajes.Find(s => s == PersonajeSeleccionado);

            switch (TipoSeleccionado)
            {
                case ETipoUnidad.Iglesia:
                {
                    modeloUnidad = new ModeloUnidadMapa
                    {
                        ETipoUnidad = TipoSeleccionado,
                        Nombre = Nombre
                    };
                    break;
                }
                case ETipoUnidad.Master:
                {
                    modeloUnidad = new ModeloUnidadMapaMasterServant
                    {
                        ETipoUnidad = TipoSeleccionado,
                        EClaseServant = ((ModeloMaster) personaje).ClaseServant,
                        Nombre = Nombre
                    };
                        
                    if (TipoSeleccionado == ETipoUnidad.Master)
                        modeloUnidad.Personaje = (ModeloMaster) personaje;

                    break;
                }
                case ETipoUnidad.Servant:
                {
                    modeloUnidad = new ModeloUnidadMapaMasterServant
                    {
                        ETipoUnidad = TipoSeleccionado,
                        EClaseServant = ((ModeloServant) personaje).ClaseServant,
                        Nombre = Nombre
                    };

                    if (TipoSeleccionado == ETipoUnidad.Servant)
                        modeloUnidad.Personaje = (ModeloServant) personaje;

                    break;
                }
                case ETipoUnidad.Invocacion:
                {
                    modeloUnidad = new ModeloUnidadMapaInvocacionTrampa()
                    {
                        ETipoUnidad = TipoSeleccionado,
                        EClaseServant = ((ModeloPersonajeJugable)((ModeloInvocacion) personaje).Invocador).ClaseServant,
                        Nombre = Nombre
                    };

                    if (TipoSeleccionado == ETipoUnidad.Invocacion)
                        modeloUnidad.Personaje = (ModeloInvocacion) personaje;
                    
                    break;
                }
            }

            modeloUnidad.Posicion  = posicionUnidad;

            mMapa.controladorMapa.AñadirUnidad(modeloUnidad);

            ControladorUnidadMapa controlador = new ControladorUnidadMapa(modeloUnidad);
            
            vmResultado = new ViewModelIngresoPosicion(mMapa, controlador);

            SistemaPrincipal.GuardarModelo(modeloUnidad);
            
            await SistemaPrincipal.GuardarDatosAsync();

            Resultado = EResultadoViewModel.Aceptar;
        }

        #endregion

    }
}