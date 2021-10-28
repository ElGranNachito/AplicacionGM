using System;
using System.ComponentModel;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un control para el ingreso de una variable de tipo conocido
	/// </summary>
	public class ViewModelIngresoVariable : ViewModel
	{
		#region Eventos

		/// <summary>
		/// Evento que se dispara cuando el valor de <see cref="EsValido"/> cambia
		/// </summary>
		public event Action OnEsValidoCambio = delegate{};

		#endregion

		#region Campos & Propiedades

		//----------------------------------------CAMPOS--------------------------------------------

		/// <summary>
		/// Contiene el valor de <see cref="EsLista"/>
		/// </summary>
		private bool mEsLista;

		/// <summary>
		/// Contiene el valor de <see cref="EsValido"/>
		/// </summary>
		private bool mEsValido;

		/// <summary>
		/// Indica si puede quedar sin un valor
		/// </summary>
		private readonly bool mPuedeQuedarSinValor;

		//-------------------------------------PROPIEDADES--------------------------------------------

		/// <summary>
		/// Tipo de la variable
		/// </summary>
		public Type TipoVariable { get; private set; }

		/// <summary>
		/// Indica si el valor ingresado es valido
		/// </summary>
		public bool EsValido
		{
			get => mEsValido;
			private set
			{
				if (value == mEsValido)
					return;

				mEsValido = value;

				OnEsValidoCambio();
			}
		}

		/// <summary>
		/// Indica si esta variable es una lista
		/// </summary>
		public bool EsLista
		{
			get => mEsLista;
			set
			{
				if (value == mEsLista)
					return;

				mEsLista = value;

				DispararPropertyChanged(nameof(DebeSeleccionarControlador));
				DispararPropertyChanged(nameof(DebeMostrarLista));
			}
		}

		/// <summary>
		/// Indica si la variable es un controlador
		/// </summary>
		public bool EsControlador => TipoVariable.IsSubclassOf(typeof(ControladorBase));

		/// <summary>
		/// Indica si se debe seleccionar un controlador para el valor de la variable
		/// </summary>
		public bool DebeSeleccionarControlador => EsControlador && !EsLista;

		/// <summary>
		/// Indica si debe mostrar la lista
		/// </summary>
		public bool DebeMostrarLista => EsControlador && EsLista;

		/// <summary>
		/// Indica si es una variable con valor numerico
		/// </summary>
		public bool EsNumerica => TipoVariable == typeof(int) || TipoVariable == typeof(float);

		/// <summary>
		/// Indica si el valor de la variable es una cadena
		/// </summary>
		public bool MostrarCampoTexto => EsNumerica || TipoVariable == typeof(string);

		/// <summary>
		/// Texto actual ingresado por el usuario
		/// </summary>
		public string TextoActual { get; set; }

		/// <summary>
		/// Contiene el controlador actualmente seleccionado
		/// </summary>
		public ViewModelItemListaBase ControladorSeleccionado { get; set; }

		/// <summary>
		/// Lista con los controladores actualmente seleccionados
		/// </summary>
		public ViewModelListaItems<ViewModelItemListaBase> ViewModelListaDeControladores { get; set; }

		/// <summary>
		/// Comando que se ejecuta cuando el usuario presiona el boton 'Seleccionar'
		/// </summary>
		public ICommand ComandoSeleccionarControlador { get; private set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_tipoVariable">Tipo de la variable para la que se ingresara el valor</param>
		/// <param name="_esLista">Indica si la variable es una lista</param>
		public ViewModelIngresoVariable(Type _tipoVariable, bool _esLista, bool _puedeQuedarSinValor = false)
		{
			TipoVariable = _tipoVariable;
			EsLista = _esLista;
			mPuedeQuedarSinValor = _puedeQuedarSinValor;

			ComandoSeleccionarControlador = new Comando(async () =>
			{
				var controladorSeleccionado = await SeleccionarControlador();

				ControladorSeleccionado = controladorSeleccionado;
			});

			ViewModelListaDeControladores = new ViewModelListaItems<ViewModelItemListaBase>(async () =>
			{
				var controladorSeleccionado = await SeleccionarControlador();

				if(controladorSeleccionado != null)
					ViewModelListaDeControladores.Items.Add(controladorSeleccionado);
			}, true, "Lista de elementos");

			//Cada vez que cambia una propiedad actualizamos la validez
			PropertyChanged += (sender, args) =>
			{
				//Nos aseguramos de que la propiedad no sea EsValido porque sino se va a poner fea la cosa
				if (args.PropertyName == nameof(EsValido))
					return;

				ActualizarValidez();
			};

			if (mPuedeQuedarSinValor)
				EsValido = true;
		}

		#endregion

		#region Metodos

		/// <summary>
		/// Obtiene el valor ingresado por el usuario
		/// </summary>
		/// <returns>Valor ingresado por el usuario</returns>
		public object ObtenerValor()
		{
			//Antes que nada nos debemos asegurar de que sea valido
			ActualizarValidez();

			if (!EsValido)
				return null;

			if (DebeSeleccionarControlador)
				return ControladorSeleccionado.Controlador;

			if (TextoActual.IsNullOrWhiteSpace() || TextoActual.Length == 0)
				return null;

			//TODO: Lidiar con listas

			if (EsNumerica)
			{
				if (TipoVariable == typeof(int))
					return int.Parse(TextoActual);

				if (TipoVariable == typeof(float))
					return float.Parse(TextoActual);
			}

			return TextoActual;
		}

		/// <summary>
		/// Actualiza el valor de <see cref="EsValido"/>
		/// </summary>
		public void ActualizarValidez()
		{
			if (TipoVariable == null)
				EsValido = false;

			//TODO: Lidiar con listas

			//Si el tipo es un controlador...
			if (DebeSeleccionarControlador)
			{
				//Si no se selecciono ningun controlador entonces la validez recae en si
				//puede quedar sin un valor establecido
				if (ControladorSeleccionado == null)
				{
					EsValido = mPuedeQuedarSinValor;
				}
				//Si hay un controlador seleccionado entonces es valido siempre
				else
				{
					EsValido = true;
				}

				return;
			}

			//Si el tipo es numerico
			if (EsNumerica)
			{
				//Si el texto actual esta vacion la validez recae nuevamente en si puede
				//quedar sin un valor establecido
				if (TextoActual.IsNullOrWhiteSpace() || TextoActual.Length == 0)
				{
					EsValido = mPuedeQuedarSinValor;
				}
				//Si hay texto ingresado entonces la validez depende de si se puede
				//convertir de ese texto al tipo actual
				else
				{
					EsValido = TipoVariable.SePuedeConvertirDesde(TextoActual);
				}

				return;
			}

			//Si llegamos hasta aqui entonces el tipo ha de ser un string
			//por lo que su validez depende en si esta vacio o no
			if (TextoActual.IsNullOrWhiteSpace())
			{
				EsValido = mPuedeQuedarSinValor;

				return;
			}

			EsValido = true;
		}

		/// <summary>
		/// Muestra un mensaje de seleccion de controlador sobre la ventana actual
		/// </summary>
		/// <returns>Controlador seleccionado por el usuario</returns>
		private async Task<ViewModelItemListaBase> SeleccionarControlador()
		{
			var vmSeleccionControlador = new ViewModelSeleccionDeControlador(
				SistemaPrincipal.ObtenerControladores(TipoVariable, TipoVariable == typeof(ControladorItem)));

			var resultado = await SistemaPrincipal.MostrarMensaje(vmSeleccionControlador, "Seleccionar entidad", true, 500, 450);

			return resultado == EResultadoViewModel.Aceptar ? vmSeleccionControlador.ItemSeleccionado : null;
		} 

		#endregion
	}
}