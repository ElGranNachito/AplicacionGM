using System.ComponentModel;
using PropertyChanged;

namespace AppGM.Core
{
    /// <summary>
    /// Viewmodel base del que heredan todos los viewmodels de la aplicacion
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public abstract class ViewModel : INotifyPropertyChanged
    {
        #region Eventos

        /// <summary>
        /// Evento que se dispara cuando el valor de una propiedad cambia
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion

        #region Funciones

        /// <summary>
        /// Dispara el evento PropertyChanged
        /// </summary>
        /// <param name="args">Argumentos del evento</param>
        public void DispararPropertyChanged(PropertyChangedEventArgs args) =>
            PropertyChanged(this, args); 

        #endregion
    }
}