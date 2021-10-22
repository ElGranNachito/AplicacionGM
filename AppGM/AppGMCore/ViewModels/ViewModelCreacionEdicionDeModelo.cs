using System;

namespace AppGM.Core
{
	/// <summary>
	/// ViewModel que representa un control para la creacion de un <see cref="ModeloBase"/>
	/// </summary>
	/// <typeparam name="TModelo">Tipo del modelo que se crea</typeparam>
	/// <typeparam name="TControlador">Tipo del controlador del <typeparamref name="TModelo"/></typeparam>
	public abstract class ViewModelCreacionEdicionDeModelo<TModelo, TControlador, TViewModel> : ViewModelConResultado<TViewModel>

		where TModelo: ModeloBase
		where TControlador: ControladorBase
		where TViewModel : ViewModelCreacionEdicionDeModelo<TModelo, TControlador, TViewModel>
	{
		#region Eventos

		/// <summary>
		/// Evento que se dispara cuando el valor de <see cref="EsValido"/> cambia
		/// </summary>
		public Action<TViewModel> OnEsValidoCambio = delegate { }; 

		#endregion

		/// <summary>
		/// Contiene el valor de <see cref="EsValido"/>
		/// </summary>
		private bool mEsValido;

		/// <summary>
		/// Contiene el valor de <see cref="ControladorSiendoEditado"/>
		/// </summary>
		protected TControlador mControladorSiendoEditado;

		/// <summary>
		/// Modelo creado
		/// </summary>
		public TModelo ModeloCreado { get; protected set; }

		/// <summary>
		/// Modelo que esta siendo editado
		/// </summary>
		public TModelo ModeloSiendoEditado { get; init; }

		/// <summary>
		/// Controlador del <see cref="ModeloSiendoEditado"/>
		/// </summary>
		public TControlador ControladorSiendoEditado
		{
			get => mControladorSiendoEditado;
			init
			{
				if (value == null)
					return;

				ModeloSiendoEditado = (TModelo)value.Modelo;
				mControladorSiendoEditado = value;
			}
		}

		/// <summary>
		/// Indica si se esta editando un modelo existente
		/// </summary>
		public bool EstaEditando => ControladorSiendoEditado != null;

		/// <summary>
		/// Indica si el estado actual del modelo es valido
		/// </summary>
		public bool EsValido
		{
			get => mEsValido;
			set
			{
				if (value == mEsValido)
					return;

				mEsValido = value;

				OnEsValidoCambio((TViewModel)this);
			}
		}

		public ViewModelCreacionEdicionDeModelo(Action<TViewModel> _accionSalir, TControlador _controladorParaEditar, Type tipoValorPorDefectoModelo = null)
			:base(_accionSalir)
		{
			ControladorSiendoEditado = _controladorParaEditar;

			if (EstaEditando)
			{
				ModeloCreado = ModeloSiendoEditado.CrearCopiaProfundaEnSubtipo(ModeloSiendoEditado.GetType()) as TModelo;
			}
			else
			{
				Type tipoModelo = typeof(TModelo);
				
				if(!tipoModelo.IsAbstract)
				{
					ModeloCreado = Activator.CreateInstance(tipoModelo) as TModelo;
				}		
				else if(tipoModelo != null)
				{
					ModeloCreado = Activator.CreateInstance(tipoValorPorDefectoModelo) as TModelo;
				}
				else
				{
					SistemaPrincipal.LoggerGlobal.LogCrash("El modelo es de tipo abstracto pero no se especifico un tipo para el valor por defecto");
				}
			}
		}

		/// <summary>
		/// Actualiza el valor de <see cref="EsValido"/>
		/// </summary>
		protected virtual void ActualizarValidez() { }

		/// <summary>
		/// Crea el <typeparamref name="TModelo"/> con los datos ingresados por el usuario o 
		/// actualiza el modelo existente en caso de estar editando
		/// </summary>
		/// <returns><typeparamref name="TModelo"/> con los datos ingresados por el usuario</returns>
		public abstract TModelo CrearModelo();

		/// <summary>
		/// Crea un <typeparamref name="TControlador"/> para el modelo creado o en caso de 
		/// estar editando devuelve el controlador preexistente luego
		/// de actualizar su modelo
		/// </summary>
		/// <returns><typeparamref name="TControlador"/> para el nuevo modelo</returns>
		public abstract TControlador CrearControlador();
	}
}