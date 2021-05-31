namespace AppGM.Core
{
    public interface IInfligeDaño
    {
        // public void InfringirDaño(ModeloPersonaje usuario, ModeloPersonaje[] objetivos, EParteDelCuerpo EObjetivo, ETipoDeDaño ETipoDeDañoAInfligir);
    }

    /// <summary>
    /// Interfaz que implementan controladores de modelos de entidades que se pueden utilizar
    /// </summary>
    public interface IUtilizable
    {
        /// <summary>
        /// Utiliza este utilizable en los <paramref name="objetivos"/>
        /// </summary>
        /// <param name="usuario"><see cref="ControladorPersonaje"/> que lo utilizara</param>
        /// <param name="objetivos"><see cref="ControladorPersonaje"/> que recibiran el efecto</param>
        /// <param name="parametroExtra">Parametro extra, por ahora nada</param>
        /// <param name="segundoParametroExtra">Parametro extra, por ahora nada tampoco</param>
        void Utilizar(ControladorPersonaje usuario, ControladorPersonaje[] objetivos, object parametroExtra, object segundoParametroExtra);

        /// <summary>
        /// Utiliza este utilizable en el mismo <paramref name="usuario"/>
        /// </summary>
        /// <param name="usuario"><see cref="ControladorPersonaje"/> que lo utilizara</param>
        /// <param name="parametroExtra">Parametro extra, por ahora nada</param>
        /// <param name="segundoParametroExtra">Parametro extra, por ahora nada tampoco</param>
        void Utilizar(ControladorPersonaje usuario, object parametroExtra, object segundoParametroExtra);

        /// <summary>
        /// Indica si el <paramref name="usuario"/> puede usar este utilizable contra ciertos <paramref name="objetivos"/>
        /// o en si mismo si <paramref name="objetivos"/> es null
        /// </summary>
        /// <param name="usuario"><see cref="ControladorPersonaje"/> que lo utilizara</param>
        /// <param name="objetivos"><see cref="ControladorPersonaje"/> que recibiran el efecto</param>
        /// <returns><see cref="bool"/> indicando si esta habilidad puede ser utilizada por este <paramref name="usuario"/> en dados <paramref name="objetivos"/></returns>
        bool PuedeUtilizar(ControladorPersonaje usuario, ControladorPersonaje[] objetivos);
    }

    //TODO: Comentar las interfaces de abajo. Por ahora no lo hago porque posiblemente las quite

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
