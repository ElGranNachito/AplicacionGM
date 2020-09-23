using System.ComponentModel;
using PropertyChanged;

namespace AppGM.Core
{
    [AddINotifyPropertyChangedInterface]
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate{};
        public virtual void DispararPropertyChangedEvent(BaseViewModel objeto, PropertyChangedEventArgs e) => 
        PropertyChanged(objeto, e);
    }
}
