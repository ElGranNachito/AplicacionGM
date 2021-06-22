namespace AppGM.Core
{
    public class BaseDesignModel<TipoDesignModel, TipoViewModel> : ViewModel
        where TipoDesignModel : BaseDesignModel<TipoDesignModel, TipoViewModel>, new()
        where TipoViewModel : ViewModel, new()
    {
        public static TipoDesignModel Designmodel { get; set; } = new TipoDesignModel();
        public static TipoViewModel   Viewmodel   { get; set; } = new TipoViewModel();
    }
}