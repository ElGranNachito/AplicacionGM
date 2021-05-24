using System.Collections.Generic;

namespace AppGM.Core
{
    public class ModeloServant : ModeloPersonajeJugable
    {
        public ERango mERangoNP { get; set; }

        /// <summary>
        /// Energia magica del servant
        /// </summary>
        public int Prana       { get; set; }
        public int PranaActual { get; set; }

        /// <summary>
        /// Origenes de la leyenda del personaje
        /// </summary>
        public string Fuente { get; set; }

        /// <summary>
        /// NoblePhantasms que posee el servant
        /// </summary>
        public List<TIServantNoblePhantasm> NoblePhantasms { get; set; } = new List<TIServantNoblePhantasm>();
    }
}