using System;

namespace AppGM.Core
{
    public interface IInfligeDaño
    {
        // public void InfringirDaño(ModeloPersonaje usuario, ModeloPersonaje[] objetivos, EParteDelCuerpo EObjetivo, ETipoDeDaño ETipoDeDañoAInfligir);
    }

    public interface IUtilizableConObjetivos
    {
        void Utilizar(ControladorPersonaje usuario, ControladorPersonaje[] objetivos, object parametroExtra, object segundoParametroExtra);
    }

    public interface IUtilizableSinObjetivos
    {
        void Utilizar(ControladorPersonaje usuario, object parametroExtra, object segundoParametroExtra);
    }

    public interface IBotonSeleccionado<T>
        where T: class
    {
        T BotonSeleccionado { get; set; }
    }

    public interface IViewModelConBotonSeleccionado
    {
        BaseViewModel ViewModelConBotonSeleccionado { get; set; }
    }
}
