using AppGM.Core.Delegados;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Windows.Input;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un control para la creacion de una tirada
	/// </summary>
	public class ViewModelCrearTirada : ViewModelConResultado<ViewModelCrearTirada>, IAutocompletable
	{
		#region Eventos

		public event DVariableCambio<string> OnTextoActualModificado = delegate { }; 

		#endregion

		#region Campos & Propiedades

		//---------------------------------------------CAMPOS-------------------------------------------

		/// <summary>
		/// Personaje para el que se esta creando la tirada
		/// </summary>
		private readonly ModeloPersonaje mPersonaje;

		/// <summary>
		/// Modelo de la tirada que se creo
		/// </summary>
		private ModeloTiradaVariable mModeloTirada;

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
		/// Tirada siendo editada actualmente
		/// </summary>
		public ControladorTiradaVariable TiradaSiendoEditada { get; init; }

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
		public string Nombre { get; set; }

		/// <summary>
		/// Descripcion de la tirada
		/// </summary>
		public string Descripcion { get; set; }

		/// <summary>
		/// Descripcion de la variable extra de la tirada
		/// </summary>
		public string DescripcionVariableExtra { get; set; }

		/// <summary>
		/// Multiplicador especialidad
		/// </summary>
		public string MultiplicadorEspecialidad { get; set; }

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

				EsValida = false;
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
		/// Indica si se esta editando una tirada existente
		/// </summary>
		public bool EstaEditando => TiradaSiendoEditada != null;

		/// <summary>
		/// Indica si la tirada siendo creada es valida
		/// </summary>
		public bool EsValida { get; set; }

		/// <summary>
		/// VM para el combobox de seleccion de tipo de tirada
		/// </summary>
		public ViewModelComboBox<ETipoTirada> ViewModelComboBoxTipoTirada { get; set; } =
			new ViewModelComboBox<ETipoTirada>("Tipo tirada", EnumHelpers.TiposDeTiradasDisponibles.Remover(ETipoTirada.Stat).ToList());

		/// <summary>
		/// VM para el combobox de seleccion de la stat de la tirada
		/// </summary>
		public ViewModelComboBox<EStat> ViewModelComboBoxStatTirada { get; set; } =
			new ViewModelComboBox<EStat>("Stat", EnumHelpers.StatsDisponibles);

		/// <summary>
		/// VM para el combobox de seleccion del tipo de daño de la tirada
		/// </summary>
		public ViewModelComboBox<ETipoDeDaño> ViewModelComboBoxTipoDeDañoTirada { get; set; } =
			new ViewModelComboBox<ETipoDeDaño>("Tipo de daño", EnumHelpers.TiposDeDañoDisponibles);

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
		/// Constructor utilizado dentro de la creacion de personajes
		/// </summary>
		/// <param name="_accionSalir">Delegado que se ejecuta al salir del control representado por este vm</param>
		/// <param name="_personaje"><see cref="ModeloPersonaje"/> para el que se esta creando la tirada</param>
		/// <param name="_variablesDisponibles"><see cref="List{T}"/> de <see cref="ViewModelVariableItem"/> con las variables disponibles en este personaje</param>
		/// <param name="_controladorTiradaSiendoEditada">Controlador de la tirada que esta siendo editada</param>
		public ViewModelCrearTirada(Action<ViewModelCrearTirada> _accionSalir, ModeloPersonaje _personaje, List<ViewModelVariableItem> _variablesDisponibles, ControladorTiradaVariable _controladorTiradaSiendoEditada = null)
			: this(_accionSalir, _personaje, _controladorTiradaSiendoEditada, false)
		{
			Autocompletado.ActualizarValoresExistentes(_variablesDisponibles.Select(var =>
			{
				return new ViewModelItemAutocompletadoVariablePersistente(var.ControladorGenerico);
			}).Cast<ViewModelItemAutocompletadoBase>().ToList());
		}

		/// <summary>
		/// Constructor utilizado fuera de la creacion de personajes
		/// </summary>
		/// <param name="_accionSalir">Delegado que se ejecuta al salir del control representado por este vm</param>
		/// <param name="_personaje"><see cref="ModeloPersonaje"/> para el que se esta creando la tirada</param>
		/// <param name="_controladorTiradaSiendoEditada">Controlador de la tirada que esta siendo editada</param>
		public ViewModelCrearTirada(Action<ViewModelCrearTirada> _accionSalir, ModeloPersonaje _personaje, ControladorTiradaVariable _controladorTiradaSiendoEditada = null)
			: this(_accionSalir, _personaje, _controladorTiradaSiendoEditada, false)
		{
			Autocompletado.ActualizarValoresExistentes(mPersonaje.Variables.Select(var =>
			{
				return new ViewModelItemAutocompletadoVariablePersistente(
					SistemaPrincipal.ObtenerControlador<ControladorVariableBase, ModeloVariableBase>(var, true));

			}).Cast<ViewModelItemAutocompletadoBase>().ToList());
		}

		/// <summary>
		/// Constructor base de esta clase
		/// </summary>
		/// <param name="_accionSalir">Delegado que se ejecuta al salir del control representado por este vm</param>
		/// <param name="_personaje"><see cref="ModeloPersonaje"/> para el que se esta creando la tirada</param>
		/// <param name="_controladorTiradaSiendoEditada">Controlador de la tirada que esta siendo editada</param>
		/// <param name="nada">Literalmente nada, solo lo uso para diferenciar este constructor del otro con los mismos parametros</param>
		private ViewModelCrearTirada(Action<ViewModelCrearTirada> _accionSalir, ModeloPersonaje _personaje, ControladorTiradaVariable _controladorTiradaSiendoEditada, bool nada)
			: base(_accionSalir)
		{
			mPersonaje = _personaje;
			TiradaSiendoEditada = _controladorTiradaSiendoEditada;

			if(TiradaSiendoEditada != null)
			{
				mModeloTirada = TiradaSiendoEditada.modelo.Clonar() as ModeloTiradaVariable;
			}

			ViewModelComboBoxTipoTirada.OnValorSeleccionadoCambio += (ViewModelItemComboBoxBase<ETipoTirada> anterior, ViewModelItemComboBoxBase<ETipoTirada> actual) =>
			{
				DispararPropertyChanged(nameof(EsTiradaDeDaño));
			};

			Autocompletado.OnValorSeleccionado += HandlerValorSeleccionado;

			ComandoComprobar = new Comando(ActualizarValidez);
		}

		#endregion

		#region Metodos

		public ControladorTiradaVariable CrearTirada()
		{
			ActualizarValidez();

			if (!EsValida)
				return null;

			ModeloTiradaVariable nuevaTirada = null;

			if(ViewModelComboBoxTipoTirada.ValorSeleccionado.valor == ETipoTirada.Daño)
			{
				nuevaTirada = new ModeloTiradaDeDaño
				{
					TipoDeDaño = ViewModelComboBoxTipoDeDañoTirada.ValorSeleccionado.valor
				};
			}

			nuevaTirada ??= new ModeloTiradaVariable();

			nuevaTirada.Tirada					 = TextoActual;
			nuevaTirada.Nombre					 = Nombre;
			nuevaTirada.Descripcion				 = Descripcion;
			nuevaTirada.DescripcionVariableExtra = DescripcionVariableExtra;
			nuevaTirada.MultiplicadorDeEspecialidad = int.Parse(MultiplicadorEspecialidad);

			return IControladorTiradaBase.CrearControladorDeTiradaCorrespondiente(nuevaTirada) as ControladorTiradaVariable;
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

				ModificarTextoActual(nuevoTexto += valorSeleccionado.RepresentacionTextual);
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
		private void ActualizarValidez()
		{
			//Comprobamos que el nombre no este vacio
			if (Nombre.IsNullOrWhiteSpace())
			{
				EsValida = false;

				return;
			}

			//Obtenemos el tipo de tirada seleccionado
			var tipoTiradaSeleccionada = ViewModelComboBoxTipoTirada.ValorSeleccionado;

			//Nos aseguramos de que no sea null
			if (tipoTiradaSeleccionada == null)
			{
				EsValida = false;

				return;
			}

			//Si el tipo de tirada es de daño nos aseguramos de que se haya seleccionado una stat
			if (tipoTiradaSeleccionada.valor == ETipoTirada.Daño && ViewModelComboBoxStatTirada.ValorSeleccionado == null)
			{
				EsValida = false;

				return;
			}

			if(!MultiplicadorEspecialidad.EsUnNumero())
			{
				EsValida = false;

				return;
			}

			//Comprobamos la validez de la tirada
			var resultadoComprobacion = ParserTiradas.TiradaEsValida(TextoActual, mPersonaje, tipoTiradaSeleccionada.valor, ViewModelComboBoxStatTirada.ValorSeleccionado.valor);

			//Si la tirada no es valida...
			if (!resultadoComprobacion.esValida)
			{
				EsValida = false;

				MensajeErrorTirada = resultadoComprobacion.error;

				return;
			}

			MensajeErrorTirada = string.Empty;

			EsValida = true;
		} 

		#endregion
	}
}