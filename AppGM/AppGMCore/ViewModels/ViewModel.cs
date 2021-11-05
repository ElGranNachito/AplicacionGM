using System.ComponentModel;
using System.Threading.Tasks;
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

        /*Aca debajo hay creado otro metodo igual con la unica diferencia de que directamente toma el nombre de la propiedad,
         la verdad es mas rapido que tener que inicializar los argumentos, pero no da cambiar todas las referencias a la funcion
        anterior. Si en algun momento alguien se siente al pedo fijese de cambiarlo, esto tambien va para mi yo del futuro UwU*/

        /// <summary>
        /// Dispara el evento PropertyChanged
        /// </summary>
        /// <param name="args">Argumentos del evento</param>
        public void DispararPropertyChanged(string nombrePropiedad) =>
	        PropertyChanged(this, new PropertyChangedEventArgs(nombrePropiedad));

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

        /// <summary>
        /// Inicializar este <see cref="ViewModel"/>
        /// </summary>
        public virtual async Task Activar(){}

        /// <summary>
        /// Realiza la limpieza pertienente de este <see cref="ViewModel"/> cuando se lo va a dejar de utilizar
        /// </summary>
        public virtual async Task Desactivar(){}

        #endregion
    }
}