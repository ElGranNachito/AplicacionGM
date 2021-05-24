using System.Windows;

namespace AppGM
{
    /// <summary>
    /// AttachedProperty base, clase abstracta para que hereden otras attached properties
    /// </summary>
    /// <typeparam name="Type">Tipo de valor que almacena la <see cref="DependencyProperty"/></typeparam>
    /// <typeparam name="OwnerType">Clase dueña de la propiedad</typeparam>
    public abstract class BaseAttachedProperty<Type, OwnerType>
        where OwnerType: BaseAttachedProperty<Type, OwnerType>, new()
    {
        /// <summary>
        /// Instancia del dueño de la propiedad, la necesitamos para llamar su implementacion de la funcion <see cref="OnValueChanged"/>
        /// </summary>
        private static OwnerType mInstancia = null;

        /// <summary>
        /// Propiedad
        /// </summary>
        public static readonly DependencyProperty ValueProperty = 
            DependencyProperty.RegisterAttached(
	            "Value",
	            typeof(Type),
	            typeof(BaseAttachedProperty<Type, OwnerType>),
	            new PropertyMetadata(default(Type), OnValueChanged, OnValueChangedCoerce));

        /// <summary>
        /// Funcion estatica encargada de establecer el valor de la propiedad para un determinado <see cref="DependencyObject"/>
        /// </summary>
        /// <param name="d">Elemento en el cual guardar el valor de la propiedad</param>
        /// <param name="nuevoValor">Valor que darle a la propiedad</param>
        public static void SetValue(DependencyObject d, Type nuevoValor) => d.SetValue(ValueProperty, nuevoValor);

        /// <summary>
        /// Funciona encarga de obtener el valor de la propiedad de un <see cref="DependencyObject"/>
        /// </summary>
        /// <param name="d">Elemento del cual obtener el valor</param>
        /// <returns>El valor de la propiedad para ese <see cref="DependencyObject"/></returns>
        public static Type GetValue(DependencyObject d) => (Type) d.GetValue(ValueProperty);

        /// <summary>
        /// Funcion llamada cuando el valor de la propiedad para un elemento es modificado
        /// </summary>
        /// <param name="d">Elemento cuyo valor de esta propiedad cambio</param>
        /// <param name="e">Argumentos del evento</param>
        public static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Si la instancia de la clase que implementa el metodo es null creamos una instancia
            mInstancia ??= new OwnerType();

            //Llamamos la version de la clase
            mInstancia.OnValueChanged_Impl(d, e);
        }

        /// <summary>
        /// Funcion que se llama cuando se asigna un valor a la propiedad aun si este valor es el mismo que ya contenia la propiedad
        /// </summary>
        /// <param name="d">Elemento cuyo valor de esta propiedad cambio</param>
        /// <param name="obj">Argumentos del evento</param>
        /// <returns></returns>
        public static object OnValueChangedCoerce(DependencyObject d, object obj)
        {
            mInstancia ??= new OwnerType();

            mInstancia.OnValueChangedCoerce_Impl(d);

            return obj;
        }

        /// <summary>
        /// Funcion llamada cuando el valor de la propiedad para un elemento es modificado.
        /// Esta funcion esta diseñada para la implementacion de clases que deriven de <see cref="BaseAttachedProperty{Type,OwnerType}"/>
        /// </summary>
        /// <param name="d">Elemento cuyo valor de esta propiedad cambio</param>
        /// <param name="e">Argumentos del evento</param>
        public virtual void OnValueChanged_Impl(DependencyObject d, DependencyPropertyChangedEventArgs e){}

        /// <summary>
        /// Funcion que se llama cuando se asigna un valor a la propiedad aun si este valor es el mismo que ya contenia la propiedad
        /// Esta funcion esta diseñada para la implementacion de clases que deriven de <see cref="BaseAttachedProperty{Type,OwnerType}"/>
        /// </summary>
        /// <param name="d">Elemento cuyo valor de esta propiedad cambio</param>
        public virtual void OnValueChangedCoerce_Impl(DependencyObject d){}
    }
}