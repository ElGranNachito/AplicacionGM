using System.ComponentModel;
using PropertyChanged;

namespace AppGM.Core
{
    [AddINotifyPropertyChangedInterface]
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate {};
        protected void DispararPropertyChanged(PropertyChangedEventArgs args) =>
            PropertyChanged(this, args);
    }
}
