using System.Collections.Generic;

namespace AppGM.Core
{
    public class ModeloPersonajeJugable : ModeloPersonaje
    {
        public TIPersonajeJugableCaracteristicas Caracteristicas { get; set; }

        public List<TIPersonajeJugableInvocacion> Invocaciones { get; set; } = new List<TIPersonajeJugableInvocacion>();
    }
}