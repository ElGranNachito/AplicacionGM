using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using AppGM.Core;

namespace AppGM
{
    /// <summary>
    /// Property para los <see cref="Button"/> de la columna derecha en el menu del rol
    /// </summary>
    public class BotonSolapaProperty : BaseAttachedProperty<ViewModel, BotonSolapaProperty>
    {
        public override void OnValueChanged_Impl(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Confirmamos que el objeto sea un boton...
            if (d is Button b)
            {
                //Obtenemos el VM
	            ViewModel vm = (ViewModel)e.NewValue;
                IBotonSeleccionado<object> botonSeleccionado = (IBotonSeleccionado<object>) vm;

                //Cuando el ususario haga click sobre el boton...
                b.Click += (o, ea) =>
                {
                    //Si el boton actualmente seleccionado es este, entonces regresamos
                    if (botonSeleccionado.BotonSeleccionado == o)
                        return;

                    //Actualizamos el boton actualmente seleccionado
                    botonSeleccionado.BotonSeleccionado = o;

                    //Cambiamos el ancho del borde del boton para mostrar que esta seleccionado
                    ((Button)o).BorderThickness = new Thickness(0, 0, 2, 0);

                    PropertyChangedEventHandler propertyChangedEventListener = null;

                    //Handler volver a cambiar el borde una vez que que el boton seleccionado cambie
                    propertyChangedEventListener = (o2, ea2) =>
                    {
	                    //Confirmamos que la propiedad modificada es la de BotonSeleccionado
                        if (ea2.PropertyName != nameof(IBotonSeleccionado<object>.BotonSeleccionado))
                            return;

                        //Hacemos que el borde nuevamente sea 0
                        ((Button)o).BorderThickness = new Thickness(0);

                        //Nos desubscribimos de PropertyChanged
                        vm.PropertyChanged -= propertyChangedEventListener;
                    };

                    //Nos subscribimos a PropertyChanged para esperar que el boton seleccionado cambie
                    vm.PropertyChanged += propertyChangedEventListener;
                };
            }
        }
    }
}
