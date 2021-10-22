﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// Representa una habilidad en un <see cref="ViewModelCrearPersonaje"/>
    /// </summary>
    public class ViewModelCrearHabilidad : ViewModelCreacionEdicionDeModelo<ModeloHabilidad, ControladorHabilidad, ViewModelCrearHabilidad>
    {
        #region Miembros

        /// <summary>
        /// Personaje para el que estamos creando la habilidad
        /// </summary>
        private ModeloPersonaje         mModeloPersonaje;

        #endregion

        #region Propiedades

        public string TextoNivelMagia => $"Lv.{ObtenerNivelDeMagia()}";

        public bool PuedeFinalizar => EsValido;
        public bool EsMagia        => ComboBoxTipoHabilidad.Valor == ETipoHabilidad.Hechizo;
        public bool PuedeElegirSiEsMagiaParticular => PuedeAñadirMagiasParticulares();
        public bool PuedeElegirSiTieneRango => !EsMagia && !RequiereRango;

        public bool RequiereRango { get; set; }

        public bool UtilizaPrana => EsMagia && (mModeloPersonaje.TipoPersonaje & (ETipoPersonaje.Servant | ETipoPersonaje.Invocacion)) != 0;
        public bool UtilizaOd => EsMagia && (mModeloPersonaje.TipoPersonaje & (ETipoPersonaje.Servant | ETipoPersonaje.NPC)) != 0;

        public bool ModeloGuardado { get; private set; } = false;

        public string CostoDeMana
        {
            get
			{
                if (ModeloCreado is ModeloMagia m)
                    return m.CostoDeMana.ToString();

                return string.Empty;
            }
            set
            {
                if(ModeloCreado is ModeloMagia m)
				{
                    m.CostoDeMana = int.Parse(value);

                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(TextoNivelMagia)));
                }       
            }
        }

        public string CostoDeOd
        {
	        get
			{
                if (ModeloCreado is ModeloMagia m)
                    return m.CostoDeOdOPrana.ToString();

                return string.Empty;
            }
            set
            {
                if (ModeloCreado is ModeloMagia m)
                {
                    m.CostoDeOdOPrana = int.Parse(value);

                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(TextoNivelMagia)));
                }                
            }
        }

        public string CostoDePrana
        {
	        get
			{
                if (ModeloCreado is ModeloMagia m)
                    return m.CostoDeOdOPrana.ToString();

                return string.Empty;
            }
	        set
	        {
                if (ModeloCreado is ModeloMagia m)
                {
                    m.CostoDeOdOPrana = int.Parse(value);

                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(TextoNivelMagia)));
                }
            }
        }

        public ViewModelListaItems<ViewModelItemLista> ContenedorListaEfectos { get; set; }
        public ViewModelListaItems<ViewModelTiradaItem> ContenedorListaTiradas { get; set; }

        public ViewModelListaItems<ViewModelVariableItem> ContenedorListaVariables  { get; set; }

        public ViewModelComboBox<ETipoHabilidad> ComboBoxTipoHabilidad { get; set; }

        public ViewModelComboBox<ERango> ComboBoxRangoHabilidad { get; set; } = new ViewModelComboBox<ERango>(EnumHelpers.RangosDisponibles);

        public ViewModelFuncionItem<ControladorFuncion_Habilidad> FuncionUtilizar { get; set; }
        public ViewModelFuncionItem<ControladorFuncion_PredicadoEfecto> FuncionCondicion { get; set; }

        public ICommand ComandoGuardar { get; set; }

        #endregion

        #region Constructor

        public ViewModelCrearHabilidad(ModeloPersonaje _modeloPersonaje, Action<ViewModelCrearHabilidad> accionSalir, ControladorHabilidad _habilidad = null)    
			:base(accionSalir, _habilidad)
        {
	        mModeloPersonaje  = _modeloPersonaje;
            ControladorSiendoEditado = _habilidad;

            if (EstaEditando)
            {
                ModeloGuardado = true;

                DispararPropertyChanged(nameof(EstaEditando));
            }
            else
            {
	            ModeloCreado.Dueño = mModeloPersonaje;
                mModeloPersonaje.Habilidades.Add(ModeloCreado);

                SistemaPrincipal.GuardarModelo(ModeloCreado);
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

            ContenedorListaTiradas   = new ViewModelListaItems<ViewModelTiradaItem>(()=>
            {            
                SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = new ViewModelCrearTirada(async (vm) =>
                {
                    SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = this;

                    if (vm.Resultado.EsAceptarOFinalizar())
                    {
                        var nuevaTirada = vm.CrearControlador();

                        nuevaTirada.modelo.HabilidadContenedora = ModeloCreado;

                        if (vm.Resultado.EsAceptarOFinalizar())
                            ModeloCreado.Tiradas.Add(nuevaTirada.modelo);

                        await nuevaTirada.GuardarAsync();

                        AñadirTirada((ViewModelTiradaItem)nuevaTirada.CrearViewModelItem());
                    }

                }, ModeloCreado, null);

            }, true, "Tiradas");

            ContenedorListaVariables = new ViewModelListaItems<ViewModelVariableItem>(() =>
            {              
	            SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = new ViewModelCreacionDeVariable(async (vm) =>
	            {
		            if (vm.Resultado.EsAceptarOFinalizar())
		            {
                        var nuevaVariable = vm.CrearControlador();

                        ModeloCreado.Variables.Add(nuevaVariable.modelo);

                        await nuevaVariable.GuardarAsync();

                        AñadirVariable((ViewModelVariableItem)vm.CrearControlador().CrearViewModelItem());
		            }

		            SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = this;
	            });
            }, true, "Variables");

            ComandoGuardar = new Comando(async () =>
            {
                await SistemaPrincipal.GuardarDatosRolAsincronicamente();

                ModeloGuardado = true;
            });

            ComandoCancelar = new Comando(() =>
            {
	            Resultado = EResultadoViewModel.Cancelar;

                if (!EstaEditando && !ModeloGuardado)
                    SistemaPrincipal.EliminarModelo(ModeloCreado);

                accionSalir(this);
            });

            ComandoAceptar = new Comando(() =>
            {
	            Resultado = EResultadoViewModel.Aceptar;

	            accionSalir(this);
            });

            PropertyChanged += (sender, args) =>
            {
                if(args.PropertyName != nameof(PuedeFinalizar))
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PuedeFinalizar)));
            };
        }

		#endregion

		#region Metodos

		public override ModeloHabilidad CrearModelo()
		{
			throw new NotImplementedException();
		}

		public override ControladorHabilidad CrearControlador()
		{
			throw new NotImplementedException();
		}

		private void FinalizarCreacion()
        {
            
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
	        if(ModeloCreado is ModeloMagia magia)
			{
                int costo = magia.CostoDeOdOPrana;

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

            return 0;
        }

        private void AñadirVariable(ViewModelVariableItem nuevaVariable)
        {
	        nuevaVariable.OnItemEliminado += item =>
	        {
		        ContenedorListaVariables.Items.Remove(item);
	        };

            ContenedorListaVariables.Items.Add(nuevaVariable);
        }

        private void AñadirTirada(ViewModelTiradaItem nuevaTirada) 
        {
            nuevaTirada.OnItemEliminado += item =>
            {
                ContenedorListaTiradas.Items.Remove(item);
            };

            ContenedorListaTiradas.Items.Add(nuevaTirada);
        }

		protected override void ActualizarValidez()
		{
			base.ActualizarValidez();
		}

		#endregion
	}
}