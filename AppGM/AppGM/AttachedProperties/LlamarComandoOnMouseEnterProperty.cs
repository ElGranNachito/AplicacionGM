

using System.Windows;
using System.Windows.Input;

namespace AppGM
{
    public class LlamarComandoOnMouseEnterProperty : BaseAttachedProperty<ICommand, LlamarComandoOnMouseEnterProperty>
    {
        public override void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement fe)
            {
                fe.MouseEnter += (o, ea) => 
                    ((ICommand) e.NewValue).Execute(null);

                ICommand OnLeaveCommand = ParametroComandoOnLeaveProperty.GetParametro(d);

                if (OnLeaveCommand != null)
                {
                    fe.MouseLeave += (o, ea) => 
                        OnLeaveCommand.Execute(null);
                }

            }
        }
    }
}
