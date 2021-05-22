using System.Collections.Generic;

namespace AppGM.Core
{
    public abstract class ModeloPersonajeJugable : ModeloPersonaje
    {
        public EClaseServant EClaseServant { get; set; }

        // Rango en el que el personaje puede realizar hechiceria
        public ushort RangoHechiceria { get; set; }
        // Caracteristicas del personaje
        public TIPersonajeJugableCaracteristicas Caracteristicas { get; set; }

        // Invocaciones activas
        public List<TIPersonajeJugableInvocacion> Invocaciones { get; set; } = new List<TIPersonajeJugableInvocacion>();
    }
}