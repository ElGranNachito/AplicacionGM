using System;
using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un control para la creacion/edicion de un <see cref="ModeloEfecto"/>
	/// </summary>
	public class ViewModelCreacionEfecto : ViewModelCreacionEdicionDeModelo<ModeloEfecto, ControladorEfecto, ViewModelCreacionEfecto>
	{
		/// <summary>
		/// Nombre del efecto
		/// </summary>
		public string Nombre
		{
			get => ModeloCreado.Nombre;
			set => ModeloCreado.Nombre = value;
		}

		/// <summary>
		/// Descripcion del efecto
		/// </summary>
		public string Descripcion
		{
			get => ModeloCreado.Descripcion;
			set => ModeloCreado.Descripcion = value;
		}

		/// <summary>
		/// Turnos que dura el efecto
		/// </summary>
		public string TurnosDeDuracion
		{
			get => ModeloCreado.TurnosDeDuracion.ToString();
			set => ModeloCreado.TurnosDeDuracion = int.Parse(value);
		}

		/// <summary>
		/// Indica si este efecto tiene duracion por turnos
		/// </summary>
		public bool EsEfectoConDuracion => ViewModelComboBoxTipoEfecto.Valor == ETipoEfecto.PorTurnos;

		/// <summary>
		/// Viewmodel del combobox para la seleccion del tipo de efecto
		/// </summary>
		public ViewModelComboBox<ETipoEfecto> ViewModelComboBoxTipoEfecto { get; set; } = new ViewModelComboBox<ETipoEfecto>(EnumHelpers.TiposDeEfectoDisponibles);

		/// <summary>
		/// Viewmodel del combobox para la seleccion del comportamiento acumulativo
		/// </summary>
		public ViewModelComboBox<EComportamientoAcumulativo> ViewModelComboBoxComportamientoAcumulativo { get; set; } = new ViewModelComboBox<EComportamientoAcumulativo>(EnumHelpers.ComportamientosAcumulativosDisponibles);

		public ViewModelFuncionItem<ControladorFuncion_Efecto> FuncionAplicarEfecto { get; set; }

		public ViewModelFuncionItem<ControladorFuncion_PredicadoEfecto> PredicadoPuedeAplicarEfecto { get; set; }

		public ViewModelListaItems<ViewModelFuncionHandlerEventoItem> FuncionesHandlerEventos { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_accionSalir">Lambda llamada al salir del control representado por este vm</param>
		/// <param name="_efectoParaEditar">Controlador del efecto que sera editado</param>
		public ViewModelCreacionEfecto(Action<ViewModelCreacionEfecto> _accionSalir, ControladorEfecto _efectoParaEditar = null)
			:base(_accionSalir, _efectoParaEditar)
		{
			ViewModelComboBoxTipoEfecto.OnValorSeleccionadoCambio += (anterior, actual) =>
			{
				DispararPropertyChanged(nameof(EsEfectoConDuracion));
			};

			ViewModelComboBoxComportamientoAcumulativo.SeleccionarValor(EComportamientoAcumulativo.Solapar);

			ComandoAceptar = new Comando(() =>
			{
				ControladorSiendoEditado?.ActulizarModelo(ModeloCreado);

				Resultado = EResultadoViewModel.Aceptar;

				mAccionSalir(this);
			});

			ComandoCancelar = new Comando(() =>
			{
				Resultado = EResultadoViewModel.Cancelar;

				mAccionSalir(this);
			});

			FuncionAplicarEfecto = new ViewModelFuncionItem<ControladorFuncion_Efecto>();
			PredicadoPuedeAplicarEfecto = new ViewModelFuncionItem<ControladorFuncion_PredicadoEfecto>();

			//AñadirHandlersModificacionModelo<ViewModelFuncionItem<ControladorFuncion_Efecto>, TIFuncio>();


			FuncionesHandlerEventos = new ViewModelListaItems<ViewModelFuncionHandlerEventoItem>(() => { }, false, "Eventos");
		}

		public override ModeloEfecto CrearModelo() => ModeloCreado;

		public override ControladorEfecto CrearControlador()
		{
			ActualizarValidez();

			if (!EsValido)
				return null;

			var modeloEfecto = CrearModelo();

			return new ControladorEfecto(modeloEfecto);
		}

		protected override void ActualizarValidez()
		{
			base.ActualizarValidez();
		}

		//private void AñadirHandlersModificacionModelo<TItem, TModelo>(TItem item, List<TModelo> coleccion)

		//	where TItem   : ViewModelItemLista
		//	where TModelo : ModeloBaseSK
		//{
		//	item.AñadirHandlerClick(ViewModelBoton.NombresComunes.Crear, b =>
		//	{
		//		coleccion.Add((TModelo)((ViewModelItemListaBase)b.ViewModelElementoContenedor).Controlador.Modelo);
		//	});
		//}

		//private void AñadirHandlersModificacionModelo<TItem, TModelo, TModeloResultadoModificacion>(TItem item, List<TModelo> coleccion, Func<TModeloResultadoModificacion, TModelo> convertidor)

		//	where TItem : ViewModelItemLista
		//	where TModelo : ModeloBaseSK
		//{
		//	item.AñadirHandlerClick(ViewModelBoton.NombresComunes.Crear, b =>
		//	{
		//		coleccion.Add();
		//	});
		//}
	}
}