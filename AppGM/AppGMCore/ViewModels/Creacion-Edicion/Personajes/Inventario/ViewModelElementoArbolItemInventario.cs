using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Viewmodel que representa un <see cref="ModeloItem"/> o una <see cref="ModeloParteDelCuerpo"/> en el inventario de un <see cref="ModeloPersonaje"/>
	/// </summary>
	public class ViewModelElementoArbolItemInventario : ViewModelElementoArbol<ControladorSlot>
	{
		protected ControladorSlot mContenido;

		public bool EsParteDelCuerpo => Contenido.ParteDelCuerpoAlmacenada != null;

		public bool EsItem => !EsParteDelCuerpo;

		public string DescripcionSlot => Contenido.ToString();

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

		public ViewModelElementoArbolItemInventario(
			ViewModelVistaArbol<ViewModelElementoArbol<ControladorSlot>, ControladorSlot> _raiz,
			ViewModelElementoArbol<ControladorSlot> _padre,
			ControladorSlot _slot) : base(_raiz, _padre, _slot)
		{
			mContenido = _slot;
		}

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
					Hijos.Add(new ViewModelElementoArbolItemInventario(Raiz, this, slot));
				}
			}

			foreach (var item in Contenido.ControladoresItemsAlmacenados)
			{
				foreach (var controladorSlot in item.Slots)
				{
					Hijos.Add(new ViewModelElementoArbolItemInventario(Raiz, this, controladorSlot));
				}
			}

			DispararPropertyChanged(nameof(EsParteDelCuerpo));
			DispararPropertyChanged(nameof(EsItem));
		}
	}
}
