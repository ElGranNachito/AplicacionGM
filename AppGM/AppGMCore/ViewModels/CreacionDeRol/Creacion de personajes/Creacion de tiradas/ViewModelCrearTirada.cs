using AppGM.Core.Delegados;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un control para la creacion de una tirada
	/// </summary>
	public class ViewModelCrearTirada : ViewModelConResultado<ViewModelCrearTirada>, IAutocompletable
	{
		public event DVariableCambio<string> OnTextoActualModificado;

		private readonly ModeloPersonaje mPersonaje;

		private string mSeccionActual;

		public string SeccionActual
		{
			get => mSeccionActual;
			set
			{
				if(value.StartsWith('@'))
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
		/// Cadena que representa a la tirada
		/// </summary>
		public string Tirada { get; set; }

		/// <summary>
		/// Indica si el tipo de la tirada siendo creada es <see cref="ETipoTirada.Daño"/>
		/// </summary>
		public bool EsTiradaDeDaño => ViewModelComboBoxTipoTirada.Valor == ETipoTirada.Daño;

		/// <summary>
		/// VM para el combobox de seleccion de tipo de tirada
		/// </summary>
		public ViewModelComboBox<ETipoTirada> ViewModelComboBoxTipoTirada { get; set; } =
			new ViewModelComboBox<ETipoTirada>(EnumHelpers.TiposDeTiradasDisponibles.Remover(ETipoTirada.Stat).ToList());

		/// <summary>
		/// VM para el combobox de seleccion de la stat de la tirada
		/// </summary>
		public ViewModelComboBox<EStat> ViewModelComboBoxStatTirada { get; set; } =
			new ViewModelComboBox<EStat>(EnumHelpers.StatsDisponibles);

		/// <summary>
		/// VM para el combobox de seleccion del tipo de daño de la tirada
		/// </summary>
		public ViewModelComboBox<ETipoDeDaño> ViewModelComboBoxTipoDeDañoTirada { get; set; } =
			new ViewModelComboBox<ETipoDeDaño>(EnumHelpers.TiposDeDañoDisponibles);
		public int PosSignoIntercalacion { get; set; }
		public string TextoActual { get; set; }
		public string TextoAnterior { get; set; }
		public Regex ExpresionRegularDetectarCaracteresNoPermitidos { get; set; } = new Regex(@"[^\w@+\-\*\\]");

		public ViewModelVentanaAutocompletado Autocompletado { get; set; } = new ViewModelVentanaAutocompletado();

		public ViewModelCrearTirada(Action<ViewModelCrearTirada> _accionSalir, ModeloPersonaje _personaje, ControladorTiradaBase _controladorTiradaSiendoEditada = null)
			:this(_accionSalir, _personaje, _controladorTiradaSiendoEditada, false)
		{
			Autocompletado.ActualizarValoresExistentes(mPersonaje.Variables.Select(var =>
			{
				return new ViewModelItemAutocompletadoVariablePersistente(
					SistemaPrincipal.ObtenerControlador<ControladorVariableBase, ModeloVariableBase>(var.Variable, true));

			}).Cast<ViewModelItemAutocompletadoBase>().ToList());
		}

		public ViewModelCrearTirada(Action<ViewModelCrearTirada> _accionSalir, ModeloPersonaje _personaje, List<ViewModelVariableItem> variablesDisponibles, ControladorTiradaBase _controladorTiradaSiendoEditada = null)
			: this(_accionSalir, _personaje, _controladorTiradaSiendoEditada, false)
		{
			Autocompletado.ActualizarValoresExistentes(variablesDisponibles.Select(var =>
			{
				return new ViewModelItemAutocompletadoVariablePersistente(var.ControladorGenerico);
			}).Cast<ViewModelItemAutocompletadoBase>().ToList());
		}

		private ViewModelCrearTirada(Action<ViewModelCrearTirada> _accionSalir, ModeloPersonaje _personaje, ControladorTiradaBase _controladorTiradaSiendoEditada, bool nada)
			: base(_accionSalir)
		{
			mPersonaje = _personaje;

			ViewModelComboBoxTipoTirada.OnValorSeleccionadoCambio += (ViewModelItemComboBoxBase<ETipoTirada> anterior, ViewModelItemComboBoxBase<ETipoTirada> actual) =>
			{
				DispararPropertyChanged(nameof(EsTiradaDeDaño));
			};
		}

		public void ActualizarPosibilidadesAutocompletado(string nuevoTexto, int nuevoIndiceIntercalacion)
		{
			ActualizarSeccionActual();

			Autocompletado.ActualizarTextoActual(mSeccionActual);
		}

		public void ActualizarPosicionSignoDeIntercalacion(int nuevaPosicion)
		{
			ActualizarSeccionActual();
		}

		public void HandlerValorSeleccionado(ViewModelItemAutocompletadoBase valorSeleccionado)
		{
			Regex.Replace(TextoActual, @"@*\z", valorSeleccionado.RepresentacionTextual);
		}

		public void FocusObtenido(){}

		public void FocusPerdido()
		{
			
		}

		private void ActualizarSeccionActual()
		{
			var indiceUltimaOperacion = TextoActual.LastIndexOfAny(new char[] { '+', '-', '/', '+' });

			if (indiceUltimaOperacion == -1)
			{
				SeccionActual = TextoActual;

				return;
			}

			if (indiceUltimaOperacion == TextoActual.Length - 1)
			{
				SeccionActual = string.Empty;

				return;
			}

			SeccionActual = TextoActual.Substring(indiceUltimaOperacion + 1);
		}
	}
}
