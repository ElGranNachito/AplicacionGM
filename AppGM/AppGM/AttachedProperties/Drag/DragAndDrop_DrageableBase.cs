using System.Windows;
using System.Windows.Input;
using AppGM.Core;

namespace AppGM
{
	/// <summary>
	/// <see cref="DependencyProperty"/> base para los <see cref="FrameworkElement"/> que puedan ser arrastrados
	/// </summary>
	public class DragAndDrop_DrageableBase<TDrag> : BaseAttachedProperty<bool, TDrag>

		where TDrag : DragAndDrop_DrageableBase<TDrag>, new()
	{
		/// <summary>
		/// Parametro extra que se pasara al drag
		/// </summary>
		public static readonly DependencyProperty ParametroDragProperty =
			DependencyProperty.RegisterAttached("ParametroDrag", typeof(object), typeof(DragAndDrop_Drageable));

		public static object GetParametroDrag(DependencyObject d) => d.GetValue(ParametroDragProperty);

		public static void SetParametroDrag(DependencyObject d, object valor) => d.SetValue(ParametroDragProperty, valor);

		/// <summary>
		/// Actualiza el offset del drag a partir de la posicion actual del mouse respecto de la ventana actual
		/// </summary>
		protected void ActualizarPosMouse()
		{
			//Obtenemos la posicion del mouse con respecto al padre del elemento
			Point posMouse = Mouse.GetPosition((IInputElement)SistemaPrincipal.Aplicacion.VentanaActual.ObtenerInstanciaVentana());

			//Actualizamos la posicion
			SistemaPrincipal.Drag.OffsetControl = new Grosor(posMouse.X, posMouse.Y, 0, 0);
		}
	}
}
