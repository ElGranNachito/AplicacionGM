using System;
using System.Collections.Generic;

using AppGM.Core.Delegados;

namespace AppGM.Core
{
	public abstract class ViewModelBloqueFuncionBase : ViewModel, IReceptorDeDrag, IDrageable
	{
		/// <summary>
		/// Evento que dispara cuando el estado de validez cambio
		/// </summary>
		public event Action<bool> OnEsValidoCambio = delegate { };

		/// <summary>
		/// Evento que se dispara cuando <see cref="indiceBloque"/> cambia
		/// </summary>
		public event DVariableCambio<int> OnIndiceBloqueModificado = delegate {};

		public event DDragElementoSoltado OnSoltado;

		#region Campos & Propiedades

		//--------------------------------------CAMPOS-------------------------------------------

		/// <summary>
		/// IndiceZ en la lista de <see cref="ViewModelBloqueFuncionBase"/> de <see cref="ViewModelCreacionDeFuncionBase"/>
		/// </summary>
		private int mIndiceBloque = -1;

		/// <summary>
		/// Indica si este argumento es valido
		/// </summary>
		private bool mEsValido = true;

		/// <summary>
		/// VM del control de creacion de funciones
		/// </summary>
		protected ViewModelCreacionDeFuncionBase mVMCreacionDeFuncion;

		/// <summary>
		/// <see cref="ViewModelBloqueContenedor"/> que contiene este <see cref="ViewModelBloqueFuncionBase"/>
		/// </summary>
		protected ViewModelBloqueContenedor mPadre;


		//-----------------------------------PROPIEDADES-----------------------------------------

		/// <summary>
		/// IndiceZ en la lista de <see cref="ViewModelBloqueFuncionBase"/> de <see cref="ViewModelCreacionDeFuncionBase"/>
		/// </summary>
		public int IndiceBloque
		{
			get => mIndiceBloque;
			set
			{
				int valorAnterior = mIndiceBloque;

				mIndiceBloque = value;

				OnIndiceBloqueModificado(valorAnterior, mIndiceBloque);
			}
		}

		/// <summary>
		/// Indica si este bloque esta bien construido
		/// </summary>
		public bool EsValido
		{
			get => mEsValido;
			set
			{
				if (value != mEsValido)
				{
					mEsValido = value;

					OnEsValidoCambio(mEsValido);
				}
			}
		}

		/// <summary>
		/// Indica si mostrar el espacio donde se posicionaria un bloque en caso de ser dropeado sobre este elemento
		/// </summary>
		public bool MostrarEspacioDrop { get; set; } = false;

		public int IndiceZ { get; set; } = 1;
		
		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_vmCreacionDeFuncion"><see cref="ViewModelCreacionDeFuncionBase"/> al que pertenece este bloque</param>
		public ViewModelBloqueFuncionBase(ViewModelCreacionDeFuncionBase _vmCreacionDeFuncion)
		{
			mVMCreacionDeFuncion = _vmCreacionDeFuncion;
		}

		public ViewModelBloqueFuncionBase(){}

		#endregion

		#region Metodos

		public virtual BloqueBase GenerarBloque() => null;

		/// <summary>
		/// Verifica que este bloque sea valido
		/// </summary>
		/// <returns><see cref="bool"/> indicando si este bloque es valido</returns>
		public virtual bool VerificarValidez() => true;

		/// <summary>
		/// Obtiene los <see cref="BloqueVariable"/> disponibles.
		/// </summary>
		/// <returns><see cref="List{T}"/> que contiene los <see cref="BloqueVariable"/> disponibles</returns>
		public virtual List<BloqueVariable> ObtenerVariables()
		{
			if (mPadre != null)
				return mPadre.ObtenerVariables();

			return mVMCreacionDeFuncion.ObtenerVariables(this);
		}

		public void OnDragEnter(IDrageable vm)
		{
			if (vm is ViewModelBloqueFuncionBase vmBloque && 
			    vm != this)
			{
				MostrarEspacioDrop = true;

				SistemaPrincipal.Drag[KeysParametrosDrag.IndiceParametroPosicionBloque] = IndiceBloque;
			}
		}

		public void OnDragLeave(IDrageable vm)
		{
			if (vm is ViewModelBloqueFuncionBase vmBloque)
			{
				MostrarEspacioDrop = false;

				SistemaPrincipal.Drag[KeysParametrosDrag.IndiceParametroPosicionBloque] = -1;
			}
		}

		public bool OnDrop(IDrageable vm)
		{
			MostrarEspacioDrop = false;

			return false;
		}

		public void OnComienzoDrag()
		{

		}

		public virtual void Soltado(IReceptorDeDrag elemento)
		{
			OnSoltado(this, elemento);
		}

		#endregion
	}
}