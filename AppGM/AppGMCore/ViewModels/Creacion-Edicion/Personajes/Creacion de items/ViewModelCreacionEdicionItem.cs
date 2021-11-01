using System;

namespace AppGM.Core
{
	/// <summary>
	/// Clase que representa un control para la creacion, edicion o vista de un <see cref="ModeloItem"/>
	/// </summary>
	public class ViewModelCreacionEdicionItem : ViewModelCreacionEdicionDeModelo<ModeloItem, ControladorItem, ViewModelCreacionEdicionItem>
	{
		/// <summary>
		/// Controlador del slot que contiene este item
		/// </summary>
		public ControladorSlot ControladorSlotContenedor { get; init; }

		public ViewModelCreacionEdicionItem(Action<ViewModelCreacionEdicionItem> _accionSalir, ControladorItem _controladorParaEditar = null, ControladorSlot _controladorSlotContenedor = null) 
			: base(_accionSalir, _controladorParaEditar)
		{
			ControladorSlotContenedor = _controladorSlotContenedor;
		}

		public override ModeloItem CrearModelo()
		{
			throw new NotImplementedException();
		}

		public override ControladorItem CrearControlador()
		{
			throw new NotImplementedException();
		}

		public override string ToString()
		{
			var prefijo = ObtenerPrefijoTituloVentana();

			if (EstaEditando)
				return $"{prefijo} - {ModeloCreado.Nombre}";

			return $"{prefijo} - {ModeloCreado.TipoItem}";
		}
	}
}
