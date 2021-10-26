using System;
using System.Collections.Generic;

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

		/// <summary>
		/// Constructor por defecto
		/// </summary>
		/// <param name="_accionSalir">Accion que se ejecuta al salir de este vm</param>
		public ViewModelCreacionEdicionDeModelo(Action<TViewModel> _accionSalir)
			:base(_accionSalir) {}

		/// <summary>
		/// Constructor que trae una implemetacion por defecto de copia de modelos al editar
		/// </summary>
		/// <param name="_accionSalir">Accion que se ejecuta al salir de este vm</param>
		/// <param name="_controladorParaEditar">Controlador del modelo que sera editado</param>
		/// <param name="tipoValorPorDefectoModelo">Tipo del modelo que se creara por defecto cuando no se esta editando. Se utiliza cuando
		/// el tipo por defecto del modelo es una clase abstracta o no instanciable por cualquier motivo</param>
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
	}
}