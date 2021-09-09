using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// Viewmodel para la creacion de un personaje
    /// </summary>
    public class ViewModelCrearPersonaje : ViewModelConResultado<ViewModelCrearPersonaje>
    {
        #region Miembros

        private int    mHP  { get; set; } = 20;
        private ushort mSTR { get; set; } = 10;
        private ushort mEND { get; set; } = 10;
        private ushort mAGI { get; set; } = 10;
        private ushort mINT { get; set; } = 10;
        private ushort mLCK { get; set; } = 10;
        private ushort mCHR { get; set; } = 10;

        private ushort mEdad     { get; set; }
        private ushort mEstatura { get; set; }
        private ushort mPeso     { get; set; }

        private bool mCheckUsarRangos = false;

        private Action mAccionAñadirHabilidad = delegate{};
        private Action mAccionAñadirItem      = delegate{};
        private Action mAccionAñadirNP        = delegate{};

        private ERango mNP  { get; set; } = ERango.F;

        private BitVector32 mValoresSolapas = new BitVector32(1);

        #endregion

        #region Propiedades

        /// <summary>
        /// Controlador del personaje creado
        /// </summary>
        public ControladorPersonaje Resultado { get; private set; }

        /// <summary>
        /// Modelo del personaje creado
        /// </summary>
        public ModeloPersonaje ModeloPersonaje { get; private set; }

        public ModeloPersonaje ModeloPersonajeSiendoEditado { get; private set; }
            
        public string Nombre     { get; set; } = "Mr Sin Nombre";
        public string NombreReal { get; set; } = "Nadie";
        public string PathImagen { get; set; } = Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioImagenes, "camarita.png");
        public string TextoPuntosDeHabilidadRestantes => $"Puntos de habilidad restantes: {PuntosHabilidadRestantes}";

        public int PuntosHabilidadRestantes => 75 - mSTR - mEND - mAGI - mINT - mLCK - mCHR;

        public ETipoPersonaje TipoPersonajeSeleccionado => ComboBoxTipoPersonaje.Valor;
        public EClaseServant  ClaseServantSeleccionada  => ComboBoxClaseServant.Valor;
        public EArquetipo     ArquetipoSeleccionado     => ComboBoxArquetipo.Valor;
        public ESexo          SexoSeleccionado          => ComboBoxSexo.Valor;

        public bool EsMasterOServant => (TipoPersonajeSeleccionado & (ETipoPersonaje.Master | ETipoPersonaje.Servant)) != 0;
        public bool EsServant    => TipoPersonajeSeleccionado == ETipoPersonaje.Servant;
        public bool EsMaster     => TipoPersonajeSeleccionado == ETipoPersonaje.Master;
        public bool EsInvocacion => TipoPersonajeSeleccionado == ETipoPersonaje.Invocacion;
        public bool UsarRangos   => EsServant || (EsInvocacion && CheckUsarRangos);
        public bool PuedeAñadirHabilidades => true;
        public bool EstaEditandoModeloExistente => ModeloPersonajeSiendoEditado != null;

        public bool PuedeFinalizar => PuedeCrearPersonaje();

        public bool MostrarCaracteristicasGenerales
        {
	        get => mValoresSolapas[1];
	        set
	        {
		        if (value == mValoresSolapas[1])
			        return;

		        if (value)
		        {
			        MostrarInventario  = false;
			        MostrarHabilidades = false;
		        }

		        mValoresSolapas[1] = value;
	        }
        }

        public bool MostrarInventario
        {
	        get => mValoresSolapas[2];
	        set
	        {
		        if (value == mValoresSolapas[2])
			        return;

		        if (value)
		        {
			        MostrarCaracteristicasGenerales = false;
			        MostrarHabilidades = false;
		        }

		        mValoresSolapas[2] = value;
            }
        }

        public bool MostrarHabilidades
        {
	        get => mValoresSolapas[4];
	        set
	        {
		        if (value == mValoresSolapas[4])
			        return;

		        if (value)
		        {
			        MostrarCaracteristicasGenerales = false;
			        MostrarInventario = false;
		        }

		        mValoresSolapas[4] = value;
            }
        }

        public ViewModelListaItems<ViewModelItem> ContenedorListaItems                { get; set; }
        public ViewModelListaItems<ViewModelHabilidadItem> ContenedorListaHabilidades { get; set; }
        public ViewModelListaItems<ViewModelHabilidadItem> ContendorListaNPs          { get; set; }

        public ViewModelComboBox<ETipoPersonaje> ComboBoxTipoPersonaje { get; set; } = new ViewModelComboBox<ETipoPersonaje>(EnumHelpers.TiposDePersonajesDisponibles);
        public ViewModelComboBox<EClaseServant>  ComboBoxClaseServant  { get; set; } = new ViewModelComboBox<EClaseServant>(EnumHelpers.ClasesDisponibles);
        public ViewModelComboBox<EArquetipo>     ComboBoxArquetipo     { get; set; } = new ViewModelComboBox<EArquetipo>(EnumHelpers.ArquetiposDisponibles);
        public ViewModelComboBox<ESexo>          ComboBoxSexo          { get; set; } = new ViewModelComboBox<ESexo>(EnumHelpers.SexosDisponibles);
        public ViewModelComboBox<EManoDominante> ComboBoxManoDominante { get; set; } = new ViewModelComboBox<EManoDominante>(EnumHelpers.TiposDeManoDominanteDisponibles);

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

        public ViewModelCrearPersonaje(Action<ViewModelCrearPersonaje> _accionSalir, ModeloPersonaje _modeloPersonaje = null)
			:base(_accionSalir)
        {
	        if (_modeloPersonaje != null)
	        {
		        ModeloPersonajeSiendoEditado = _modeloPersonaje;

		        ModeloPersonaje = (ModeloPersonaje) ModeloPersonajeSiendoEditado.Clonar();

                DispararPropertyChanged(nameof(EstaEditandoModeloExistente));
	        }
	        else
		        ModeloPersonaje = new ModeloPersonaje();

	        mAccionAñadirHabilidad = () =>
	        {
		        SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = new ViewModelCrearHabilidad(ModeloPersonaje, 
			        vm =>
			        {
				        SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = this;
			        });
	        };

            ContenedorListaHabilidades = new ViewModelListaItems<ViewModelHabilidadItem>(mAccionAñadirHabilidad, true, "Habilidades");
            ContendorListaNPs          = new ViewModelListaItems<ViewModelHabilidadItem>(mAccionAñadirNP, true, "NPs");

            ContenedorListaItems       = new ViewModelListaItems<ViewModelItem>(mAccionAñadirItem, true, "Items");

            var modeloHabilidad = new ModeloHabilidad
            {
	            Nombre = "Nada interesante", TipoDeHabilidad = ETipoHabilidad.Skill,
	            Dueño = new TIPersonajeHabilidad {Personaje = ModeloPersonaje}
            };

            ContenedorListaHabilidades.Items.Elementos.Add(new ViewModelHabilidadItem(new ControladorHabilidad(modeloHabilidad)));

            ComboBoxTipoPersonaje.OnValorSeleccionadoCambio += (anterior, actual) =>
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

                mAccionSalir(this);
            });

            ComandoCancelar = new Comando(()=>mAccionSalir(this));
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
            //for (int i = 0; i < mDatosCreacionRol.personajes.Count; ++i)
            //{
            //    if (mDatosCreacionRol.personajes[i] is ModeloPersonajeJugable mpj)
            //        if (mpj.EClaseServant == claseDeseada && TipoPersonajeSeleccionado == mpj.TipoPersonaje)
            //            return false;
            //}

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
                if (ArquetipoSeleccionado == EArquetipo.NINGUNO)
                    return false;
            }

            if (EsMasterOServant && (ClaseServantSeleccionada == EClaseServant.NINGUNO || ArquetipoSeleccionado == EArquetipo.NINGUNO))
                return false;

            return true;
        }

        private void ActualizarStatsPersonaje()
        {
            //if (mModeloPersonaje == null)
            //    mModeloPersonaje = Creador.CrearPersonaje(TipoPersonajeSeleccionado);

            //mModeloPersonaje.MaxHp = mHP;
            //mModeloPersonaje.Str   = mSTR;
            //mModeloPersonaje.End   = mEND;
            //mModeloPersonaje.Agi   = mAGI;
            //mModeloPersonaje.Int   = mINT;
            //mModeloPersonaje.Lck   = mLCK;

            //if (EsServant)
            //    ((ModeloServant)mModeloPersonaje).mERangoNP = mNP;
            //else if (EsMaster)
            //    ((ModeloMaster) mModeloPersonaje).Chr = mCHR;

            //DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuedeAñadirHabilidades)));
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

        public ushort Edad
        {
            get => mEdad;
            set => mEdad = value;
        }

        public ushort Estatura
        {
            get => mEstatura;
            set => mEstatura = value;
        }

        public ushort Peso
        {
            get => mPeso;
            set => mPeso = value;
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