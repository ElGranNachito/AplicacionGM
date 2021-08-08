using System.Windows.Input;

namespace AppGM.Core
{
    public class ViewModelCrearRol_PersonajeItem : ViewModel
    {
        #region Propiedades
        public bool EsServant => Personaje.TipoPersonaje == ETipoPersonaje.Servant;

        public ModeloPersonaje Personaje { get; private set; }

        public ICommand ComandoEliminar { get; set; } 

        #endregion

        #region Constrcutor

        public ViewModelCrearRol_PersonajeItem(DatosCreacionRol _datosRol, ModeloPersonaje _personaje, ViewModelMensajeCrearRol_ListaPersonajes _listaPersonajes)
        {
            Personaje = _personaje;

            ComandoEliminar = new Comando(() =>
            {
                _datosRol.personajes.Remove(_personaje);

                _listaPersonajes.Personajes.Remove(this);

            });
        } 

        #endregion
    }
}