using System.Windows;

namespace AppGM.AttachedProperties
{
    //Para comentarios detallados de lo que ocurre aqui ir a BaseAttachedProperty

	/// <summary>
	/// AttachedPropertyParametro base, clase abstracta para que hereden otras attached properties parametro.
	/// El proposito de estas propiedades es aportar datos extra para el uso de otras <see cref="BaseAttachedProperty{Type,OwnerType}"/>
	/// </summary>
	/// <typeparam name="Type">Tipo de valor que almacena la <see cref="DependencyProperty"/></typeparam>
	/// <typeparam name="OwnerType">Clase dueña de la propiedad</typeparam>
	//TODO: Considerar remover esta clase y combinarla con BaseAttachedProperty para simplificar el tramite
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
