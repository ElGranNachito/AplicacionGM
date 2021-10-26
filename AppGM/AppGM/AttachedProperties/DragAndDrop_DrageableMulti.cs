using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using AppGM.Core;
using CoolLogs;

namespace AppGM
{
	public class DragAndDrop_DrageableMulti : DragAndDrop_Drageable
	{
		public override void OnValueChanged_Impl(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is FrameworkElement fe)
			{
				RoutedEventHandler elementoCargadoEventHandler = null;

				elementoCargadoEventHandler = (sender, args) =>
				{
					if (fe.DataContext is IReceptorDeDragMultiple receptorDeDrag)
					{
						PrepararEventos(fe, receptorDeDrag);
					}
					else
					{
						SistemaPrincipal.LoggerGlobal.Log($"Datacontext de {fe} debe implementar la interfaz {nameof(IReceptorDeDragMultiple)}", ESeveridad.Error);

						return;
					}

					fe.Loaded -= elementoCargadoEventHandler;
				};

				fe.Loaded += elementoCargadoEventHandler;
			}
		}

		private void PrepararEventos(FrameworkElement fe, IReceptorDeDragMultiple receptorDeDrag)
		{
			MouseButtonEventHandler mouseSobreElementoEventHandler = null;

			mouseSobreElementoEventHandler = (sender, args) =>
			{
				//Comenzamos el drag
				SistemaPrincipal.Drag.ComenzarDragMultiple(
					receptorDeDrag.HostDragAndDrop.ElementosSeleccionados,
					new KeyValuePair<int, object>[] { new(KeysParametrosDrag.IndicePrametroExtra, GetParametroDrag(fe)) });

				EventoVentana eventoMouseMovido = null;

				ActualizarPosMouse(fe);

				//Handler para actualizar la posicion del elemento cuando se mueve el mouse
				eventoMouseMovido = ventana =>
				{
					ActualizarPosMouse(fe);
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

			fe.MouseDown += mouseSobreElementoEventHandler;
		}
	}
}
