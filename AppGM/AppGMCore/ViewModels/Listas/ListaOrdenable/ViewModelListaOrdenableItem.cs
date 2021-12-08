using System.Windows.Input;

using AppGM.Core.Delegados;

namespace AppGM.Core
{
	/// <summary>
	/// Item de una <see cref="ViewModelListaOrdenable{TItems}"/>
	/// </summary>
	/// <typeparam name="TItem">Tipo del contenido de este item</typeparam>
	public class ViewModelListaOrdenableItem<TItem, TContenido> : ViewModel

		where TItem: ViewModelListaOrdenableItem<TItem, TContenido>
	{
		#region Eventos

		/// <summary>
		/// Evento que se dispara cuando la posicion de este item en la lista es modificada
		/// </summary>
		public event DVariableCambio<int> OnPosicionModificada = delegate { }; 

		#endregion

		#region Campos & Propiedades

		/// <summary>
		/// <see cref="ViewModelListaOrdenable{TItems, TContenido}"/> que contiene este item
		/// </summary>
		public readonly ViewModelListaOrdenable<TItem, TContenido> contenedor;

		/// <summary>
		/// Posicion actual de este item en la lista
		/// </summary>
		public int Posicion
		{
			get => contenedor.Items.Elementos.IndexOf((TItem)this);
			set
			{
				var posicionAnterior = Posicion;

				if (contenedor.ModificarPosicionItem((TItem)this, value) == -1)
					return;

				OnPosicionModificada(posicionAnterior, value);
			}
		}

		/// <summary>
		/// Contenido de este item
		/// </summary>
		public TContenido Contenido { get; init; }

		/// <summary>
		/// Comando que se ejecuta cuando el usuario presiona el boton para subir la posicion del item
		/// </summary>
		public ICommand ComandoSubirPosicion { get; private set; }

		/// <summary>
		/// Comando que se ejecuta cuando el usuario presiona el boton para bajar la posicion del item
		/// </summary>
		public ICommand ComandoBajarPosicion { get; private set; } 

		#endregion

		#region Constructor

		public ViewModelListaOrdenableItem(TContenido _contenido, ViewModelListaOrdenable<TItem, TContenido> _contenedor)
		{
			Contenido = _contenido;
			contenedor = _contenedor;

			if (Contenido == null)
				SistemaPrincipal.LoggerGlobal.LogCrash($"{nameof(_contenido)} no puede ser null");

			if (contenedor == null)
				SistemaPrincipal.LoggerGlobal.LogCrash($"{nameof(_contenedor)} no puede ser null");

			contenedor.Items.Elementos.CollectionChanged += (sender, args) =>
			{
				DispararPropertyChanged(nameof(Posicion));
			};

			ComandoSubirPosicion = new Comando(() =>
			{
				Posicion--;
			});

			ComandoBajarPosicion = new Comando(() =>
			{
				Posicion++;
			});
		} 

		#endregion
	}
}