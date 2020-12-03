using System.Windows.Controls;
using AppGM.Core;

namespace AppGM
{
    public abstract class BaseUserControl<TipoViewModel> : UserControl
        where TipoViewModel : BaseViewModel, new()
    {
        private TipoViewModel mViewModel;

        public TipoViewModel ViewModel
        {
            get => mViewModel;
            set
            {
                if (value == mViewModel)
                    return;

                mViewModel = value;
                DataContext = mViewModel;
            }
        }
    }
}
