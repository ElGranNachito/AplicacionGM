namespace AppGMCore
{
    public interface IInfligeDaño
    {
        // public void InfringirDaño(ModeloPersonaje usuario, ModeloPersonaje[] objetivos, EParteDelCuerpo EObjetivo, ETipoDeDaño ETipoDeDañoAInfligir);
    }

    public interface IDescripcion
    {
        string Nombre { get; set; }
        string Descripcion { get; set; }
    }

    public interface IUtilizableConObjetivos
    {
        void Utilizar(ControladorPersonaje<ModeloPersonaje> usuario, ControladorPersonaje<ModeloPersonaje>[] objetivos, object parametroExtra, object segundoParametroExtra);
    }

    public interface IUtilizableSinObjetivos
    {
        void Utilizar(ControladorPersonaje<ModeloPersonaje> usuario, object parametroExtra, object segundoParametroExtra);
    }
}
