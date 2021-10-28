using System.Collections.Generic;
using System.Windows;
using AppGM.Core;

namespace AppGM
{
	/// <summary>
	/// <see cref="DependencyProperty"/> para los <see cref="FrameworkElement"/> que puedan ser arrastrados
	/// </summary>
	public class DragAndDrop_Drageable : DragAndDrop_DrageableBase<DragAndDrop_Drageable>
	{
		public override void OnValueChanged_Impl(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is FrameworkElement fe)
			{
				fe.MouseDown += (sender, args) =>
				{
					//Comenzamos el drag
					SistemaPrincipal.Drag.ComenzarDrag(
						(IDrageable)((FrameworkElement)sender).DataContext,
						new KeyValuePair<int, object>[]{new (KeysParametrosDrag.IndicePrametroExtra, GetParametroDrag(fe))});

					EventoVentana eventoMouseMovido = null;

					ActualizarPosMouse();

					//Handler para actualizar la posicion del elemento cuando se mueve el mouse
					eventoMouseMovido = ventana =>
					{
						ActualizarPosMouse();
					};

					SistemaPrincipal.Aplicacion.VentanaActual.OnMouseMovido += eventoMouseMovido;

					Core.Delegados.DDrag finDragHandler = null;

					//Handler para dejar de actualizar la posicion del mouse una vez el drag finalice
					finDragHandler = contenido =>
					{
						SistemaPrincipal.Aplicacion.VentanaActual.OnMouseMovido -= eventoMouseMovido;
						SistemaPrincipal.Drag.OnFinDrag -= finDragHandler;
					};

					SistemaPrincipal.Drag.OnFinDrag += finDragHandler;
				};
			}
		}
	}
}
