namespace AppGM.Core
{
    public abstract class ViewModelPaso<TipoVMVentana> : BaseViewModel
        where TipoVMVentana: ViewModelVentanaConPasos<TipoVMVentana>
    {
        public virtual void Activar(TipoVMVentana vm){}
        public virtual void Desactivar(TipoVMVentana vm) {}
    }
}
