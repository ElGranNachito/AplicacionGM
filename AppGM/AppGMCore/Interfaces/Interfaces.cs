namespace AppGM.Core
{
	//TODO: Comentar las interfaces de abajo. Por ahora no lo hago porque posiblemente las quite

    public interface IBotonSeleccionado<T>
        where T: class
    {
        T BotonSeleccionado { get; set; }
    }

    public interface IViewModelConBotonSeleccionado
    {
        ViewModel ViewModelConBotonSeleccionado { get; set; }
    }
}
