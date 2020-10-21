namespace AppGM.Core
{
    public class DesingModelListaRoles : BaseDesignModel<DesingModelListaRoles, ViewModelListaCombates>
    {
        public static ViewModelListaCombates ListaCombates { get; set; } = new ViewModelListaCombates();
        public DesingModelListaRoles()
        {
        }
    }
}
