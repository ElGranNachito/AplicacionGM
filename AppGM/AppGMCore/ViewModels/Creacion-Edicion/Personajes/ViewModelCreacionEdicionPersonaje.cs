using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoolLogs;

namespace AppGM.Core
{
    /// <summary>
    /// Viewmodel para la creacion de un personaje
    /// </summary>
    public class ViewModelCreacionEdicionPersonaje : ViewModelCreacionEdicionDeModelo<ModeloPersonaje, ControladorPersonaje, ViewModelCreacionEdicionPersonaje>
    {
        #region Campos & Propiedades

        private bool mCheckUsarRangos = false;

        private Action mAccionAñadirHabilidad = delegate{};
        private Action mAccionAñadirItem      = delegate{};
        private Action mAccionAñadirNP        = delegate{};

        private ERango mNP  { get; set; } = ERango.F;

        private BitVector32 mValoresSolapas = new BitVector32(1);

        public string Nombre
		{
            get => ModeloCreado.Nombre;
            set => ModeloCreado.Nombre = value;
		}

        public string NombreReal
		{
            get
			{
                if (ModeloCreado is ModeloPersonajeJugable pjJugable)
                    return pjJugable.NombreReal;

                return string.Empty;
			}
			set
			{
                if (ModeloCreado is ModeloPersonajeJugable pjJugable)
                    pjJugable.NombreReal = value;
            }
		}

        public object Imagen
        {
			get
			{
                if(ModeloCreado.Imagen == null)
                    return Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioImagenes, "camarita.png");

                return ModeloCreado.Imagen;
			}
			set
			{
				if (value is byte[] bytesImagen)
					ModeloCreado.Imagen = bytesImagen;
			}
        }

        public string Fisico
        {
	        get
	        {
		        if (ModeloCreado is ModeloPersonajeJugable pjJugable)
			        return pjJugable.Caracteristicas.Fisico;

		        return string.Empty;
	        }

	        set
	        {
		        if (ModeloCreado is ModeloPersonajeJugable pjJugable)
			        pjJugable.Caracteristicas.Fisico = value;
            }
        }

        public string Origen
        {
	        get
	        {
		        if (ModeloCreado is ModeloPersonajeJugable pjJugable)
			        return pjJugable.Caracteristicas.Origen;

		        return string.Empty;
	        }

	        set
	        {
		        if (ModeloCreado is ModeloPersonajeJugable pjJugable)
			        pjJugable.Caracteristicas.Origen = value;
	        }
        }

        public string Afinidad
        {
	        get
	        {
		        if (ModeloCreado is ModeloPersonajeJugable pjJugable)
			        return pjJugable.Caracteristicas.Afinidad;

		        return string.Empty;
	        }

	        set
	        {
		        if (ModeloCreado is ModeloPersonajeJugable pjJugable)
			        pjJugable.Caracteristicas.Afinidad = value;
	        }
        }

        public string TextoPuntosDeHabilidadRestantes => $"Puntos de habilidad restantes: {PuntosHabilidadRestantes}";

