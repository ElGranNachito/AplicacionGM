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
        #region Campos & Propiedades

        //-----------------------------------CAMPOS--------------------------------------

        /// <summary>
        /// VM del mapa en el que se añadira la unidad
        /// </summary>
        private ViewModelMapa mMapa;

        /// <summary>
        /// VM resultante de la creacion
        /// </summary>
        public ViewModelIngresoPosicion vmResultado;


        //---------------------------------PROPIEDADES-----------------------------------

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
        /// TODO: Cambiar para que sea un modelo o un controlador del personaje
        public string PersonajeSeleccionado { get; set; } = string.Empty;

        /// <summary>
        /// Cantidad de unidades en el grupo.
        /// Solamente se utiliza si el <see cref="TipoSeleccionado"/> es <see cref="ETipoUnidad.Invocacion"/> o <see cref="ETipoUnidad.Trampa"/>
        /// </summary>
        public int CantidadInicialDeUnidades { get; set; } = 0;

        /// <summary>
        /// Indica si es necesario seleccionar una <see cref="EClaseServant"/>
        /// </summary>
        public bool DebeSeleccionarClaseServant   => TipoSeleccionado != ETipoUnidad.NINGUNO && TipoSeleccionado != ETipoUnidad.Iglesia;

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
        /// Clase de servant seleccionada.
        /// Solamente aplica si el <see cref="TipoSeleccionado"/> no es <see cref="ETipoUnidad.Iglesia"/>
        /// </summary>
        public EClaseServant ClaseSeleccionada { get; set; }

        /// <summary>
        /// Valores del enum <see cref="ETipoUnidad"/>
        /// </summary>
        /// TODO: Remover
        public List<ETipoUnidad>   TiposUnidades  => Enum.GetValues(typeof(ETipoUnidad)).Cast<ETipoUnidad>().ToList();

        /// <summary>
        /// Valores del enum <see cref="EClaseServant"/>
        /// </summary>
        /// TODO: Remover
        public List<EClaseServant> ClasesServants => Enum.GetValues(typeof(EClaseServant)).Cast<EClaseServant>().ToList();

        /// <summary>
        /// Comando que ejecutara cuando el usuario presiones el boton 'Finalizar'
        /// </summary>
        public ICommand ComandoFinalizar { get; set; }

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
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(DebeSeleccionarClaseServant)));
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(DebeSeleccionarPersonaje)));
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(DebeSeleccionarCantidad)));

                    if(DebeSeleccionarPersonaje)
                        DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PersonajesDisponibles)));
                }
                //Si la propiedad no es el tipo sileccionado ni si podemos finalizar la creacion...
                else if(e.PropertyName != nameof(PuedeFinalizarCreacion))
                    //Disparamos el evento property changed en PuedeFinalizarCreacion
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuedeFinalizarCreacion)));
            };
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Crea el VM
        /// </summary>
        private void GenerarViewModel()
        {
            ModeloUnidadMapa      modeloUnidad        = null;
            ModeloVector2         posicionUnidad      = new ModeloVector2();

            //Nos aseguramos que los valores ingresados queden dentro de los limites del mMapa
            double PosY = Math.Clamp(double.Parse(PosInicialX), 0, mMapa.TamañoCanvasX);
            double PosX = Math.Clamp(double.Parse(PosInicialY), 0, mMapa.TamañoCanvasY);

            posicionUnidad.X = PosX;
            posicionUnidad.Y = PosY;

            switch (TipoSeleccionado)
            {
                case ETipoUnidad.Iglesia:
                    modeloUnidad = new ModeloUnidadMapa
                    {
                        ETipoUnidad = TipoSeleccionado,
                        Nombre = Nombre
                    };
                    break;

                case ETipoUnidad.Master:
                case ETipoUnidad.Servant:
                    modeloUnidad = new ModeloUnidadMapaMasterServant
                    {
                        ETipoUnidad = TipoSeleccionado,
                        EClaseServant = ClaseSeleccionada,
                        Nombre = Nombre
                    };

                    if (TipoSeleccionado == ETipoUnidad.Master)
                        modeloUnidad.Personaje = SistemaPrincipal.DatosRolSeleccionado.Masters.Find(m => m.ToString() == PersonajeSeleccionado).modelo;
                    else
                        modeloUnidad.Personaje = SistemaPrincipal.DatosRolSeleccionado.Servants.Find(s => s.ToString() == PersonajeSeleccionado).modelo;

                    break;

                case ETipoUnidad.Invocacion:
                case ETipoUnidad.Trampa:
                    modeloUnidad = new ModeloUnidadMapaInvocacionTrampa
                    {
                        ETipoUnidad = TipoSeleccionado,
                        EClaseServant = ClaseSeleccionada,
                        Nombre = Nombre
                    };

                    if (TipoSeleccionado == ETipoUnidad.Invocacion)
                        modeloUnidad.Personaje = SistemaPrincipal.DatosRolSeleccionado.Invocaciones.Find(i => i.ToString() == PersonajeSeleccionado).modelo;
                    break;
            }

            modeloUnidad.Posicion  = posicionUnidad;

            mMapa.controladorMapa.AñadirUnidad(modeloUnidad);

            ControladorUnidadMapa controlador = new ControladorUnidadMapa(modeloUnidad);
            vmResultado = new ViewModelIngresoPosicion(mMapa, controlador);

            SistemaPrincipal.GuardarModelo(modeloUnidad);
            SistemaPrincipal.GuardarDatosAsync();

            Resultado = EResultadoViewModel.Aceptar;
        }

        #endregion

        public bool PuedeFinalizarCreacion
        {
            get
            {
                if (TipoSeleccionado == ETipoUnidad.NINGUNO
                || Nombre == string.Empty)
                    return false;

                if (DebeSeleccionarClaseServant && ClaseSeleccionada == EClaseServant.NINGUNO)
                    return false;

                if(DebeSeleccionarPersonaje && PersonajeSeleccionado == string.Empty)
                    return false;

                if (DebeSeleccionarCantidad && CantidadInicialDeUnidades == 0)
                    return false;

                return true;
            }
        }

        public List<string> PersonajesDisponibles
        {
            get
            {
                switch (TipoSeleccionado)
                {
                    case ETipoUnidad.Servant:
                        return SistemaPrincipal.DatosRolSeleccionado.Servants.Select(s => s.ToString()).ToList();
                    case ETipoUnidad.Master:
                        return SistemaPrincipal.DatosRolSeleccionado.Masters.Select(m => m.ToString()).ToList();
                    case ETipoUnidad.Invocacion:
                        return SistemaPrincipal.DatosRolSeleccionado.Invocaciones.Select(i => i.ToString()).ToList();
                    default:
                        return new List<string>(0);
                }
            }
        }

    }
}