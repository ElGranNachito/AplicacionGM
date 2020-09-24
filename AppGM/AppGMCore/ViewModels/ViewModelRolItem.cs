using System.Windows.Input;
using AppGMCore;

namespace AppGM.Core
{
    public class ViewModelRolItem : BaseViewModel
    {
        public ModeloRol ModeloRol { get; set; }
        public bool GloboDescripcionCompletaVisible { get; set; }
        public ICommand ComandoClickeado { get; set; }
        public ICommand ComandoMouseEnter { get; set; }
        public ICommand ComandoMouseLeave { get; set; }

        public ViewModelRolItem()
        {
            ComandoMouseEnter = new Comando(()=> GloboDescripcionCompletaVisible = true);
            ComandoMouseLeave = new Comando(()=> GloboDescripcionCompletaVisible = false);
        }
    }

}
