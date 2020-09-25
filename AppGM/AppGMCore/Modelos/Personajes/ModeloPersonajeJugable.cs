using System.Collections.Generic;

namespace AppGM.Core
{
    public class ModeloPersonajeJugable : ModeloPersonaje
    {
        public ModeloCaracteristicas Caracteristicas { get; set; }

        public List<TIPersonajeJugableInvocacion> Invocaciones { get; set; }
    }
}