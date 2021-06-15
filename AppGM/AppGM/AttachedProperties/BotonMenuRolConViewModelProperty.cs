using AppGM.Core;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace AppGM
{
    /// <summary>
    /// Propiedad utilizada para pasar datos del VM de un <see cref="Button"/> a un <see cref="IBotonSeleccionado{T}"/>
    /// </summary>
    public class BotonMenuRolConViewModelProperty : BaseAttachedProperty<ViewModel, BotonMenuRolConViewModelProperty>
    {
        public override void OnValueChanged_Impl(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Obtenemos el VM que fue pasado como parametro y los convertimos al tipo que necesitamos
            ViewModel vm = ((IViewModelConBotonSeleccionado)(ViewModel)e.NewValue).ViewModelConBotonSeleccionado;
            IBotonSeleccionado<ViewModel> botonSeleccionado = (IBotonSeleccionado<ViewModel>) vm;

            if (d is Button b)
            {
                //Cuando el usuario haga click sobre este boton...
                b.Click += (o, ea) =>
                {
                    //Verificamos que este boton no se encuentre ya seleccionado
                    if (botonSeleccionado.BotonSeleccionado == b.DataContext)
                        return;

                    //Cambiamos el borde para señalizar que esta seleccionado
                    b.BorderThickness = new Thickness(0, 0, 2, 0);

                    //Actualizamos el boton actualmente seleccionado
                    botonSeleccionado.BotonSeleccionado = (ViewModel) b.DataContext;

                    PropertyChangedEventHandler propertyChangedEventListener = null;

                    //Handler volver a cambiar el borde una vez que que el boton seleccionado cambie
                    propertyChangedEventListener = (o2, ea2) =>
                    {
                        //Confirmamos que la propiedad modificada es la de BotonSeleccionado
                        if (ea2.PropertyName != nameof(botonSeleccionado.BotonSeleccionado))
                            return;

                        //Hacemos que el borde nuevamente sea 0
                        b.BorderThickness = new Thickness(0);

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
