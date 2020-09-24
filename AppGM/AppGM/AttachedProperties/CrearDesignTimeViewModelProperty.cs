using System;
using System.ComponentModel;
using System.Windows;

namespace AppGM
{
    public class CrearDesignTimeViewModelProperty : BaseAttachedProperty<Type, CrearDesignTimeViewModelProperty>
    {
        public override void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(d))
                return;

            if (d is FrameworkElement elemento && e.NewValue is Type nuevoValor)
            {
                elemento.DataContext = Activator.CreateInstance(nuevoValor);
            }
        }
    }
}
