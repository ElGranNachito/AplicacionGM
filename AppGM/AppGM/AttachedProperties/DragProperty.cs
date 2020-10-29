﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using AppGM.Core;

namespace AppGM
{
    public class DragProperty : BaseAttachedProperty<bool, DragProperty>
    {
        public override void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement fe)
            {
                ViewModelIngresoPosicion vm = null;
                FrameworkElement contenedorImagenes = null;

                RoutedEventHandler loadedEventHandler = null;

                //Esperamos a que el elemento cargue para poder obtener el view model
                loadedEventHandler = (obj, ea) =>
                {
                    vm = (ViewModelIngresoPosicion)fe.DataContext;
                    contenedorImagenes = (FrameworkElement)vm.mapa.ContenedorImagenes;

                    fe.Loaded -= loadedEventHandler;
                };

                fe.Loaded += loadedEventHandler;

                MouseEventHandler mouseMovedHandler = (obj, ea) =>
                {
                    Point nuevaPosicion = Mouse.GetPosition((IInputElement)fe.Parent);

                    //Revisamos que la nueva posicion este dentro de los limites del canvas
                    if (nuevaPosicion.X <= vm.mapa.TamañoCanvasX
                        && nuevaPosicion.X >= 0)
                        vm.Posicion.X = nuevaPosicion.X;

                    if (nuevaPosicion.Y <= vm.mapa.TamañoCanvasY
                        && nuevaPosicion.Y >= 0)
                        vm.Posicion.Y = nuevaPosicion.Y;

                    //Disparamos los eventos de property changed para que se actualice el texto de las textbox y la posicion de la imagen
                    vm.DispararPropertyChanged(new PropertyChangedEventArgs(nameof(vm.TextoPosicionX)));
                    vm.DispararPropertyChanged(new PropertyChangedEventArgs(nameof(vm.TextoPosicionY)));
                    vm.DispararPropertyChanged(new PropertyChangedEventArgs(nameof(vm.PosicionImg)));
                };

                MouseButtonEventHandler añadirMouseMovedHandler = null;
                MouseButtonEventHandler quitarMouseMovedHandler = null;

                añadirMouseMovedHandler = (obj, ea) =>
                {
                    //Nos desuscribimos del evento para asegurarnos que no se pueda volver a disparar hasta que la imagen haya sido soltada
                    fe.MouseDown -= añadirMouseMovedHandler;
                    contenedorImagenes.MouseMove += mouseMovedHandler;

                    fe.MouseUp += quitarMouseMovedHandler;
                };

                quitarMouseMovedHandler = (obj, ea) =>
                {
                    fe.MouseUp -= quitarMouseMovedHandler;
                    contenedorImagenes.MouseMove -= mouseMovedHandler;

                    fe.MouseDown += añadirMouseMovedHandler;
                };

                fe.MouseDown += añadirMouseMovedHandler;
            }
        }
    }
}