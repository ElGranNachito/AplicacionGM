using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AppGM.Core;

namespace AppGM
{
    public class DragProperty : BaseAttachedProperty<bool, DragProperty>
    {
        //Sobreescribimos la funcion OnValueChanged para saber cuando el valor de esta propiedad cambie
        public override void OnValueChanged_Impl(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Si el objeto pertenece a la UI
            if (d is FrameworkElement fe)
            {
                ViewModelIngresoPosicion vm = null;

                RoutedEventHandler loadedEventHandler = null;

                //Esperamos a que el elemento cargue para poder obtener el view model
                loadedEventHandler = (obj, ea) =>
                {
                    //Casteamos el data context del elemento a un vm de ingreso de posicion
                    vm = (ViewModelIngresoPosicion)fe.DataContext;

                    //Nos desuscribimos del evento
                    fe.Loaded -= loadedEventHandler;
                    loadedEventHandler = null;
                };

                fe.Loaded += loadedEventHandler;

                //Evento que se llama cuando el mouse se mueve
                EventoVentana mouseMovedHandler = ventana =>
                {
                    var padreActual = VisualTreeHelper.GetParent(d);

                    while (padreActual is not Canvas)
                    {
                        padreActual = VisualTreeHelper.GetParent(padreActual);
                    }

                    //Obtenemos la posicion del mouse con respecto al padre de este elemento
                    Point nuevaPosicion = Mouse.GetPosition((IInputElement)padreActual);

                    //Revisamos que la nueva posicion este dentro de los limites del canvas
                    if (nuevaPosicion.X <= vm.mapa.TamañoCanvasX
                        && nuevaPosicion.X >= 0)
                        vm.Posicion.X = Math.Clamp(nuevaPosicion.X, 0, vm.mapa.TamañoCanvasX);

                    if (nuevaPosicion.Y <= vm.mapa.TamañoCanvasY
                        && nuevaPosicion.Y >= 0)
                        vm.Posicion.Y = Math.Clamp(nuevaPosicion.Y, 0, vm.mapa.TamañoCanvasY);

                    SistemaPrincipal.LoggerGlobal.Log($"X: {vm.Posicion.X}, Y: {vm.Posicion.Y} ");

                    //Disparamos los eventos de property changed para que se actualice el texto de las textbox y la posicion de la imagen
                    vm.DispararPropertyChanged(new PropertyChangedEventArgs(nameof(vm.TextoPosicionX)));
                    vm.DispararPropertyChanged(new PropertyChangedEventArgs(nameof(vm.TextoPosicionY)));
                    vm.DispararPropertyChanged(new PropertyChangedEventArgs(nameof(vm.PosicionImg)));
                    vm.DispararPropertyChanged(new PropertyChangedEventArgs(nameof(vm.PosicionCantidadUnidades)));
                };

                MouseButtonEventHandler añadirMouseMovedHandler = null;
                EventoVentana quitarMouseMovedHandler           = null;

                //Evento que se llama cuando el se hace click sobre este elemento
                añadirMouseMovedHandler = (obj, ea) =>
                {
                    //Nos desubscribimos del evento para asegurarnos que no se pueda volver a disparar hasta que la imagen haya sido soltada
                    fe.MouseDown -= añadirMouseMovedHandler;

                    //Nos subscribimos al evento de movimiento de mouse de la ventana principal
                    SistemaPrincipal.Aplicacion.VentanaPrincipal.OnMouseMovido += mouseMovedHandler;

                    //Nos subscribimos al evento de boton de mouse soltado de la ventana principal
                    SistemaPrincipal.Aplicacion.VentanaPrincipal.OnMouseUp += quitarMouseMovedHandler;
                };

                //Evento que se llama cuando el boton del mouse es soltado
                quitarMouseMovedHandler = v =>
                {
                    //Nos desubscribimos de los eventos a los que nos habiamos subscrito antes
                    SistemaPrincipal.Aplicacion.VentanaPrincipal.OnMouseUp -= quitarMouseMovedHandler;
                    SistemaPrincipal.Aplicacion.VentanaPrincipal.OnMouseMovido -= mouseMovedHandler;

                    //Nos volvemos a subscribir al evento de mouse presionado sobre este elemento
                    fe.MouseDown += añadirMouseMovedHandler;
                };

                fe.MouseDown += añadirMouseMovedHandler;

                //Cuando el elemento sea descargado...
                fe.Unloaded += (sender, args) =>
                {
                    //Colocamos en null todos los eventos para que no mantengan vivos a los elementos
                    añadirMouseMovedHandler = null;
                    quitarMouseMovedHandler = null;
                    mouseMovedHandler = null;
                };
            }
        }
    }
}
