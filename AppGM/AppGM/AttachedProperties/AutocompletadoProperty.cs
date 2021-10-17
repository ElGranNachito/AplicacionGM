using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using AppGM.Core;
using AppGM.Core.Delegados;

using CoolLogs;

namespace AppGM
{
	/// <summary>
	/// Propiedad para permitir el autocompletado en un <see cref="TextBox"/>
	/// </summary>
	public class AutocompletadoProperty : BaseAttachedProperty<bool, AutocompletadoProperty>
	{
		public override void OnValueChanged_Impl(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			//Confirmamos que el DO que implementa esta propiedad sea un TextBox
			if (d is TextBox textBox)
			{
				RoutedEventHandler textBoxLoadedEventHandler = null;

				//Delegado que se llama cuando la TextBox carga
				textBoxLoadedEventHandler = (sender, args) =>
				{
					//Verificamos que el DataContext implemente IAutocompletable
					if (!(textBox.DataContext is IAutocompletable vm))
						return;

					//Configuramos eventos
					PrepararAutocompletado(textBox, vm);

					//Nos desubscribimos del evento
					textBox.Loaded -= textBoxLoadedEventHandler;
				};

				textBox.Loaded += textBoxLoadedEventHandler;
			}
			else
				SistemaPrincipal.LoggerGlobal.Log($"Se intento implementar {nameof(AutocompletadoProperty)} en un {nameof(DependencyObject)} que no es un {nameof(TextBox)}", ESeveridad.Advertencia);
		}

		private void PrepararAutocompletado(TextBox textBox, IAutocompletable vm)
		{
			RoutedEventHandler gotFocusHandler = null;
			RoutedEventHandler lostFocusHandler = null;
			RoutedEventHandler selectionChangedHandler = null;

			KeyEventHandler teclaPresionadaHandler = null;

			TextChangedEventHandler textChangedHandler = null;

			DVariableCambio<string> textoModificadoEnVMHandler = null;

			//Delegado que se llama cuando el usuario seleccione el TextBox
			gotFocusHandler += (sender, args) =>
			{
				//Verifivamos que el sender sea un TextBox y su DataContext implemente IAutocompletable
				if (sender is TextBox textBox
				    && textBox.DataContext is IAutocompletable vm)
				{
					//TODO: Mejorar esta tecnica de tomar posicion
					Point posTextboxAbs = textBox.PointToScreen(new Point(0, 0));
					Point posParentAbs = ((FrameworkElement)textBox.Parent).PointToScreen(new Point(0, 0));

					var parentActual = (FrameworkElement)textBox.Parent;

					while(!parentActual.GetType().IsSubclassOf(typeof(UserControl)))
					{
						parentActual = (FrameworkElement)parentActual.Parent;
					}

					posTextboxAbs = Mouse.GetPosition(parentActual);

					//Actualizamos la posicion
					vm.Autocompletado.PosicionX = posTextboxAbs.X - textBox.ActualWidth / 2;
					vm.Autocompletado.PosicionY = posTextboxAbs.Y + textBox.ActualHeight;

					//Llamamos la funcion FocusObtenido del vm
					vm.FocusObtenido();

					//Nos subscribimos a todos estos eventitos
					textBox.LostFocus        += lostFocusHandler;
					textBox.TextChanged      += textChangedHandler;
					textBox.KeyUp            += teclaPresionadaHandler;
					textBox.SelectionChanged += selectionChangedHandler;
				}
			};
			
			//Cuando el TextBox pierde el Focus
			lostFocusHandler += (sender, args) =>
			{
				//Mas de lo mismo
				if (sender is TextBox textBox
				    && textBox.DataContext is IAutocompletable vm)
				{
					//Llamamos la funcion FocusPerdido del vm
					vm.FocusPerdido();

					//Hacemos invisible la Ventana de autocompletado
					vm.Autocompletado.EsVisible = false;

					//Nos desubscribimos de los eventos a los que nos subscribimos antes
					textBox.LostFocus        -= lostFocusHandler;
					textBox.TextChanged      -= textChangedHandler;
					textBox.KeyUp            -= teclaPresionadaHandler;
					textBox.SelectionChanged -= selectionChangedHandler;
				}
			};

			//Delegado llamado cuando el texto del TextBox cambia
			textChangedHandler += (sender, args) =>
			{
				if (sender is TextBox textBox
				    && textBox.DataContext is IAutocompletable vm)
				{
					if (textBox.Text == vm.TextoAnterior)
						return;

					vm.TextoActual = textBox.Text;

					//Si en el nuevo texto ingresado hay caracteres no permitidos...
					if (vm.ExpresionRegularDetectarCaracteresNoPermitidos.IsMatch(textBox.Text))
					{
						//Vamo pa tra
						textBox.Text = vm.TextoAnterior;
						
						return;
					}

					//TODO: Ver
					
					//Hacemos visible la ventana
					vm.Autocompletado.EsVisible = true;

					//Actualizamos el autocompletado
					vm.ActualizarPosibilidadesAutocompletado(textBox.Text, textBox.CaretIndex);

					vm.TextoAnterior = vm.TextoActual;
				}
			};

			//Delegado llamado cuando se cambia el texto de la textBox a traves del VM
			textoModificadoEnVMHandler += (anterior, actual) =>
			{
				textBox.TextChanged -= textChangedHandler;

				textBox.Text = actual;

				if(textBox.IsFocused)
					textBox.TextChanged += textChangedHandler;
			};

			//Delegado llamado cuando la posicion del signo de intercalacion cambia
			selectionChangedHandler += (sender, args) =>
			{
				if (sender is TextBox textBox
				    && textBox.DataContext is IAutocompletable vm)
				{
					vm.PosSignoIntercalacion = textBox.CaretIndex;
				}
			};

			//Delegado llamado cuando el usuario presiona cualqueir tecla
			teclaPresionadaHandler += (sender, args) =>
			{
				if (sender is TextBox textBox
				    && textBox.DataContext is IAutocompletable vm)
				{
					//Hacemos switch sobre la tecla presionada
					switch (args.Key)
					{
						//Si fue enter...
						case Key.Enter:
							vm.Autocompletado.SeleccionarValor();

							//Actualizamos la posicion del indicador
							textBox.CaretIndex = textBox.Text.Length;

							vm.PosSignoIntercalacion = textBox.CaretIndex;

							break;

						//Si fue la flechita hacia abajo
						case Key.Down:
							vm.Autocompletado.IncrementarIndice();
							break;

						//Si fue la flechita hacia arriba
						case Key.Up:
							vm.Autocompletado.DisminuirIndice();
							break;

						//Si fue cualquier otra no hacemos nada
						default:
							break;
					}
				}
			};

			textBox.GotFocus           += gotFocusHandler;
			//vm.OnTextoActualModificado += textoModificadoEnVMHandler;
		}
	}
}