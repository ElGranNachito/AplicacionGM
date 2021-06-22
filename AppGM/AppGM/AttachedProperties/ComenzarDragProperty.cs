using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using AppGM.Core;

namespace AppGM
{
	/// <summary>
	/// <see cref="DependencyProperty"/> para los <see cref="FrameworkElement"/> que puedan ser arrastrados
	/// </summary>
	public class ComenzarDragProperty : BaseAttachedProperty<bool, ComenzarDragProperty>
	{
		/// <summary>
		/// Parametro extra que se pasara al drag
		/// </summary>
		public static readonly DependencyProperty ParametroDragProperty = 
			DependencyProperty.RegisterAttached("ParametroDrag", typeof(object), typeof(ComenzarDragProperty));

		public static object GetParametroDrag(DependencyObject d) => d.GetValue(ParametroDragProperty);

		public static void SetParametroDrag(DependencyObject d, object valor) => d.SetValue(ParametroDragProperty, valor);

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
			}
		}

		private void ActualizarPosMouse(FrameworkElement fe)
		{
			//Obtenemos la posicion del mouse con respecto al padre del elemento
			Point posMouse = Mouse.GetPosition((IInputElement)SistemaPrincipal.Aplicacion.VentanaActual.ObtenerInstanciaVentana());

			//Actualizamos la posicion
			SistemaPrincipal.Drag.OffsetControl = new Grosor(posMouse.X, posMouse.Y, 0, 0);
		}
	}
}
