using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// Viewmodel para la creacion de un personaje
    /// </summary>
    public class ViewModelMensajeCrearRol_CrearPersonaje : ViewModelMensajeBase
    {
        #region Miembros

        private DatosCreacionRol         mDatosCreacionRol;
        private ViewModelMensajeCrearRol mViewModelCrearRol;

        private int    mHP  { get; set; } = 20;
        private ushort mSTR { get; set; } = 10;
        private ushort mEND { get; set; } = 10;
        private ushort mAGI { get; set; } = 10;
        private ushort mINT { get; set; } = 10;
        private ushort mLCK { get; set; } = 10;
        private ushort mCHR { get; set; } = 10;

        private bool mCheckUsarRangos = false;

        private ERango mNP  { get; set; } = ERango.NINGUNO;

        #endregion

        #region Propiedades

        public string Nombre     { get; set; } = "Mr Sin Nombre";
        public string NombreReal { get; set; } = "Nadie";
        public string PathImagen { get; set; } = Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioImagenes, "camarita.png");
        public string TextoPuntosDeHabilidadRestantes => $"Puntos de habilidad restantes: {PuntosHabilidadRestantes}";

        public int PuntosHabilidadRestantes => 75 - mSTR - mEND - mAGI - mINT - mLCK - mCHR;

        public ETipoPersonaje TipoPersonajeSeleccionado { get; set; } = ETipoPersonaje.Servant;
        public EClaseServant  ClaseServantSeleccionada { get; set; }  = EClaseServant.NINGUNO;
        public EAlineamiento  AlineamientoSeleccionado { get; set; }  = EAlineamiento.NINGUNO;

        public bool EsMasterOServant => (TipoPersonajeSeleccionado & (ETipoPersonaje.Master | ETipoPersonaje.Servant)) != 0;
        public bool EsServant    => TipoPersonajeSeleccionado == ETipoPersonaje.Servant;
        public bool EsMaster     => TipoPersonajeSeleccionado == ETipoPersonaje.Master;
        public bool EsInvocacion => TipoPersonajeSeleccionado == ETipoPersonaje.Invocacion;
        public bool UsarRangos   => EsServant || EsInvocacion && CheckUsarRangos;

        public bool PuedeFinalizar => PuedeCrearPersonaje();

        public List<ETipoPersonaje> TiposDePersonajeDisponibles => Enum.GetValues(typeof(ETipoPersonaje)).Cast<ETipoPersonaje>().ToList();
        public List<EAlineamiento>  AlineamientosDisponibles    => Enum.GetValues(typeof(EAlineamiento)).Cast<EAlineamiento>().ToList();
        public List<ERango>         RangosDisponibles           => Enum.GetValues(typeof(ERango)).Cast<ERango>().ToList();
        public List<EClaseServant>  ClasesDeServantDisponibles  => ObtenerClasesDeServantDisponibles();

        public ICommand ComandoConfirmar { get; set; }
        public ICommand ComandoCancelar  { get; set; }

        public ICommand ComandoSeleccionarImagen { get; set; }

        public ICommand ComandoAñadirPerk  { get; set; }
        public ICommand ComandoAñadirSkill { get; set; }
        public ICommand ComandoAñadirMagia { get; set; }
        public ICommand ComandoAñadirNP    { get; set; }

        #endregion

        #region Constructor

        public ViewModelMensajeCrearRol_CrearPersonaje(DatosCreacionRol _datosCreacionRol, ViewModelMensajeCrearRol _viewModelCrearRol)
        {
            mDatosCreacionRol  = _datosCreacionRol;
            mViewModelCrearRol = _viewModelCrearRol;

            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(TipoPersonajeSeleccionado))
                {
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(ClasesDeServantDisponibles)));
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(EsMasterOServant)));
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(EsServant)));
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(EsMaster)));
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(EsInvocacion)));
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(UsarRangos)));
                }

                if(args.PropertyName != nameof(PuedeFinalizar))
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuedeFinalizar)));
            };

            ComandoConfirmar = new Comando(() =>
            {
                ModeloPersonaje nuevoPersonaje = CrearPersonaje();

                mDatosCreacionRol.personajes.Add(nuevoPersonaje);

                switch (nuevoPersonaje)
                {
                    case ModeloMaster mm:
                        mDatosCreacionRol.masters.Add(mm);
                        break;
                    case ModeloServant ms:
                        mDatosCreacionRol.servants.Add(ms);
                        break;
                    case ModeloInvocacion mi:
                        mDatosCreacionRol.invocaciones.Add(mi);
                        break;
                    default:
                        mDatosCreacionRol.npcs.Add(nuevoPersonaje);
                        break;
                }

                SistemaPrincipal.Aplicacion.VentanaPopups.EstablecerViewModel(_viewModelCrearRol);
            });

            ComandoCancelar = new Comando(() =>
            {
                SistemaPrincipal.Aplicacion.VentanaPopups.EstablecerViewModel(_viewModelCrearRol);
            });
        }

        #endregion

        #region Funciones

        private List<EClaseServant> ObtenerClasesDeServantDisponibles()
        {
            List<EClaseServant> ClasesTotales = Enum.GetValues(typeof(EClaseServant)).Cast<EClaseServant>().ToList();

            for (int i = 0; i < ClasesTotales.Count; ++i)
                if (!PuedeSeleccionarClase(ClasesTotales[i]))
                {
                    ClasesTotales.RemoveAt(i);
                    --i;
                }

            return ClasesTotales;
        }

        private bool PuedeSeleccionarClase(EClaseServant claseDeseada)
        {
            for (int i = 0; i < mDatosCreacionRol.personajes.Count; ++i)
            {
                if (mDatosCreacionRol.personajes[i] is ModeloPersonajeJugable mpj)
                    if (mpj.EClaseServant == claseDeseada && TipoPersonajeSeleccionado == mpj.TipoPersonaje)
                        return false;
            }

            return true;
        }

        private bool PuedeCrearPersonaje()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
                return false;

            if (EsServant)
            {
                if(string.IsNullOrWhiteSpace(NombreReal))
                    return false;

                if ((ERango)STR == ERango.NINGUNO)
                    return false;
                if ((ERango)END == ERango.NINGUNO)
                    return false;
                if ((ERango)AGI == ERango.NINGUNO)
                    return false;
                if ((ERango)INT == ERango.NINGUNO)
                    return false;
                if ((ERango)LCK == ERango.NINGUNO)
                    return false;
                if (NP == ERango.NINGUNO)
                    return false;
            }

            if (EsMasterOServant && (ClaseServantSeleccionada == EClaseServant.NINGUNO || AlineamientoSeleccionado == EAlineamiento.NINGUNO))
                return false;

            return true;
        }

        private ModeloPersonaje CrearPersonaje()
        {
            switch (TipoPersonajeSeleccionado)
            {
                case ETipoPersonaje.Master:
                    ModeloMaster nuevoMaster = new ModeloMaster();


                    return nuevoMaster;

                case ETipoPersonaje.Servant:
                    ModeloServant nuevoServant = new ModeloServant();

                    return nuevoServant;

                case ETipoPersonaje.Invocacion:
                    ModeloInvocacion nuevaInvocacion = new ModeloInvocacion();

                    return nuevaInvocacion;

                case ETipoPersonaje.NPC:
                    ModeloPersonaje nuevoPersonaje = new ModeloPersonaje();

                    return nuevoPersonaje;

                default:
                    return null;
                    
            }
        }

        #endregion

        public int HP
        {
            get => mHP;
            set => mHP = value;
        }

        public object STR
        {
            get => mSTR;
            set
            {
                if (value is ERango rango)
                    mSTR = rango.ToUshort();
                else if (value is string s)
                    mSTR = ushort.Parse(s);
                else
                    mSTR = (ushort) value;


                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuntosHabilidadRestantes)));
            }
        }

        public object END
        {
            get => mEND;
            set
            {
                if (value is ERango rango)
                    mEND = rango.ToUshort();
                else if (value is string s)
                    mEND = ushort.Parse(s);
                else
                    mEND = (ushort)value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuntosHabilidadRestantes)));
            }
        }

        public object AGI
        {
            get => mAGI;
            set
            {
                if (value is ERango rango)
                    mAGI = rango.ToUshort();
                else if (value is string s)
                    mAGI = ushort.Parse(s);
                else
                    mAGI = (ushort)value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuntosHabilidadRestantes)));
            }
        }

        public object INT
        {
            get => mINT;
            set
            {
                if (value is ERango rango)
                    mINT = rango.ToUshort();
                else if (value is string s)
                    mINT = ushort.Parse(s);
                else
                    mINT = (ushort)value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuntosHabilidadRestantes)));
            }
        }

        public object LCK
        {
            get => mLCK;
            set
            {
                if (value is ERango rango)
                    mLCK = rango.ToUshort();
                else if (value is string s)
                    mLCK = ushort.Parse(s);
                else
                    mLCK = (ushort)value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuntosHabilidadRestantes)));
            }
        }

        public object CHR
        {
            get => mCHR;
            set
            {
                if (value is ERango rango)
                    mCHR = rango.ToUshort();
                else if (value is string s)
                    mCHR = ushort.Parse(s);
                else
                    mCHR = (ushort)value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuntosHabilidadRestantes)));
            }
        }

        public ERango NP
        {
            get => mNP;
            set => mNP = value;
        }

        public bool CheckUsarRangos
        {
            get => mCheckUsarRangos;
            set
            {
                mCheckUsarRangos = value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(UsarRangos)));
            }
        }
    }
}