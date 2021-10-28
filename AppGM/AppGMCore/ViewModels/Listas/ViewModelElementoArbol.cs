using System;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa a un elemento de un <see cref="ViewModelVistaArbol{TViewModelElementos,TContenidoElementos}"/>
	/// </summary>
	/// <typeparam name="TContenido">Tipo del contenido almacenado por este viewmodel</typeparam>
	public class ViewModelElementoArbol<TContenido> : ViewModelElementoArbolBase
	{
		#region Eventos

		/// <summary>
		/// Evento que se dispara cuando este elementos es seleccionado
		/// </summary>
		public event Action<ViewModelElementoArbol<TContenido>, ViewModelVistaArbol<ViewModelElementoArbol<TContenido>, TContenido>> OnSeleccionado = delegate { };

		/// <summary>
		/// Evento que se dispara cuando este elementos es seleccionado
		/// </summary>
		public event Action<ViewModelElementoArbol<TContenido>, ViewModelVistaArbol<ViewModelElementoArbol<TContenido>, TContenido>> OnDeseleccionado = delegate { };

		#endregion

		/// <summary>
		/// Indica si este elemento se encuentra seleccionado
		/// </summary>
		public override bool EstaSeleccionado
		{
			get => mEstaSeleccionado;
			set
			{
				if (value && !PuedeSerSeleccionado())
					return;

				mEstaSeleccionado = value;

				if (mEstaSeleccionado)
				{
					HostDragAndDrop.ElementosSeleccionados.Add(this);

					OnSeleccionado(this, Raiz);
				}
				else
				{
					HostDragAndDrop.ElementosSeleccionados.Remove(this);

					OnDeseleccionado(this, Raiz);
				}
			}
		}

		/// <summary>
		/// Indica si este elemento se encuentra espandido actualmente
		/// </summary>
		public override bool EstaExpandido
		{
			get => mEstaExpandido;
			set
			{
				mEstaExpandido = value;

				if (Hijos.Elementos.Count == 0)
					ActualizarHijos();
			}
		}

		/// <summary>
		/// Contenido de este elemento
		/// </summary>
		public virtual TContenido Contenido { get; set; }

		/// <summary>
		/// Raiz del arbol
		/// </summary>
		public ViewModelVistaArbol<ViewModelElementoArbol<TContenido>, TContenido> Raiz { get; init; }

		/// <summary>
		/// Padre de este elemento
		/// </summary>
		public ViewModelElementoArbol<TContenido> Padre { get; set; }

		/// <summary>
		/// Hijos de este elemento
		/// </summary>
		public ViewModelListaDeElementos<ViewModelElementoArbol<TContenido>> Hijos { get; set; } = new ViewModelListaDeElementos<ViewModelElementoArbol<TContenido>>();

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_raiz"></param>
		/// <param name="_padre"></param>
		/// <param name="_contenido"></param>
		public ViewModelElementoArbol(
			ViewModelVistaArbol<ViewModelElementoArbol<TContenido>, TContenido> _raiz,
			ViewModelElementoArbol<TContenido> _padre,
			TContenido _contenido)
		{
			Raiz            = _raiz;
			Padre           = _padre;
			Contenido       = _contenido;
			HostDragAndDrop = Raiz;

			if (Raiz == null)
			{
				SistemaPrincipal.LoggerGlobal.LogCrash($"{nameof(Raiz)} no puede ser null");
			}
		}
	}
}
