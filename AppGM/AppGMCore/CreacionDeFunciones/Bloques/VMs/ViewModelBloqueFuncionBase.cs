using System;
using System.Collections.Generic;

using AppGM.Core.Delegados;
using CoolLogs;

namespace AppGM.Core
{
	
	public abstract class ViewModelBloqueFuncionBase : ViewModel, IReceptorDeDrag, IDrageable
	{
		/// <summary>
		/// Evento que dispara cuando el estado de validez cambio
		/// </summary>
		public event Action<bool> OnEsValidoCambio = delegate { };

		/// <summary>
		/// Evento que se dispara cuando <see cref="IndiceBloque"/> cambia
		/// </summary>
		public event DVariableCambio<int> OnIndiceBloqueModificado = delegate {};

		/// <summary>
		/// Evento que se dispara cuando el <see cref="Padre"/> de este bloque cambia
		/// </summary>
		public event DVariableCambio<IContenedorDeBloques> OnPadreModificado = delegate {};

		/// <summary>
		/// Evenyo que se dispara cuando este bloque es soltado como parte de una operacion de Drag & Drop
		/// </summary>
		public event DDragElementoSoltado OnSoltado = delegate{};

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
		/// <see cref="ViewModelBloqueContenedorConDrop{TipoBloque}"/> que contiene este <see cref="ViewModelBloqueFuncionBase"/>
		/// </summary>
		protected IContenedorDeBloques mPadre;

		/// <summary>
		/// Variable utilizada para almacenar el valor de la propiedad <see cref="IndiceZ"/>
		/// </summary>
		protected int mIndiceZ;

		//-----------------------------------PROPIEDADES-----------------------------------------

		/// <summary>
		/// VM del control de creacion de funciones
		/// </summary>
		public ViewModelCreacionDeFuncionBase VMCreacionDeFuncion { get; protected set; }

		/// <summary>
		/// ID de este bloque
		/// </summary>
		public int IDBloque { get; protected set; } = -1;

		/// <summary>
		/// <see cref="ViewModelBloqueContenedorConDropConDrop{TipoBloque}"/> que contiene este <see cref="ViewModelBloqueFuncionBase"/>
		/// </summary>
		public virtual IContenedorDeBloques Padre
		{
			get => mPadre;
			set
			{
				if (value == mPadre)
					return;

				mPadre?.QuitarBloque(this);

				var valorAnterior = mPadre;
				
				mPadre = value;

				OnPadreModificado(valorAnterior, mPadre);
			}
		}

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

		public virtual int IndiceZ
		{
			get => mIndiceZ;
			set => EstablecerIndiceZ(value);
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

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_vmCreacionDeFuncion"><see cref="ViewModelCreacionDeFuncionBase"/> al que pertenece este bloque</param>
		public ViewModelBloqueFuncionBase(IContenedorDeBloques _padre = null, int _idBloque = -1)
		{
			VMCreacionDeFuncion = SistemaPrincipal.VMCreacionDeFuncionActual;

			if (VMCreacionDeFuncion == null)
			{
				SistemaPrincipal.LoggerGlobal.Log($"Se intento crear un {this.GetType()} pero {nameof(SistemaPrincipal.VMCreacionDeFuncionActual)} es null!", ESeveridad.Error);

				return;
			}

			Padre = _padre ?? VMCreacionDeFuncion;

			IDBloque = _idBloque == -1 ? VMCreacionDeFuncion.ObtenerID() : _idBloque;
		}

		public ViewModelBloqueFuncionBase(){}

		#endregion

		#region Metodos

		public virtual ViewModelBloqueFuncionBase Copiar(IContenedorDeBloques destino)
		{
			if(Padre == null)
				return Activator.CreateInstance(GetType(), destino ?? VMCreacionDeFuncion, IDBloque) as ViewModelBloqueFuncionBase;

			return this;
		}

		public virtual BloqueBase GenerarBloque() => null;

		/// <summary>
		/// Funcion llamada una vez el bloque fue inicializado por su padre.
		/// Esta funcion solo es llamada cuando el bloque es añadido por primera vez a un contenedor
		/// </summary>
		public virtual void Inicializar(){}

		/// <summary>
		/// Verifica que este bloque sea valido
		/// </summary>
		/// <returns><see cref="bool"/> indicando si este bloque es valido</returns>
		public virtual bool VerificarValidez() => true;

		/// <summary>
		/// Actualiza el valor de <see cref="EsValido"/>
		/// </summary>
		/// <returns></returns>
		public virtual bool ActualizarValidez() => EsValido = VerificarValidez();

		/// <summary>
		/// Obtiene los <see cref="BloqueVariable"/> disponibles.
		/// </summary>
		/// <returns><see cref="List{T}"/> que contiene los <see cref="BloqueVariable"/> disponibles</returns>
		public virtual List<BloqueVariable> ObtenerVariables()
		{
			return mPadre?.ObtenerVariables(this);
		}

		/// <summary>
		/// Funcion llamada cuando el bloque es eliminado de su <see cref="IContenedorDeBloques"/>.
		/// (Transladar el elemento a otro <see cref="IContenedorDeBloques"/> no dispara causa que se llame la funcion)
		/// </summary>
		public virtual void OnBloqueRemovido(){}

		public void OnDragEntro(IDrageable vm)
		{
			if (vm is ViewModelBloqueFuncionBase vmBloque &&
			    vm != this)
			{
				OnDragEntro_Impl(vm);
			}
		}

		public void OnDragSalio(IDrageable vm)
		{
			if (vm is ViewModelBloqueFuncionBase vmBloque)
			{
				OnDragSalio_Impl(vm);
			}
		}

		public void OnDrop(IDrageable vm)
		{
			if (vm is ViewModelBloqueFuncionBase vmBloque &&
			    vm != this)
			{
				OnDrop_Impl(vm);
			}
		}

		public virtual void OnDragEntro_Impl(IDrageable vm)
		{
			MostrarEspacioDrop = true;
		}

		public virtual void OnDragSalio_Impl(IDrageable vm)
		{
			MostrarEspacioDrop = false;
		}

		public virtual bool OnDrop_Impl(IDrageable vm)
		{
			MostrarEspacioDrop = false;

			Padre.AñadirBloque(((ViewModelBloqueFuncionBase)vm).Copiar(Padre), IndiceBloque);

			return true;
		}

		public void OnComienzoDrag()
		{

		}

		public virtual void Soltado(List<IReceptorDeDrag> receptores) => OnSoltado(this, receptores);

		protected virtual void EstablecerIndiceZ(int nuevoIndice) => mIndiceZ = nuevoIndice;

		#endregion
	}
}