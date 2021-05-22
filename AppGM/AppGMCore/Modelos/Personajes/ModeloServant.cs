using System.Collections.Generic;

namespace AppGM.Core
{
    public class ModeloServant : ModeloPersonajeJugable
    {
        public ERango mERangoNP { get; set; }

        // Energia magica del servant
        public int Prana       { get; set; }
        public int PranaActual { get; set; }

        // Origenes de la leyenda del personaje
        public string Fuente { get; set; }

        public List<TIServantNoblePhantasm> NoblePhantasms { get; set; } = new List<TIServantNoblePhantasm>();
    }
}