using System.Collections.ObjectModel;

namespace AppGM.Core
{
    public class ViewModelListaDeHabilidades : ViewModel
    {
        public ObservableCollection<ViewModelHabilidadItem> Habilidades { get; set; } = new ObservableCollection<ViewModelHabilidadItem>();
    }
}
