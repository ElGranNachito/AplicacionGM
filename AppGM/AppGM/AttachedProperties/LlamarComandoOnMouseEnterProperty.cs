using System.Windows;
using System.Windows.Input;

namespace AppGM
{
    /// <summary>
    /// Ejecuta un <see cref="ICommand"/> cuando se dispara el evento MouseEnter en un determinado elemento
    /// Es posible añadir como parametro un <see cref="ParametroComandoOnLeaveProperty"/> para que ejecute
    /// otro comando cuando se dispara el evento MouseLeave
    /// </summary>
    public class LlamarComandoOnMouseEnterProperty : BaseAttachedProperty<ICommand, LlamarComandoOnMouseEnterProperty>
    {
        public override void OnValueChanged_Impl(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement fe)
            {
                //Subscribimos al evento MouseEnter del elemento
                fe.MouseEnter += (o, ea) => 
                    ((ICommand) e.NewValue).Execute(null);

                ICommand OnLeaveCommand = ParametroComandoOnLeaveProperty.GetParametro(d);

                if (OnLeaveCommand != null)
                {
                    //Subscribimos al evento MouseLeave del elemento
                    fe.MouseLeave += (o, ea) => 
                        OnLeaveCommand.Execute(null);
                }
            }
        }
    }
}
