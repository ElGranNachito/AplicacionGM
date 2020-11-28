using System.Collections.Generic;

namespace AppGM.Core
{
    public class ModeloServant : ModeloPersonajeJugable
    {
        public string NombreReal { get; set; }

        public ERango mERangoNP { get; set; }

        public List<TIServantNoblePhantasm> NoblePhantasms { get; set; } = new List<TIServantNoblePhantasm>();
    }
}