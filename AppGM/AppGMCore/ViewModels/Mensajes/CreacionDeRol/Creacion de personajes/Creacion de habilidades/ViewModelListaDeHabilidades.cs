using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AppGM.Core
{
    public class ViewModelListaDeHabilidades : BaseViewModel
    {
        public ObservableCollection<ViewModelHabilidadItem> Habilidades { get; set; } = new ObservableCollection<ViewModelHabilidadItem>();
    }
}
