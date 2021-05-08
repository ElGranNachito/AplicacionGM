using System.Windows;

namespace AppGM
{
    public abstract class BaseAttachedProperty<Type, OwnerType>
        where OwnerType: BaseAttachedProperty<Type, OwnerType>, new()
    {
        private static OwnerType mInstancia = new OwnerType();

        public static readonly DependencyProperty ValueProperty = 
            DependencyProperty.RegisterAttached("Value", typeof(Type), typeof(BaseAttachedProperty<Type, OwnerType>), new PropertyMetadata(default(Type), OnValueChanged, OnValueChangedCoerce));

        public static void SetValue(DependencyObject d, Type nuevoValor) => d.SetValue(ValueProperty, nuevoValor);
        public static Type GetValue(DependencyObject d) => (Type) d.GetValue(ValueProperty);

        public static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            mInstancia = mInstancia ?? new OwnerType();

            mInstancia.OnValueChanged_Impl(d, e);
        }

        public static object OnValueChangedCoerce(DependencyObject d, object obj)
        {
            mInstancia = mInstancia ?? new OwnerType();

            mInstancia.OnValueChangedCoerce_Impl(d);

            return obj;
        }
        public virtual void OnValueChanged_Impl(DependencyObject d, DependencyPropertyChangedEventArgs e){}
        public virtual void OnValueChangedCoerce_Impl(DependencyObject d){}
    }
}
