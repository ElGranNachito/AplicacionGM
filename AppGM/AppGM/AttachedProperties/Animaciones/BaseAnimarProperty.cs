using System.Threading.Tasks;
using System.Windows;

namespace AppGM
{
    //Comentarios mas detallados en BaseAttachedProperty

    /// <summary>
    /// Propiedad que sirve para realizar una animacion sobre un <see cref="FrameworkElement"/>
    /// </summary>
    /// <typeparam name="OwnerType">Tipo que hereda la clase base</typeparam>
    public class BaseAnimarProperty<OwnerType>
        where OwnerType: BaseAnimarProperty<OwnerType>, new()
    {
        private static OwnerType mInstancia = new OwnerType();

        public static readonly DependencyProperty DebeRealizarAnimacionProperty = 
            DependencyProperty.RegisterAttached("DebeRealizarAnimacion", typeof(bool), typeof(BaseAnimarProperty<OwnerType>), new PropertyMetadata(mInstancia.OnValueChanged));

        public static void SetDebeRealizarAnimacion(DependencyObject d, bool nuevoValor) =>
            d.SetValue(DebeRealizarAnimacionProperty, nuevoValor);
        public static bool GetDebeRealizarAnimacion(DependencyObject d) => 
            (bool)d.GetValue(DebeRealizarAnimacionProperty);

        /// <summary>
        /// Metodo que se encarga de lidiar con los cambios de valor de la propiedad
        /// </summary>
        /// <param name="d">Elemento que implementa la propiedad</param>
        /// <param name="e">Argumentos del evento</param>
        protected virtual async void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement fe)
                await mInstancia.RealizarAnimacion(fe, (bool)e.NewValue);
        }

        /// <summary>
        /// Metodo encargado de realizar la animacion
        /// </summary>
        /// <param name="fe">Elemento sobre el que se realizara la animacion</param>
        /// <param name="valor"><see cref="bool"/> indicando que animacion se realizara</param>
        /// <returns></returns>
        protected virtual async Task RealizarAnimacion(FrameworkElement fe, bool valor){}
    }
}
