using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using AppGM.Core;

namespace AppGM
{
    public class BotonMenuRolProperty : BaseAttachedProperty<BaseViewModel, BotonMenuRolProperty>
    {
        public override void OnValueChanged_Impl(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Button b)
            {
                BaseViewModel vm = (BaseViewModel)e.NewValue;
                IBotonSeleccionado<object> botonSeleccionado = (IBotonSeleccionado<object>) vm;

                b.Click += (o, ea) =>
                {
                    if (botonSeleccionado.BotonSeleccionado == o)
                        return;

                    botonSeleccionado.BotonSeleccionado = o;

                    ((Button)o).BorderThickness = new Thickness(0, 0, 2, 0);

                    PropertyChangedEventHandler propertyChangedEventListener = null;

                    propertyChangedEventListener = (o2, ea2) =>
                    {
                        if (ea2.PropertyName != nameof(IBotonSeleccionado<object>.BotonSeleccionado))
                            return;

                        ((Button)o).BorderThickness = new Thickness(0);

                        vm.PropertyChanged -= propertyChangedEventListener;
                    };

                    vm.PropertyChanged += propertyChangedEventListener;
                };
            }
        }
    }
}
