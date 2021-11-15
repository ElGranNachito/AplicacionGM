using System;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa un <see cref="ModeloItem"/> o una <see cref="ModeloParteDelCuerpo"/> en el inventario de un <see cref="ModeloPersonaje"/>
	/// </summary>
	public class ViewModelElementoArbolItemInventario : ViewModelElementoArbol<ControladorSlot>
	{
		#region Eventos

		/// <summary>
		/// Delegado que representa un metodo destinado a lidiar con eventos de transferencia de contenido
		/// </summary>
		/// <param name="contenedorAnterior">Contenedor anterior del contenido</param>
		/// <param name="contenedorActual">Contenedor actual del contenido</param>
		public delegate void dContenidoTransferido(ViewModelElementoArbolItemInventario contenedorAnterior, ViewModelElementoArbolItemInventario contenedorActual);

		/// <summary>
		/// Evento disparado cuando el contenido de este elemento es transferido a otro
		/// </summary>
		public event dContenidoTransferido OnContenidoTransferido = delegate { };

		#endregion

		#region Campos & Propiedades

		protected ControladorSlot mContenido;

		public bool EsParteDelCuerpo => Contenido.ParteDelCuerpoAlmacenada != null;

		public bool EsItem => !EsParteDelCuerpo;

		public string DescripcionSlot => Contenido.ToString();

		public ETipoItem TiposDeItemQueMostrar { get; set; }

		public override ControladorSlot Contenido
		{
			get => mContenido;
			set
			{
				if (value != null)
				{
					mContenido = value;

					return;
				}

				SistemaPrincipal.LoggerGlobal.Log($"Se intento asignar {value} al {nameof(Contenido)} de un {nameof(ViewModelElementoArbolItemInventario)}", ESeveridad.Error);
			}
		}

		#endregion

		#region Constructor

		public ViewModelElementoArbolItemInventario(
			ViewModelVistaArbol<ViewModelElementoArbol<ControladorSlot>, ControladorSlot> _raiz,
			ViewModelElementoArbol<ControladorSlot> _padre,
			ControladorSlot _slot,
			ETipoItem _tiposDeItemQueMostrar) : base(_raiz, _padre, _slot)
		{
			mContenido = _slot;
			TiposDeItemQueMostrar = _tiposDeItemQueMostrar;

			mContenido.modelo.OnModeloEliminado += m =>
			{
				RemoverDePadre();
			};
		} 

		#endregion

		#region Metodos

		public override bool PuedeSerSeleccionado()
		{
			return base.PuedeSerSeleccionado();
		}

		public override void ActualizarHijos()
		{
			Hijos.Elementos.Clear();

			if (Contenido.ParteDelCuerpoAlmacenada != null)
			{
				foreach (var slot in Contenido.ParteDelCuerpoAlmacenada.Slots)
				{
					Hijos.Add(new ViewModelElementoArbolItemInventario(Raiz, this, slot, TiposDeItemQueMostrar));
				}
			}

			foreach (var item in Contenido.ControladoresItemsAlmacenados)
			{
				foreach (var controladorSlot in item.Slots)
				{
					Hijos.Add(new ViewModelElementoArbolItemInventario(Raiz, this, controladorSlot, TiposDeItemQueMostrar));
				}
			}
		}

		public override void Actualizar()
		{
			base.Actualizar();

			DispararPropertyChanged(nameof(DescripcionSlot));
			DispararPropertyChanged(nameof(EsItem));
			DispararPropertyChanged(nameof(EsParteDelCuerpo));
		} 

		#endregion
	}
}
