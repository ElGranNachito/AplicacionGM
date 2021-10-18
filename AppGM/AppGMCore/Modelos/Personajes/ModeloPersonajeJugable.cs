using System.Collections.Generic;

namespace AppGM.Core
{
    public abstract class ModeloPersonajeJugable : ModeloPersonaje
    {
        public EClaseServant EClaseServant { get; set; }

        /// <summary>
        /// Rango en el que el personaje puede realizar hechiceria
        /// </summary>
        public ushort RangoHechiceria { get; set; }

        /// <summary>
        /// Caracteristicas del personaje
        /// </summary>
        public virtual ModeloCaracteristicas Caracteristicas { get; set; }

        /// <summary>
        /// Invocaciones activas
        /// </summary>
        public virtual List<ModeloInvocacion> Invocaciones { get; set; } = new List<ModeloInvocacion>();
    }
}