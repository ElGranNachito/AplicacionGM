using System.Linq;

using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa a la solapa de ingreso de datos de arma en la creacion de items
	/// </summary>
	public sealed class ViewModelIngresoDatosArma : ViewModelCreacionEdicionDeModelo<ModeloDatosArma, Controlador<ModeloDatosArma>, ViewModelIngresoDatosArma>
	{
		#region Campos & Propiedades

		/// <summary>
		/// Viewmodel de creacion/edicion de item que contiene a este objeto
		/// </summary>
		public readonly ViewModelCreacionEdicionItem viewModelCreacionEdicionItem;

		/// <summary>
		/// Numero de cargadores del arma
		/// </summary>
		public string NumeroDeCargadores
		{
			get => ModeloCreado.NumeroDeCargadores.ToString();
			set => ModeloCreado.NumeroDeCargadores = int.Parse(value);
		}

		/// <summary>
		/// Numero de municiones por cargador
		/// </summary>
		public string NumeroDeMunicionesPorCargador
		{
			get => ModeloCreado.NumeroDeMunicionesPorCargador.ToString();
			set => ModeloCreado.NumeroDeMunicionesPorCargador = int.Parse(value);
		}

		/// <summary>
		/// Indica si este arma utiliza municion
		/// </summary>
		public bool TieneMunicion
		{
			get => ModeloCreado.IgnoraDefensa;
			set => ModeloCreado.IgnoraDefensa = value;
		}

		/// <summary>
		/// Indica si este arma ignora la defensa del objetivo
		/// </summary>
		public bool IgnaraDefensa
		{
			get => ModeloCreado.IgnoraDefensa;
			set => ModeloCreado.IgnoraDefensa = value;
		}

		/// <summary>
		/// Viewmodel de la combobox para la seleccion del tipo de daño
		/// </summary>
		public ViewModelMultiselectComboBox<ETipoDeDaño> ViewModelMultiselectTiposDeDaño { get; set; }

		/// <summary>
		/// Viewmodel de la combobox para la se leccion de las fuentes de daño que abarcan a este item
		/// </summary>
		public ViewModelMultiselectComboBox<ModeloFuenteDeDaño> ViewModelMultiselectFuentesDeDañoQueAbarca { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_datosArma">Modelo que se editara</param>
		/// <param name="_vmCreacionEdicionItem">Viewmodel de creacion/edicion de item que contiene a este objeto</param>
		public ViewModelIngresoDatosArma(
			ModeloDatosArma _datosArma,
			ViewModelCreacionEdicionItem _vmCreacionEdicionItem)

			: base(null, true)
		{
			ModeloSiendoEditado = _datosArma;
			viewModelCreacionEdicionItem = _vmCreacionEdicionItem;

			ViewModelMultiselectTiposDeDaño = new ViewModelMultiselectComboBox<ETipoDeDaño>(
				EnumHelpers.ObtenerValoresEnum(new []{ETipoDeDaño.NINGUNO}).Select(t => new ViewModelMultiselectComboBoxItem<ETipoDeDaño>(t, t.ToString(), ViewModelMultiselectTiposDeDaño)).ToList(),
				new FlagsEnumEqualityComparer<ETipoDeDaño>());

			ViewModelMultiselectFuentesDeDañoQueAbarca = new ViewModelMultiselectComboBox<ModeloFuenteDeDaño>(
				SistemaPrincipal.ModeloRolActual.FuentesDeDaño.Select(f => new ViewModelMultiselectComboBoxItem<ModeloFuenteDeDaño>(f, f.ToString(), ViewModelMultiselectFuentesDeDañoQueAbarca)).ToList());

			ModeloCreado = ModeloSiendoEditado ?? new ModeloDatosArma();

			if (EstaEditando)
			{
				ViewModelMultiselectTiposDeDaño.ModificarEstadoSeleccionItem(ModeloCreado.TiposDeDañoQueInflige, true);

				foreach (var fuenteDeDaño in ModeloSiendoEditado.FuentesDeDañoQueAbarcaEsteArma)
				{
					ViewModelMultiselectFuentesDeDañoQueAbarca.ModificarEstadoSeleccionItem(fuenteDeDaño, true);
				}
			}

			ViewModelMultiselectTiposDeDaño.OnEstadoSeleccionItemCambio += item => ActualizarValidez();
		}

		#endregion

		#region Metodos

		public override void ActualizarValidez()
		{
			EsValido = false;

			if (ModeloCreado.TieneMunicion)
			{
				if (ModeloCreado.NumeroDeCargadores < 0)
					return;

				if (ModeloCreado.NumeroDeMunicionesPorCargador <= 0)
					return;
			}

			if (ViewModelMultiselectTiposDeDaño.ItemsSeleccionados.Count <= 0)
				return;

			EsValido = true;
		}

		public override ModeloDatosArma CrearModelo()
		{
			ActualizarValidez();

			if (!EsValido)
				return null;

			return ModeloCreado;
		}

		public override Controlador<ModeloDatosArma> CrearControlador()
		{
			SistemaPrincipal.LoggerGlobal.Log($"{nameof(ModeloDatosArma)} no requiere un controlador", ESeveridad.Error);

			return null;
		} 

		#endregion
	}
}