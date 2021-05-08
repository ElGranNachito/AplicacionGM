using System.Windows;
using AppGM.Core;

namespace AppGM.AttachedProperties
{
    public abstract class BaseAttachedPropertyParametro<Type, OwnerType>
        where OwnerType: BaseAttachedPropertyParametro<Type, OwnerType>, new()
    {
        public static OwnerType instancia = null;

        public static readonly DependencyProperty ParametroProperty = 
            DependencyProperty.RegisterAttached("Parametro", typeof(Type), typeof(BaseAttachedPropertyParametro<Type, OwnerType>), new PropertyMetadata(default(Type), OnValueChanged, OnValueChangedCoerce));

        public static void SetParametro(DependencyObject d, Type nuevoValor) =>
            d.SetValue(ParametroProperty, nuevoValor);
        public static Type GetParametro(DependencyObject d) => 
            (Type) d.GetValue(ParametroProperty);

        public static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            instancia = instancia ?? new OwnerType();

            instancia.OnValueChanged_Impl(d, e);
        }

        public static object OnValueChangedCoerce(DependencyObject d, object valor)
        {
            instancia = instancia ?? new OwnerType();

            instancia.OnValueChangedCoerce_Impl(d, valor);

            return valor;
        }

        public virtual void OnValueChanged_Impl(DependencyObject d, DependencyPropertyChangedEventArgs e){}
        public virtual void OnValueChangedCoerce_Impl(DependencyObject d, object valor){}
    }
}
