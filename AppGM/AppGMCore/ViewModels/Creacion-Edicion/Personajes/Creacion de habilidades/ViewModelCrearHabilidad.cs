using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// Representa una habilidad en un <see cref="ViewModelCreacionEdicionPersonaje"/>
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

        public string TextoNivelMagia
        {
	        get
	        {
		        if (ModeloCreado is ModeloMagia m)
		        {
			        m.Nivel = ObtenerNivelDeMagia();

			       return $"Lv.{m.Nivel}";
                }

		        return string.Empty;
	        }
        } 

        public bool PuedeFinalizar => EsValido;
        public bool EsMagia        => ComboBoxTipoHabilidad.Valor == ETipoHabilidad.Hechizo;
        public bool PuedeElegirSiEsMagiaParticular => PuedeAñadirMagiasParticulares();
        public bool PuedeElegirSiTieneRango => !EsMagia && !RequiereRango;

        public bool RequiereRango { get; set; }

        public bool UtilizaPrana => EsMagia && (mModeloPersonaje.TipoPersonaje & (ETipoPersonaje.Servant | ETipoPersonaje.Invocacion)) != 0;
        public bool UtilizaOd => EsMagia && (mModeloPersonaje.TipoPersonaje & (ETipoPersonaje.Servant | ETipoPersonaje.NPC)) != 0;

        public string Nombre
        {
	        get => ModeloCreado.Nombre;
	        set => ModeloCreado.Nombre = value;
        }

        public string Descripcion
        {
	        get => ModeloCreado.Descripcion;
	        set => ModeloCreado.Descripcion = value;
        }

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

        public ViewModelListaItems<ViewModelEfectoItem> ContenedorListaEfectos { get; set; }
        public ViewModelListaItems<ViewModelTiradaItem> ContenedorListaTiradas { get; set; }

        public ViewModelListaItems<ViewModelVariableItem> ContenedorListaVariables  { get; set; }

        public ViewModelComboBox<ETipoHabilidad> ComboBoxTipoHabilidad { get; set; }

        public ViewModelComboBox<ERango> ComboBoxRangoHabilidad { get; set; } = new ViewModelComboBox<ERango>(EnumHelpers.RangosDisponibles);

        public ViewModelListaItems<ViewModelFuncionItem<ControladorFuncion_Habilidad>> FuncionUtilizar { get; set; }
        public ViewModelListaItems<ViewModelFuncionItem<ControladorFuncion_Item>> FuncionCondicion { get; set; }

        #endregion

        #region Constructor

        public ViewModelCrearHabilidad(Action<ViewModelCrearHabilidad> accionSalir, ModeloPersonaje _modeloPersonaje, ControladorHabilidad _habilidad = null)    
			:base(accionSalir, _habilidad)
        {
	        mModeloPersonaje  = _modeloPersonaje;
            ControladorSiendoEditado = _habilidad;

            CrearComandoGuardar();

            if (EstaEditando)
            {
	            DispararPropertyChanged(nameof(EstaEditando));
            }

            ComboBoxTipoHabilidad = new ViewModelComboBox<ETipoHabilidad>(_modeloPersonaje.TipoPersonaje.ObtenerTiposDeHabilidadDisponibles());

            ComboBoxTipoHabilidad.OnValorSeleccionadoCambio += async (anterior, actual) =>
            {
	            var resultado = await ModeloCreado.CrearCopiaProfundaEnSubtipoAsync(actual.valor.ObtenerTipoModeloHabilidad());

	            ModeloCreado = (ModeloHabilidad)resultado.resultado;
                
	            ModeloCreado.TipoDeHabilidad = actual.valor;

                DispararPropertyChanged(nameof(EsMagia));
                DispararPropertyChanged(nameof(UtilizaOd));
                DispararPropertyChanged(nameof(UtilizaPrana));
                DispararPropertyChanged(nameof(PuedeElegirSiTieneRango));
            };

            ComboBoxRangoHabilidad.OnValorSeleccionadoCambio += (anterior, actual) =>
            {
	            ModeloCreado.Rango = actual.valor;
            };

            FuncionUtilizar = new ViewModelListaItems<ViewModelFuncionItem<ControladorFuncion_Habilidad>>(async () =>
            {
                var vmCreacion = await new ViewModelCreacionDeFuncionHabilidad(vm =>
                {
	                if (vm.Resultado.EsAceptarOFinalizar())
	                {
		                var nuevaFuncion = ((ViewModelCreacionDeFuncionHabilidad) vm).ControladorFuncion;

		                var nuevaRelacion = new TIFuncionHabilidad
		                {
			                Funcion = nuevaFuncion.modelo,
			                Habilidad = ModeloCreado,

                            PropositoFuncionRelacion = EPropositoFuncionRelacion.Uso
		                };

		                AñadirFuncionDesdeListaItems<TIFuncionHabilidad, ViewModelFuncionItem<ControladorFuncion_Habilidad>>((ViewModelFuncionItem<ControladorFuncion_Habilidad>)nuevaFuncion.CrearViewModelItem(), nuevaRelacion, ModeloCreado.Funciones, FuncionUtilizar);
	                }
                }).Inicializar();

                SistemaPrincipal.MostrarViewModelCreacionEdicion<ViewModelCreacionDeFuncionBase, ModeloFuncion, ControladorFuncionBase>(vmCreacion);

            }, true, "Funcion Utilizar", 1);

            FuncionCondicion = new ViewModelListaItems<ViewModelFuncionItem<ControladorFuncion_Item>>(async () =>
            {
	            var vmCreacion = await new ViewModelCreacionDeFuncionItem(vm =>
	            {
		            if (vm.Resultado.EsAceptarOFinalizar())
		            {
			            var nuevaFuncion = ((ViewModelCreacionDeFuncionItem)vm).ControladorFuncion;

			            var nuevaRelacion = new TIFuncionHabilidad
			            {
				            Funcion = nuevaFuncion.modelo,
				            Habilidad = ModeloCreado,

                            PropositoFuncionRelacion = EPropositoFuncionRelacion.PredicadoUso
			            };

			            AñadirFuncionDesdeListaItems<TIFuncionHabilidad, ViewModelFuncionItem<ControladorFuncion_Item>>((ViewModelFuncionItem<ControladorFuncion_Item>)nuevaFuncion.CrearViewModelItem(), nuevaRelacion, ModeloCreado.Funciones, FuncionCondicion);
		            }
	            }).Inicializar();

	            SistemaPrincipal.MostrarViewModelCreacionEdicion<ViewModelCreacionDeFuncionBase, ModeloFuncion, ControladorFuncionBase>(vmCreacion);

            }, true, "Predicado puede utilizar", 1);

            //FuncionCondicion = new ViewModelFuncionItem<ControladorFuncion_Predicado>(null);

            ContenedorListaEfectos   = new ViewModelListaItems<ViewModelEfectoItem>(async () =>
            {
	            SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = await new ViewModelCreacionEdicionEfecto(async vm =>
	            {
		            SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = this;

		            if (vm.Resultado.EsAceptarOFinalizar())
		            {
			            var nuevoEfecto = vm.CrearControlador();

			            nuevoEfecto.modelo.HabilidadContenedora = ModeloCreado;

			            await nuevoEfecto.GuardarAsync();

                        //AñadirFuncionDesdeListaItems<ModeloEfecto, ViewModelEfectoItem>((ViewModelEfectoItem)nuevoEfecto.CrearViewModelItem(), ModeloCreado.Efectos, ContenedorListaEfectos);
		            }

                }, mModeloPersonaje, typeof(ControladorHabilidad), null).Inicializar();

            }, true, "Efectos");
            
            ContenedorListaTiradas   = new ViewModelListaItems<ViewModelTiradaItem>(async ()=>
            {            
                SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = await new ViewModelCreacionEdicionDeTirada(async (vm) =>
                {
                    SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = this;

                    if (vm.Resultado.EsAceptarOFinalizar())
                    {
                        var nuevaTirada = vm.CrearControlador();

                        AñadirModeloDesdeListaItems<ModeloTiradaBase, ViewModelTiradaItem>((ViewModelTiradaItem)nuevaTirada.CrearViewModelItem(), ModeloCreado.Tiradas, ContenedorListaTiradas);
                    }

                }, ModeloCreado, null).Inicializar();

            }, true, "Tiradas");

            ContenedorListaVariables = new ViewModelListaItems<ViewModelVariableItem>(async () =>
            {              
	            SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = await new ViewModelCreacionDeVariable(async (vm) =>
	            {
		            if (vm.Resultado.EsAceptarOFinalizar())
		            {
			            var nuevaVariable = vm.CrearModelo();

                        if (vm.EstaEditando)
                        {
	                        await nuevaVariable.CrearCopiaProfundaEnSubtipoAsync(vm.ModeloSiendoEditado.GetType(), vm.ModeloSiendoEditado);

                            var controladorModeloEditado = SistemaPrincipal.ObtenerControlador(vm.ModeloSiendoEditado);

                            if (controladorModeloEditado != null)
	                            await controladorModeloEditado.Recargar();
                        }
                        else
                        {
	                        ModeloCreado.Variables.Add(nuevaVariable);

	                        await nuevaVariable.GuardarAsync();

	                        AñadirModeloDesdeListaItems<ModeloVariableBase, ViewModelVariableItem>((ViewModelVariableItem)vm.CrearControlador().CrearViewModelItem(), ModeloCreado.Variables, ContenedorListaVariables);
                        }
		            }
		            else if(vm.Resultado == EResultadoViewModel.Eliminar)
		            {
			            ModeloCreado.Variables.Remove(vm.ModeloSiendoEditado);

                        ContenedorListaVariables.Items.RemoveFirst(i => i.ControladorGenerico.modelo == vm.ModeloSiendoEditado);
		            }

		            SistemaPrincipal.Aplicacion.VentanaActual.DataContextContenido = this;
	            }).Inicializar(typeof(ModeloVariableInt));
            }, true, "Variables");

            ComandoCancelar = new Comando(() =>
            {
	            ModeloCreado.Dueño = null;

	            Resultado = EResultadoViewModel.Cancelar;

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

		public override async Task<ViewModelCrearHabilidad> Inicializar(Type tipoValorPorDefectoModelo = null)
		{
			await base.Inicializar(tipoValorPorDefectoModelo);

			if (!EstaEditando)
			{
				ModeloCreado.Dueño = mModeloPersonaje;

				ModeloCreado.TipoDeHabilidad = ETipoHabilidad.Skill;
            }

			return this;
		}

		public override ModeloHabilidad CrearModelo()
		{
			ActualizarValidez();

			if (!EsValido)
				return null;

			return ModeloCreado;
		}

		public override ControladorHabilidad CrearControlador()
		{
			var modeloCreado = CrearModelo();

			return modeloCreado == null ? null : new ControladorHabilidad(modeloCreado);
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

        public override void ActualizarValidez()
        {
	        EsValido = true;
        }

		#endregion
	}
}