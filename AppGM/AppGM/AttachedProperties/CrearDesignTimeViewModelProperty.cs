using System;
using System.ComponentModel;
using System.Windows;

namespace AppGM
{
    /// <summary>
    /// Crea una instancia de un VM solo en el diseñador. Es decir que cuando el programa este corriendo
    /// no se creara el VM
    /// </summary>
    //TODO: Posiblemente borrar, no estoy seguro de porque existe esta propiedad pero por las dudas la comento
    public class CrearDesignTimeViewModelProperty : BaseAttachedProperty<Type, CrearDesignTimeViewModelProperty>
    {
        public override void OnValueChanged_Impl(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(d))
                return;

            if (d is FrameworkElement elemento && e.NewValue is Type nuevoValor)
            {
                //Creamos el VM a partir de un Type
                elemento.DataContext = Activator.CreateInstance(nuevoValor);
            }
        }
    }
}
