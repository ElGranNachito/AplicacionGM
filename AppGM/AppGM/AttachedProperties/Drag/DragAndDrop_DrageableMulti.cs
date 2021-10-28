using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using AppGM.Core;
using CoolLogs;

namespace AppGM
{
	public class DragAndDrop_DrageableMulti : DragAndDrop_DrageableBase<DragAndDrop_DrageableMulti>
	{
		public override void OnValueChanged_Impl(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is FrameworkElement fe)
			{
				RoutedEventHandler elementoCargadoEventHandler = null;

				elementoCargadoEventHandler = (sender, args) =>
				{
					if (fe.DataContext is IDrageableMultiple dragueable)
					{
						PrepararEventos(fe, dragueable);
					}
					else
					{
						SistemaPrincipal.LoggerGlobal.Log($"Datacontext de {fe} debe implementar la interfaz {nameof(IDrageableMultiple)}", ESeveridad.Error);

						return;
					}

					fe.Loaded -= elementoCargadoEventHandler;
				};

				fe.Loaded += elementoCargadoEventHandler;
			}
		}

		private void PrepararEventos(FrameworkElement fe, IDrageableMultiple dragueable)
		{
			MouseButtonEventHandler mouseSobreElementoEventHandler = null;

			mouseSobreElementoEventHandler = (sender, args) =>
			{
				//Si alguna de estas teclas esta presionada nos pegamos la vuelta
				if(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
					return;

				//Comenzamos el drag
				SistemaPrincipal.Drag.ComenzarDragMultiple(dragueable.HostDragAndDrop.ElementosSeleccionados,
					new KeyValuePair<int, object>[] { new(KeysParametrosDrag.IndicePrametroExtra, GetParametroDrag(fe)) });

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

			fe.PreviewMouseDown += mouseSobreElementoEventHandler;
		}
	}
}
