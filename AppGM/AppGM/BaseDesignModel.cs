using AppGM.Core;

namespace AppGM
{
    public class BaseDesignModel<TipoDesignModel, TipoViewModel> : BaseViewModel
        where TipoDesignModel : BaseDesignModel<TipoDesignModel, TipoViewModel>, new()
        where TipoViewModel : BaseViewModel, new()
    {
        public static TipoDesignModel Designmodel { get; set; } = new TipoDesignModel();
        public static TipoViewModel   Viewmodel   { get; set; } = new TipoViewModel();
    }
}