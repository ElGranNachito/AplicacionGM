﻿using AppGM.Core.Delegados;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un control para la creacion de una tirada
	/// </summary>
	public class ViewModelCreacionEdicionDeTirada : ViewModelCreacionEdicionDeModelo<ModeloTiradaBase, ControladorTiradaBase, ViewModelCreacionEdicionDeTirada>, IAutocompletable
	{
		#region Eventos

		public event DVariableCambio<string> OnTextoActualModificado = delegate { }; 

		#endregion

		#region Campos & Propiedades

		//---------------------------------------------CAMPOS-------------------------------------------

		/// <summary>
		/// Personaje para el que se esta creando la tirada
		/// </summary>
		private readonly ModeloConVariablesYTiradas mModeloContenedor;

		/// <summary>
		/// Contiene el valor de <see cref="TextoActual"/>
		/// </summary>
		private string mTextoActual;

		/// <summary>
		/// Contiene el valor de <see cref="SeccionActual"/>
		/// </summary>
		private string mSeccionActual;

		//------------------------------------------PROPIEDADES---------------------------------------------

		/// <summary>
		/// Seccion en la que el usuario se encuentra actualmente posicionado
		/// </summary>
		public string SeccionActual
		{
			get => mSeccionActual.ToString();
			set
			{
				if (value.StartsWith('@'))
				{
					Autocompletado.EsVisible = true;

					mSeccionActual = value.Remove(0, 1);

					return;
				}

				mSeccionActual = value;

				Autocompletado.EsVisible = false;
			}
		}

		/// <summary>
		/// Nombre de la tirada
		/// </summary>
		public string Nombre
		{
			get => ModeloCreado.Nombre;
			set => ModeloCreado.Nombre = value;
		}

		/// <summary>
		/// Descripcion de la tirada
		/// </summary>
		public string Descripcion
		{
			get => ModeloCreado.Descripcion;
			set => ModeloCreado.Descripcion = value;
		}

		/// <summary>
		/// Descripcion de la variable extra de la tirada
		/// </summary>
		public string DescripcionVariableExtra 
		{
			get => ModeloCreado.DescripcionVariableExtra;
			set => ModeloCreado.DescripcionVariableExtra = value;
		}

		/// <summary>
		/// Multiplicador especialidad
		/// </summary>
		public string MultiplicadorEspecialidad
		{
			get => ModeloCreado.MultiplicadorDeEspecialidad.ToString();
			set => ModeloCreado.MultiplicadorDeEspecialidad = int.Parse(value);
		}

		/// <summary>
		/// Mensaje de error para la tirada ingresada
		/// </summary>
		public string MensajeErrorTirada { get; set; }

		public string TextoActual
		{
			get => mTextoActual;
			set
			{
				if (value == mTextoActual)
					return;

				mTextoActual = value;

				EsValido = false;
			}
		}

		public string TextoAnterior { get; set; }

		/// <summary>
		/// Propiedad a la que se encuentra atado el texto del textbox de la tirada
		/// </summary>
		public string TextoTextbox { get; set; }

		/// <summary>
		/// Indica si el tipo de la tirada siendo creada es <see cref="ETipoTirada.Daño"/>
		/// </summary>
		public bool EsTiradaDeDaño => ViewModelComboBoxTipoTirada.Valor == ETipoTirada.Daño;

		/// <summary>
		/// VM para el combobox de seleccion de tipo de tirada
		/// </summary>
		public ViewModelComboBox<ETipoTirada> ViewModelComboBoxTipoTirada { get; set; } =
			new ViewModelComboBox<ETipoTirada>("Tipo tirada", EnumHelpers.ObtenerValoresEnum<ETipoTirada>(new []{ETipoTirada.Stat}));

		/// <summary>
		/// VM para el combobox de seleccion de la stat de la tirada
		/// </summary>
		public ViewModelComboBox<EStat> ViewModelComboBoxStatTirada { get; set; } =
			new ViewModelComboBox<EStat>("Stat", EnumHelpers.ObtenerValoresEnum<EStat>(new [] {EStat.NINGUNA, EStat.NP, EStat.HP}));

		/// <summary>
		/// Viewmodel para el combobox de seleccion del rango del daño aplicado por la tirada
		/// </summary>
		public ViewModelComboBox<ERango> ViewModelComboBoxRangoTirada { get; set; } =
			new ViewModelComboBox<ERango>("Rango del daño", EnumHelpers.ObtenerValoresEnum<ERango>());

		/// <summary>
		/// Viewmodel para el combobox de seleccion del nivel de magia del daño aplicado por la tirada
		/// </summary>
		public ViewModelComboBox<ENivelMagia> ViewModelComboBoxNivelMagia { get; set; } =
			new ViewModelComboBox<ENivelMagia>("Nivel de la magia", EnumHelpers.ObtenerValoresEnum<ENivelMagia>());

		/// <summary>
		/// VM para el combobox de seleccion del tipo de daño de la tirada
		/// </summary>
		public ViewModelMultiselectComboBox<ETipoDeDaño> ViewModelComboBoxTipoDeDañoTirada { get; set; }

		/// <summary>
		/// VM para el combobox de seleccion del tipo de daño de la tirada
		/// </summary>
		public ViewModelMultiselectComboBox<ModeloFuenteDeDaño> ViewModelComboBoxFuentesDeDañoTirada { get; set; }

		public int PosSignoIntercalacion { get; set; }

		public Regex ExpresionRegularDetectarCaracteresNoPermitidos { get; set; } = new Regex(@"[^\w@+\-\*\\]");

		public ViewModelVentanaAutocompletado Autocompletado { get; set; } = new ViewModelVentanaAutocompletado();

		/// <summary>
		/// Comando que se ejecuta cuando el usuario presiona el boton comprobar
		/// </summary>
		public ICommand ComandoComprobar { get; set; }

		#endregion

		#region Constructores

		/// <summary>
		/// Constructor base de esta clase
		/// </summary>
		/// <param name="_accionSalir">Delegado que se ejecuta al salir del control representado por este vm</param>
		/// <param name="_contenedorTirada"><see cref="ModeloConVariablesYTiradas"/> para el que se esta creando la tirada</param>
		/// <param name="_controladorTiradaSiendoEditada">Controlador de la tirada que esta siendo editada</param>
		public ViewModelCreacionEdicionDeTirada(Action<ViewModelCreacionEdicionDeTirada> _accionSalir, ModeloConVariablesYTiradas _contenedorTirada, ControladorTiradaBase _controladorTiradaSiendoEditada)
			: base(_accionSalir, _controladorTiradaSiendoEditada)
		{
			mModeloContenedor = _contenedorTirada;

			if (!EstaEditando)
			{
				ViewModelComboBoxRangoTirada.SeleccionarValor(ERango.NINGUNO);
				ViewModelComboBoxNivelMagia.SeleccionarValor(ENivelMagia.NINGUNO);
			}

			ViewModelComboBoxTipoTirada.OnValorSeleccionadoCambio += async (ViewModelItemComboBoxBase<ETipoTirada> anterior, ViewModelItemComboBoxBase<ETipoTirada> actual) =>
			{
				if (actual.valor == ETipoTirada.Daño)
				{
					if(ModeloCreado is not ModeloTiradaDeDaño)
					{
						ModeloCreado = (await ModeloCreado.CrearCopiaProfundaEnSubtipoAsync<ModeloTiradaDeDaño, ModeloTiradaBase>()).resultado;
					}
				}
				else
				{
					if (ModeloCreado is ModeloTiradaDeDaño)
					{
						ModeloCreado = (await ModeloCreado.CrearCopiaProfundaEnSubtipoAsync<ModeloTiradaBase, ModeloTiradaDeDaño>()).resultado;
					}
				}

				ModeloCreado.TipoTirada = actual.valor;

				DispararPropertyChanged(nameof(EsTiradaDeDaño));
			};

			ViewModelComboBoxStatTirada.OnValorSeleccionadoCambio += (anterior, actual) =>
			{
				if (ModeloCreado is ModeloTiradaDeDaño d)
				{
					d.StatDeLaQueDepende = actual.valor;
				}
			};

			ViewModelComboBoxTipoDeDañoTirada = new ViewModelMultiselectComboBox<ETipoDeDaño>(EnumHelpers.TiposDeDañoDisponibles.Select(t =>
			{
				return new ViewModelMultiselectComboBoxItem<ETipoDeDaño>(t, t.ToString(), ViewModelComboBoxTipoDeDañoTirada);
			}).ToList(), new FlagsEnumEqualityComparer<ETipoDeDaño>());

			ViewModelComboBoxFuentesDeDañoTirada = new ViewModelMultiselectComboBox<ModeloFuenteDeDaño>(SistemaPrincipal
				.ModeloRolActual.FuentesDeDaño.Select(f => new ViewModelMultiselectComboBoxItem<ModeloFuenteDeDaño>(f, f.ToString(), ViewModelComboBoxFuentesDeDañoTirada)).ToList());

			Autocompletado.OnValorSeleccionado += HandlerValorSeleccionado;

			ComandoComprobar = new Comando(ActualizarValidez);

			Autocompletado.ActualizarValoresExistentes(mModeloContenedor.ObtenerVariablesDisponibles().Select(var =>
			{
				return new ViewModelItemAutocompletadoVariablePersistente(
					SistemaPrincipal.ObtenerControlador<ControladorVariableBase, ModeloVariableBase>(var, true));

			}).Cast<ViewModelItemAutocompletadoBase>().ToList());
		}

		#endregion

		#region Metodos

		public override async Task<ViewModelCreacionEdicionDeTirada> Inicializar(Type tipoValorPorDefectoModelo = null)
		{
			await base.Inicializar(tipoValorPorDefectoModelo);

			if (EstaEditando)
			{
				TextoTextbox = ModeloCreado.Tirada;
				TextoActual  = ModeloCreado.Tirada;

				ViewModelComboBoxTipoTirada.SeleccionarValor(ModeloCreado.TipoTirada);

				if (ModeloCreado is ModeloTiradaDeDaño d)
				{
					ViewModelComboBoxStatTirada.SeleccionarValor(d.StatDeLaQueDepende);

					ViewModelComboBoxRangoTirada.SeleccionarValor(d.Rango);

					ViewModelComboBoxNivelMagia.SeleccionarValor(d.NivelMagia);

					ViewModelComboBoxTipoDeDañoTirada.ModificarEstadoSeleccionItem(d.TipoDeDaño, true);

					foreach (var fuenteDaño in d.FuentesDeDañoAbarcadas)
					{
						ViewModelComboBoxFuentesDeDañoTirada.ModificarEstadoSeleccionItem(fuenteDaño, true);
					}
				}
			}

			return this;
		}

		public override ModeloTiradaBase CrearModelo()
		{
			ActualizarValidez();

			if (!EsValido)
				return null;

			if (ModeloCreado is ModeloTiradaDeDaño tiradaDaño)
			{
				if (ViewModelComboBoxTipoTirada.ValorSeleccionado.valor == ETipoTirada.Daño)
				{
					//Reseteamos los tipos de daño de la tirada para que si esta editando los tipos de daños queden en blanco
					tiradaDaño.TipoDeDaño = ETipoDeDaño.NINGUNO;
					tiradaDaño.FuentesDeDañoAbarcadas.Clear();

					foreach (var tipoDeDaño in ViewModelComboBoxTipoDeDañoTirada.ItemsSeleccionados)
					{
						tiradaDaño.TipoDeDaño |= tipoDeDaño;
					}

					foreach (var fuenteDeDaño in ViewModelComboBoxFuentesDeDañoTirada.ItemsSeleccionados)
					{
						tiradaDaño.FuentesDeDañoAbarcadas.Add(fuenteDeDaño);
					}
				}

				tiradaDaño.Rango      = ViewModelComboBoxRangoTirada.Valor;
				tiradaDaño.NivelMagia = ViewModelComboBoxNivelMagia.Valor;
			}

			ModeloCreado.Tirada = TextoActual;

			return ModeloCreado;
		}

		public override ControladorTiradaBase CrearControlador()
		{
			var nuevaTirada = CrearModelo();

			if (nuevaTirada == null)
				return null;

			return ControladorTiradaBase.CrearControladorDeTiradaCorrespondiente(nuevaTirada) as ControladorTiradaBase;
		}

		public void ActualizarPosibilidadesAutocompletado(string nuevoTexto, int nuevoIndiceIntercalacion)
		{
			ActualizarSeccionActual();

			Autocompletado.ActualizarTextoActual(Autocompletado.EsVisible ? SeccionActual : string.Empty);
		}

		public void ActualizarPosicionSignoDeIntercalacion(int nuevaPosicion)
		{
			ActualizarSeccionActual();
		}

		public void HandlerValorSeleccionado(ViewModelItemAutocompletadoBase valorSeleccionado)
		{
			var indiceComienzoVariable = ObtenerIndiceOperacionAnterior() + 2;
			var indiceFinVariable = ObtenerIndiceOperacionProxima();

			string nuevoTexto = TextoActual;

			if (indiceFinVariable == -1)
			{
				nuevoTexto = nuevoTexto.Remove(indiceComienzoVariable);

				ModificarTextoActual(nuevoTexto + valorSeleccionado.RepresentacionTextual);
			}
			else
			{
				nuevoTexto = TextoActual.Remove(indiceComienzoVariable, indiceFinVariable);

				ModificarTextoActual(nuevoTexto.Insert(indiceComienzoVariable + 2, valorSeleccionado.RepresentacionTextual));
			}
		}

		public void FocusObtenido() { }

		public void FocusPerdido()
		{
			Autocompletado.EsVisible = false;

			ActualizarValidez();
		}

		/// <summary>
		/// Obtiene el indice de la operacion aritmetica anterior al <see cref="PosSignoIntercalacion"/>
		/// </summary>
		/// <returns>Indice de la operacion aritmetica anterior o -1 en caso de que no exista una operacion anterior</returns>
		private int ObtenerIndiceOperacionAnterior() => TextoActual.LookBehindFirstIndexOf(new char[] { '+', '-', '/', '+' }, Math.Max(PosSignoIntercalacion - 1, 0));

		/// <summary>
		/// Obtiene el indice de la operacion aritmetica proxima al <see cref="PosSignoIntercalacion"/>
		/// </summary>
		/// <returns>Indice de la operacion aritmetica anterior o -1 en caso de que no exista una proxima operacion</returns>
		private int ObtenerIndiceOperacionProxima() => TextoActual.IndexOfAny(new char[] { '+', '-', '/', '+' }, Math.Max(PosSignoIntercalacion - 1, 0));

		/// <summary>
		/// Actualiza el valor de <see cref="SeccionActual"/> en base a <see cref="PosSignoIntercalacion"/>
		/// </summary>
		private void ActualizarSeccionActual()
		{
			var indiceOperacionAnterior = Math.Max(ObtenerIndiceOperacionAnterior(), 0);
			var indiceProximaOperacion = ObtenerIndiceOperacionProxima();

			var modificador = indiceOperacionAnterior != 0 ? 1 : 0;

			if (indiceProximaOperacion == -1)
			{
				SeccionActual = TextoActual.Substring(indiceOperacionAnterior + modificador);

				return;
			}

			if (indiceOperacionAnterior >= indiceProximaOperacion - 2)
			{
				SeccionActual = string.Empty;

				return;
			}

			SeccionActual = TextoActual.Substring(indiceOperacionAnterior + modificador, indiceProximaOperacion - indiceOperacionAnterior - modificador);
		}

		/// <summary>
		/// Modifica el texto de la textbox de la tirada
		/// </summary>
		/// <param name="nuevoTexto">Nuevo texto que se colocara en la textbox</param>
		private void ModificarTextoActual(string nuevoTexto)
		{
			//Si el nuevo texto es igual al texto actual nos pegamos la vuelta
			if (nuevoTexto == TextoActual)
				return;

			TextoAnterior = TextoActual;

			TextoActual = nuevoTexto;
			TextoTextbox = nuevoTexto;

			//Dispramos el evento de texto actual modificado
			OnTextoActualModificado(TextoAnterior, TextoActual);
		}

		/// <summary>
		/// Comprueba la validez de la tirada siendo creada
		/// </summary>
		public override void ActualizarValidez()
		{
			//Comprobamos que el nombre no este vacio
			if (Nombre.IsNullOrWhiteSpace())
			{
				EsValido = false;

				return;
			}

			//Obtenemos el tipo de tirada seleccionado
			var tipoTiradaSeleccionada = ViewModelComboBoxTipoTirada.ValorSeleccionado;

			//Nos aseguramos de que no sea null
			if (tipoTiradaSeleccionada == null)
			{
				EsValido = false;

				return;
			}

			//Si el tipo de tirada es de daño nos aseguramos de que se haya seleccionado una stat
			if (tipoTiradaSeleccionada.valor == ETipoTirada.Daño && ViewModelComboBoxStatTirada.ValorSeleccionado == null)
			{
				EsValido = false;

				return;
			}

			if(!MultiplicadorEspecialidad.EsUnNumero())
			{
				EsValido = false;

				return;
			}

			//Comprobamos la validez de la tirada
			var resultadoComprobacion = ParserTiradas.VerificarValidezTirada(TextoActual, mModeloContenedor.ObtenerVariablesDisponibles(), tipoTiradaSeleccionada.valor, tipoTiradaSeleccionada.valor == ETipoTirada.Daño ? ViewModelComboBoxStatTirada.ValorSeleccionado.valor : EStat.NINGUNA);

			//Si la tirada no es valida...
			if (!resultadoComprobacion.esValida)
			{
				EsValido = false;

				MensajeErrorTirada = resultadoComprobacion.error;

				return;
			}

			MensajeErrorTirada = string.Empty;

			EsValido = true;
		} 

		#endregion
	}
}