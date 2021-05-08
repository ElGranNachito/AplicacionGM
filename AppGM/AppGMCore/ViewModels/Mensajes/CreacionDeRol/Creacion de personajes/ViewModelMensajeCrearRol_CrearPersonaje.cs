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
        private ModeloPersonaje          mModeloPersonaje;

        private int    mHP  { get; set; } = 20;
        private ushort mSTR { get; set; } = 10;
        private ushort mEND { get; set; } = 10;
        private ushort mAGI { get; set; } = 10;
        private ushort mINT { get; set; } = 10;
        private ushort mLCK { get; set; } = 10;
        private ushort mCHR { get; set; } = 10;

        private bool mCheckUsarRangos = false;

        private Action mAccionAñadirHabilidad = delegate{};
        private Action mAccionAñadirItem      = delegate{};
        private Action mAccionAñadirNP        = delegate{};

        private ERango mNP  { get; set; } = ERango.F;

        #endregion

        #region Propiedades

        public string Nombre     { get; set; } = "Mr Sin Nombre";
        public string NombreReal { get; set; } = "Nadie";
        public string PathImagen { get; set; } = Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioImagenes, "camarita.png");
        public string TextoPuntosDeHabilidadRestantes => $"Puntos de habilidad restantes: {PuntosHabilidadRestantes}";

        public int PuntosHabilidadRestantes => 75 - mSTR - mEND - mAGI - mINT - mLCK - mCHR;

        public ETipoPersonaje TipoPersonajeSeleccionado => VMSeleccionTipoPersonaje.OpcionSeleccionada;
        public EClaseServant  ClaseServantSeleccionada  => VMSeleccionClaseServant.OpcionSeleccionada;
        public EAlineamiento  AlineamientoSeleccionado  => VMSeleccionAlineamito.OpcionSeleccionada;
        public ESexo          SexoSeleccionado          => VMSeleccionSexo.OpcionSeleccionada;

        public bool EsMasterOServant => (TipoPersonajeSeleccionado & (ETipoPersonaje.Master | ETipoPersonaje.Servant)) != 0;
        public bool EsServant    => TipoPersonajeSeleccionado == ETipoPersonaje.Servant;
        public bool EsMaster     => TipoPersonajeSeleccionado == ETipoPersonaje.Master;
        public bool EsInvocacion => TipoPersonajeSeleccionado == ETipoPersonaje.Invocacion;
        public bool UsarRangos   => EsServant || (EsInvocacion && CheckUsarRangos);
        public bool PuedeAñadirHabilidades => mModeloPersonaje != null;

        public bool PuedeFinalizar => PuedeCrearPersonaje();

        public ViewModelListaItems ContenedorListaItems       { get; set; }
        public ViewModelListaItems ContenedorListaHabilidades { get; set; }
        public ViewModelListaItems ContendorListaNPs          { get; set; }

        public ViewModelListaDeHabilidades ListaHabilidades { get; set; }

        public ViewModelComboBoxConDescripcion<ETipoPersonaje> VMSeleccionTipoPersonaje { get; set; } = new ViewModelComboBoxConDescripcion<ETipoPersonaje>();
        public ViewModelComboBoxConDescripcion<EClaseServant>  VMSeleccionClaseServant  { get; set; } = new ViewModelComboBoxConDescripcion<EClaseServant>();
        public ViewModelComboBoxConDescripcion<EAlineamiento>  VMSeleccionAlineamito    { get; set; } = new ViewModelComboBoxConDescripcion<EAlineamiento>();
        public ViewModelComboBoxConDescripcion<ESexo>          VMSeleccionSexo          { get; set; } = new ViewModelComboBoxConDescripcion<ESexo>();
        public ViewModelComboBoxConDescripcion<EManoDominante> VMSeleccionManoDominante { get; set; } = new ViewModelComboBoxConDescripcion<EManoDominante>();

        public List<EClaseServant>  ClasesDeServantDisponibles => ObtenerClasesDeServantDisponibles();

        public ICommand ComandoConfirmar { get; set; }
        public ICommand ComandoCancelar  { get; set; }

        public ICommand ComandoSeleccionarImagen { get; set; }

        public ICommand ComandoAñadirPerk  { get; set; }
        public ICommand ComandoAñadirSkill { get; set; }
        public ICommand ComandoAñadirMagia { get; set; }
        public ICommand ComandoAñadirNP    { get; set; }

        public ICommand ComandoActualizarStats { get; set; }

        #endregion

        #region Constructor

        public ViewModelMensajeCrearRol_CrearPersonaje(DatosCreacionRol _datosCreacionRol, ViewModelMensajeCrearRol _viewModelCrearRol)
        {
            mDatosCreacionRol  = _datosCreacionRol;
            mViewModelCrearRol = _viewModelCrearRol;

            mAccionAñadirHabilidad = () =>
            {
                SistemaPrincipal.Aplicacion.VentanaPopups.EstablecerViewModel(new ViewModelMensajeCrearRol_CrearHabilidad(this, mModeloPersonaje));
            };

            ContenedorListaHabilidades = new ViewModelListaItems(mAccionAñadirHabilidad, true, "Habilidades");
            ContenedorListaItems       = new ViewModelListaItems(mAccionAñadirItem, true, "Items");
            ContendorListaNPs          = new ViewModelListaItems(mAccionAñadirNP, true, "NPs");

            VMSeleccionTipoPersonaje.PropertyChanged += (sender, args) =>
            {
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(ClasesDeServantDisponibles)));
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(EsMasterOServant)));
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(EsServant)));
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(EsMaster)));
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(EsInvocacion)));
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(UsarRangos)));
            };

            PropertyChanged += (sender, args) =>
            {
                if(args.PropertyName != nameof(PuedeFinalizar))
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuedeFinalizar)));
            };

            ComandoActualizarStats = new Comando(ActualizarStatsPersonaje);

            ComandoConfirmar = new Comando(() =>
            {
                CrearPersonaje();

                mDatosCreacionRol.personajes.Add(mModeloPersonaje);

                switch (mModeloPersonaje.TipoPersonaje)
                {
                    case ETipoPersonaje.Master:
                        mDatosCreacionRol.masters.Add(mModeloPersonaje);
                        break;
                    case ETipoPersonaje.Servant:
                        mDatosCreacionRol.servants.Add(mModeloPersonaje);
                        break;
                    case ETipoPersonaje.Invocacion:
                        mDatosCreacionRol.invocaciones.Add(mModeloPersonaje);
                        break;
                    default:
                        mDatosCreacionRol.npcs.Add(mModeloPersonaje);
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

                //Omitimos algunas stats porque pueden existir servants que no posean algunas stats

                if ((ERango)END == ERango.NINGUNO)
                    return false;
                if ((ERango)INT == ERango.NINGUNO)
                    return false;
                if ((ERango)LCK == ERango.NINGUNO)
                    return false;
                if (NP == ERango.NINGUNO)
                    return false;
                if (AlineamientoSeleccionado == EAlineamiento.NINGUNO)
                    return false;
            }

            if (EsMasterOServant && (ClaseServantSeleccionada == EClaseServant.NINGUNO || AlineamientoSeleccionado == EAlineamiento.NINGUNO))
                return false;

            return true;
        }

        private void ActualizarStatsPersonaje()
        {
            if (mModeloPersonaje == null)
                mModeloPersonaje = Creador.CrearPersonaje(TipoPersonajeSeleccionado);

            mModeloPersonaje.MaxHp = mHP;
            mModeloPersonaje.Str   = mSTR;
            mModeloPersonaje.End   = mEND;
            mModeloPersonaje.Agi   = mAGI;
            mModeloPersonaje.Int = mINT;
            mModeloPersonaje.Lck   = mLCK;

            if (EsServant)
                ((ModeloServant)mModeloPersonaje).mERangoNP = mNP;
            else if (EsMaster)
                ((ModeloMaster) mModeloPersonaje).Chr = mCHR;

            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuedeAñadirHabilidades)));
        }

        private void CrearPersonaje()
        {
            
        }

        #endregion

        public int HP
        {
            get => mHP;
            set => mHP = value;
        }

        public ushort STR
        {
            get => mSTR;
            set
            {
                mSTR = value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuntosHabilidadRestantes)));
            }
        }

        public ushort END
        {
            get => mEND;
            set
            { 
                mEND = value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuntosHabilidadRestantes)));
            }
        }

        public ushort AGI
        {
            get => mAGI;
            set
            {
                mAGI = value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuntosHabilidadRestantes)));
            }
        }

        public ushort INT
        {
            get => mINT;
            set
            {
                mINT = value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuntosHabilidadRestantes)));
            }
        }

        public ushort LCK
        {
            get => mLCK;
            set
            {
                mLCK = value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuntosHabilidadRestantes)));
            }
        }

        public ushort CHR
        {
            get => mCHR;
            set
            {
                mCHR = value;

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