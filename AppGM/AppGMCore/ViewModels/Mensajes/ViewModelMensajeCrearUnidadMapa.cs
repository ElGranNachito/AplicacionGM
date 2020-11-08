using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace AppGM.Core
{
    public class ViewModelMensajeCrearUnidadMapa : ViewModelMensajeBase
    {
        #region Miembros
        private ViewModelMapa mapa;

        public ViewModelIngresoPosicion vmResultado; 
        #endregion

        #region Propiedades

        public string Nombre      { get; set; } = "Mr. Sin nombre";
        public string PosInicialX { get; set; } = "0";
        public string PosInicialY { get; set; } = "0";
        public string PersonajeSeleccionado { get; set; } = string.Empty;
        public int CantidadInicialDeUnidades { get; set; } = 0;
        public bool DebeSeleccionarClaseServant   => TipoSeleccionado != ETipoUnidad.NINGUNO && TipoSeleccionado != ETipoUnidad.Iglesia;
        public bool DebeSeleccionarPersonaje      => (TipoSeleccionado & (ETipoUnidad.Master | ETipoUnidad.Servant | ETipoUnidad.Invocacion)) != 0;
        public bool DebeSeleccionarCantidad       => (TipoSeleccionado & (ETipoUnidad.Invocacion | ETipoUnidad.Trampa)) != 0;
        public List<ETipoUnidad>   TiposUnidades  => Enum.GetValues(typeof(ETipoUnidad)).Cast<ETipoUnidad>().ToList();
        public List<EClaseServant> ClasesServants => Enum.GetValues(typeof(EClaseServant)).Cast<EClaseServant>().ToList();
        public ETipoUnidad TipoSeleccionado { get; set; }
        public EClaseServant ClaseSeleccionada { get; set; }
        public ICommand ComandoFinalizar { get; set; }

        #endregion

        #region Constrcutores
        public ViewModelMensajeCrearUnidadMapa(ViewModelMapa _mapa)
        {
            mapa = _mapa;

            ComandoFinalizar = new Comando(GenerarViewModel);

            PropertyChanged += (obj, e) =>
            {
                if (e.PropertyName == nameof(TipoSeleccionado))
                {
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(DebeSeleccionarClaseServant)));
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(DebeSeleccionarPersonaje)));
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(DebeSeleccionarCantidad)));

                    if(DebeSeleccionarPersonaje)
                        DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PersonajesDisponibles)));
                }
                else if(e.PropertyName != nameof(PuedeFinalizarCreacion))
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuedeFinalizarCreacion)));
            };
        }

        #endregion

        #region Funciones
        private void GenerarViewModel()
        {
            ModeloUnidadMapa      modeloUnidad        = null;
            ModeloVector2         posicionUnidad      = new ModeloVector2();
            TIPersonajeUnidadMapa tiPersonajeUnidadMapa = new TIPersonajeUnidadMapa();
            TIUnidadMapaVector2   tiUnidadPosicion      = new TIUnidadMapaVector2();

            //Nos aseguramos que los valores ingresados queden dentro de los limites del mapa
            double PosY = SuperDll.SuperUtilidades.Math.Clamp(double.Parse(PosInicialX), 0, mapa.TamañoCanvasX);
            double PosX = SuperDll.SuperUtilidades.Math.Clamp(double.Parse(PosInicialY), 0, mapa.TamañoCanvasY);

            posicionUnidad.X = PosX;
            posicionUnidad.Y = PosY;

            tiUnidadPosicion.Posicion = posicionUnidad;

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
                        tiPersonajeUnidadMapa.Personaje = SistemaPrincipal.DatosRolSeleccionado.Masters.Find(m => m.ToString() == PersonajeSeleccionado).modelo;
                    else
                        tiPersonajeUnidadMapa.Personaje = SistemaPrincipal.DatosRolSeleccionado.Servants.Find(s => s.ToString() == PersonajeSeleccionado).modelo;

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
                        tiPersonajeUnidadMapa.Personaje = SistemaPrincipal.DatosRolSeleccionado.Invocaciones.Find(i => i.ToString() == PersonajeSeleccionado).modelo;
                    break;
            }

            tiPersonajeUnidadMapa.Unidad = modeloUnidad;
            tiUnidadPosicion.Unidad      = modeloUnidad;

            modeloUnidad.Personaje = tiPersonajeUnidadMapa;
            modeloUnidad.Posicion  = tiUnidadPosicion;

            mapa.controladorMapa.AñadirUnidad(modeloUnidad);

            ControladorUnidadMapa controlador = new ControladorUnidadMapa(modeloUnidad);
            vmResultado = new ViewModelIngresoPosicion(mapa, controlador);

            SistemaPrincipal.GuardarModelo(modeloUnidad);
            SistemaPrincipal.GuardarModelo(tiPersonajeUnidadMapa);
            SistemaPrincipal.GuardarModelo(tiUnidadPosicion);
            SistemaPrincipal.GuardarDatosRolAsync();

            mVentana.CerrarVentana();
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