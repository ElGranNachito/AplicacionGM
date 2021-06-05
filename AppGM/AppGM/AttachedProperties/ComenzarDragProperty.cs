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
		public override void OnValueChanged_Impl(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			//Verificamos que el objeto sea un framework element
			if (d is FrameworkElement fe)
			{
				//Cuando el usuario hace click sobre este elemento...
				fe.MouseDown += (sender, args) =>
				{
					//Comenzamos el drag
					SistemaPrincipal.Drag.ComenzarDrag((BaseViewModel)((FrameworkElement)sender).DataContext);

					EventoVentana eventoMouseMovido = null;

					//Handler para actualizar la posicion del elemento cuando se mueve el mouse
					eventoMouseMovido = ventana =>
					{
						//Obtenemos la posicion del mouse con respecto al padre del elemento
						Point posMouse = Mouse.GetPosition((FrameworkElement)fe.Parent);

						//Actualizamos la posicion
						SistemaPrincipal.Drag.OffsetControl = new Grosor(posMouse.X, posMouse.Y, 0, 0);
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
