using System.Windows;

namespace AppGM
{
    public abstract class BaseAttachedProperty<Type, OwnerType>
        where OwnerType: BaseAttachedProperty<Type, OwnerType>, new()
    {
        private static OwnerType instancia = new OwnerType();

        public static readonly DependencyProperty ValueProperty = 
            DependencyProperty.RegisterAttached("Value", typeof(Type), typeof(BaseAttachedProperty<Type, OwnerType>), new PropertyMetadata(instancia.OnValueChanged));

        public static void SetValue(DependencyObject d, Type nuevoValor) => d.SetValue(ValueProperty, nuevoValor);
        public static Type GetValue(DependencyObject d) => (Type) d.GetValue(ValueProperty);

        public virtual void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e){}
    }
}
