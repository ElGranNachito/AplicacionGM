using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// Representa una habilidad en un <see cref="ViewModelCrearPersonaje"/>
    /// </summary>
    public class ViewModelCrearHabilidad : ViewModelConResultado<ViewModelCrearHabilidad>
    {
        #region Miembros

        /// <summary>
        /// Personaje para el que estamos creando la habilidad
        /// </summary>
        private ModeloPersonaje         mModeloPersonaje;

        private string mCostoDeMana  = "0";
        private string mCostoDeOd    = "0";
        private string mCostoDePrana = "0";

        #endregion

        #region Propiedades

        public ModeloHabilidad ModeloHabilidad { get; private set; }

        public ControladorHabilidad HabilidadSiendoEditada { get; private set; }

        public string TextoNivelMagia => $"Lv.{ObtenerNivelDeMagia()}";

        public bool PuedeFinalizar => PuedeFinalizarCreacion();
        public bool EsMagia        => ComboBoxTipoHabilidad.Valor == ETipoHabilidad.Hechizo;
        public bool PuedeElegirSiEsMagiaParticular => PuedeAñadirMagiasParticulares();
        public bool PuedeElegirSiTieneRango => !EsMagia && !RequiereRango;

        public bool RequiereRango { get; set; }

        public bool UtilizaPrana => EsMagia && (mModeloPersonaje.TipoPersonaje & (ETipoPersonaje.Servant | ETipoPersonaje.Invocacion)) != 0;
        public bool UtilizaOd => EsMagia && (mModeloPersonaje.TipoPersonaje & (ETipoPersonaje.Servant | ETipoPersonaje.NPC)) != 0;

        public bool EstaEditandoHabilidadExistente => HabilidadSiendoEditada != null;
        
        public string CostoDeMana
        {
            get => mCostoDeMana;
            set
            {
                mCostoDeMana = value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(TextoNivelMagia)));
            }
        }

        public string CostoDeOd
        {
	        get => mCostoDeOd;
            set
            {
                mCostoDeOd = value;

                DispararPropertyChanged(new PropertyChangedEventArgs(nameof(TextoNivelMagia)));
            }
        }

        public string CostoDePrana
        {
	        get => mCostoDePrana;
	        set
	        {
		        mCostoDeOd = value;

		        DispararPropertyChanged(new PropertyChangedEventArgs(nameof(TextoNivelMagia)));
            }
        }

        public ViewModelListaItems<ViewModelItemLista> ContenedorListaEfectos { get; set; }
        public ViewModelListaItems<ViewModelItemLista> ContenedorListaTiradas { get; set; }

        public ViewModelListaItems<ViewModelVariableItem> ContenedorListaVariables  { get; set; }

        public ViewModelComboBox<ETipoHabilidad> ComboBoxTipoHabilidad { get; set; }

        public ViewModelComboBox<ERango> ComboBoxRangoHabilidad { get; set; } = new ViewModelComboBox<ERango>(EnumHelpers.RangosDisponibles);

        public ViewModelFuncionItem<ControladorFuncion_Habilidad> FuncionUtilizar { get; set; }
        public ViewModelFuncionItem<ControladorFuncion_PredicadoEfecto> FuncionCondicion { get; set; }

        #endregion

        #region Constructor

        public ViewModelCrearHabilidad(ModeloPersonaje _modeloPersonaje, Action<ViewModelCrearHabilidad> accionSalir, ControladorHabilidad _habilidad = null)    
			:base(accionSalir)
        {
	        mModeloPersonaje  = _modeloPersonaje;

            if (_habilidad != null)
            {
                HabilidadSiendoEditada = _habilidad;

                ModeloHabilidad = HabilidadSiendoEditada.modelo.Clonar() as ModeloHabilidad;

                DispararPropertyChanged(nameof(EstaEditandoHabilidadExistente));
            }
            else
            {
	            ModeloHabilidad = new ModeloHabilidad();

	            ModeloHabilidad.Dueño = mModeloPersonaje;
            }

            ComboBoxTipoHabilidad = new ViewModelComboBox<ETipoHabilidad>(_modeloPersonaje.TipoPersonaje.ObtenerTiposDeHabilidadDisponibles());

            ComboBoxTipoHabilidad.OnValorSeleccionadoCambio += (anterior, actual) =>
            {
	            DispararPropertyChanged(nameof(EsMagia));
                DispararPropertyChanged(nameof(UtilizaOd));
                DispararPropertyChanged(nameof(UtilizaPrana));
                DispararPropertyChanged(nameof(PuedeElegirSiTieneRango));
            };

            FuncionUtilizar = new ViewModelFuncionItem<ControladorFuncion_Habilidad>(new ControladorFuncion_Habilidad(new ModeloFuncion{NombreFuncion = "CoolerFunc"}));
            //FuncionCondicion = new ViewModelFuncionItem<ControladorFuncion_Predicado>(null);

            ContenedorListaEfectos   = new ViewModelListaItems<ViewModelItemLista>(()=>{}, true, "Efectos");

            ContenedorListaTiradas   = new ViewModelListaItems<ViewModelItemLista>(()=>
            {            
                SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = new ViewModelCrearTirada((vm) =>
                {
                    SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = this;

                }, mModeloPersonaje, ContenedorListaVariables.Items.ToList(), null);

            }, true, "Tiradas");

            ContenedorListaVariables = new ViewModelListaItems<ViewModelVariableItem>(() =>
            {              
	            SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = new ViewModelCreacionDeVariable((vm) =>
	            {
		            if (vm.Resultado == EResultadoViewModel.Aceptar)
		            {
                        AñadirVariable((ViewModelVariableItem)vm.CrearVariable().CrearViewModelItem());
		            }

		            SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = this;
	            });
            }, true, "Variables");

            ComandoCancelar = new Comando(() =>
            {
	            Resultado = EResultadoViewModel.Aceptar;

                accionSalir(this);
            });

            ComandoAceptar = new Comando(() =>
            {
	            Resultado = EResultadoViewModel.Cancelar;

	            accionSalir(this);
            });

            PropertyChanged += (sender, args) =>
            {
                if(args.PropertyName != nameof(PuedeFinalizar))
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuedeFinalizar)));
            };
        }

        #endregion

        #region Funciones

        private void FinalizarCreacion()
        {
            
        }

        private bool PuedeFinalizarCreacion()
        {
            return false;
        }

        private bool PuedeAñadirMagiasParticulares()
        {
            if (!EsMagia || mModeloPersonaje.TipoPersonaje != ETipoPersonaje.Master)
                return false;

            if (mModeloPersonaje.Magias.Count(m => m.EsParticular) >= 2)
                return false;

            return true;
        }

        private byte ObtenerNivelDeMagia()
        {
	        int costo = 0;

            if((mModeloPersonaje.TipoPersonaje & (ETipoPersonaje.Servant | ETipoPersonaje.Invocacion)) != 0)
                costo = int.Parse(CostoDePrana);
            else if ((mModeloPersonaje.TipoPersonaje & (ETipoPersonaje.Master | ETipoPersonaje.NPC)) != 0)
	            costo = int.Parse(CostoDeOd);

            if (costo < 10)
                return 0;
            if (costo < 20)
                return 1;
            if (costo < 40)
                return 2;
            if (costo < 50)
                return 3;
            if (costo < 100)
                return 4;
            if (costo < 150)
                return 5;
            if (costo < 200)
                return 6;
            if (costo < 250)
                return 7;

            return 8;
        }

        private void AñadirVariable(ViewModelVariableItem nuevaVariable)
        {
	        nuevaVariable.OnBotonInferiorPresionado += item =>
	        {
		        ContenedorListaVariables.Items.Remove(item);
	        };

            ContenedorListaVariables.Items.Add(nuevaVariable);
        }

        private void AñadirTirada() { }

        #endregion
    }
}