using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using AppGM.Core;

namespace AppGM
{
    public class BotonMenuRolProperty : BaseAttachedProperty<BaseViewModel, BotonMenuRolProperty>
    {
        public override void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BaseViewModel vm = (BaseViewModel) e.NewValue;
            IBotonSeleccionado<object> botonSeleccionado = (IBotonSeleccionado<object>)vm;

            if (d is Button b)
            {
                b.Click += (o, ea) =>
                {
                    if (botonSeleccionado.BotonSeleccionado == b)
                        return;

                    b.BorderThickness = new Thickness(0, 0, 2, 0);

                    botonSeleccionado.BotonSeleccionado = b;

                    PropertyChangedEventHandler propertyChangedEventListener = null;

                    propertyChangedEventListener = (o2, ea2) =>
                    {
                        if (ea2.PropertyName != nameof(botonSeleccionado.BotonSeleccionado))
                            return;

                        b.BorderThickness = new Thickness(0);
                        vm.PropertyChanged -= propertyChangedEventListener;
                    };

                    vm.PropertyChanged += propertyChangedEventListener;
                };
            }
        }

        
    }
}
