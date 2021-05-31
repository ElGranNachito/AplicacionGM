using System.Windows.Input;
using AppGM.AttachedProperties;

namespace AppGM
{
    /// <summary>
    /// Contiene un <see cref="ICommand"/> que se ejecutara cuando el mouse deje de estar sobre algun elemento
    /// </summary>
    public class ParametroComandoOnLeaveProperty : BaseAttachedPropertyParametro<ICommand, ParametroComandoOnLeaveProperty> {}
}
