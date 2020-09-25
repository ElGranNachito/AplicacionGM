using System.ComponentModel;
using PropertyChanged;

namespace AppGM.Core
{
    [AddINotifyPropertyChangedInterface]
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region Eventos
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        #endregion

        #region Funciones
        protected void DispararPropertyChanged(PropertyChangedEventArgs args) =>
            PropertyChanged(this, args); 
        #endregion
    }
}
