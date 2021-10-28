using System.Collections.ObjectModel;

namespace AppGM.Core
{
    public class ViewModelMensajeCrearRol_ListaPersonajes : ViewModel
    { 
        public ObservableCollection<ViewModelCrearRol_PersonajeItem> Personajes { get; set; } = new ObservableCollection<ViewModelCrearRol_PersonajeItem>();

        public ViewModelMensajeCrearRol_ListaPersonajes(DatosCreacionRol _datosRol, ObservableCollection<ModeloPersonaje> _personajes)
        {
            for (int i = 0; i < _personajes.Count; ++i)
                Personajes.Add(new ViewModelCrearRol_PersonajeItem(_datosRol, _personajes[i], this));
        }
    }
}
