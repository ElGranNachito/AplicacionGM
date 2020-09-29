using System.Windows.Input;
using AppGM.Core;

namespace AppGM
{
    public class ViewModelCarta : BaseViewModel
    {
        public int ZIndex { get; set; } = 0;
        public ICommand Comando { get; set; }
        public ICommand ComandoMouseEnter { get; set; }
        public ICommand ComandoMouseLeave { get; set; }
    }
}
