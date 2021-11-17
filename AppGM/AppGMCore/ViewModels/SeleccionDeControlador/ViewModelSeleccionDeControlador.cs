using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa un control cuyo proposito es la seleccion de un <see cref="ControladorBase"/>
	/// </summary>
	public class ViewModelSeleccionDeControlador<TControlador> : ViewModelSeleccionDeControladorBase

		where TControlador: ControladorBase
	{
		#region Eventos

		/// <summary>
		/// Evento disparado cuando el usuario selecciona un item
		/// </summary>
		public event Action<ViewModelItemListaBase, TControlador> OnControladorSeleccionado = delegate { };  

		#endregion

		#region Campos & Propiedades

		//------------------------------------CAMPOS----------------------------------------

		/// <summary>
		/// Contiene el valor de <see cref="Filtro"/>
		/// </summary>
		private string mFiltro;

		/// <summary>
		/// Contiene todos los controladores disponibles
		/// </summary>
		private List<TControlador> mControladoresDisponibles;

		//---------------------------------PROPIEDADES----------------------------------------

		/// <summary>
		/// Cadena que se utiliza para filtrar los <see cref="mControladoresDisponibles"/>
		/// </summary>
		public string Filtro
		{
			get => mFiltro;
			set
			{
				if (mFiltro == value)
					return;

				mFiltro = value;

				ControladoresConcordantes.Elementos.Clear();

				ControladoresConcordantes.AddRange(
					(mFiltro.IsNullOrWhiteSpace()
						//Si el filtro es una cadena en vacia entonces añadimos todos los controladores disponibles
						? mControladoresDisponibles
						//Si no lo es entonces añadimos todos los controladores que concuerden con la cadena
						: mControladoresDisponibles.FindAll(c => c.Equals(Filtro)))
					.Select(c =>
					{
						var vmNuevoItem = c.CrearViewModelItem();

						vmNuevoItem.IndiceGrupoDeBotonesActivo = -1;

						return vmNuevoItem;
					}));
			}
		}

		/// <summary>
		/// Lista de solo lectura de los controladores disponibles
		/// </summary>
		public IReadOnlyList<TControlador> ControladoresDisponibles => mControladoresDisponibles.AsReadOnly();

		/// <summary>
		/// Contiene los vms de los controladores que concuerdan con el <see cref="Filtro"/>
		/// </summary>
		public ViewModelListaDeElementos<ViewModelItemListaBase> ControladoresConcordantes { get; set; } =
			new ViewModelListaDeElementos<ViewModelItemListaBase>();

		/// <summary>
		/// Controlador seleccionado
		/// </summary>
		public TControlador ControladorSeleccionado => (TControlador)ItemSeleccionado?.Controlador;

		/// <summary>
		/// Comando que se ejecuta cuando el usuario presiona el boton para seleccion un controlador
		/// </summary>
		public ICommand ComandoSeleccionarControlador { get; private set; }

		#endregion

		#region Constructores

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_controladoresDisponibles"></param>
		public ViewModelSeleccionDeControlador(List<TControlador> _controladoresDisponibles)
		{
			ActualizarControladorDisponibles(_controladoresDisponibles);

			ComandoSeleccionarControlador = new Comando(async () =>
			{
				mResultado = EResultadoViewModel.NoEstablecido;

				await SistemaPrincipal.MostrarMensajeAsync(this, "Seleccionar Controlador", true, 400, 300);

				if (ItemSeleccionado != null && Resultado == EResultadoViewModel.Aceptar)
					OnControladorSeleccionado(ItemSeleccionado, ControladorSeleccionado);
			});
		}

		#endregion

		#region Metodos

		/// <summary>
		/// Selecciona un controlador
		/// </summary>
		/// <param name="controlador">Controlador que seleccionar</param>
		public void SeleccionarControlador(ControladorBase controlador)
		{
			if(controlador == null)
				return;

			var nuevoItemSeleccionado = mControladoresDisponibles.Find(c => c == controlador);

			ItemSeleccionado = nuevoItemSeleccionado.CrearViewModelItem();

			OnControladorSeleccionado(ItemSeleccionado, ControladorSeleccionado);
		}

		/// <summary>
		/// Actualiza los <typeparamref name="TControlador"/> disponibles
		/// </summary>
		/// <param name="nuevosControladoresDisponibles">Nueva <see cref="List{T}"/> con los nuevos <typeparamref name="TControlador"/> disponibles</param>
		public void ActualizarControladorDisponibles(List<TControlador> nuevosControladoresDisponibles)
		{
			mControladoresDisponibles = nuevosControladoresDisponibles;

			ItemSeleccionado = null;
			ControladoresConcordantes.Elementos.Clear();

			ControladoresConcordantes.AddRange(mControladoresDisponibles.Select(c =>
			{
				var nuevoVm = c.CrearViewModelItem();

				nuevoVm.IndiceGrupoDeBotonesActivo = -1;

				return nuevoVm;
			}));
		}
		#endregion
	}
}