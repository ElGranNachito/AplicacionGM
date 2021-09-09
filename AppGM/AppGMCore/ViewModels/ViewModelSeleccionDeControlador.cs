using System.Collections.Generic;
using System.Linq;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa un control cuyo proposito es la seleccion de un <see cref="ControladorBase"/>
	/// </summary>
	public class ViewModelSeleccionDeControlador : ViewModelConResultado<ViewModelSeleccionDeControlador>
	{
		#region Campos & Propiedades

		//------------------------------------CAMPOS----------------------------------------

		/// <summary>
		/// Contiene el valor de <see cref="Filtro"/>
		/// </summary>
		private string mFiltro;

		/// <summary>
		/// Contiene todos los controladores disponibles
		/// </summary>
		private List<ControladorBase> mControladoresDisponibles;

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
						: mControladoresDisponibles.FindAll(c => c.CompararConCadena(Filtro)))
					.Select(c => c.CrearViewModelItem(false)));
			}
		}

		/// <summary>
		/// Contiene los vms de los controladores que concuerdan con el <see cref="Filtro"/>
		/// </summary>
		public ViewModelListaDeElementos<ViewModelItemListaBase> ControladoresConcordantes { get; set; } =
			new ViewModelListaDeElementos<ViewModelItemListaBase>();

		/// <summary>
		/// Contiene el vm del controlador actualmente seleccionado
		/// </summary>
		public ViewModelItemListaBase ItemSeleccionado { get; set; }

		/// <summary>
		/// Controlador seleccionado
		/// </summary>
		public ControladorBase ControladorSeleccionado => ItemSeleccionado.Controlador;

		#endregion

		#region Constructores

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_controladoresDisponibles"></param>
		public ViewModelSeleccionDeControlador(List<ControladorBase> _controladoresDisponibles)
		{
			mControladoresDisponibles = _controladoresDisponibles;

			ControladoresConcordantes.AddRange(mControladoresDisponibles.Select(c => c.CrearViewModelItem(false)));
		}

		#endregion
	}
}