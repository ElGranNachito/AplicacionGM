namespace AppGM.Core
{
    public interface IInfligeDaño
    {
        // public void InfringirDaño(ModeloPersonaje usuario, ModeloPersonaje[] objetivos, EParteDelCuerpo EObjetivo, ETipoDeDaño ETipoDeDañoAInfligir);
    }

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
