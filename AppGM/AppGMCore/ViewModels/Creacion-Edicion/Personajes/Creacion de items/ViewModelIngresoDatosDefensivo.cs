﻿using System.Linq;
using System.Windows.Input;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa un control para el ingreso de los datos de defensa de un <see cref="ModeloItem"/>
	/// </summary>
	public sealed class ViewModelIngresoDatosDefensivo : ViewModelCreacionEdicionDeModelo<ModeloDatosDefensa, Controlador<ModeloDatosDefensa>, ViewModelIngresoDatosDefensivo>
	{
		#region Campos & Propiedades

		/// <summary>
		/// Contiene el valor de <see cref="ReduccionDeDañoSeleccionada"/>
		/// </summary>
		private ViewModelCreacionEdicionDatosReduccionDaño mReduccionDeDañoSeleccionada;

		/// <summary>
		/// Indica si hay una reduccion de daño seleccionado y por lo tanto podemos quitarla
		/// </summary>
		public bool PuedeQuitarReduccionDeDaño => ReduccionDeDañoSeleccionada != null;

		/// <summary>
		/// Viewmodel que representa a un combobox de seleccion multiple para la seleccion de las estrategias de deteccion de daño que se pueden utilizar
		/// </summary>
		public ViewModelMultiselectComboBox<EEstrategiaDeDeteccionDeDaño> ViewModelComboBoxSeleccionEstrategiaDeteccionDeDaño { get; set; }

		/// <summary>
		/// Reducciones de daño que aporta este item
		/// </summary>
		public ViewModelListaDeElementos<ViewModelCreacionEdicionDatosReduccionDaño> ReduccionesDeDaño { get; set; } = new ViewModelListaDeElementos<ViewModelCreacionEdicionDatosReduccionDaño>();

		/// <summary>
		/// Elemento de <see cref="ReduccionesDeDaño"/> actualmente seleccionado
		/// </summary>
		public ViewModelCreacionEdicionDatosReduccionDaño ReduccionDeDañoSeleccionada
		{
			get => mReduccionDeDañoSeleccionada;
			set
			{
				mReduccionDeDañoSeleccionada = value;

				DispararPropertyChanged(nameof(PuedeQuitarReduccionDeDaño));
			}
		}

		/// <summary>
		/// Comando que se ejecuta cuando el usuario quiere añadir una nueva reduccion de daño
		/// </summary>
		public ICommand ComandoAñadirReduccionDeDaño { get; set; }

		/// <summary>
		/// Coamando que se ejecuta cuando el usuario quiere quitar la <see cref="ReduccionDeDañoSeleccionada"/>
		/// </summary>
		public ICommand ComandoQuitarReduccionDeDaño { get; set; } 

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_datosDefensa">Modelo que se editara</param>
		public ViewModelIngresoDatosDefensivo(ModeloDatosDefensa _datosDefensa)

			: base(null, true, false)
		{
			ModeloSiendoEditado = _datosDefensa;

			ViewModelComboBoxSeleccionEstrategiaDeteccionDeDaño = new ViewModelMultiselectComboBox<EEstrategiaDeDeteccionDeDaño>(
				EnumHelpers.TiposDeDeteccionDeDañoDisponibles.Select(t => new ViewModelMultiselectComboBoxItem<EEstrategiaDeDeteccionDeDaño>(t, t.ToString(),
						ViewModelComboBoxSeleccionEstrategiaDeteccionDeDaño)).ToList());

			if (EstaEditando)
			{
				ViewModelComboBoxSeleccionEstrategiaDeteccionDeDaño.ModificarEstadoSeleccionItem(ModeloSiendoEditado.EstrategiasDeDeteccionDeDañoUtilizadas, true);

				foreach (var reduccion in ModeloSiendoEditado.ReduccionesDeDaños)
				{
					ReduccionesDeDaño.Add(new ViewModelCreacionEdicionDatosReduccionDaño(this, reduccion));
				}
			}

			ComandoAñadirReduccionDeDaño = new Comando(() =>
			{
				var nuevoVmCreacionDatosReduccion = new ViewModelCreacionEdicionDatosReduccionDaño(this, null);

				ReduccionesDeDaño.Add(nuevoVmCreacionDatosReduccion);

				ModeloCreado.ReduccionesDeDaños.Add(nuevoVmCreacionDatosReduccion.Resultado);
			});

			ComandoQuitarReduccionDeDaño = new Comando(() =>
			{
				if (!PuedeQuitarReduccionDeDaño)
					return;

				ModeloCreado.ReduccionesDeDaños.Remove(ReduccionDeDañoSeleccionada.Resultado);

				ReduccionesDeDaño.Remove(ReduccionDeDañoSeleccionada);

				ReduccionDeDañoSeleccionada = null;
			});

			ModeloCreado = ModeloSiendoEditado ?? new ModeloDatosDefensa();
		} 

		#endregion

		#region Metodos

		public override void ActualizarValidez()
		{
			EsValido = false;

			if (ViewModelComboBoxSeleccionEstrategiaDeteccionDeDaño.ItemsSeleccionados.Count == 0)
				return;

			foreach (var reduccion in ReduccionesDeDaño)
			{
				if (!reduccion.EsValido)
					return;
			}

			EsValido = true;
		}

		public override ModeloDatosDefensa CrearModelo()
		{
			ActualizarValidez();

			if (!EsValido)
				return null;

			foreach (var reduccion in ReduccionesDeDaño)
			{
				reduccion.ActulizarModelo();
			}
			
			return ModeloCreado;
		}

		public override Controlador<ModeloDatosDefensa> CrearControlador()
		{
			SistemaPrincipal.LoggerGlobal.Log($"{nameof(ModeloDatosDefensa)} no requiere un controlador", ESeveridad.Error);

			return null;
		} 

		#endregion
	}
}
