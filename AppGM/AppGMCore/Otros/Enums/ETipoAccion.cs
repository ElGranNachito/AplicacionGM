namespace AppGM.Core
{
    /// <summary>
    /// Representa el tipo de accion de un <see cref="ModeloAccion"/>
    /// </summary>
    public enum ETipoAccion
    {
        /// <summary>
        /// Personalizada
        /// </summary>
        Personalizada = 0,

        /// <summary>
        /// Uso de una habilidad.
        /// </summary>
        Habilidad = 1,

        /// <summary>
        /// Uso de un item.
        /// </summary>
        Item = 2
    }
}