        public int PuntosHabilidadRestantes
		{
			get
			{
                var puntosRestantes = 75 - ModeloCreado.Str - ModeloCreado.End - ModeloCreado.Agi - ModeloCreado.Int - ModeloCreado.Lck;

                if (ModeloCreado is ModeloMaster master)
                    puntosRestantes -= master.Chr;

                return puntosRestantes;
            }
        } 

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
			        MostrarPartesDelCuerpo = false;
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
			        MostrarPartesDelCuerpo = false;
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
			        MostrarPartesDelCuerpo = false;
		        }

		        mValoresSolapas[4] = value;
            }
        }

        public bool MostrarPartesDelCuerpo
        {
	        get => mValoresSolapas[8];
	        set
	        {
		        if (value == mValoresSolapas[8])
			        return;

		        if (value)
		        {
			        MostrarCaracteristicasGenerales = false;
			        MostrarInventario = false;
			        MostrarHabilidades = false;

			        SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = new ViewModelCreacionPartesDelCuerpo(
				        vm =>
				        {
					        SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = this;

					        MostrarCaracteristicasGenerales = true;

				        }, ModeloCreado);
		        }

		        mValoresSolapas[8] = value;
	        }
        }

        public ViewModelListaItems<ViewModelItemListaItems> ContenedorListaItems                { get; set; }
        public ViewModelListaItems<ViewModelHabilidadItem> ContenedorListaHabilidades { get; set; }
        public ViewModelListaItems<ViewModelHabilidadItem> ContendorListaNPs          { get; set; }

        public ViewModelComboBox<ETipoPersonaje> ComboBoxTipoPersonaje { get; set; } = new ViewModelComboBox<ETipoPersonaje>(EnumHelpers.TiposDePersonajesDisponibles);
        public ViewModelComboBox<EClaseServant>  ComboBoxClaseServant  { get; set; } = new ViewModelComboBox<EClaseServant>(EnumHelpers.ClasesDisponibles);
        public ViewModelComboBox<EArquetipo>     ComboBoxArquetipo     { get; set; } = new ViewModelComboBox<EArquetipo>(EnumHelpers.ArquetiposDisponibles);
        public ViewModelComboBox<ESexo>          ComboBoxSexo          { get; set; } = new ViewModelComboBox<ESexo>(EnumHelpers.SexosDisponibles);
        public ViewModelComboBox<EManoDominante> ComboBoxManoDominante { get; set; } = new ViewModelComboBox<EManoDominante>(EnumHelpers.TiposDeManoDominanteDisponibles);

        public List<EClaseServant>  ClasesDeServantDisponibles => ObtenerClasesDeServantDisponibles();

        public ICommand ComandoSeleccionarImagen { get; set; }

        public ICommand ComandoAñadirPerk  { get; set; }
        public ICommand ComandoAñadirSkill { get; set; }
        public ICommand ComandoAñadirMagia { get; set; }
        public ICommand ComandoAñadirNP    { get; set; }

        public ICommand ComandoActualizarStats { get; set; }

        #endregion

        #region Constructor

        public ViewModelCreacionEdicionPersonaje(Action<ViewModelCreacionEdicionPersonaje> _accionSalir, ControladorPersonaje _personajeEditar = null)
			:base(_accionSalir, _personajeEditar)
        {
	        CrearComandoFinalizar();

            mAccionAñadirHabilidad = async () =>
	        {
		        SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = await new ViewModelCrearHabilidad( 
			        async vm =>
			        {
				        if (vm.Resultado.EsAceptarOFinalizar() || vm.EstaEditando)
				        {
					        var nuevaHabilidad = vm.CrearControlador();

                            AñadirModeloDesdeListaItems<ModeloHabilidad, ViewModelHabilidadItem>((ViewModelHabilidadItem)nuevaHabilidad.CrearViewModelItem(), ModeloCreado.Habilidades, ContenedorListaHabilidades);
				        }
				        SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = this;
			        }, ModeloCreado).Inicializar();
	        };

            ContenedorListaHabilidades = new ViewModelListaItems<ViewModelHabilidadItem>(mAccionAñadirHabilidad, true, "Habilidades");
            ContendorListaNPs          = new ViewModelListaItems<ViewModelHabilidadItem>(mAccionAñadirNP, true, "NPs");

            ContenedorListaItems       = new ViewModelListaItems<ViewModelItemListaItems>(mAccionAñadirItem, true, "Items");

            var modeloHabilidad = new ModeloHabilidad
            {
	            Nombre = "Nada interesante", TipoDeHabilidad = ETipoHabilidad.Skill,
	            Dueño = ModeloCreado
            };

            ContenedorListaHabilidades.Items.Elementos.Add(new ViewModelHabilidadItem(new ControladorHabilidad(modeloHabilidad)));

            ComboBoxTipoPersonaje.OnValorSeleccionadoCambio += async (anterior, actual) =>
            {
	            var modeloAnterior = ModeloCreado;

	            ModeloCreado = (await ModeloCreado.CrearCopiaProfundaEnSubtipoAsync(actual.valor.ObtenerTipoPersonaje())).resultado as ModeloPersonaje;

                if (!EstaEditando)
                {
	                await modeloAnterior.Eliminar();

		            await SistemaPrincipal.GuardarModeloAsync(ModeloCreado);
	            }

                ModeloCreado.TipoPersonaje = actual.valor;

                if (ModeloCreado is ModeloPersonajeJugable p)
                {
	                p.Caracteristicas = new ModeloCaracteristicas
	                {
                        Personaje = p
	                };
                }

                await SistemaPrincipal.GuardarDatosAsync();

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(ClasesDeServantDisponibles)));
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(EsMasterOServant)));
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(EsServant)));
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(EsMaster)));
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(EsInvocacion)));
                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(UsarRangos)));
            };

            ComboBoxArquetipo.OnValorSeleccionadoCambio += (anterior, actual) =>
            {
                if (ModeloCreado is ModeloPersonajeJugable pJugable)
                    pJugable.Caracteristicas.Arquetipo = actual.valor;
            };

            ComboBoxClaseServant.OnValorSeleccionadoCambio += (anterior, actual) =>
            {
                if (ModeloCreado is ModeloPersonajeJugable pJugable)
                    pJugable.ClaseServant = actual.valor;
            };

            ComboBoxManoDominante.OnValorSeleccionadoCambio += (anterior, actual) =>
            {
                if (ModeloCreado is ModeloPersonajeJugable pJugable)
                    pJugable.Caracteristicas.ManoDominante = actual.valor;
            };

            ComboBoxSexo.OnValorSeleccionadoCambio += (anterior, actual) =>
            {
                if (ModeloCreado is ModeloPersonajeJugable pJugable)
                    pJugable.Caracteristicas.Sexo = actual.valor;
            };

            ComandoActualizarStats = new Comando(ActualizarStatsPersonaje);
            
            ComandoCancelar = new Comando(() =>
            {
	            Resultado = EResultadoViewModel.Cancelar;

	            mAccionSalir(this);
            });

            ComandoSeleccionarImagen = new Comando(() =>
            {
	            var archivoSeleccionado = SistemaPrincipal.ControladorDeArchivos.MostrarDialogoAbrirArchivo(
		            "Seleccionar imagen para personaje", "Formatos de imagen (*.jpg, *.png)|*.jpg;*.png",
		            SistemaPrincipal.Aplicacion.VentanaActual);

                if(archivoSeleccionado == null)
                    return;

	            try
	            {
		            using BinaryReader bReader =
			            new BinaryReader(File.Open(archivoSeleccionado.Ruta, FileMode.Open, FileAccess.Read));

		            Imagen = bReader.ReadBytes((int) bReader.BaseStream.Length);
	            }
	            catch (Exception ex)
	            {
		            SistemaPrincipal.LoggerGlobal.Log(
			            $"Error al intentar leer imagen {archivoSeleccionado.Nombre}.{Environment.NewLine}{ex.Message}", ESeveridad.Error);
	            }
            });

            PropertyChanged += (sender, args) =>
            {
	            if (args.PropertyName != nameof(EsValido))
		            ActualizarValidez();
            };
        }

        #endregion

		#region Metodos

		public override async Task<ViewModelCreacionEdicionPersonaje> Inicializar(Type tipoValorPorDefectoModelo = null)
		{
			await base.Inicializar(tipoValorPorDefectoModelo);

			if (!EstaEditando)
			{
				await SistemaPrincipal.GuardarModeloAsync(ModeloCreado);

				ModeloCreado.Rol = SistemaPrincipal.ModeloRolActual;

                await SistemaPrincipal.GuardarDatosAsync();
			}

			return this;
		}

		public override ModeloPersonaje CrearModelo()
		{
			ActualizarValidez();

			if (!EsValido)
				return null;

            return ModeloCreado;
		}

		public override ControladorPersonaje CrearControlador()
		{
			var modeloCreado = CrearModelo();

			return modeloCreado == null ? null : SistemaPrincipal.ObtenerControlador<ControladorPersonaje, ModeloPersonaje>(modeloCreado, true);
		}

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
	        var rolActual = SistemaPrincipal.ModeloRolActual;

            for (int i = 0; i < rolActual.Personajes.Count; ++i)
            {
                if (rolActual.Personajes[i] is ModeloPersonajeJugable mpj)
                    if (mpj.ClaseServant == claseDeseada && TipoPersonajeSeleccionado == mpj.TipoPersonaje)
                        return false;
            }

            return true;
        }

        public override void ActualizarValidez()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
			{
                EsValido = false;

                return;
			}

            if (EsServant)
            {
                //Omitimos algunas stats porque pueden existir servants que no posean algunas stats

                if ((ERango)END == ERango.NINGUNO)
				{
                    EsValido = false;

                    return;
				}
                if ((ERango)INT == ERango.NINGUNO)
                {
                    EsValido = false;

                    return;
                }
                if ((ERango)LCK == ERango.NINGUNO)
                {
                    EsValido = false;

                    return;
                }
                if (NP == ERango.NINGUNO)
                {
                    EsValido = false;

                    return;
                }
                if (ArquetipoSeleccionado == EArquetipo.NINGUNO)
                {
                    EsValido = false;

                    return;
                }
            }

            if (EsMasterOServant && (ClaseServantSeleccionada == EClaseServant.NINGUNO || ArquetipoSeleccionado == EArquetipo.NINGUNO))
            {
                EsValido = false;

                return;
            }

            EsValido = true;
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

        private void Guardar()
		{
           
		}

        #endregion

        public int HP
        {
            get => ModeloCreado.Hp;
            set => ModeloCreado.Hp = value;
        }

        public int STR
        {
            get => ModeloCreado.Str;
            set
            {
                ModeloCreado.Str = value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(TextoPuntosDeHabilidadRestantes)));
            }
        }

        public int END
        {
            get => ModeloCreado.End;
            set
            {
                ModeloCreado.End = value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(TextoPuntosDeHabilidadRestantes)));
            }
        }

        public int AGI
        {
            get => ModeloCreado.Agi;
            set
            {
                ModeloCreado.Agi = value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(TextoPuntosDeHabilidadRestantes)));
            }
        }

        public int INT
        {
            get => ModeloCreado.Int;
            set
            {
                ModeloCreado.Int = value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(TextoPuntosDeHabilidadRestantes)));
            }
        }

        public int LCK
        {
            get => ModeloCreado.Lck;
            set
            {
                ModeloCreado.Lck = value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(TextoPuntosDeHabilidadRestantes)));
            }
        }

        public int CHR
        {
            get
			{
                if (ModeloCreado is ModeloMaster m)
                    return m.Chr;

                return 0;
			}
            set
            {
                if (ModeloCreado is ModeloMaster m)
                {
                    m.Chr = value;

                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(TextoPuntosDeHabilidadRestantes)));
                }
            }
        }

        public int Edad
        {
            get
			{
                if (ModeloCreado is ModeloPersonajeJugable pJugable)
                    return pJugable.Caracteristicas.Edad;

                return 0;
			}
			set
			{
                if (ModeloCreado is ModeloPersonajeJugable pJugable)
                    pJugable.Caracteristicas.Edad = value;
            }
        }

        public int Estatura
        {
            get
            {
                if (ModeloCreado is ModeloPersonajeJugable pJugable)
                    return pJugable.Caracteristicas.Estatura;

                return 0;
            }
            set
			{
                if (ModeloCreado is ModeloPersonajeJugable pJugable)
                    pJugable.Caracteristicas.Estatura = value;
            }
        }

        public int Peso
        {
            get
            {
                if (ModeloCreado is ModeloPersonajeJugable pJugable)
                    return pJugable.Caracteristicas.Peso;

                return 0;
            }
            set
            {
                if (ModeloCreado is ModeloPersonajeJugable pJugable)
                    pJugable.Caracteristicas.Peso = value;
            }
        }


        public ERango NP
        {
            get
			{
                if (ModeloCreado is ModeloServant servant)
                    return servant.RangoNP;

                return ERango.NINGUNO;
            }
            set
			{
                if (ModeloCreado is ModeloServant servant)
                    servant.RangoNP = value;
            }
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