using System;
using System.Windows;

namespace AppGM.AttachedProperties
{
    public abstract class BaseAttachedPropertyParametro<Type, OwnerType>
        where OwnerType: BaseAttachedPropertyParametro<Type, OwnerType>, new()
    {
        public static OwnerType instancia = new OwnerType();

        public static readonly DependencyProperty ParametroProperty = 
            DependencyProperty.RegisterAttached("Parametro", typeof(Type), typeof(BaseAttachedPropertyParametro<Type, OwnerType>), new PropertyMetadata(instancia.OnValueChanged));

        public static void SetParametro(DependencyObject d, Type nuevoValor) =>
            d.SetValue(ParametroProperty, nuevoValor);
        public static Type GetParametro(DependencyObject d) => 
            (Type) d.GetValue(ParametroProperty);

        public virtual void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e){}
    }
}
