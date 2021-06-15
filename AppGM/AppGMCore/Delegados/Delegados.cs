namespace AppGM.Core.Delegados
{
    /// <summary>
    /// Delegado utilizado para eventos que propelen el cambio de valor de una variable
    /// </summary>
    /// <typeparam name="TipoVariable">Tipo de la variable</typeparam>
    /// <param name="valorAnterior">Valor anterior de la variable</param>
    /// <param name="valorActual">Valor actual de la variable</param>
    public delegate void DVariableCambio<TipoVariable>(TipoVariable valorAnterior, TipoVariable valorActual);

    /// <summary>
    /// Delegado utilizado para eventos de Drag
    /// </summary>
    /// <param name="vmContenido"><see cref="ViewModel"/> del contenido del drag</param>
    public delegate void DDrag(ViewModel vmContenido);
}
