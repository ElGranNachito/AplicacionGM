using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa un control para la creacion/edicion de un <see cref="ModeloDatosDefensa"/>
	/// </summary>
	public class ViewModelCreacionEdicionDatosReduccionDaño : ViewModel
	{
		#region Campos & Propiedades

		/// <summary>
		/// Viewmodel contenedor
		/// </summary>
		public readonly ViewModelIngresoDatosDefensivo viewModelIngresoDatosDefensivo;

		/// <summary>
		/// Indica si debemos mostrar el campo de texto para ingresar el valor de la reduccion
		/// </summary>
		public bool DebeIngresarValorDeReduccion => ViewModelComboBoxMetodoDeReduccionDeDaño.Valor is EMetodoDeReduccionDeDaño.Porcentual or EMetodoDeReduccionDeDaño.ValorFijo;

		/// <summary>
		/// Indica si la estrategia de deteccion de daño es <see cref="EEstrategiaDeDeteccionDeDaño.FuenteDelDaño"/>
		/// </summary>
		public bool EstrategiaDeReduccionEsDetectarFuenteDelDaño => ViewModelComboBoxEstrategiaDeDeteccionDeDaño.Valor == EEstrategiaDeDeteccionDeDaño.FuenteDelDaño;

		/// <summary>
		/// Descripcion que mostrar en la combobox de seleccion de valor del tipo de deteccion
		/// </summary>
		public string DescripcionValorTipoQueDetectar => ViewModelComboBoxEstrategiaDeDeteccionDeDaño.ValorSeleccionado != null ? ViewModelComboBoxEstrategiaDeDeteccionDeDaño.ValorSeleccionado.valor.ToString() : "Nada" + ":";

		/// <summary>
		/// Valor de la reduccion al daño
		/// </summary>
		public string ValorReduccion
		{
			get => Resultado.ValorReduccion.ToString("##.###");
			set => Resultado.ValorReduccion = decimal.Parse(value);
		}

		/// <summary>
		/// Nombre de la reduccion de daño
		/// </summary>
		public string NombreReduccion
		{
			get => Resultado.Nombre;
			set => Resultado.Nombre = value;
		}

		/// <summary>
		/// Indica si los datos ingresados son validos
		/// </summary>
		public bool EsValido { get; private set; }

		/// <summary>
		/// <see cref="ModeloDatosReduccionDeDaño"/> creado con los datos ingresados
		/// </summary>
		public ModeloDatosReduccionDeDaño Resultado { get; private set; }

		/// <summary>
		/// Metodo que utilizaremos para detectar el daño
		/// </summary>
		public ViewModelComboBox<EEstrategiaDeDeteccionDeDaño> ViewModelComboBoxEstrategiaDeDeteccionDeDaño { get; private set; }

		/// <summary>
		/// Metodo de reduccion de daño
		/// </summary>
		public ViewModelComboBox<EMetodoDeReduccionDeDaño> ViewModelComboBoxMetodoDeReduccionDeDaño { get; private set; }

		/// <summary>
		/// Valores que buscamos detectar, basados en la <see cref="EEstrategiaDeDeteccionDeDaño"/> seleccionada en <see cref="ViewModelComboBoxEstrategiaDeDeteccionDeDaño"/>
		/// </summary>
		public ViewModelMultiselectComboBox<Enum> ViewModelMultiselectComboBoxValorTipoDeDeteccion { get; private set; }

		public ViewModelMultiselectComboBox<ModeloFuenteDeDaño> ViewModelMultiselectComboBoxSeleccionFuentesDeDaño { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Contructor
		/// </summary>
		/// <param name="_vmIngresoDatosDefensivo">Viewmodel que contiene a este</param>
		/// <param name="_modelo">Datos de reduccion de daño que se editaran</param>
		public ViewModelCreacionEdicionDatosReduccionDaño(ViewModelIngresoDatosDefensivo _vmIngresoDatosDefensivo, ModeloDatosReduccionDeDaño _modelo)
		{
			viewModelIngresoDatosDefensivo = _vmIngresoDatosDefensivo;
			Resultado = _modelo;

			//Nos aseguramos de que el contenedor no sea null
			if (viewModelIngresoDatosDefensivo == null)
				SistemaPrincipal.LoggerGlobal.LogCrash($"{nameof(viewModelIngresoDatosDefensivo)} no puede ser null");

			//Inicializamos la combo boxes
			ViewModelComboBoxEstrategiaDeDeteccionDeDaño = new ViewModelComboBox<EEstrategiaDeDeteccionDeDaño>(viewModelIngresoDatosDefensivo.ViewModelComboBoxSeleccionEstrategiaDeteccionDeDaño.ItemsSeleccionados.ToList());

			ViewModelComboBoxMetodoDeReduccionDeDaño = new ViewModelComboBox<EMetodoDeReduccionDeDaño>(EnumHelpers.MetodosDeReduccionDeDañoDisponibles);

			ViewModelMultiselectComboBoxValorTipoDeDeteccion = new ViewModelMultiselectComboBox<Enum>(new List<ViewModelMultiselectComboBoxItem<Enum>>());

			ViewModelMultiselectComboBoxSeleccionFuentesDeDaño = new ViewModelMultiselectComboBox<ModeloFuenteDeDaño>(
				SistemaPrincipal.ModeloRolActual.FuentesDeDaño.Select(m =>
						new ViewModelMultiselectComboBoxItem<ModeloFuenteDeDaño>(m, m.ToString(), ViewModelMultiselectComboBoxSeleccionFuentesDeDaño)).ToList());

			//Nos subscribimos al evento de estado de seleccion modificado en el combobox de estrategias de deteccion de daño del contenedor
			viewModelIngresoDatosDefensivo.ViewModelComboBoxSeleccionEstrategiaDeteccionDeDaño.OnEstadoSeleccionItemCambio += AsignarValoresDisponiblesTipoDeDeteccion;

			//Nos subscribimos al evento de estado de seleccion modificado del combo box de estrategia de deteccion de daño
			ViewModelComboBoxEstrategiaDeDeteccionDeDaño.OnValorSeleccionadoCambio += (anterior, actual) =>
			{
				//Quitamos todos los items disponibles y los seleccionados
				ViewModelMultiselectComboBoxValorTipoDeDeteccion.Items.Elementos.Clear();
				ViewModelMultiselectComboBoxValorTipoDeDeteccion.ItemsSeleccionados.Clear();

				if (actual == null)
					return;
				
				//Añadimos los nuevos items disponibles
				ViewModelMultiselectComboBoxValorTipoDeDeteccion.Items.AddRange(actual.valor.ObtenerValoresDeDeteccionDeDañoDisponibles().Select(v => new ViewModelMultiselectComboBoxItem<Enum>(v, v.ToString(), ViewModelMultiselectComboBoxValorTipoDeDeteccion)));

				DispararPropertyChanged(nameof(DescripcionValorTipoQueDetectar));
				DispararPropertyChanged(nameof(EstrategiaDeReduccionEsDetectarFuenteDelDaño));
			};

			//Cuando el metodo de reduccion de daño seleccionado cambia disparamos property changed en el bool que indica si debemos mostrar el campo de text para el ingreso de la reduccion
			ViewModelComboBoxMetodoDeReduccionDeDaño.OnValorSeleccionadoCambio += (anterior, actual) => DispararPropertyChanged(nameof(DebeIngresarValorDeReduccion));

			ViewModelMultiselectComboBoxValorTipoDeDeteccion.OnEstadoSeleccionItemCambio   += item => ActualizarValidez();
			ViewModelMultiselectComboBoxSeleccionFuentesDeDaño.OnEstadoSeleccionItemCambio += item => ActualizarValidez(); 

			//Actualizamos la validez de los datos si la propiedad modificada no 'EsValido'
			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName == nameof(EsValido))
					return;

				ActualizarValidez();
			};

			//Si el resultado es null, es decir, no estamos editando datos preexistentes, creamos un nuevo modelo y nos pegamos la vuelta
			if (Resultado == null)
			{
				Resultado = new ModeloDatosReduccionDeDaño();

				if(viewModelIngresoDatosDefensivo.ViewModelComboBoxSeleccionEstrategiaDeteccionDeDaño.ItemsSeleccionados.Count > 0)
					ViewModelComboBoxEstrategiaDeDeteccionDeDaño.SeleccionarValor(viewModelIngresoDatosDefensivo.ViewModelComboBoxSeleccionEstrategiaDeteccionDeDaño.ItemsSeleccionados.First());

				ViewModelComboBoxMetodoDeReduccionDeDaño.SeleccionarValor(EMetodoDeReduccionDeDaño.ValorFijo);

				return;
			}

			//Si llegamos a este punto significa que estamos editando datos preexistentes asi que actualizamos el valor seleccionado de la comboboxes
			ViewModelComboBoxEstrategiaDeDeteccionDeDaño.SeleccionarValor(Resultado.EstrategiaDeDeteccionDeDaño);

			ViewModelComboBoxMetodoDeReduccionDeDaño.SeleccionarValor(Resultado.MetodoDeReduccionDeDaño);

			ViewModelMultiselectComboBoxValorTipoDeDeteccion.Items.AddRange(Resultado.EstrategiaDeDeteccionDeDaño.ObtenerValoresDeDeteccionDeDañoDisponibles().Select(v => new ViewModelMultiselectComboBoxItem<Enum>(v, v.ToString(), ViewModelMultiselectComboBoxValorTipoDeDeteccion)));

			//Aqui abajo seleccionamos los valores que buscamos detectar de una maneria bastante precaria porque al ser diferentes tipos de enums
			//se complicaba recaer en el comparador del combobox de seleccion multiple
			Enum valoresDeteccion = null;

			switch (Resultado.EstrategiaDeDeteccionDeDaño)
			{
				case EEstrategiaDeDeteccionDeDaño.Nivel:
					valoresDeteccion = Resultado.NivelesDeLasMagiasCuyosDañosReduce;
					break;
				case EEstrategiaDeDeteccionDeDaño.Rango:
					valoresDeteccion = Resultado.RangosDelDañoQueReduce;
					break;
				case EEstrategiaDeDeteccionDeDaño.TipoDeDaño:
					valoresDeteccion = Resultado.TiposDeDañoQueReduce;
					break;
			}

			foreach (var t in ViewModelMultiselectComboBoxValorTipoDeDeteccion.Items)
			{
				if (valoresDeteccion.HasFlag(t.Valor))
					t.EstaSeleccionado = true;
			}
		} 

		#endregion

		#region Metodos

		/// <summary>
		/// Crea el <see cref="ModeloDatosReduccionDeDaño"/> con los datos ingresados
		/// </summary>
		/// <returns><see cref="ModeloDatosReduccionDeDaño"/> creado con los datos existentes  <remarks> o null si los datos ingresados no son validos</remarks></returns>
		public ModeloDatosReduccionDeDaño CrearModelo()
		{
			ActualizarValidez();

			if (!EsValido)
				return null;

			Resultado.EstrategiaDeDeteccionDeDaño = ViewModelComboBoxEstrategiaDeDeteccionDeDaño.Valor;

			switch (Resultado.EstrategiaDeDeteccionDeDaño)
			{
				case EEstrategiaDeDeteccionDeDaño.TipoDeDaño:
				{
					foreach (var tipoDeDaño in ViewModelMultiselectComboBoxValorTipoDeDeteccion.ItemsSeleccionados.Cast<ETipoDeDaño>())
					{
						Resultado.TiposDeDañoQueReduce |= tipoDeDaño;
					}

					break;
				}
				case EEstrategiaDeDeteccionDeDaño.Rango:
				{
					foreach (var rango in ViewModelMultiselectComboBoxValorTipoDeDeteccion.ItemsSeleccionados.Cast<ERangoFlags>())
					{
						Resultado.RangosDelDañoQueReduce |= rango;
					}

					break;
				}
				case EEstrategiaDeDeteccionDeDaño.Nivel:
				{
					foreach (var nivel in ViewModelMultiselectComboBoxValorTipoDeDeteccion.ItemsSeleccionados.Cast<ENivelMagiaFlags>())
					{
						Resultado.NivelesDeLasMagiasCuyosDañosReduce |= nivel;
					}

					break;
				}
				case EEstrategiaDeDeteccionDeDaño.FuenteDelDaño:
				{
					Resultado.FuentesDeDañoQueReduce = ViewModelMultiselectComboBoxSeleccionFuentesDeDaño.ItemsSeleccionados.ToList();

					break;
				}
			}

			return Resultado;
		}

		/// <summary>
		/// Valida los datos ingresados y actualiza el valor de <see cref="EsValido"/>
		/// </summary>
		private void ActualizarValidez()
		{
			EsValido = false;

			if (ViewModelComboBoxEstrategiaDeDeteccionDeDaño.ValorSeleccionado == null)
				return;

			//Nos aseguramos de que la estrategia seleccionada este habilitada
			if(!viewModelIngresoDatosDefensivo.ViewModelComboBoxSeleccionEstrategiaDeteccionDeDaño.ItemsSeleccionados.Contains(ViewModelComboBoxEstrategiaDeDeteccionDeDaño.Valor))
				return;

			if (ViewModelComboBoxMetodoDeReduccionDeDaño.ValorSeleccionado == null)
				return;

			IList listaValoresEstrategiaDeteccion = EstrategiaDeReduccionEsDetectarFuenteDelDaño
				? ViewModelMultiselectComboBoxSeleccionFuentesDeDaño.ItemsSeleccionados
				: ViewModelMultiselectComboBoxValorTipoDeDeteccion.ItemsSeleccionados;

			if (listaValoresEstrategiaDeteccion.Count == 0)
				return;

			EsValido = true;
		}

		/// <summary>
		/// Asigna los valores disponibles al <see cref="ViewModelMultiselectComboBoxValorTipoDeDeteccion"/> en base a la <see cref="EEstrategiaDeDeteccionDeDaño"/> seleccionada
		/// </summary>
		/// <param name="elementoModificado">Nueva <see cref="EEstrategiaDeDeteccionDeDaño"/> seleccionada</param>
		private void AsignarValoresDisponiblesTipoDeDeteccion(ViewModelMultiselectComboBoxItem<EEstrategiaDeDeteccionDeDaño> elementoModificado)
		{
			if (elementoModificado.EstaSeleccionado)
				ViewModelComboBoxEstrategiaDeDeteccionDeDaño.ValoresPosibles.Add(new ViewModelItemComboBoxBase<EEstrategiaDeDeteccionDeDaño>(elementoModificado.Valor, elementoModificado.Valor.ToString()));
			else
				ViewModelComboBoxEstrategiaDeDeteccionDeDaño.ValoresPosibles.RemoveFirst(v => v.valor == elementoModificado.Valor);
		}
	} 

	#endregion
}