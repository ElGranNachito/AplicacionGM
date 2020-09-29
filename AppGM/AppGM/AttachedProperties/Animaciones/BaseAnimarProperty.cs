using System.Threading.Tasks;
using System.Windows;

namespace AppGM
{
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
        public virtual async void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement fe)
            {
                await mInstancia.RealizarAnimacion(fe, (bool)e.NewValue);
            }
        }

        public virtual async Task RealizarAnimacion(FrameworkElement fe, bool valor){}
    }
}
