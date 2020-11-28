using System.Collections.Generic;

namespace AppGM.Core
{
    public abstract class ModeloPersonajeJugable : ModeloPersonaje
    {
        public EClaseServant EClaseServant { get; set; }
        public TIPersonajeJugableCaracteristicas Caracteristicas { get; set; }

        public List<TIPersonajeJugableInvocacion> Invocaciones { get; set; } = new List<TIPersonajeJugableInvocacion>();
    }
}