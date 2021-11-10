using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

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
		public event Action<TViewModel> OnEsValidoCambio = delegate { };

		/// <summary>
		/// Evento que se dispara cuando el valor de <see cref="PuedeEditar"/> cambio
		/// </summary>
		public event Action<TViewModel> OnPuedeEditarCambio = delegate { };

		#endregion

		#region Campos & Propiedades

		//-----------------------------------------CAMPOS----------------------------------------

		/// <summary>
		/// Contiene el valor de <see cref="EsValido"/>
		/// </summary>
		private bool mEsValido;

		/// <summary>
		/// Contiene el valor de <see cref="PuedeEditar"/>
		/// </summary>
		private bool mPuedeEditar;

		/// <summary>
		/// Contiene el valor de <see cref="ControladorSiendoEditado"/>
		/// </summary>
		protected TControlador mControladorSiendoEditado;

		//--------------------------------------PROPIEDADES--------------------------------------

		/// <summary>
		/// Modelo creado
		/// </summary>
		public virtual TModelo ModeloCreado { get; protected set; }

		/// <summary>
		/// Modelo que esta siendo editado
		/// </summary>
		public virtual TModelo ModeloSiendoEditado { get; init; }

		/// <summary>
		/// Controlador del <see cref="ModeloSiendoEditado"/>
		/// </summary>
		public virtual TControlador ControladorSiendoEditado
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
		/// Si esta editando, indica si puede realizar cambios al modelo siendo editado
		/// </summary>
		public bool PuedeEditar
		{
			get => mPuedeEditar;
			set
			{
				if (value == mPuedeEditar)
					return;

				mPuedeEditar = value;

				OnPuedeEditarCambio((TViewModel)this);
			}
		}

		/// <summary>
		/// Indica si se esta editando un modelo existente
		/// </summary>
		public bool EstaEditando => ModeloSiendoEditado != null;

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

		/// <summary>
		/// Comando que se ejecuta cuando el usuario presiona el boton 'Eliminar'
		/// </summary>
		public ICommand ComandoEliminar { get; protected set; }

		/// <summary>
		/// Comando que se ejecuta al presionar 'Finalizar'
		/// </summary>
		public ICommand ComandoFinalizar { get; protected set; }

		#endregion

		#region Constructores

		/// <summary>
		/// Constructor por defecto
		/// </summary>
		/// <param name="_accionSalir">Accion que se ejecuta al salir de este vm</param>
		public ViewModelCreacionEdicionDeModelo(Action<TViewModel> _accionSalir, bool _actualizarValidezOnPropertyChanged = false)
			: base(_accionSalir)
		{
			if (!_actualizarValidezOnPropertyChanged)
				return;

			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName != nameof(EsValido) && ModeloCreado != null)
					ActualizarValidez();
			};
		}

		/// <summary>
		/// Constructor que trae una implemetacion por defecto de copia de modelos al editar
		/// </summary>
		/// <param name="_accionSalir">Accion que se ejecuta al salir de este vm</param>
		/// <param name="_controladorParaEditar">Controlador del modelo que sera editado</param>
		/// el tipo por defecto del modelo es una clase abstracta o no instanciable por cualquier motivo</param>
		public ViewModelCreacionEdicionDeModelo(Action<TViewModel> _accionSalir, TControlador _controladorParaEditar, bool _puedeEditar = true, bool _actualizarValidezOnPropertyChanged = false)
			: this(_accionSalir, _actualizarValidezOnPropertyChanged)
		{
			ControladorSiendoEditado = _controladorParaEditar;
			PuedeEditar = _puedeEditar;
		}

		#endregion

		#region Metodos

		/// <summary>
		/// Inicializa este <see cref="ViewModelCreacionEdicionDeModelo{TModelo,TControlador,TViewModel}"/>
		/// </summary>
		/// <param name="tipoValorPorDefectoModelo">Tipo del modelo que se creara por defecto cuando no se esta editando. Se utiliza cuando
		public virtual async Task<TViewModel> Inicializar(Type tipoValorPorDefectoModelo = null)
		{
			if (EstaEditando)
			{
				ModeloCreado = (await ModeloSiendoEditado.CrearCopiaProfundaEnSubtipoAsync(ModeloSiendoEditado.GetType())).resultado as TModelo;
			}
			else
			{
				Type tipoModelo = typeof(TModelo);

				if (!tipoModelo.IsAbstract)
				{
					ModeloCreado = Activator.CreateInstance(tipoModelo) as TModelo;
				}
				else if (tipoModelo != null)
				{
					ModeloCreado = Activator.CreateInstance(tipoValorPorDefectoModelo) as TModelo;
				}
				else
				{
					SistemaPrincipal.LoggerGlobal.LogCrash("El modelo es de tipo abstracto pero no se especifico un tipo para el valor por defecto");
				}
			}

			return (TViewModel)this;
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

		/// <summary>
		/// Añade un modelo a un <see cref="ViewModelListaItems{TItem}"/>
		/// </summary>
		/// <typeparam name="TModeloAñadir">Tipo del modelo que se quiere añadir</typeparam>
		/// <typeparam name="TItemLista">Tipo del viewmodel de lista del modelo</typeparam>
		/// <param name="nuevoElemento"><typeparamref name="TItemLista"/> que se quiere añadir</param>
		/// <param name="listaModelo">Lista con los <typeparamref name="TModeloAñadir"/> de este <typeparamref name="TModelo"/></param>
		/// <param name="listaItems"><see cref="ViewModelListaItems{TItem}"/> que contiene los <typeparamref name="TItemLista"/></param>
		protected void AñadirModeloDesdeListaItems<TModeloAñadir, TItemLista>(

			TItemLista nuevoElemento,
			List<TModeloAñadir> listaModelo,
			ViewModelListaItems<TItemLista> listaItems)

			where TModeloAñadir : ModeloBase
			where TItemLista : ViewModelItemListaGenerico<TItemLista>
		{
			nuevoElemento.OnItemEliminado += item =>
			{
				listaItems.Items.Remove(item);
			};

			listaItems.Items.Add(nuevoElemento);

			listaModelo.Add((TModeloAñadir)nuevoElemento.Controlador.Modelo);
		}

		/// <summary>
		/// Añade una funcion a un <see cref="ViewModelListaItems{TItem}"/>
		/// </summary>
		/// <typeparam name="TModeloAñadir">Tipo del modelo que se quiere añadir</typeparam>
		/// <typeparam name="TRelacion">Tipo de la relacion entre la funcion y el <typeparamref name="TModeloAñadir"/></typeparam>
		/// <typeparam name="TItemLista">Tipo del viewmodel de lista del modelo</typeparam>
		/// <param name="nuevoElemento">Nuevo <see cref="ViewModelItemListaGenerico{TViewModel}"/> que se añadira</param>
		/// <param name="nuevaRelacion">Nueva <typeparamref name="TRelacion"/> entre el <see cref="ModeloFuncion"/> creado y el <typeparamref name="TModeloAñadir"/><</param>
		/// <param name="listaModelo">Lista de <typeparamref name="TRelacion"/> del <typeparamref name="TModeloAñadir"/></param>
		/// <param name="listaItems"><see cref="ViewModelListaItems{TItem}"/> al que añadir el <paramref name="nuevoElemento"/></param>
		protected void AñadirFuncionDesdeListaItems<TRelacion, TItemLista>(

			TItemLista nuevoElemento,
			TRelacion nuevaRelacion,
			List<TRelacion> listaModelo,
			ViewModelListaItems<TItemLista> listaItems)

			where TRelacion : TIFuncion
			where TItemLista : ViewModelItemListaGenerico<TItemLista>
		{
			nuevoElemento.OnItemEliminado += item =>
			{
				listaItems.Items.Remove(item);
			};

			listaItems.Items.Add(nuevoElemento);
			listaModelo.Add(nuevaRelacion);
		}

		/// <summary>
		/// Inicializar el <see cref="ComandoFinalizar"/>
		/// </summary>
		protected void CrearComandoFinalizar()
		{
			ComandoFinalizar = new Comando(async () =>
			{
				ActualizarValidez();

				if (!EsValido)
					return;

				Resultado = EResultadoViewModel.Finalizar;

				mAccionSalir?.Invoke((TViewModel)this);
			});
		}

		/// <summary>
		/// Inicializar el <see cref="ComandoEliminar"/>
		/// </summary>
		protected void CrearComandoEliminar()
		{
			ComandoEliminar = new Comando(async () =>
			{
				if (!EstaEditando)
					return;

				var resultado = await (ControladorSiendoEditado?.EliminarAsync() ?? ModeloSiendoEditado.Eliminar(true));

				if(resultado != EResultadoViewModel.Aceptar)
					return;

				Resultado = EResultadoViewModel.Eliminar;

				mAccionSalir?.Invoke((TViewModel)this);
			});
		}

		protected string ObtenerPrefijoTituloVentana()
		{
			if (EstaEditando)
			{
				if (PuedeEditar)
					return "Editando";

				return "Viendo";
			}

			return "Creando";
		} 

		#endregion
	}
}