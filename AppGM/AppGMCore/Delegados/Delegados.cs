using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace AppGM.Core.Delegados
{
    /// <summary>
    /// Delegado utilizado para eventos que propelen el cambio de valor de una variable
    /// </summary>
    /// <typeparam name="TipoVariable">Tipo de la variable</typeparam>
    /// <param name="valorAnterior">Valor anterior de la variable</param>
    /// <param name="valorActual">Valor actual de la variable</param>
    public delegate void DVariableCambio<TipoVariable>([MaybeNull]TipoVariable valorAnterior, [MaybeNull]TipoVariable valorActual);

    /// <summary>
    /// Delegado utilizado para eventos de Drag
    /// </summary>
    /// <param name="vmContenido"><see cref="ViewModel"/> del contenido del drag</param>
    public delegate void DDrag(IDrageable vmContenido);

    /// <summary>
    /// Delegado que representa metodos que lidian con el evento de recibir un <paramref name="vmContenido"/> soltado
    /// </summary>
    /// <param name="vmContenido"><see cref="IDrageable"/> que se solto sobre el elemento</param>
    /// <returns><see cref="bool"/> indicando si se debe detener la propagacion del evento a otros receptores</returns>
    public delegate bool DDragHandlerElementoSoltado(IDrageable vmContenido);

    /// <summary>
    /// Delegado utilizado para eventos de soltar un <see cref="IDrageable"/> sobre un <see cref="IReceptorDeDrag"/>
    /// </summary>
    /// <param name="vmContenido"><see cref="ViewModel"/> del contenido del drag</param>
    public delegate void DDragElementoSoltado(IDrageable vmContenido, List<IReceptorDeDrag> vmReceptor);
}
