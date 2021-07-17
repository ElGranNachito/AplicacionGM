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

        /// <summary>
        /// Permite acceder a la implementacion base de una funcion de una interfaz de manera mas limpia
        /// </summary>
        /// <typeparam name="TipoInterface"></typeparam>
        /// <returns></returns>
        public TipoInterface Base<TipoInterface>()
            where TipoInterface: class
        {
	        if (this is TipoInterface i)
		        return i;
	        return null;
        }

        #endregion
    }
}