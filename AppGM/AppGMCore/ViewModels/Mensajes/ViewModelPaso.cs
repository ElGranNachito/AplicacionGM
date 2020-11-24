using System.Runtime.Remoting.Messaging;

namespace AppGM.Core
{
    /// <summary>
    /// Base para viewmodels de pasos
    /// </summary>
    /// <typeparam name="TipoVMVentana">Tipo de viewmodel del que seran pasos</typeparam>
    public abstract class ViewModelPaso<TipoVMVentana> : BaseViewModel
        where TipoVMVentana: ViewModelVentanaConPasos<TipoVMVentana>
    {
        public virtual void Activar(TipoVMVentana vm){}
        public virtual void Desactivar(TipoVMVentana vm) {}

        public virtual bool PuedeAvanzar() => true;
    }
}
