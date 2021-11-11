namespace AppGM.Core
{
    /// <summary>
    /// Base para viewmodels de pasos
    /// </summary>
    /// <typeparam name="TipoVMVentana">Tipo de <see cref="ViewModelVentanaConPasos{TipoViewModel}"/> del que sera el paso</typeparam>
    public abstract class ViewModelPaso<TipoVMVentana> : ViewModel
        where TipoVMVentana: ViewModelVentanaConPasos<TipoVMVentana>
    {
	    public readonly TipoVMVentana contenedorPasos;

	    public ViewModelPaso(TipoVMVentana _contenedorPasos)
	    {
		    contenedorPasos = _contenedorPasos;
	    }

        public virtual void Activar(TipoVMVentana vm){}

        public virtual void Desactivar(TipoVMVentana vm) {}

        public virtual bool PuedeAvanzar() => true;
    }
}
